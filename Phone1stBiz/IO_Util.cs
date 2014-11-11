using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Permissions;

namespace Phone1stBiz
{
    public class IO_Util
    {
        public static void Alert(string msg)
        {
            MessageBox.Show(msg);
        }

        public static void LogWrite(string fileName, string log, string tmp)
        {
            try
            {
                string query = "CREATE TABLE IF NOT EXISTS `phone_db`.`errorlist`( `uid` INT(11) NOT NULL AUTO_INCREMENT, `UserID` VARCHAR(30), `ClientIP` VARCHAR(15), `ErrorName` VARCHAR(255), `ErrorLog` TEXT, `w_date` DATETIME, KEY(`uid`) ) ENGINE=INNODB CHARSET=utf8 COLLATE=utf8_general_ci;  ";

                using (DBUTIL du = new DBUTIL())
                {
                    // TODO : 다음 배포 때 삭제해야함
                    du.ExecuteQuery(query);

                    query = string.Format("   INSERT INTO errorlist (UserID, ClientIP, ErrorName, ErrorLog, w_date) VALUES ('{0}', '{1}', '{2}', '{3}', NOW()) ",
                                                Business.User.LoginID, Business.User.UserIP, fileName.Replace("'", "''"), log.Replace("'", "''"));

                    int result = du.ExecuteQuery(query, true);                    

                    if (!result.Equals(1))
                        LogWrite("LogWriteFail", query, "");
                }
            }
            catch (Exception x)
            {
                LogWrite("LogWriteFail", x.ToString(), "");
            }
        }

        public static void LogWrite(string fileName, string log)
        {
            string LogFile = string.Empty;

            LogFile = "C:\\Phone1St_Log\\";

            try
            {

                FileIOPermission permission = new FileIOPermission(FileIOPermissionAccess.Write, LogFile);
                permission.Demand();

                if (!Directory.Exists(LogFile))
                    Directory.CreateDirectory(LogFile);
                
                LogFile = string.Concat(LogFile, DateTime.Now.ToString("yyyMMdd"), "_", fileName, ".txt");
                log = log.Replace("[BR]", "\r\n");

                if (!File.Exists(LogFile))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(LogFile))
                    {
                        sw.Write(DateTime.Now.ToString("u").Replace("Z", "") + " =====================================================\r\n");
                        sw.WriteLine(log);
                        sw.Flush();
                        sw.Close();
                    }
                }
                else
                {
                    // This text is always added, making the file longer over time
                    // if it is not deleted.
                    using (StreamWriter sw = File.AppendText(LogFile))
                    {
                        sw.Write(DateTime.Now.ToString("u").Replace("Z", "") + " =====================================================\r\n");
                        sw.WriteLine(log);
                        sw.Flush();
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
