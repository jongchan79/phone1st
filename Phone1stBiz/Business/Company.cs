using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;

namespace Phone1stBiz.Business
{
    /// <summary>
    /// 매입/매도 업체 관련 비지니스 로직
    /// </summary>
    public class Company
    {
        /// <summary>
        /// 회사 유형
        /// </summary>
        public enum CompanySection
        {
            [Description("매입/매도 업체")]
            sell_buy,
            [Description("매도 업체")]
            sell,
            [Description("매입 업체")]
            buy
        }

        public List<CompanyInfo> GetCompanyDropDown(int section)
        {
            List<CompanyInfo> result = new List<CompanyInfo>();

            try
            {
                string query = string.Empty;
                DataSet ds = new DataSet();
                query = string.Format(" (SELECT '0' AS uid, '업체' AS companyName) UNION ALL  (SELECT CAST(uid AS CHAR) AS uid, companyName FROM companylist WHERE section = {0} AND clientID = {1})  ORDER BY uid "
                                    , section, User.ClientNo);

                using (DBUTIL du = new DBUTIL())
                {
                    ds = du.ExecuteDataSet(query);
                }

                int cnt = ds.Tables[0].Rows.Count;

                if (cnt > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        result.Add(new CompanyInfo()
                        {
                            uid = ds.Tables[0].Rows[i]["uid"].ToString(),
                            CompanyName = ds.Tables[0].Rows[i]["CompanyName"].ToString()
                        });
                    }
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("GetCompanyList", x.ToString());
            }

            return result;
        }

        /// <summary>
        /// 회사 리스트 가져오기
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public List<CompanyInfo> GetCompanyList(int section)
        {
            List<CompanyInfo> result = new List<CompanyInfo>();

            try
            {
                string query = string.Empty;
                DataSet ds = new DataSet();
                query = string.Format("   SELECT * FROM companylist WHERE section = {0} AND clientID = {1} ORDER BY uid DESC ", section, User.ClientNo);

                using (DBUTIL du = new DBUTIL())
                {
                    ds = du.ExecuteDataSet(query);
                }

                int cnt = ds.Tables[0].Rows.Count;

                if (cnt > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        result.Add( new CompanyInfo()
                        {
                            uid = ds.Tables[0].Rows[i]["uid"].ToString(),
                            section = Convert.ToInt16(ds.Tables[0].Rows[i]["section"]),
                            CompanyName = ds.Tables[0].Rows[i]["CompanyName"].ToString(),
                            ContactName = ds.Tables[0].Rows[i]["contactName"].ToString(),
                            hp = ds.Tables[0].Rows[i]["hp"].ToString(),
                            tel = ds.Tables[0].Rows[i]["tel"].ToString(),
                            addr = ds.Tables[0].Rows[i]["addr"].ToString(),
                            bankName = ds.Tables[0].Rows[i]["bankName"].ToString(),
                            bankNo = ds.Tables[0].Rows[i]["bankNo"].ToString(),
                            bankOwner = ds.Tables[0].Rows[i]["bankOwner"].ToString(),
                            memo = ds.Tables[0].Rows[i]["memo"].ToString(),
                            bonus = ds.Tables[0].Rows[i]["bonus"].ToString(),
                            register = ds.Tables[0].Rows[i]["register"].ToString(),
                            w_Date = ds.Tables[0].Rows[i]["w_date"].ToString()
                        });
                    }
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("GetCompanyList", x.ToString());
            }

            return result;
        }

