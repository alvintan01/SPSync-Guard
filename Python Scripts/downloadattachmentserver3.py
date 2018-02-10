#!/usr/bin/env python
import email
import getpass, imaplib
import os
import sys
import re
import zipfile
import glob
import shutil
import datetime
import MySQLdb
import pyAesCrypt

def addtodict(a, trusted):
    global processdict
    a=a.decode("utf-8")
    if a in processdict:
        if trusted == b"Trusted":
            processdict[a][0]=processdict[a][0]+1
        else:
            processdict[a][1]=processdict[a][1]+1
    else:
        if trusted == b"Trusted":
            processdict[a]=[1,0]
        else:
            processdict[a]=[0,1]

secretkeys = []
noofmonths = {}
processdict = {}
# Open database connection
db = MySQLdb.connect("localhost","webapp","1qwer$#@!","webapp")

# prepare a cursor object using cursor() method
cursor = db.cursor()
sql = "SELECT c_secretkey FROM t_secretkey"

try:
   # Execute the SQL command
    cursor.execute(sql)
    results = cursor.fetchall()
    for row in results:
        secretkeys.append(row[0])
except Exception as e:
    print(e)

sql = "SELECT * FROM t_users"

try:
   # Execute the SQL command
    cursor.execute(sql)
    results = cursor.fetchall()
    for row in results:
        noofmonths[row[1]] = int(row[4])
except Exception as e:
    print(e)

sql = "SELECT * FROM t_userprocess"

try:
   # Execute the SQL command
    cursor.execute(sql)
    results = cursor.fetchall()
    for row in results:
        processdict[row[1]] = [int(row[2]),int(row[3])]
except Exception as e:
    print(e)

pattern_uid = re.compile(b'\d+ \(UID (?P<uid>\d+)\)')

detach_dir = '/home/server'
if 'attachments' not in os.listdir(detach_dir):
    os.mkdir(detach_dir + '/attachments')
    os.chmod(detach_dir + '/attachments', 0o777)

userName = ""#server email address
passwd = ""#server email password
clientemailaddress = "" #your client email address


