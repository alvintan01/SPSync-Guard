import string
import random
import MySQLdb
import datetime

key = ''.join(random.choice(string.ascii_uppercase + string.ascii_lowercase  + string.digits) for _ in range(16))

# Open database connection
db = MySQLdb.connect("localhost","webapp","1qwer$#@!","webapp" )

# prepare a cursor object using cursor() method
cursor = db.cursor()
sql = "INSERT INTO t_secretkey(c_secretkey) VALUES ('"+key+"')"
try:
   # Execute the SQL command
   cursor.execute(sql)
   # Commit your changes in the database
   db.commit()
except Exception as e:
   # Rollback in case there is any error
   db.rollback()
   print e

# disconnect from server
db.close()
