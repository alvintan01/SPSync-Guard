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
import requests
import json
import smtplib
import pyAesCrypt
from email import encoders
from email.mime.base import MIMEBase
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText
from Crypto.Cipher import AES
import base64

roomname="T2031"#Declare room name here
secretkeys = []
noofmonths = {}
directfile = []
emailuserName = ""#server email address for email download
passwd = ""#server email password for email download
smtpUser = ''#to send direct backup files
smtpPass = ''#to send direct backup files
cloudserveraddress = "" #your server ip
pythonscriptdirectory = "/home/pi/"#directory of the python script


if os.path.isfile(pythonscriptdirectory+'Keys.txt'):
    lines = [line.strip('\n') for line in open(pythonscriptdirectory+'Keys.txt')]
    secretkeys = secretkeys+lines
else:
    secretkeys.append("defaultkey123456")

if os.path.isfile('Receivedfiles.txt'):
    lines = [line.strip('\n') for line in open('Receivedfiles.txt')]
    directfile = directfile+lines
    
def getsession():
    global sessionkey
    global auth
    url = "https://"+cloudserveraddress+"/getSession"
    payload = {}
    r = requests.post(url, data=payload, verify=False)
    sessionkey = r.text.strip()
    aes = AES.new(secretkeys[-1], AES.MODE_CBC, IV = "1234567890SPSync")
    auth = base64.b64encode(aes.encrypt(sessionkey))

sessionkey = ""
auth = ""

try:
    getsession()
    url = "https://"+cloudserveraddress+"/getSecretKey"
    payload = {'auth': auth, 'sessionkey': sessionkey}
    r = requests.post(url, data=payload, verify=False)

    if r.text.strip()!= "No changes":
        aes = AES.new(secretkeys[-1], AES.MODE_CBC, IV = "1234567890SPSync")
        key = aes.decrypt(base64.b64decode(r.text.strip())).decode("utf-8")
        secretkeys.append(key)

    getsession()
    url = "https://dmit2.bulletplus.com/getDataPi"
    payload = {'auth': auth, 'sessionkey': sessionkey}
    r = requests.post(url, data=payload, verify=False)
    jsonstring = json.loads(r.text.strip())
    for a in jsonstring:
        noofmonths[a['Username']] = a['Noofmonths']
except:
    print("Could not connect to the cloud server")
    sys.exit()

#write to file
f = open(pythonscriptdirectory+'Keys.txt', 'w')
for a in secretkeys:
    f.write(a+'\n')
f.close()

pattern_uid = re.compile(b'\d+ \(UID (?P<uid>\d+)\)')
detach_dir = '/media/USBHDD1'
if 'attachments' not in os.listdir(detach_dir):
    os.mkdir(detach_dir + '/attachments')
    os.chmod(detach_dir + '/attachments', 0o777)


for file in glob.glob(detach_dir+"/attachments/*/*.syncnoemail"):
    directfile.append(file.split("/")[-1])#add filename to direct file list
    print(file)
    groups = re.search('([^\/]+) [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]+.syncnoemail$', file)
    username = groups.group(1)
    key = secretkeys[-1]
    index = -1
    while True:
        try:
            bufferSize = 64 * 1024
            pyAesCrypt.decryptFile(file, "decrypted.zip", key, bufferSize)                                
            zip_ref = zipfile.ZipFile("decrypted.zip", 'r')                                
            zip_ref.extractall(os.path.join(detach_dir + '/attachments/'+username))
            zip_ref.close()
            os.remove("decrypted.zip")
            break
        except Exception as e:
            print(e)
            index = index - 1
            key = secretkeys[index]

    os.rename(file,file.replace('.syncnoemail','.sync'))
    sendMail( [toAdd], subject, subject, file.replace('.syncnoemail','.sync'))#make the subject and email body the same
    os.remove(file.replace('.syncnoemail','.sync'))
 
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


