import pyAesCrypt
import zipfile
import sys
import os
#import random
#doing encrypting
bufferSize = 64 * 1024
secretkeys =[]
if os.path.isfile('/home/pi/Keys.txt'):
	lines = [line.rstrip('\n') for line in open('/home/pi/Keys.txt')]
	secretkeys = secretkeys+lines
else:
	secretkeys.append("defaultkey123456")
password = secretkeys[-1]


if len(sys.argv) == 2:
	f_name1 = sys.argv[1]
	pyAesCrypt.encryptFile(f_name1, f_name1 + '.aes', password, bufferSize)
