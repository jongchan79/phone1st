using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Text;

namespace Phone1stBiz
{
    public class DBUTIL : IDisposable
    {
        private MySqlConnection myConn;
        private string _projectName = "DB_Util";

        #region DBUTIL

        /// <summary>
        /// 지정된 서버 주소를 이용하여 DB에 커넥션
        /// </summary>
        /// <param name="dbFullConnString">DB연결정보</param>
        /// <returns>
        /// DB에 커넥션
        /// </returns>
        /// <example>
        /// <code lang="C#">
        /// SoftnyxDB.DBUTIL du = new SoftnyxDB.DBUTIL("dbFullConnString");
        /// </code>
        /// </example>
        /// <remarks>
        ///  특수한 경우에만 사용함[사용 하지 말것]
        ///  커넥션 도중 예외상황이 있을 시 PROJECTNAME \\ ERROR.DBUTIL 에 로그 파일을 남깁니다.
        /// </remarks>
        public DBUTIL()
        {
            try
            {                
                myConn = new MySqlConnection("server=1.223.75.250; user id=phone1st; password=cksl7903; database=phone_db; Max Pool Size = 300; Min Pool Size = 10; pooling=true; Connection LifeTime = 600; Connection Timeout = 60;Charset=utf8;");
                // myConn = new MySqlConnection("server=localhost; user id=root; password=apmsetup; database=phone_db; Max Pool Size = 300; Min Pool Size = 10; pooling=true; Connection LifeTime = 600; Connection Timeout = 60;Charset=utf8;");
                myConn.Open();
            }
            catch (Exception ex)
            {
                DBError(ex, "");
            }
        }

        #endregion

        #region CreateMySqlExecuteScalar

        /// <summary>
        /// MySql 연결상태를 확인하고, 쿼리의 결과물을 object형으로 리턴
        /// </summary>
        /// <param name="dataQueryString">쿼리문</param>
        /// <returns>
        /// (object) 쿼리문의 결과 값, 결과값이 없을 경우 Null
        /// </returns>
        /// <example>
        /// <code lang="C#">
        /// SoftnyxDB.DBUTIL du = new SoftnyxDB.DBUTIL("serviceValue", "dbConnString", "fileOrFolderName");
        /// object example = du.CreateMySqlExecuteScalar("dataQueryString");
        /// </code>
        /// </example>
        /// <remarks></remarks>
        public string CreateMySqlExecuteString(string dataQueryString)
        {
            return CreateMySqlExecuteString(dataQueryString, 30);
        }

        public int CreateMySqlExecuteInt(string dataQueryString)
        {
            return CreateMySqlExecuteInt(dataQueryString, 30);
        }

        public long CreateMySqlExecuteLong(string dataQueryString)
        {
            return CreateMySqlExecuteLong(dataQueryString, 30);
        }

        public string CreateMySqlExecuteString(string dataQueryString, int commandTimeOut)
        {
            string returnValue = null;

            try
            {
                if (!object.Equals(myConn, null) && myConn.State == ConnectionState.Open)
                {
                    MySqlCommand myCommand = new MySqlCommand(dataQueryString, myConn);
                    myCommand.CommandTimeout = commandTimeOut;
                    returnValue = myCommand.ExecuteScalar().ToString();
                }
            }
            catch (MySqlException x)
            {
                DBError(x, dataQueryString);
            }
            catch (Exception e)
            {
                DBError(e, dataQueryString);
            }

            return returnValue;
        }

