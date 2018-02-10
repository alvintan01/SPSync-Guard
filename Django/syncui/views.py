# -*- coding: utf-8 -*-
#from __future__ import unicode_literals

from django.shortcuts import render
from django.http import HttpResponse
from django.utils.encoding import smart_str
from django.template import RequestContext
import os
import StringIO
import zipfile
import unicodedata
import re
import datetime
import requests
import hashlib
import random
from subprocess import call
from Crypto.Cipher import AES
import base64
#from syncui.forms import datetimeForm
roomname='T2031' #roomname of the Raspberry Pi

def getfoldersize(start_path = '.'):
    total_size = 0
    for dirpath, dirnames, filenames in os.walk(start_path):
        for f in filenames:
            fp = os.path.join(dirpath, f)
            total_size += os.path.getsize(fp)
    return total_size

def zipdir(path, ziph,date_time):# ziph is zipfile handle, Using
	print type(date_time)
	for root, dirs, files in os.walk(path):
		file_info = {}#example would be {filename.extension:[version 1, version 2],...}
		for file in files:
			regex_WExt = '(.+) ([0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9])(\.[^.]+$)'
			regex_WOExt = '(.+) ([0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9])'
			#f = file.split('.')
			if re.search(regex_WExt,file):
				ext = re.search(regex_WExt,file).group(3)
				file_name = re.search(regex_WExt,file).group(1) + ext
				filedate = re.search(regex_WExt,file).group(2)
			else:
				print 'without extension'
				ext = ''
				file_name = re.search(regex_WOExt,file).group(1) + ext
				filedate = re.search(regex_WOExt,file).group(2)

			#ext = f[1]#getting extension
			#file_name = f[0].split(' ')[0] + '.' + ext#getfilename
			 
			#if len(f[0].split(' ')) == 3:
			#	file_name = f[0].split(' ')[0] + '.' + ext#getfilename
			#else:
			#	file_name = ""
			#	n = 1
			#	for z in f[0].split(' '):
			#		if n <= len(f[0].split(' ')) - 2:
			#			file_name += z
			#			if n < len(f[0].split(' ')) - 2:
			#				file_name += ' '
			#			n += 1
			#		else:
			#			break
			#	file_name = file_name + '.' + ext#get filename

			#groups = re.search('(.+) ([0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9])(\.[^.]+$)', file)
			#filedate = groups.group(2)

			if file_name in file_info:
				file_info[file_name].append(filedate)
			else:
				file_info[file_name] = [filedate]

		#print [file_info,root]
		for i in file_info:
			#print 'Lastest version: ' + max(file_info[i])
			pending = []
			for dt in file_info[i]:
				if datetime.datetime.strptime(dt, '%d-%m-%Y %H-%M-%S') <= datetime.datetime.strptime(date_time, '%d-%m-%Y %H-%M-%S'):#compare dates
					pending.append(dt)
			if len(pending) != 0:
				#print pending
				latest = max(pending)#get lastest version after filtering
				#name = i.split('.')[0] + ' ' + latest + '.' + i.split('.')[1]
				name_len = len(i.split('.'))
				if name_len != 1:
					name = ''
					counter = 1
					#print name_len
					#print i
					for x in i.split('.'):
						#print counter
						if counter == 1:
							name += x
						elif counter < name_len:
							new_x = '.' + x
							name += new_x
						else:
							name = name + ' ' + latest + '.' + x
						counter += 1
				else:
					name = i + ' ' + latest
				ziph.write(os.path.join(root, name),get_fakedir(os.path.join(root, name)))

def format_chromeDT(date_time):
	#if re.search('([0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]T[0-9][0-9]:[0-9][0-9])', date_time):
		#return 'format'
	date_time_formated = ""
	sp = date_time.split('T')
	dates = sp[0]
	times = sp[1]

	dates_lst = dates.split('-')
	times_lst = times.split(':')

	date_time_formated += dates_lst[2] + '-'
	date_time_formated += dates_lst[1] + '-'
	date_time_formated += dates_lst[0]

	date_time_formated += ' '

	date_time_formated += times_lst[0] + '-'
	date_time_formated += times_lst[1] + '-'
	date_time_formated += '00'

	print date_time_formated

	return date_time_formated