try:
    imapSession = imaplib.IMAP4_SSL('imap.gmail.com')
    typ, accountDetails = imapSession.login(userName, passwd)
    if typ != 'OK':
        print('Not able to sign in!')
        raise

    imapSession.select('"[Gmail]/All Mail"')
    typ, data = imapSession.search(None, 'ALL')
    if typ != 'OK':
        print('Error searching Inbox.')
        raise

    # Iterating over all emails
    for msgId in data[0].split():
        t, label = imapSession.fetch(msgId, '(X-GM-LABELS)')
        
        if b"server" not in label[0]:
            typ, messageParts = imapSession.fetch(msgId, '(RFC822)')
            if typ != 'OK':
                print('Error fetching mail.')
                raise

            emailBody = messageParts[0][1]
            
            mail = email.message_from_bytes(emailBody)

            if mail['From']==clientemailaddress:
                for part in mail.walk():
                    if part.get_content_type() == 'text/plain':
                        emailtext = part.get_payload(decode=True).strip()
                        if b" request to delete all versions" in emailtext:
                            username = emailtext.split(b' request to delete all versions')[0].decode("utf-8")
                            #clean up
                            pathtodelete = []
                            print(username + " requested delete all versions")
                            for dirpath,_,filenames in os.walk(detach_dir + '/attachments/' + username):#clean up all user files which have > 1 copy
                                for f in filenames:
                                    path = os.path.abspath(os.path.join(dirpath, f))#get path of currentfile
                                    groups = re.search('(.+) ([0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9])((?:\.[^.]+$)?)', f)
                                    actualfilenamewithoutext = groups.group(1)
                                    if groups.group(3) is None:
                                        extension = ""
                                    else:
                                        extension = groups.group(3)#get file extension
                                    filelist = [f for f in os.listdir(dirpath) if re.match(actualfilenamewithoutext+' [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]'+extension, f)]#get all files matching the file path, name and extension
                                    if len(filelist) > 1:#if there are more than one file
                                        filelist = sorted(filelist)
                                        for i in range(len(filelist[:-1])):
                                            newpath = os.path.abspath(os.path.join(dirpath, filelist[i]))
                                            if newpath not in pathtodelete:
                                                pathtodelete.append(newpath)                        
                            for a in pathtodelete:
                                os.remove(a)#remove the current file
                                print("Deleted " + a)
                        
                            resp, datas = imapSession.fetch(msgId, "(UID)")
                            msg_uid = pattern_uid.match(datas[0]).group('uid')
                            result = imapSession.uid('COPY', msg_uid, 'server')


                        elif b" request to delete account" in emailtext:
                            username = emailtext.split(b' request to delete account')[0].decode("utf-8")
                            try:
                                shutil.rmtree(detach_dir + '/attachments/' + username)
                            except:
                                pass
                            sql="Delete from t_users where c_username ='"+username+"'"
                            try:
                               # Execute the SQL command
                               cursor.execute(sql)
                               # Commit your changes in the database
                               db.commit()
                            except Exception as e:
                               # Rollback in case there is any error
                               db.rollback()
                               print(e)
                            
                            print("Deleted account: "+username)
                            resp, datas = imapSession.fetch(msgId, "(UID)")
                            msg_uid = pattern_uid.match(datas[0]).group('uid')
                            result = imapSession.uid('COPY', msg_uid, 'server')
                            break

                        else:                        
                            if emailtext != b"" and emailtext!=b"Files sent to pi directly":
                                tempprocesses = emailtext.split(b",") # prints the raw text
                                for a in tempprocesses:
                                    a=a.split(b"|")
                                    addtodict(a[0],a[1])

                                resp, datas = imapSession.fetch(msgId, "(UID)")
                                msg_uid = pattern_uid.match(datas[0]).group('uid')
                                result = imapSession.uid('COPY', msg_uid, 'server')

                    if part.get_content_maintype() == 'multipart':
                        # print part.as_string()
                        continue
                    if part.get('Content-Disposition') is None:
                        # print part.as_string()
                        continue

                    fileName = part.get_filename()

                    username = fileName.split()[0]

                    if username not in os.listdir(detach_dir+"/attachments"):
                        os.mkdir(detach_dir+"/attachments/"+username)

                    if bool(fileName):
                        filePath = os.path.join(detach_dir, 'attachments/'+username, fileName)
                        if not os.path.isfile(filePath) :
                            print(fileName)
                            fp = open(filePath, 'wb')
                            fp.write(part.get_payload(decode=True))
                            fp.close()

                            resp, datas = imapSession.fetch(msgId, "(UID)")
                            msg_uid = pattern_uid.match(datas[0]).group('uid')

                            result = imapSession.uid('COPY', msg_uid, 'server')

                            key = secretkeys[-1]#str.encode(secretkeys[-1])
                            index = -1
                            while True:
                                try:
                                    bufferSize = 64 * 1024
                                    pyAesCrypt.decryptFile(filePath, "decrypted.zip", key, bufferSize)                                
                                    zip_ref = zipfile.ZipFile("decrypted.zip", 'r')
                                    zip_ref.extractall(os.path.join(detach_dir + '/attachments/'+username))
                                    zip_ref.close()
                                    os.remove("decrypted.zip")
                                    break
                                except Exception as e:
                                    print(e)
                                    index = index - 1
                                    key = secretkeys[index]

                            os.remove(filePath)
                            for dirpath,_,filenames in os.walk(detach_dir + '/attachments/'+username+'/Sync Backup'):
                                for f in filenames:
                                    path = os.path.abspath(os.path.join(dirpath, f))
                                    newpath= detach_dir + '/attachments/'+username+'/'+str(path.split(re.search(r"Sync Backup/[0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]/", path).group(0),1)[1])
                                    if not os.path.exists(os.path.dirname(newpath)):
                                        try:
                                            os.makedirs(os.path.dirname(newpath))
                                        except OSError as exc: # Guard against race condition
                                            if exc.errno != errno.EEXIST:
                                                raise
                                    shutil.move(path, newpath)
                            shutil.rmtree(detach_dir+'/attachments/'+username+'/Sync Backup')
            else:
                 print("There is an email with an unknown sender.")

    imapSession.select("pi")
    imapSession.store("1:*",'+X-GM-LABELS', '\\Trash')

    imapSession.close()
    imapSession.logout()
    #write to file
    #    f = open("Process.txt", 'w')
    #    for a in processes:
    #        f.write(a+'\n')
    #    f.close()
    sql = "Delete from t_userprocess"
    try:
       # Execute the SQL command
       cursor.execute(sql)
       # Commit your changes in the database
       db.commit()
    except Exception as e:
       # Rollback in case there is any error
       db.rollback()
       print(e)

    for processname in processdict:
        sql = "Insert into t_userprocess (c_processname, c_approvedcount, c_denycount)values ('"+processname +"','"+str(processdict.get(processname)[0]) +"','"+str(processdict.get(processname)[1])+"')"
        try:
           # Execute the SQL command
           cursor.execute(sql)
           # Commit your changes in the database
           db.commit()
        except Exception as e:
           # Rollback in case there is any error
           db.rollback()
           print(e)

    #clean up
    for g in os.listdir(detach_dir + '/attachments'):#get all user names
        filesize = 0
        tendaysago = (datetime.datetime.today() - datetime.timedelta(days=10)).strftime('%d-%m-%Y %H-%M-%S')#get date 10 days ago
        for dirpath,_,filenames in os.walk(detach_dir + '/attachments/' + g):#clean up all user files > 10 days old which have > 1 copy
            for f in sorted(filenames):
                filesize += os.path.getsize(os.path.join(dirpath, f))#compute file size
                path = os.path.abspath(os.path.join(dirpath, f))#get path of currentfile
                groups = re.search('(.+) ([0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9])((?:\.[^.]+$)?)', f)
                filedate = groups.group(2)#get file date
                if groups.group(3) is None:
                    extension = ""
                else:
                    extension = groups.group(3)#get file extension
                actualfilename = groups.group(1)+groups.group(3)#get full filename
                actualfilename = groups.group(1)+extension#get full filename
                actualfilenamewithoutext = groups.group(1)#get filename only

                if datetime.datetime.strptime(filedate, '%d-%m-%Y %H-%M-%S') < datetime.datetime.strptime(tendaysago, '%d-%m-%Y %H-%M-%S'):#compare dates
                    filelist = [f for f in os.listdir(dirpath) if re.match(actualfilenamewithoutext+' [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]'+extension, f)]#get all files matching the file path, name and extension
                    if len(filelist) > 1:#if there are more than one file
                        filelist = sorted(filelist)
                        filesize -= os.path.getsize(os.path.abspath(os.path.join(dirpath, filelist[0])))#compute file size
                        os.remove(os.path.abspath(os.path.join(dirpath, filelist[0])))#remove the first file
                        print("Deleted version as > 10 days " + path)