        public int CreateMySqlExecuteInt(string dataQueryString, int commandTimeOut)
        {
            int returnValue = 0;

            try
            {
                if (!object.Equals(myConn, null) && myConn.State == ConnectionState.Open)
                {
                    MySqlCommand myCommand = new MySqlCommand(dataQueryString, myConn);
                    myCommand.CommandTimeout = commandTimeOut;
                    returnValue = Convert.ToInt32(myCommand.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                DBError(e, dataQueryString);
                returnValue = -1;
            }

            return returnValue;
        }

        public long CreateMySqlExecuteLong(string dataQueryString, int commandTimeOut)
        {
            long returnValue = 0;

            try
            {
                if (!object.Equals(myConn, null) && myConn.State == ConnectionState.Open)
                {
                    MySqlCommand myCommand = new MySqlCommand(dataQueryString, myConn);
                    myCommand.CommandTimeout = commandTimeOut;
                    returnValue = Convert.ToInt64(myCommand.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                DBError(e, dataQueryString);
                returnValue = -1;
            }

            return returnValue;
        }

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// 쿼리의 결과물을 DataSet에 담은 후 리턴
        /// </summary>
        /// <param name="dataQueryString">쿼리문</param>
        /// <returns>
        /// (DataSet) 쿼리문의 결과값
        /// </returns>
        /// <example>
        /// <code lang="C#">
        /// SoftnyxDB.DBUTIL du = new SoftnyxDB.DBUTIL("serviceValue", "dbConnString", "fileOrFolderName");
        /// DataSet ds = du.ExecuteDataSet("dataQueryString");
        /// </code>
        /// </example>
        /// <remarks></remarks>
        public DataSet ExecuteDataSet(string dataQueryString)
        {
            return ExecuteDataSet(dataQueryString, 30);
        }

        /// <summary>
        /// 쿼리의 결과물을 DataSet에 담은 후 리턴
        /// </summary>
        /// <param name="dataQueryString">쿼리문</param>
        /// <param name="commandTimeOut">명령 실행을 종료하고 오류를 생성하기 전 대기 시간(default : 30초)</param>
        /// <returns>
        /// (DataSet) 쿼리문의 결과값
        /// </returns>
        /// <example>
        /// <code lang="C#">
        /// SoftnyxDB.DBUTIL du = new SoftnyxDB.DBUTIL("serviceValue", "dbConnString", "fileOrFolderName");
        /// DataSet ds = du.ExecuteDataSet("dataQueryString", 30);
        /// </code>
        /// </example>
        /// <remarks></remarks>
        public DataSet ExecuteDataSet(string dataQueryString, int commandTimeOut)
        {
            DataSet ds = new DataSet();

            try
            {
                MySqlDataAdapter mDataAdapter = new MySqlDataAdapter();
                MySqlCommand myCommand = new MySqlCommand();
                myCommand.Connection = myConn;

                if (!object.Equals(myConn, null) && myConn.State == ConnectionState.Open)
                {
                    myCommand.CommandText = "set names utf8";
                    myCommand.ExecuteNonQuery();

                    myCommand.CommandText = dataQueryString;
                    myCommand.CommandTimeout = commandTimeOut;
                    mDataAdapter.SelectCommand = myCommand;
                    mDataAdapter.Fill(ds);
                }
            }
            catch (Exception e)
            {
                DBError(e, dataQueryString);
            }

            return ds;
        }

        #endregion

        #region ExecuteDataArray

        /// <summary>
        /// 쿼리의 결과물을 DataSet에 담은 후 Colums의 개수만큼 배열에 담아 리턴 (구분자: ',')
        /// </summary>
        /// <param name="dataQueryString">쿼리문</param>
        /// <returns>
        /// (string[]) 쿼리문의 결과값
        /// </returns>
        /// <example>
        /// <code lang="C#">
        /// SoftnyxDB.DBUTIL du = new SoftnyxDB.DBUTIL("serviceValue", "dbConnString", "fileOrFolderName");
        /// string[] example = du.ExecuteDataArray("dataQueryString");
        /// </code>
        /// </example>
        /// <remarks></remarks>
        public string[] ExecuteDataArray(string dataQueryString)
        {
            return ExecuteDataArray(dataQueryString, 30);
        }

        /// <summary>
        /// 쿼리의 결과물을 DataSet에 담은 후 Colums의 개수만큼 배열에 담아 리턴 (구분자: ',')
        /// </summary>
        /// <param name="dataQueryString">쿼리문</param>
        /// <param name="commandTimeOut">명령 실행을 종료하고 오류를 생성하기 전 대기 시간(default : 30초)</param>
        /// <returns>
        /// (string[]) 쿼리문의 결과값
        /// </returns>
        /// <example>
        /// <code lang="C#">
        /// SoftnyxDB.DBUTIL du = new SoftnyxDB.DBUTIL("serviceValue", "dbConnString", "fileOrFolderName");
        /// string[] example = du.ExecuteDataArray("dataQueryString", 30);
        /// </code>
        /// </example>
        /// <remarks>
        /// array 개수 체크는 이 함수를 사용 하는 상위 코드에서 체크 할것
        /// </remarks>
        public string[] ExecuteDataArray(string dataQueryString, int commandTimeOut)
        {
            string[] returnValue = new string[1] { "" };
            string returnTemp = string.Empty;
            List<string> strData = new List<string>();

            try
            {
                using (DataSet ds = ExecuteDataSet(dataQueryString, commandTimeOut))
                {
                    if (!ds.Equals(null) && !ds.Tables.Count.Equals(0) && !ds.Tables[0].Rows.Count.Equals(0))
                    {
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            strData.Add(ds.Tables[0].Rows[0][dc].ToString());
                        }

                        returnValue = strData.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                DBError(e, dataQueryString);
            }

            return returnValue;
        }

        #endregion

        #region ExecuteQuery

        /// <summary>
        /// 쿼리를 실행
        /// </summary>
        /// <param name="dataQueryString">쿼리문</param>
        /// <returns>
        /// (int) 0 : 실패 /  1: 성공
        /// </returns>
        /// <example>
        /// <code lang="C#">
        /// SoftnyxDB.DBUTIL du = new SoftnyxDB.DBUTIL("serviceValue", "dbConnString", "fileOrFolderName");
        /// int example = du.ExecuteQuery("dataQueryString");
        /// </code>
        /// </example>
        /// <remarks>
        /// 예외 상황 시  PROJECTNAME \\ ERROR.DBUTIL 에 로그 파일을 남김.
        /// </remarks>
        public int ExecuteQuery(string dataQueryString)
        {
            return ExecuteQuery(dataQueryString, 30);
        }

        /// <summary>
        /// 로그남기는 메서드용
        /// </summary>
        /// <param name="dataQueryString"></param>
        /// <returns></returns>
        public int ExecuteQuery(string dataQueryString, bool textLog)
        {
            return ExecuteQuery(dataQueryString, textLog, "LogMakeError");
        }

        public int ExecuteQuery(string dataQueryString, bool chk, string log)
        {
            int returnValue = 0;

            try
            {
                MySqlCommand myCommand = new MySqlCommand();
                myCommand.Connection = myConn;

                if (!object.Equals(myConn, null) && myConn.State == ConnectionState.Open)
                {
                    myCommand.CommandText = "SET NAMES UTF8";
                    myCommand.ExecuteNonQuery();

                    myCommand.CommandText = dataQueryString;
                    myCommand.CommandTimeout = 50;
                    myCommand.ExecuteNonQuery();
                    returnValue = 1;
                }
            }
            catch (Exception e)
            {
                if (chk)
                    IO_Util.LogWrite("LogInsertError", e.ToString());
                
                throw e;
            }

            return returnValue;
        }

        /// <summary>
        /// 쿼리를 실행
        /// </summary>
        /// <param name="dataQueryString">쿼리문</param>
        /// <param name="commandTimeOut">명령 실행을 종료하고 오류를 생성하기 전 대기 시간(default : 30초)</param>
        /// <returns>
        /// (int) 0 : 실패 /  1: 성공
        /// </returns>
        /// <example>
        /// <code lang="C#">
        /// SoftnyxDB.DBUTIL du = new SoftnyxDB.DBUTIL("serviceValue", "dbConnString", "fileOrFolderName");
        /// int example = du.ExecuteQuery("dataQueryString", 30);
        /// </code>
        /// </example>
        /// <remarks>
        /// 예외 상황 시  PROJECTNAME \\ ERROR.DBUTIL 에 로그 파일을 남김.
        /// </remarks>
        public int ExecuteQuery(string dataQueryString, int commandTimeOut)
        {
            int returnValue = 0;

            try
            {
                MySqlCommand myCommand = new MySqlCommand();
                myCommand.Connection = myConn;

                if (!object.Equals(myConn, null) && myConn.State == ConnectionState.Open)
                {
                    myCommand.CommandText = "SET NAMES UTF8";
                    myCommand.ExecuteNonQuery();

                    myCommand.CommandText = dataQueryString;
                    myCommand.CommandTimeout = commandTimeOut;
                    returnValue = myCommand.ExecuteNonQuery();
                    // returnValue = 1;
                }
            }
            catch (Exception e)
            {
                DBError(e, dataQueryString);
            }

            return returnValue;
        }

        #endregion

        #region GetLastInsertExecuteSN

        /// <summary>
        /// string[] dataQueryString 의 첫번째 값을 먼저 실행하여 성공을 하였을 시 두번째 값의 쿼리를 실행하여 결과값을 리턴
        /// </summary>
        /// <param name="dataQueryString">쿼리문</param>
        /// <returns>
        /// (int) 쿼리문의 결과값, 결과값이 없을경우 Null
        /// </returns>
        /// <example>
        /// <code lang="C#">
        /// SoftnyxDB.DBUTIL du = new SoftnyxDB.DBUTIL("serviceValue", "dbConnString", "fileOrFolderName");
        /// int example = du.GetLastInsertExecuteSN(new string[2] {"dataQueryString1", "dataQueryString2"});
        /// </code>
        /// </example>
        /// <remarks></remarks>
        public int GetLastInsertExecuteSN(string dataQueryString)
        {
            return GetLastInsertExecuteSN(dataQueryString, 30);
        }

        /// <summary>
        /// string[] dataQueryString 의 첫번째 값을 먼저 실행하여 성공을 하였을 시 두번째 값의 쿼리를 실행하여 결과값을 리턴
        /// </summary>
        /// <param name="dataQueryString">쿼리문</param>
        /// <param name="commandTimeOut">명령 실행을 종료하고 오류를 생성하기 전 대기 시간(default : 30초)</param>
        /// <returns>
        /// (int) 쿼리문의 결과값, 결과값이 없을경우 Null
        /// </returns>
        /// <example>
        /// <code lang="C#">
        /// SoftnyxDB.DBUTIL du = new SoftnyxDB.DBUTIL("serviceValue", "dbConnString", "fileOrFolderName");
        /// int example = du.GetLastInsertExecuteSN(new string[2] {"dataQueryString1", "dataQueryString2"}, 30);
        /// </code>
        /// </example>
        /// <remarks></remarks>
        public int GetLastInsertExecuteSN(string dataQueryString, int commandTimeOut)
        {
            int returnValue = 0;

            try
            {
                if (ExecuteQuery(dataQueryString, commandTimeOut).Equals(1))
                {
                    returnValue = CreateMySqlExecuteInt("SELECT LAST_INSERT_ID()", commandTimeOut);
                }
            }
            catch (Exception x)
            {
                DBError(x, string.Concat("\r\n", dataQueryString[0], "\r\n", dataQueryString[1]));
            }

            return returnValue;
        }

        #endregion

        #region Procedure

        #region ExecuteProcedure


        /// <summary>
        /// 지정된 프로시저를 호출하여 데이터 셋으로 리턴받습니다.
        /// </summary>
        /// <param name="spName">프로시저 이름</param>
        /// <param name="arrSqlParam">프로시저에서 사용되는 파라미터정보</param>
        /// <param name="lsTransaction">DB 트랜젝션 커밋여부( true : commit 실행 / false : commit 실행되지 않음)</param>
        /// <param name="returnType">0:int , 1:string</param>
        /// <returns>
        /// DataSet
        /// </returns>
        /// <example>
        /// <code lang="C#">
        /// </code>
        /// </example>
        /// <remarks></remarks>
        public DataSet ExecuteProcedureDataSet(string spName, MySqlParameter[] arrSqlParam, bool lsTransaction)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                MySqlTransaction transaction = myConn.BeginTransaction();

                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = myConn;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spName;
                    if (lsTransaction) command.Transaction = transaction;

                    for (int i = 0; i < arrSqlParam.Length; i++)
                    {
                        command.Parameters.Add(arrSqlParam[i]);
                    }

                    da = new MySqlDataAdapter(command);
                    ds = new DataSet();
                    da.Fill(ds);

                }
                if (lsTransaction) transaction.Commit();

            }
            catch (Exception x)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(spName);
                sb.Append("\r\n");

                foreach (MySqlParameter para in arrSqlParam)
                {
                    sb.Append(para.ParameterName);
                    sb.Append(" : ");
                    sb.Append(para.Value);
                    sb.Append("\r\n");
                }

                DBError(x, sb.ToString());
            }

            return ds;
        }

        #endregion



        /// <summary>
        /// 프로시저와 명령어를 지정하여 connection 한 후 command 리턴
        /// </summary>
        /// <param name="storedProcedure">프로시저 명</param>
        /// <returns>
        /// (MySqlCommand) command
        /// </returns>
        /// <example>
        /// <code lang="C#">
        /// SoftnyxDB.DBUTIL du = new SoftnyxDB.DBUTIL("serviceValue", "dbConnString", "fileOrFolderName");
        /// MySqlCommand cmd = du.GetCommand("storedProcedure");
        /// </code>
        /// </example>
        /// <remarks></remarks>
        public MySqlCommand GetCommand(string storedProcedure)
        {
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                cmd.Connection = myConn;
            }
            catch (Exception x)
            {
                DBError(x, string.Concat("\r\nsp name : ", storedProcedure));
            }

            return cmd;
        }