        /// <summary>
        /// 하나의 회사 정보 가져오기
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public CompanyInfo GetCompanyInfo(string uid)
        {
            CompanyInfo cinfo = new CompanyInfo();

            try
            {
                string query = string.Format("  SELECT * FROM companylist WHERE uid = {0} ", uid);

                using (DBUTIL du = new DBUTIL())
                {
                    string[] arrTmp = du.ExecuteDataArray(query);

                    if (arrTmp.Length.Equals(15))
                    {
                        cinfo.uid = arrTmp[0];
                        cinfo.section = Convert.ToInt16(arrTmp[1]);
                        cinfo.CompanyName = arrTmp[2];
                        cinfo.ContactName = arrTmp[3];
                        cinfo.hp = arrTmp[4];
                        cinfo.tel = arrTmp[5];
                        cinfo.addr = arrTmp[6];
                        cinfo.bankName = arrTmp[7];
                        cinfo.bankNo = arrTmp[8];
                        cinfo.bankOwner = arrTmp[9];
                        cinfo.memo = arrTmp[10];
                        cinfo.bonus = arrTmp[11];
                        cinfo.register = arrTmp[12];
                        cinfo.w_Date = arrTmp[13];
                    }
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("GetCompanyList", x.ToString());
            }

            return cinfo;
        }

        /// <summary>
        /// 회사정보 업데이트/인서트
        /// </summary>
        /// <param name="cinfo"></param>
        /// <returns></returns>
        public int SetCompanyInfo(CompanyInfo cinfo)
        {
            int result = 0;

            try
            {
                string query = string.Empty;

                query = string.Format(" section = {0}, companyName = '{1}', contactName = '{2}', `hp` = '{3}', `tel` = '{4}', `addr` = '{5}', " +
                                            "   `bankName` = '{6}', `bankNo` = '{7}', `bankOwner` = '{8}', `memo` = '{9}', `bonus` = '{10}', `register` = '{11}',`w_date` = NOW(), clientID = {12} ",
                                            cinfo.section, cinfo.CompanyName, cinfo.ContactName, cinfo.hp, cinfo.tel, cinfo.addr, cinfo.bankName, cinfo.bankNo, cinfo.bankOwner,
                                            cinfo.memo, cinfo.bonus, cinfo.register, User.ClientNo);

                if (cinfo.uid.Equals("0") || string.IsNullOrEmpty(cinfo.uid))
                    query = string.Concat("   INSERT INTO companylist SET ", query);
                else
                    query = string.Format("   UPDATE companylist SET {0} WHERE uid = {1}", query, cinfo.uid);

                using (DBUTIL du = new DBUTIL())
                {
                    result = du.ExecuteQuery(query);
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("GetCompanyList", x.ToString());
            }

            return result;
        }

        public int Delete(string uid)
        {
            int result = 0;

            try
            {
                string query = string.Format("  DELETE FROM companylist WHERE uid = {0} ", uid);

                using (DBUTIL du = new DBUTIL())
                {
                    result = du.ExecuteQuery(query);
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("GetCompanyList", x.ToString());
            }

            return result;
        }
    }

    /// <summary>
    /// 매입/매도 업체 클래스
    /// 
    /// CREATE TABLE `companylist` (
    ///  `uid` int(11) NOT NULL AUTO_INCREMENT,
    ///  `section` tinyint(1) DEFAULT '1' COMMENT '0 : 매입/매도 1 : 매도업체, 2 : 매입업체',
    ///  `companyName` varchar(255) NOT NULL,
    ///  `contactName` varchar(50) DEFAULT NULL,
    ///  `hp` varchar(15) DEFAULT NULL,
    ///  `tel` varchar(20) DEFAULT NULL,
    ///  `addr` varchar(255) DEFAULT NULL,
    ///  `bankName` varchar(100) DEFAULT NULL,
    ///  `bankNo` varchar(30) DEFAULT NULL,
    ///  `bankOwner` varchar(50) DEFAULT NULL,
    ///  `memo` text,
    ///  `bonus` int(11) DEFAULT '0',
    ///  `register` varchar(20) DEFAULT NULL,
    ///  `w_date` datetime DEFAULT NULL,
    ///  PRIMARY KEY (`uid`)
    ///) ENGINE=MyISAM DEFAULT CHARSET=utf8
    /// </summary>
    public class CompanyInfo
    {
        /// <summary>
        /// 일련번호
        /// </summary>
        public string uid { get; set; }

        /// <summary>
        /// 매도/매입 구분
        /// </summary>
        public int section { get; set; }

        /// <summary>
        /// 회사명
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 담당자
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 핸드폰번호
        /// </summary>
        public string hp { get; set; }

        /// <summary>
        /// 전화번호
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// 주소
        /// </summary>
        public string addr { get; set; }

        /// <summary>
        /// 은행명
        /// </summary>
        public string bankName { get; set; }
        
        /// <summary>
        /// 계좌번호
        /// </summary>
        public string bankNo { get; set; }

        /// <summary>
        /// 예금주
        /// </summary>
        public string bankOwner { get; set; }

        /// <summary>
        /// 메모
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 보너스 금액(?) TODO : 추가할인액 정도 인 듯...
        /// </summary>
        public string bonus { get; set; }

        /// <summary>
        /// 등록자
        /// </summary>
        public string register { get; set; }

        /// <summary>
        /// 등록일자
        /// </summary>
        public string w_Date { get; set; }
    }
}