for file in glob.glob(detach_dir+"/attachments/*/*.sync"):
    directfile.append(file.split("/")[-1])#add filename to direct file list
    print("Unzipping direct file: "+file)
    groups = re.search('([^\/]+) [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]+.sync$', file)
    username = groups.group(1)
    key = secretkeys[-1]
    index = -1
    while True:
        try:
            bufferSize = 64 * 1024
            pyAesCrypt.decryptFile(file, "decrypted.zip", key, bufferSize)                                
            zip_ref = zipfile.ZipFile("decrypted.zip", 'r')                                
            zip_ref.extractall(os.path.join(detach_dir + '/attachments/'+username))
            zip_ref.close()
            os.remove("decrypted.zip")
            break
        except Exception as e:
            print(e)
            index = index - 1
            key = secretkeys[index]

    os.remove(file)
 
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



#Check for .syncnoemail

toAdd = emailuserName
fromAdd = smtpUser

subject  = 'Files sent to pi directly'


def sendMail(to, subject, text, files):
    assert type(to)==list
    COMMASPACE = ', '
    msg = MIMEMultipart()
    msg['From'] = smtpUser
    msg['To'] = COMMASPACE.join(to)
    #msg['Date'] = formatdate(localtime=True)
    msg['Subject'] = subject
    msg.attach( MIMEText(text) )


    part = MIMEBase('application', "octet-stream")
    part.set_payload( open(files,"rb").read() )
    encoders.encode_base64(part)
    part.add_header('Content-Disposition', 'attachment; filename="%s"'
                   % os.path.basename(files))
    msg.attach(part)

    server = smtplib.SMTP('smtp.gmail.com:587')
    server.ehlo_or_helo_if_needed()
    server.starttls()
    server.ehlo_or_helo_if_needed()
    server.login(smtpUser,smtpPass)
    server.sendmail(smtpUser, to, msg.as_string())
    server.quit()



    
#try:
imapSession = imaplib.IMAP4_SSL('imap.gmail.com')
typ, accountDetails = imapSession.login(emailuserName, passwd)
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
    
    if b"pi" not in label[0]:
        typ, messageParts = imapSession.fetch(msgId, '(RFC822)')
        if typ != 'OK':
            print('Error fetching mail.')
            raise

        emailBody = messageParts[0][1]
        mail = email.message_from_bytes(emailBody)

        if mail['From']==smtpUser:
            for part in mail.walk():
                if roomname in mail['subject']:
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
                            result = imapSession.uid('COPY', msg_uid, 'pi')

                        if b" request to delete account" in emailtext:
                            username = emailtext.split(b' request to delete account')[0].decode("utf-8")
                            try:
                                shutil.rmtree(detach_dir + '/attachments/' + username)
                            except:
                                pass
                            print("Deleted account: "+username)
                            resp, datas = imapSession.fetch(msgId, "(UID)")
                            msg_uid = pattern_uid.match(datas[0]).group('uid')
                            result = imapSession.uid('COPY', msg_uid, 'pi')
                            break

                        if b"Files sent to pi directly" in emailtext:#don't need to download files that were sent to pi directly
                            resp, datas = imapSession.fetch(msgId, "(UID)")
                            msg_uid = pattern_uid.match(datas[0]).group('uid')
                            result = imapSession.uid('COPY', msg_uid, 'pi')#add pi label
                            print("Skipping file as it was sent directly")
                            break
                        
                        if emailtext!=b"":#skip processlist
                            resp, datas = imapSession.fetch(msgId, "(UID)")
                            msg_uid = pattern_uid.match(datas[0]).group('uid')
                            result = imapSession.uid('COPY', msg_uid, 'pi')#add pi label
                            print("Skipping process list")
                            break
                        
                    if part.get_content_maintype() == 'multipart':
                        # print part.as_string()
                        continue
                    if part.get('Content-Disposition') is None:
                        # print part.as_string()
                        continue

                    fileName = part.get_filename()
                    if fileName in directfile:#received directly already
                        print("Skipping "+fileName)
                        directfile.remove(fileName)
                        resp, datas = imapSession.fetch(msgId, "(UID)")#label email
                        msg_uid = pattern_uid.match(datas[0]).group('uid')
                        result = imapSession.uid('COPY', msg_uid, 'pi')
                        
                    else:#did not receive directly already
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

                                result = imapSession.uid('COPY', msg_uid, 'pi')

                                key = secretkeys[-1]
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