        public string LoginProc(string id, string pwd, string ip)
        {
            string result = string.Empty;

            try          
            {
                
                using (MySqlCommand cmd = new MySqlCommand("sp_login", myConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("id", MySqlDbType.VarChar, 20).Value = id;
                    cmd.Parameters.Add("pwd", MySqlDbType.VarChar, 20).Value = pwd;
                    cmd.Parameters.Add("ip", MySqlDbType.VarChar, 20).Value = ip;
                    MySqlParameter param = new MySqlParameter("result", MySqlDbType.Int16);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();

                    result = cmd.Parameters["result"].Value.ToString();
                }
            }
            catch (Exception x)
            {
                DBError(x, string.Concat("\r\nLoginproc : ", id, ",", pwd, ",", ip));
            }

            return result;
        }

        #endregion

        #region DBError
        /// <summary>
        /// 쿼리문 실행하는 메쏘드에서 발생하는 오류 남김
        /// </summary>
        /// <param name="x">Exception</param>
        /// <param name="etc">추가로 남길 내용</param>
        private void DBError(Exception ex, string etc)
        {
            IO_Util.LogWrite(_projectName, string.Concat(ex.ToString(), "\r\n", etc));
        }

        #endregion

        #region IDisposable 멤버

        /// <summary>
        /// 현재 연결 상태 확인 후 연결 닫기
        /// </summary>
        void IDisposable.Dispose()
        {
            if (!object.Equals(myConn, null) && myConn.State == ConnectionState.Open)
            {
                myConn.Close();
                myConn.Dispose();
            }
        }

        #endregion
    }
}
