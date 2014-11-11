using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Phone1stBiz.Business
{
    public class User
    {
        public static string LoginID
        {
            get;
            set;
        }
                
        public static string ClientNo
        {
            get;
            set;
        }
        public string loginChk(string id, string pwd)
        {
            string result = string.Empty;
            
            using (DBUTIL du = new DBUTIL())
            {
                result = du.CreateMySqlExecuteString(string.Format("    SELECT clientID FROM chan_membership WHERE userid = '{0}' AND userpassword = RIGHT(MD5('{1}'),20);", id, pwd));
            }

            return result;
        }

        public string LoginCheck(string id, string pwd)
        {
            string result = string.Empty;
            int intResult = 0;

            using (DBUTIL du = new DBUTIL())
            {
                result = du.CreateMySqlExecuteString(string.Format("   CALL ", id, pwd));

                //if (!string.IsNullOrEmpty(result))
                //{
                //    intResult = du.CreateMySqlExecuteInt(string.Format(" SELECT COUNT(*) FROM clientlist WHERE uid = {0} AND clientUntilDate >= NOW() ", result));

                //    if (intResult.Equals(1))
                //    {
                //        intResult = du.ExecuteQuery(string.Format("   UPDATE chan_membership SET LastLogin = NOW() WHERE userid = '{0}' ", id));

                //        if (intResult.Equals(1))
                //            intResult = du.ExecuteQuery(string.Format(" INSERT INTO loginlog (clientID, LoginId, ipAddr, w_date) VALUES ({0}, '{1}', '{2}', NOW()) ", result, id, UserIP));
                //    }
                //    else
                //        result = "end";
                //}
            }

            if (!intResult.Equals(1))
            {
                result = "";
            }

            return result;
        }

        public string LoginProcess(string id, string pwd)
        {
            string result = string.Empty;

            try
            {
                using (DBUTIL du = new DBUTIL())
                {
                    result = du.LoginProc(id, pwd, UserIP);

                    // result = du.CreateMySqlExecuteString(string.Format("  CALL sp_login('{0}', '{1}', '{2}', @cnt); SELECT @cnt;", id, pwd, UserIP));
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("LoginProcess", x.ToString());
            }

            return result;
        }

        private static string _userip;

        public static string UserIP
        {
            get
            {
                if (string.IsNullOrEmpty(_userip))
                    GetIp();

                return _userip;
            }
        }

        public static void GetIp()
        {

            IPHostEntry myIP = Dns.GetHostEntry(Dns.GetHostName());
            _userip = myIP.AddressList[2].ToString();
        }
    }
}