imapSession.select("server")
typ, data = imapSession.search(None, 'ALL')
if typ != 'OK':
    print('Error searching Inbox.')
    raise

# Iterating over all emails
for msgId in data[0].split():
    t, label = imapSession.fetch(msgId, '(X-GM-LABELS)')
    if b"pi" in label[0]:
        imapSession.store(msgId, '+X-GM-LABELS', '\\Trash')
        
imapSession.close()
imapSession.logout()

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
            actualfilename = groups.group(1)+extension#get full filename
            actualfilenamewithoutext = groups.group(1)#get filename only

            if datetime.datetime.strptime(filedate, '%d-%m-%Y %H-%M-%S') < datetime.datetime.strptime(tendaysago, '%d-%m-%Y %H-%M-%S'):#compare dates
                filelist = [f for f in os.listdir(dirpath) if re.match(actualfilenamewithoutext+' [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]'+extension, f)]#get all files matching the file path, name and extension
                if len(filelist) > 1:#if there are more than one file
                    filelist = sorted(filelist)
                    filesize -= os.path.getsize(os.path.abspath(os.path.join(dirpath, filelist[0])))#compute file size
                    os.remove(os.path.abspath(os.path.join(dirpath, filelist[0])))#remove the first file
                    print("Deleted version as > 10 days " + os.path.abspath(os.path.join(dirpath, filelist[0])))
##Would not happen as if >10 days versioning is already deleted
##    if (filesize > 10 * 1024 * 1024 * 1024):#if the user directory is > 10GB, delete files older than user selected months but keep one version
##        xmonthsago = (datetime.datetime.today() - datetime.timedelta(days=(int(noofmonths.get(g))*30))).strftime('%d-%m-%Y %H-%M-%S')#get date x months ago
##        for dirpath,_,filenames in os.walk(detach_dir + '/attachments/' + g):
##            for f in sorted(filenames):
##                path = os.path.abspath(os.path.join(dirpath, f))#get path of currentfile
##                groups = re.search('(.+) ([0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9])(\.[^.]+$)', f)
##                filedate = groups.group(2)#get file date
##                actualfilename = groups.group(1)+groups.group(3)#get full filename
##                extension = groups.group(3)#get file extension
##                actualfilenamewithoutext = groups.group(1)#get filename only
##
##                if datetime.datetime.strptime(filedate, '%d-%m-%Y %H-%M-%S') < datetime.datetime.strptime(xmonthsago, '%d-%m-%Y %H-%M-%S'):#compare dates
##                    filelist = [f for f in os.listdir(dirpath) if re.match(actualfilenamewithoutext+' [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]'+extension, f)]#get all files matching the file path, name and extension
##                    if len(filelist) > 1:#if there are more than one file
##                        filelist = sorted(filelist)
##                        filesize -= os.path.getsize(os.path.abspath(os.path.join(dirpath, filelist[0])))#compute file size
##                        os.remove(os.path.abspath(os.path.join(dirpath, filelist[0])))#remove the first file
##                        print("Deleted as exceeded storage limit but keeping one version " + path)
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

#write to file
f = open(pythonscriptdirectory+'Receivedfiles.txt', 'w')
for a in directfile:
    f.write(a+'\n')
f.close()

#except Exception as ex:
#    print('Not able to download all attachments.')
#    print(ex)



