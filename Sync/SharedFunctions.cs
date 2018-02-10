using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sync
{
    class SharedFunctions
    {
        public static void sendpassword()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(Preference.clientemail);
                mail.To.Add(Preference.email);
                mail.Subject = "Your Recovery Password";
                mail.Body = "Your system has been infected with ransomware. Please do not switch on your computer and use another computer to recover your files from your backups. Your decryption password is " + Preference.usersecret + "."; //Text in email

                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(Preference.clientemail, Preference.clientemailpassword);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                mail.Dispose();
            }
            catch (Exception)
            {
            }
        }
        public static String createuser()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    byte[] response = client.UploadValues("https://" + Preference.serverip + "/createUser", new NameValueCollection()
                    {
                       { "key", Preference.secretkey },
                       { "username", Preference.email},
                       { "password", getstringhash(Preference.webuipassword)},
                       { "hash", getstringhash(Preference.salt+Preference.usersecret)},
                       { "salt", Preference.salt },
                       { "noofmonths", Preference.noofmonths.ToString()},
                       { "roomname", Preference.roomname}
                    });

                    string result = Encoding.UTF8.GetString(response).Replace("\n", ""); ;
                    Console.WriteLine(result);
                    if (result.Contains("User already"))
                    {
                        Preference.email = result.Substring(5, result.IndexOf("already")-6);//-6 to remove " alrea"
                        Preference.salt = result.Substring(26+Preference.email.Length);//25 as to remove User <email> already exists Salt:
                        return "true";
                    }
                    else
                    {
                        MessageBox.Show("User account created.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return "false";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "No internet";
            }
        }

        public static string getstringhash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }

        public static Boolean updatenoofmonths(int noofmonths)
        {
            getsession();
            try
            {
                using (WebClient client = new WebClient())
                {
                    byte[] response = client.UploadValues("https://" + Preference.serverip + "/updateNoofmonths", new NameValueCollection()
                    {
                       { "auth", Preference.auth },
                       { "sessionkey", Preference.sessionkey},
                       { "username", Preference.email},
                       { "noofmonths", noofmonths.ToString()}
                    });
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static Boolean getroomnames()
        {
            getsession();
            try
            {
                using (WebClient client = new WebClient())
                {
                    byte[] response = client.UploadValues("https://" + Preference.serverip + "/getRoom", new NameValueCollection()
                    {
                       { "auth", Preference.auth },
                       { "sessionkey", Preference.sessionkey}
                    });
                    
                    string result = Encoding.UTF8.GetString(response).Replace("\n", "");
                    if (result != "")
                    {
                        pi[] jsonlist = JsonConvert.DeserializeObject<pi[]>(result);
                        foreach (var a in jsonlist)
                        {
                            int roomid = a.RoomID;
                            String roomname = a.RoomName;
                            String ip = a.IP;
                            String sshsignature = a.SSHfingerprint;
                            Preference.pilist.Add(new pi(roomid, roomname, ip, sshsignature));
                        }
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public static void setstartup(Boolean enabled)
        {
            if (enabled)
            {
                try
                {
                    File.Copy(Application.StartupPath + "\\Sync.lnk", Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\Sync.lnk");
                }
                catch
                {
                }
                //Set the application to run at startup
                //RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                //key.SetValue(Process.GetCurrentProcess().ProcessName, Application.ExecutablePath.ToString());
            }
            else
            {
                try
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\Sync.lnk");
                }
                catch
                {
                }
            }
        }


        public static string EncryptStringToBytes_Aes(string plainText, byte[] Key)
        {
            byte[] IV = Encoding.ASCII.GetBytes("1234567890SPSync");
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Padding = PaddingMode.Zeros;


                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return Convert.ToBase64String(encrypted);

        }


        public static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key)
        {
            byte[] IV = Encoding.ASCII.GetBytes("1234567890SPSync");
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Padding = PaddingMode.Zeros;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        public static Boolean changeemail(String oldemail, String newemail)
        {
            getsession();
            try
            {
                using (WebClient client = new WebClient())
                {
                    byte[] response = client.UploadValues("https://" + Preference.serverip + "/changeEmail", new NameValueCollection()
                    {
                       { "auth", Preference.auth },
                       { "sessionkey", Preference.sessionkey},
                       { "username", oldemail},
                       { "newusername", newemail}
                    });
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static void getsession()
        {
            try {
                using (WebClient client = new WebClient())
                {
                    byte[] response = client.UploadValues("https://" + Preference.serverip + "/getSession", new NameValueCollection()
                    {
                    });

                    Preference.sessionkey = Encoding.UTF8.GetString(response).Replace("\n", "");
                    Preference.auth = SharedFunctions.EncryptStringToBytes_Aes(Preference.sessionkey, Encoding.ASCII.GetBytes(Preference.secretkey));

                }
            }
            catch (Exception)
            {
            }
        }
    }

}