##Would not happen as if >10 days versioning is already deleted
##        if (filesize > 10 * 1024 * 1024 * 1024):#if the user directory is > 10GB, delete files older than user selected months but keep one version
##            xmonthsago = (datetime.datetime.today() - datetime.timedelta(days=(int(noofmonths.get(g))*30))).strftime('%d-%m-%Y %H-%M-%S')#get date x months ago
##            for dirpath,_,filenames in os.walk(detach_dir + '/attachments/' + g):
##                for f in sorted(filenames):
##                    path = os.path.abspath(os.path.join(dirpath, f))#get path of currentfile
##                    groups = re.search('(.+) ([0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9])(\.[^.]+$)', f)
##                    filedate = groups.group(2)#get file date
##                    actualfilename = groups.group(1)+groups.group(3)#get full filename
##                    extension = groups.group(3)#get file extension
##                    actualfilenamewithoutext = groups.group(1)#get filename only
##
##                    if datetime.datetime.strptime(filedate, '%d-%m-%Y %H-%M-%S') < datetime.datetime.strptime(xmonthsago, '%d-%m-%Y %H-%M-%S'):#compare dates
##                        filelist = [f for f in os.listdir(dirpath) if re.match(actualfilenamewithoutext+' [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]'+extension, f)]#get all files matching the file path, name and extension
##                        if len(filelist) > 1:#if there are more than one file
##                            filelist = sorted(filelist)
##                            filesize -= os.path.getsize(os.path.abspath(os.path.join(dirpath, filelist[0])))#compute file size
##                            os.remove(os.path.abspath(os.path.join(dirpath, filelist[0])))#remove the first file
##                            print("Deleted as exceeded storage limit but keeping one version " + path)

        if (filesize > 10 * 1024 * 1024 * 1024):#if the user directory is still > 10GB, delete files older than user selected months without keeping any version
            xmonthsago = (datetime.datetime.today() - datetime.timedelta(days=(int(noofmonths.get(g))*30))).strftime('%d-%m-%Y %H-%M-%S')#get date x months ago
            for dirpath,_,filenames in os.walk(detach_dir + '/attachments/' + g):
                for f in sorted(filenames):
                    path = os.path.abspath(os.path.join(dirpath, f))#get path of currentfile
                    groups = re.search('(.+) ([0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9])((?:\.[^.]+$)?)', f)
                    filedate = groups.group(2)#get file date
                    if groups.group(3) is None:
                        extension = ""
                    else:
                        extension = groups.group(3)#get file extension
                    actualfilename = groups.group(1)+extension#get full filename
                    actualfilenamewithoutext = groups.group(1)#get filename only

                    if datetime.datetime.strptime(filedate, '%d-%m-%Y %H-%M-%S') < datetime.datetime.strptime(xmonthsago, '%d-%m-%Y %H-%M-%S'):#compare dates
                        filesize -= os.path.getsize(path)#compute file size
                        os.remove(path)#remove the current file
                        print("Deleted as exceeded storage limit " + path)

        sql = "Update t_users set c_directorysize="+str(filesize)+" where c_username='"+g+"'"
        try:
           # Execute the SQL command
           cursor.execute(sql)
           # Commit your changes in the database
           db.commit()
        except Exception as e:
           # Rollback in case there is any error
           db.rollback()
           print(e)

except Exception as ex:
    print('Not able to download all attachments.')
    print(ex)

# disconnect from server
db.close()