def get_fakedir(org_path):
	path_to_return = "/C/"
	org_path = org_path.split('/')
	print org_path
	#print f_name_org
	n = 0

	for bases in org_path:
		if n >= 5:
			if n == len(org_path) - 1:
				path_to_return += bases
				break
			else:
				print 'else'
				path_to_return += bases + '/'
		n += 1
	#path_to_return += f_name_org
	#print path_to_return
	return path_to_return




def zipdir_bkup(path, ziph):#backup of original zipdir, most likely not needed
	# ziph is zipfile handle
	for root, dirs, files in os.walk(path):
		for file in files:
			ziph.write(os.path.join(root, file))

def get_size(start_path):#size of directory, not implemented due to error
	total_size = 0
	for dirpath, dirnames, filenames in os.walk(start_path):
		for f in filenames:
			fp = os.path.join(dirpath, f)
			print [f,fp]
			total_size += os.path.getsize(fp)
	return total_size

def format_dir_name(base,lst):#using
	if len(lst) > 0:
		for i in lst:
			base = base + i + '/'
	return base

# Create your views here.
def index(request, **kwargs):
	if 'user' in request.session:
		return view_file_new(request,**kwargs)
	else:
		return render(request, 'syncui/home.html',{'placeholder':'placeholder', 'status': 'out'},RequestContext(request))

def test(request, **kwargs):#soon to be deleted
	data = request.GET
	para = data.dict()
	cwd = para['field1']
	print cwd
	return render(request, 'syncui/test.html')

def setting(request, **kwargs):#soon to be deleted
	return render(request, 'syncui/setting.html')

def download_file(request, **kwargs):#POST implementation
	if 'user' in request.session:
		data = request.POST
		para = data.dict()
		print para
		f = para['field1']#file path
		f_name = para['field2']#file name
		f_name_org = para['field2']
		if len(para) == 4:
			date_time = para['date']
			if re.search('([0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]T[0-9][0-9]:[0-9][0-9])', date_time):
				date_time = format_chromeDT(date_time)
		print f

		#if os.path.exists("/home/el/myfile.txt")

		if os.path.isfile(f):
			print 'file'
			ran1 = random.randint(1,50)*10
			ran2 = random.randint(1,50)*101
			f_name = str(ran1)+ f_name + str(ran2) + ".zip"
			f_name1 = "/media/USBHDD1/attachments/" + f_name
			zipf = zipfile.ZipFile(f_name1, 'w')
			#print get_fakedir(f)
			zipf.write(f,str(get_fakedir(f)))
			zipf.close()

			call(['python3','/home/pi/fyp/aesCrypt.py', f_name1])

			response = HttpResponse(open(f_name1 + '.aes', 'rb').read(), content_type='application/force-download')
			response['Content-Disposition'] = 'attachment; filename=%s' % smart_str("SPsync.sync")
			response['X-Sendfile'] = smart_str(f)

			os.remove(f_name1)
			os.remove(f_name1 + '.aes')
		else:
			print date_time
			ran1 = random.randint(1,50)*10
			ran2 = random.randint(1,50)*101
			f_name = str(ran1)+ f_name + str(ran2) + ".zip"
			f_name1 = "/media/USBHDD1/attachments/" + f_name
			#s = StringIO.StringIO()
			zipf = zipfile.ZipFile(f_name1, 'w')
			zipdir(f, zipf, date_time)
			zipf.close()

			call(['python3','/home/pi/fyp/aesCrypt.py', f_name1])

			response = HttpResponse(open(f_name1 + '.aes', 'rb').read(), content_type='application/x-zip-compressed')
			response['Content-Disposition'] = 'attachment; filename=%s' % smart_str("SPsync.sync")
			response['X-Sendfile'] = smart_str(f)

			os.remove(f_name1)
			os.remove(f_name1 + '.aes')
		return response
	else:
		return render(request, 'syncui/home.html',{'placeholder':'placeholder'},RequestContext(request))

#def sort_dict(a):
#	a1 = {}
#	keys = sorted(a.keys(),key=str.lower)
#	for i in keys:
#		a1[i] = a[i]
#	print a1
#	return a1


