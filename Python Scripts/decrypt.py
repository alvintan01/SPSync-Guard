#!/usr/bin/env python
#This script is to decrypt the files sent to the raspberry pi at a faster rate.
#You can choose to only use downloadattachmentpi3.py
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

secretkeys = []
directfile = []
noofmonths = {}
emailuserName = ""#your server email address
smtpUser = ''#your client email address to send direct backup 
smtpPass = ''#your client email password
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
    url = "https://"+cloudserveraddress+"/getDataPi"
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

    
#write to file
f = open(pythonscriptdirectory+'Receivedfiles.txt', 'w')
for a in directfile:
    f.write(a+'\n')
f.close()
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
