
using System;
using System.Collections.Generic;

namespace Sync
{
    class Preference
    {
        public static int backupfrequency = 30;
        public static String backupdirectory = "C:\\Sync Backup";
        public static long directorysize = 4096;
        public static String usersecret = "";
        public static String usersecret2 = "";
        public static String webuipassword = "";
        public static String webuipassword2 = "";
        public static String email = "";
        public static String oldpassword = "";//to be used in restore function only
        public static List<String> processlist = new List<String>();
        public static List<String> trustedprocesslist = new List<String>();
        public static List<String> tempprocesslist = new List<String>();
        public static String serverip = "";//enter your server ip
        public static String piip = "";
        public static String pisshfingerprint = "";
        public static String piusername = "pi";
        public static String pipassword = "raspberry";
        public static String serveremail = "";//enter your email addresses
        public static String serveremailpassword = "";
        public static String clientemail = "";
        public static String clientemailpassword = "";
        public static String secretkey = "defaultkey123456";
        public static int noofmonths = 6;
        public static long usage = 0;
        public static String salt = "";
        public static DateTime startdate;
        public static DateTime enddate;
        public static Boolean closeform = false;
        public static Boolean usercloseform = false;
        public static string sessionkey = "";
        public static string auth = "";
        public static string roomname = "";
        public static List<pi> pilist = new List<pi>();
        public static int roomid = 0;
    }
}