def view_file_new(request, **kwargs):#POST implementation
	if 'user' in request.session:#will load page if user session is active
		data = request.POST
		para = data.dict()
		print para
		if ('loginID' in para and 'password' in para):
			cwd = '/media/USBHDD1/attachments/' + request.session['user'] + '/'
			base = []
		elif len(para) == 0:
			cwd = '/media/USBHDD1/attachments/' + request.session['user'] + '/'
			base = []
		elif len(para) != 3:
			cwd = para['field1']# base directory
			base = []
		else:
			cwd = para['field1']#selected directory
			base = data.getlist('field2') #base directory
		#print cwd
		#print type(cwd)
		if os.path.exists(cwd):
			dir_list = os.listdir(cwd)
			a = {}
			b1 = {}
			#b = u''
			#file_size = []
			print dir_list
			for i in dir_list:
                            if not i.endswith(".sync"):
				b = cwd + i
				#print i
				#print b
				if os.path.isdir(b):
					a[i] = ['Folder',int(getfoldersize(b)), re.sub(r"/media/USBHDD1/attachments/[^/]+/", "C:/", b, 1), 'Folder'+re.sub(r"[^0-9a-zA-Z-]", "", i)]
				else:
					#f = i.split('.')
					regex_WExt = '(.+) ([0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9])(\.[^.]+$)'
					regex_WOExt = '(.+) ([0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9])'
					if re.search(regex_WExt,i):
						ext = re.search(regex_WExt,i).group(3)
						file_name = re.search(regex_WExt,i).group(1) + ext
					else:
						ext = ''
						file_name = re.search(regex_WOExt,i).group(1) + ext

					#groups = re.search('(.+) ([0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9])(\.[^.]+$)', i)
					#filedate = groups.group(2)

					if file_name in a:
						b1[file_name].append(i)
					else:
						a[file_name] = ['File',int(os.path.getsize(b)), re.sub(r"/media/USBHDD1/attachments/[^/]+/", "C:/", cwd+file_name, 1), 'File'+re.sub(r"[^0-9a-zA-Z-]", "", file_name)]
						#print type(i)
						#print type(file_name)
						b1[file_name] = [i]
			print base
			print a
			print b1
		else:
			a = {}
			b1 = {}
		#print b
		#print 'test1'
		#new_cwd = format_dir_name('/home/user/account/bob/real/',base)
		return render(request, 'syncui/view_file.html',{'entries':a,'base':base,'length':len(base),'cwd':cwd,'version': b1, 'username': request.session['user'], 'status': 'in'}, RequestContext(request))
	else:#redirect user back to login page if user session is not active
		print 'test'
		return render(request, 'syncui/home.html',{'placeholder':'placeholder'},RequestContext(request))

def login(request, **kwargs):
	global roomname
	if 'user' in request.session:
		#print 'insession'
		return view_file_new(request,**kwargs)
	else:
		data = request.POST
		para = data.dict()
		#print para
		print 'para'
		if len(para) != 0:
			try:
				username = para["loginID"]
				password = para["password"]
				secretkeys = []

				if os.path.isfile('/home/pi/Keys.txt'):
					lines = [line.rstrip('\n') for line in open('/home/pi/Keys.txt')]
					secretkeys = secretkeys+lines
				else:
					secretkeys.append("defaultkey123456")

				url = "https://<yourserverip>/getSession"
    				payload = {}
   				r = requests.post(url, data=payload, verify=False)
    				sessionkey = r.text.strip()
    				aes = AES.new(secretkeys[-1], AES.MODE_CBC, IV = "1234567890SPSync")
    				auth = base64.b64encode(aes.encrypt(sessionkey))

				url = "https://<yourserverip>/validateUser"
				payload = {'sessionkey': sessionkey, 'auth': auth, 'username':username, 'hash':hashlib.sha512(password).hexdigest(), 'roomname': roomname}
				r = requests.post(url, data=payload, verify=False)
				print r.text
				if r.text == 'Success\n':
				#if True:
					print 'setting session'
					request.session['user'] = username
					print 'loading page'
					return view_file_new(request,**kwargs)
				elif r.text == 'Exceeded\n':
					return render(request, 'syncui/home.html',{'placeholder':'You have exceeded your login attempts. Check your email for help.'},RequestContext(request))#Exceeded attempts
				else:
					return render(request, 'syncui/home.html',{'placeholder':'Invalid Login Credentials.'},RequestContext(request))#Invalid Login Credentials
			except Exception as e:
				print e
				return render(request, 'syncui/home.html',{'placeholder':'Could not connect to the server.'},RequestContext(request))
		else:
			return render(request, 'syncui/home.html',{'placeholder':'placeholder'},RequestContext(request))


def logout(request, **kwargs):
	if 'user' in request.session:
		try:
			del request.session['user']
		except KeyError:
			pass
	return render(request, 'syncui/home.html',{'placeholder':'placeholder'},RequestContext(request))
