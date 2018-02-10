from __future__ import absolute_import
from django.contrib.staticfiles.storage import staticfiles_storage
from django.urls import reverse
from jinja2 import Environment

import re
import os



def regex_search(file_name,cwd):
	#filename+" [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]"+fileextension
	real_list = []
	print 'regex:_cwd'+cwd

	file_name = file_name.split('.')
	if len(file_name) > 2:
		print ''
		#do something

	file_regex = file_name[0] + " [0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9] [0-9][0-9]-[0-9][0-9]-[0-9][0-9]."+file_name[1]
	dir_list = os.listdir(cwd)
	for i in dir_list:
		print i
		if re.search(file_regex, i):
			real_list.append(i)
	print file_name
	print real_list
	return real_list

def environment(**options):
	env = Environment(**options)
	env.globals.update({'static': staticfiles_storage.url,'url': reverse,})
	env.globals['regex_search'] = regex_search
	return env