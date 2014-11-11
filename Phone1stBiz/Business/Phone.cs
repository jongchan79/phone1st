using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq;

namespace Phone1stBiz.Business
{
    public class Phone
    {
        public List<PhoneTotalInfo> TotalSellBuyList(string Maker, string agency, string startDate, string endDate, string dateGbn)
        {
            List<PhoneTotalInfo> list = new List<PhoneTotalInfo>();

            try
            {
                string query = string.Format("   SELECT a.uid, a.iemiNo, a.uniqueKey, a.phoneState, FORMAT(a.buyPrice + a.AddPrice,0) AS buy, FORMAT(a.sellPrice, 0) AS sell, b.PhoneName, b.ModelName, " +
                                    "   b.Manufacturer, b.agency, " +
                                    "   c.w_date, e.w_date AS sellDate, c.CompanySn, IF(IFNULL(a.sellOrderno, '') = '', '', CAST(e.CompanySn AS CHAR)) AS sellCompany " +
                                    "   FROM phonelist a LEFT JOIN phonemodellist b ON a.p_uid = b.uid " +
                                    "   LEFT JOIN orderlist c ON a.orderno = c.orderno " +
                                    "   LEFT JOIN orderlist e ON a.sellOrderno = e.orderno WHERE a.clientid = {0} ", User.ClientNo);

                if (!string.IsNullOrEmpty(Maker) && !Maker.Equals("0"))
                    query = string.Concat(query, " AND b.Manufacturer = ", Maker);

                if (!string.IsNullOrEmpty(agency) && !agency.Equals("0"))
                    query = string.Concat(query, " AND b.agency = ", agency);

                if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                    query = string.Concat(query, " AND ", dateGbn, "w_date BETWEEN '", startDate, " 00:00:00' AND '", endDate, " 23:59:59' ");

                DataSet ds = new DataSet();
                DataSet dsCompany = new DataSet();

                using (DBUTIL du = new DBUTIL())
                {
                    ds = du.ExecuteDataSet(query);

                    if (ds.Tables[0].Rows.Count > 0)
                        dsCompany = du.ExecuteDataSet("SELECT uid, companyName FROM companylist ");
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string sellcompanyName = string.Empty;
                    string buycomapnyName = string.Empty;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow[] dr = dsCompany.Tables[0].Select("uid = " + ds.Tables[0].Rows[i]["CompanySn"]);

                        if (dr.Count() > 0)
                            buycomapnyName = dr[0][1].ToString();
                        else
                            buycomapnyName = "";

                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["sellCompany"].ToString()))
                        {
                            dr = dsCompany.Tables[0].Select("uid = " + ds.Tables[0].Rows[i]["sellCompany"]);

                            if (dr.Count() > 0)
                                sellcompanyName = dr[0][1].ToString();
                            else
                                sellcompanyName = "";
                        }
                        else
                            sellcompanyName = "";                      

                        list.Add(new PhoneTotalInfo() { 
                            uid = ds.Tables[0].Rows[i]["uid"].ToString(),
                            iemiNo = ds.Tables[0].Rows[i]["iemiNo"].ToString(),
                            uniqueKey = ds.Tables[0].Rows[i]["uniqueKey"].ToString(),
                            phoneState = ds.Tables[0].Rows[i]["phoneState"].ToString(),
                            buyPrice = ds.Tables[0].Rows[i]["buy"].ToString(),
                            sellPrice = ds.Tables[0].Rows[i]["sell"].ToString(),
                            phoneName = ds.Tables[0].Rows[i]["phoneName"].ToString(),
                            modelName = ds.Tables[0].Rows[i]["modelName"].ToString(),
                            Manufacturer = GetMakerName(ds.Tables[0].Rows[i]["Manufacturer"].ToString()),
                            Agency = GetAgencyName(ds.Tables[0].Rows[i]["agency"].ToString()),
                            buyDate = ds.Tables[0].Rows[i]["w_date"].ToString(),
                            sellDate = ds.Tables[0].Rows[i]["sellDate"].ToString(),
                            buyCompanyName = buycomapnyName,
                            sellCompanyName = sellcompanyName,
                            buyCompany = ds.Tables[0].Rows[i]["CompanySn"].ToString(),
                            sellCompany = ds.Tables[0].Rows[i]["sellCompany"].ToString()                            
                        });
                    }
                }

            }
            catch (Exception x)
            {
                IO_Util.LogWrite("TotalSellBuyList", x.ToString());
            }

            return list;
        }

        /// <summary>
        /// 현재 보유중인 상품 리스트
        /// </summary>
        /// <param name="Maker"></param>
        /// <param name="agency"></param>
        /// <returns></returns>
        public List<stockInfo> GetStockList(string Maker, string agency, string company)
        {
            List<stockInfo> result = new List<stockInfo>();

            try
            {
                string query = string.Format("   SELECT a.uid, a.p_uid, a.orderno, a.iemiNo, a.uniqueKey, a.phoneState, FORMAT((a.buyPrice + a.AddPrice), 0) AS buyPrice, a.barcode, b.PhoneName, b.ModelName, b.Manufacturer, b.agency, c.register, c.w_date, d.CompanyName " +
                                    "   FROM phonelist a LEFT JOIN phonemodellist b ON a.p_uid = b.uid " +
                                    "   LEFT JOIN orderlist c ON a.orderno = c.orderno " +
                                    "   LEFT JOIN companylist d ON c.companySn = d.uid WHERE (a.sellOrderNo IS NULL OR a.sellOrderNo = '') AND a.clientID = {0} ", User.ClientNo);

                if (!string.IsNullOrEmpty(Maker) && !Maker.Equals("0"))
                    query = string.Concat(query, " AND b.Manufacturer = ", Maker);

                if (!string.IsNullOrEmpty(agency) && !agency.Equals("0"))
                    query = string.Concat(query, " AND b.agency = ", agency);

                if (!string.IsNullOrEmpty(company) && !company.Equals("0"))
                    query = string.Concat(query, " AND c.companySn = ", company);

                query = string.Concat(query, " ORDER BY a.uid ");
                DataSet ds = new DataSet();

                using (DBUTIL du = new DBUTIL())
                {
                    ds = du.ExecuteDataSet(query);
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        result.Add(new stockInfo()
                        {
                            uid = ds.Tables[0].Rows[i]["uid"].ToString(),
                            ordernoBuy = ds.Tables[0].Rows[i]["orderno"].ToString(),
                            phoneModelUid = ds.Tables[0].Rows[i]["p_uid"].ToString(),
                            phoneName = ds.Tables[0].Rows[i]["phoneName"].ToString(),
                            modelName = ds.Tables[0].Rows[i]["modelName"].ToString(),
                            uniqueKey = ds.Tables[0].Rows[i]["uniqueKey"].ToString(),
                            iemiNo = ds.Tables[0].Rows[i]["iemiNo"].ToString(),
                            phoneState = ds.Tables[0].Rows[i]["phoneState"].ToString(),
                            buyPrice = ds.Tables[0].Rows[i]["buyPrice"].ToString(),
                            register = ds.Tables[0].Rows[i]["register"].ToString(),
                            barcodeNo = ds.Tables[0].Rows[i]["barcode"].ToString(),
                            Manufacturer = GetMakerName(ds.Tables[0].Rows[i]["Manufacturer"].ToString()),
                            Agency = GetAgencyName(ds.Tables[0].Rows[i]["Agency"].ToString()),
                            buyCompany = ds.Tables[0].Rows[i]["CompanyName"].ToString(),
                            w_Date = ds.Tables[0].Rows[i]["w_date"].ToString()
                        });
                    }
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("GetStockList", x.ToString());
            }
            return result;
        }

        /// <summary>
        /// 재고 상품이 있는지 체크
        /// </summary>
        /// <param name="data">검색할 데이터</param>
        /// <param name="SearchType">0:barcode / 1:uniquekey</param>
        /// <returns></returns>
        public string CheckStock(string data, int SearchType)
        {
            string result = string.Empty;

            try
            {
                string query = string.Format("    SELECT uid FROM phonelist WHERE clientID = {0} AND (sellorderno = '' OR sellorderno IS NULL) AND ", User.ClientNo);

                if (SearchType.Equals(0))   // 바코드 검색                   
                    query = string.Concat(query, " barcode = '", data, "' ");
                else// 일련번호 검색
                    query = string.Concat(query, " uniqueKey = '", data, "' ");

                DataSet ds = new DataSet();

                using (DBUTIL du = new DBUTIL())
                {
                    ds = du.ExecuteDataSet(query);
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    result = string.Concat(result, ",", ds.Tables[0].Rows[i][0]);
                }

                if (result.Length > 0)
                    result = result.Substring(1);
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("CheckStock", x.ToString());
            }

            return result;
        }
        
        /// <summary>
        /// 여러개의 상품 출고 처리
        /// </summary>
        /// <param name="phonelist"></param>
        /// <param name="order"></param>
        /// <param name="orderUid"></param>
        /// <param name="orderNo"></param>
        /// <param name="arrPhoneUid"></param>
        /// <returns></returns>
        public int SetSellAll(List<phoneInfo> phonelist, OrderInfo order, ref string orderUid, ref string orderNo)
        {
            int result = 0;

            string query = string.Empty;
            int phoneCount = phonelist.Count();

            if (!string.IsNullOrEmpty(order.uid) && !string.IsNullOrEmpty(order.orderno))
            {
                #region 출고 정보 수정
                orderNo = order.orderno;
                orderUid = order.uid;
                query = string.Format("   UPDATE orderlist SET companySn = {0}, memo = '{1}', buyCount = {2} WHERE uid = {3} ", order.companyNo, order.memo, phoneCount, order.uid);

                using (DBUTIL du = new DBUTIL())
                {
                    result = du.ExecuteQuery(query);

                    if (result.Equals(1))
                    {
                        foreach (phoneInfo info in phonelist)
                        {
                            if (!string.IsNullOrEmpty(info.uid) && !info.uid.Equals("0"))
                            {
                                query = string.Format("   UPDATE phonelist SET p_uid = {0}, sellPrice = {1}, barcode = '{2}', sellOrderno = '{3}' WHERE uid = {4} ",
                                                               info.phoneModelUid, info.sellPrice, info.barcodeNo, info.ordernoSell, info.uid);

                                result = du.ExecuteQuery(query);
                            }
                            else
                                result = -1;
                        }
                    }
                }

                #endregion
            }
            else
            {
                // 출고 정보 입력
                #region orderno 생성
                if (string.IsNullOrEmpty(orderNo))
                {
                    string tmpcnt = string.Empty;

                    orderNo = DateTime.Now.ToString("yyyyMMddHHmmss");

                    using (DBUTIL du = new DBUTIL())
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            query = string.Concat("   SELECT COUNT(*) FROM orderlist WHERE orderno = '", orderNo, i, "'");
                            tmpcnt = du.CreateMySqlExecuteString(query);

                            if (tmpcnt.Equals("0"))
                            {
                                orderNo = string.Concat(orderNo, i);
                                break;
                            }
                        }
                    }
                }
                #endregion

                query = string.Format("   INSERT INTO orderlist SET orderno = '{0}', companySn = {1}, buyCount = {2}, memo = '{3}', register = '{4}', section = '1', w_date = NOW(), clientID = {5} ",
                                orderNo, order.companyNo, phoneCount, order.memo, order.register, User.ClientNo);

                using (DBUTIL du = new DBUTIL())
                {
                    orderUid = du.GetLastInsertExecuteSN(query).ToString();

                    if (!string.IsNullOrEmpty(orderUid) && Convert.ToInt32(orderUid) > 0)
                    {
                        result = 1;

                        foreach (phoneInfo info in phonelist)
                        {
                            if (!string.IsNullOrEmpty(info.uid))
                            {
                                query = string.Format(" UPDATE phonelist SET sellPrice = {0}, sellorderno = '{1}' WHERE uid = {2} ",
                                info.sellPrice, orderNo, info.uid);
                                result = du.ExecuteQuery(query);

                                if (!result.Equals(1))
                                {
                                    // 정보입력 실패시 모두 롤백
                                    du.ExecuteQuery("   DELETE FROM orderlist WHERE uid = " + orderUid);
                                    du.ExecuteQuery("   UPDATE phonelist SET sellOrderno = null, sellPrice = null WHERE sellORderno = '" + orderNo + "' ");
                                    break;
                                }
                            }
                            else
                            {
                                result = -1;
                                break;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public int DeleteSellOrder(string orderno)
        {
            int result = 0;

            try
            {
                string query = string.Format("    DELETE FROM orderlist WHERE orderno = '{0}' ", orderno);

                using (DBUTIL du = new DBUTIL())
                {
                    result = du.ExecuteQuery(query);

                    if (result.Equals(1))
                    {
                        query = string.Format("UPDATE phonelist SET sellOrderno = NULL, sellPrice = 0 WHERE sellOrderno = '{0}' AND clientID = {1} ", orderno, User.ClientNo);
                        result = du.ExecuteQuery(query);
                    }
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("DeleteSellOrder", x.ToString());
            }

            return result;
        }

        #region 입고 관련 처리

        public int DeleteBuyOrder(string orderno)
        {
            int result = 0;

            try
            {
                string query = string.Format("    SELECT COUNT(*) FROM phonelist WHERE clientID = {1} AND orderno = '{0}'  AND sellOrderNo <> '' ", orderno, User.ClientNo);

                using (DBUTIL du = new DBUTIL())
                {
                    result = du.CreateMySqlExecuteInt(query);

                    if (result.Equals(0))
                    {
                        query = string.Format("    DELETE FROM orderlist WHERE orderno = '{0}' ", orderno);
                        result = du.ExecuteQuery(query);

                        if (result.Equals(1))
                        {
                            query = string.Format("DELETE FROM phonelist WHERE orderno = '{0}' ", orderno);
                            result = du.ExecuteQuery(query);
                        }
                    }
                    else
                        result = 99;
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("DeleteBuyOrder", x.ToString());
            }

            return result;
        }

        /// <summary>
        /// 한 개의 입고 오더 정보 가져오기
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="arrPhoneUid"></param>
        /// <returns></returns>
        public OrderInfo GetOrderInfo(string uid, string section, ref string[] arrPhoneUid)
        {
            OrderInfo result = new OrderInfo();

            try
            {
                string query = string.Empty;
                string[] arrInfo = { "" };

                query = string.Concat("   SELECT * FROM orderlist WHERE uid = ", uid);

                using (DBUTIL du = new DBUTIL())
                {
                    arrInfo = du.ExecuteDataArray(query);
                }

                if (arrInfo.Length.Equals(9))
                {
                    result.uid = arrInfo[0];
                    result.orderno = arrInfo[1];
                    result.companyNo = arrInfo[2];
                    result.count = arrInfo[3];
                    result.memo = arrInfo[4];
                    result.register = arrInfo[6];
                    result.date = arrInfo[7];

                    query = string.Concat("   SELECT uid FROM phonelist WHERE ", section.Equals("2") ? "orderno" : "sellorderno", " = '", result.orderno, "' ");

                    DataSet ds = new DataSet();

                    using (DBUTIL du = new DBUTIL())
                    {
                        ds = du.ExecuteDataSet(query);
                    }

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string tmpStr = string.Empty;

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            tmpStr = string.Concat(tmpStr, ",", ds.Tables[0].Rows[i][0]);    
                        }

                        arrPhoneUid = tmpStr.Substring(1).Split(',');
                    }
                }
             }
            catch (Exception x)
            {
                IO_Util.LogWrite("GetCompanyList", x.ToString());
            }

            return result;
        }

        public OrderInfo GetOrderInfo(string orderno)
        {
            OrderInfo result = new OrderInfo();

            try
            {
                string query = string.Empty;
                string[] arrInfo = { "" };

                query = string.Concat("   SELECT * FROM orderlist WHERE orderno = '", orderno, "'");

                using (DBUTIL du = new DBUTIL())
                {
                    arrInfo = du.ExecuteDataArray(query);
                }

                if (arrInfo.Length.Equals(9))
                {
                    result.uid = arrInfo[0];
                    result.orderno = arrInfo[1];
                    result.companyNo = arrInfo[2];
                    result.count = arrInfo[3];
                    result.memo = arrInfo[4];
                    result.register = arrInfo[6];
                    result.date = arrInfo[7];
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("GetCompanyList", x.ToString());
            }

            return result;
        }

        /// <summary>
        /// 입/출고 오더 목록
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="company"></param>
        /// <param name="section">입출고 구분(2 : 입고, 1 : 출고)</param>
        /// <returns></returns>
        public List<OrderInfo> GetOrderList(string startDate, string endDate, string company, string section)
        {
            List<OrderInfo> result = new List<OrderInfo>();

            try
            {
                DataSet ds = new DataSet();

                string query = string.Concat("    SELECT a.*, b.companyName FROM orderlist a LEFT JOIN companylist b ON a.companySn = b.uid  WHERE a.clientID = ", User.ClientNo, " AND a.section = ", section);

                if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                    query = string.Concat(query, string.Format(" AND a.w_date BETWEEN '{0} 00:00:00' AND '{1} 23:59:59' ", startDate, endDate));

                if (!string.IsNullOrEmpty(company) && !company.Equals("0"))
                    query = string.Concat(query, string.Format(" AND a.companySn = {0}", company));

                query = string.Concat(query, " ORDER BY uid DESC ");

                using (DBUTIL du = new DBUTIL())
                {
                    ds = du.ExecuteDataSet(query);
                }
                
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int cnt = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < cnt; i++)
                    {
                        result.Add(new OrderInfo()
                        {
                            uid = ds.Tables[0].Rows[i]["uid"].ToString(),
                            orderno = ds.Tables[0].Rows[i]["orderno"].ToString(),
                            companyNo = ds.Tables[0].Rows[i]["companySn"].ToString(),
                            companyName = ds.Tables[0].Rows[i]["companyName"].ToString(),
                            count = ds.Tables[0].Rows[i]["buyCount"].ToString(),
                            memo = ds.Tables[0].Rows[i]["memo"].ToString(),
                            register = ds.Tables[0].Rows[i]["register"].ToString(),
                            date = ds.Tables[0].Rows[i]["w_date"].ToString()
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
        /// 입/출고 상품 삭제
        /// </summary>
        /// <param name="phoneUid"></param>
        /// <param name="orderNo"></param>
        /// <param name="section"></param> 
        /// <returns></returns>
        public int DeletePhoneOrder(string phoneUid, string orderNo, string section)
        {
            int result = 0;

            try
            {
                string query = string.Empty;

                if (section.Equals("2"))
                    query = string.Concat("    DELETE FROM phonelist WHERE (sellOrderno = '' OR sellOrderno IS NULL) AND uid = ", phoneUid);
                else
                    query = string.Concat(" UPDATE phonelist SET sellOrderno = '', SellPrice = 0 WHERE uid = ", phoneUid);

                using (DBUTIL du = new DBUTIL())
                {
                    result = du.ExecuteQuery(query);

                    if (result.Equals(1))
                    {
                        query = string.Format(" SELECT COUNT(*) FROM phonelist WHERE {0} = '{1}'", section.Equals("2") ? "orderno" : "sellOrderno", orderNo);
                        string cnt = du.ExecuteDataArray(query)[0];

                        if (cnt.Equals("0"))
                            query = string.Format(" DELETE FROM orderlist WHERE orderno = '{0}' ", orderNo);
                        else
                            query = string.Format(" UPDATE orderlist SET buyCount = {0} WHERE orderno = '{1}' ", cnt, orderNo);

                        result = du.ExecuteQuery(query);
                    }
                    else
                        result = 99;
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("DeleteOrder", x.ToString());
            }

            return result;
        }

        public string MakeBarcode()
        {
            string result = string.Empty;
            string query = string.Empty;
            string tmpbarcode = DateTime.Now.ToString("MMddHHmmss");
            string barcode = string.Empty;
            try
            {
                string log = "=====================\r\n";
                using (DBUTIL duBarcode = new DBUTIL())
                {
                    for (int i = 0; i < 99; i++)
                    {
                        if (i < 10)
                            barcode = string.Concat(tmpbarcode, "0", i.ToString());
                        else
                            barcode = string.Concat(tmpbarcode, i.ToString());

                        log += barcode + "\r\n";
                        query = string.Format(" SELECT COUNT(*) FROM phonelist WHERE barcode = '{0}' ", barcode);
                        log += query + "\r\n";
                        result = duBarcode.ExecuteDataArray(query)[0];
                        log += result + "\r\n";

                        if (result.Equals("0"))
                            break;
                    }
                }

                IO_Util.LogWrite("aaaa", log);
            }
            catch (Exception ex)
            {
                IO_Util.LogWrite("MakeBarcode", ex.ToString());
            }

            return barcode;
        }

        /// <summary>
        /// 1개의 상품 입고 처리
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="order"></param>
        /// <param name="orderUid"></param>
        /// <param name="orderNo"></param>
        /// <param name="PhoneUid"></param>
        /// <returns></returns>
        public int SetOrderOne(phoneInfo phone, OrderInfo order, ref string orderUid, ref string orderNo, ref string PhoneUid)
        {
            int result = 0;

            try
            {
                string query = string.Empty;
                string barcode = string.Empty;
                string cnt = string.Empty;

                if (string.IsNullOrEmpty(phone.buyAddPrice))
                    phone.buyAddPrice = "0";

                if (!string.IsNullOrEmpty(order.uid) && !string.IsNullOrEmpty(order.orderno))
                {
                    // order 정보 수정임
                    orderNo = order.orderno;
                    orderUid = order.uid;

                    query = string.Format("   UPDATE orderlist SET companySn = {0}, memo = '{1}' WHERE uid = {2} ", order.companyNo, order.memo, order.uid);                    

                    using (DBUTIL du = new DBUTIL())
                    {
                        result = du.ExecuteQuery(query);

                        if (result.Equals(1))
                        {
                            if (!string.IsNullOrEmpty(phone.uid) && !phone.uid.Equals("0"))
                            {
                                query = string.Format("   UPDATE phonelist SET p_uid = {0}, iemiNo = '{1}', uniqueKey = '{2}', phoneState = '{3}', buyPrice = {4}, barcode = '{5}', addPriceText = '{6}', addPrice = {7} WHERE uid = {8} ",
                                            phone.phoneModelUid, phone.iemiNo, phone.uniqueKey, phone.phoneState, phone.buyPrice, phone.barcodeNo, phone.buyAddText, phone.buyAddPrice, phone.uid);

                                result = du.ExecuteQuery(query);
                                PhoneUid = phone.uid;
                            }
                            else
                            {
                                query = string.Format(" INSERT INTO phonelist SET p_uid = {0}, iemiNo = '{1}', uniqueKey = '{2}', phoneState = '{3}', buyPrice = {4}, barcode = '{5}', orderno = '{6}', addPriceText = '{7}', addPrice = {8}, clientID = {9} ",
                                            phone.phoneModelUid, phone.iemiNo, phone.uniqueKey, phone.phoneState, phone.buyPrice, phone.barcodeNo, order.orderno, phone.buyAddText, phone.buyAddPrice, User.ClientNo);

                                PhoneUid = du.GetLastInsertExecuteSN(query).ToString();

                                if (!string.IsNullOrEmpty(PhoneUid))
                                    result = 1;
                            }
                        }

                        if (result.Equals(1))
                        {
                            query = string.Concat("UPDATE orderlist a SET buyCount = (SELECT COUNT(*) FROM phonelist WHERE orderno = a.orderno) WHERE uid = ", order.uid);
                            result = du.ExecuteQuery(query);
                        }
                    }
                }
                else
                { 
                    // order 정보 입력
                    if (string.IsNullOrEmpty(orderNo))
                    {
                        #region orderno 생성
                        string tmpcnt = string.Empty;

                        orderNo = DateTime.Now.ToString("yyyyMMddHHmmss");
                        bool chkMakeOrderno = false;

                        using (DBUTIL du = new DBUTIL())
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                query = string.Concat("   SELECT COUNT(*) FROM orderlist WHERE orderno = '", orderNo, i, "'");
                                tmpcnt = du.CreateMySqlExecuteString(query);

                                if (tmpcnt.Equals("0"))
                                {
                                    orderNo = string.Concat(orderNo, i);
                                    chkMakeOrderno = true;
                                    break;
                                }
                            }
                        }
                        #endregion

                        if (chkMakeOrderno)
                        {
                            query = string.Format("   INSERT INTO orderlist SET orderno = '{0}', companySn = {1}, buyCount = {2}, memo = '{3}', register = '{4}', section = '{5}', w_date = NOW(), clientID = {6} ",
                                     orderNo, order.companyNo, "1", order.memo, User.LoginID, order.section, User.ClientNo);

                            using (DBUTIL du = new DBUTIL())
                            {
                                orderUid = du.GetLastInsertExecuteSN(query).ToString();
                                result = -1;

                                if (!string.IsNullOrEmpty(orderUid) && Convert.ToInt32(orderUid) > 0)
                                {
                                    query = string.Format(" INSERT INTO phonelist SET p_uid = {0}, iemiNo = '{1}', uniqueKey = '{2}', phoneState = '{3}', buyPrice = {4}, barcode = '{5}', orderno = '{6}', addPriceText = '{7}', addPrice = {8}, clientID = {9} ",
                                            phone.phoneModelUid, phone.iemiNo, phone.uniqueKey, phone.phoneState, phone.buyPrice, phone.barcodeNo, orderNo, phone.buyAddText, phone.buyAddPrice, User.ClientNo);

                                    PhoneUid = du.GetLastInsertExecuteSN(query).ToString();

                                    if (!string.IsNullOrEmpty(PhoneUid))
                                        result = 1;
                                }
                            }
                        }
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
        /// 여러개의 상품 입고 처리
        /// </summary>
        /// <param name="phonelist"></param>
        /// <param name="order"></param>
        /// <param name="orderUid"></param>
        /// <param name="orderNo"></param>
        /// <param name="arrPhoneUid"></param>
        /// <returns></returns>
        public int SetOrderAll(List<phoneInfo> phonelist, OrderInfo order, ref string orderUid, ref string orderNo, ref string[] arrPhoneUid)
        {
            int result = 0;

            try
            {
                string query = string.Empty;
                int phoneCount = phonelist.Count();
                int intLoop = 0;
                arrPhoneUid = new string[phoneCount];

                if (!string.IsNullOrEmpty(order.uid) && !string.IsNullOrEmpty(order.orderno))
                {
                    #region order 정보 수정임
                    orderNo = order.orderno;
                    orderUid = order.uid; 
                    query = string.Format("   UPDATE orderlist SET companySn = {0}, memo = '{1}', buyCount = {2} WHERE uid = {3} ", order.companyNo, order.memo, phoneCount, order.uid);
                    
                    using (DBUTIL du = new DBUTIL())
                    {
                        result = du.ExecuteQuery(query);

                        if (result.Equals(1))
                        {
                            intLoop = 0;
                            foreach (phoneInfo info in phonelist)
                            {
                                if (string.IsNullOrEmpty(info.barcodeNo))
                                    info.barcodeNo = MakeBarcode();

                                if (string.IsNullOrEmpty(info.buyAddPrice))
                                    info.buyAddPrice = "0";

                                if (!string.IsNullOrEmpty(info.uid) && !info.uid.Equals("0"))
                                {
                                    query = string.Format("   UPDATE phonelist SET p_uid = {0}, iemiNo = '{1}', uniqueKey = '{2}', phoneState = '{3}', buyPrice = {4}, barcode = '{5}', addPriceText = '{6}', addPrice = {7} WHERE uid = {8} ",
                                                info.phoneModelUid, info.iemiNo, info.uniqueKey, info.phoneState, info.buyPrice, info.barcodeNo, info.buyAddText, info.buyAddPrice, info.uid);

                                    result = du.ExecuteQuery(query);

                                    arrPhoneUid[intLoop] = info.uid;
                                }
                                else
                                {
                                    query = string.Format(" INSERT INTO phonelist SET p_uid = {0}, iemiNo = '{1}', uniqueKey = '{2}', phoneState = '{3}', buyPrice = {4}, barcode = '{5}', orderno = '{6}', addPriceText = '{7}', addPrice = {8}, clientID = {9} ",
                                                                    info.phoneModelUid, info.iemiNo, info.uniqueKey, info.phoneState, info.buyPrice, info.barcodeNo, orderNo, info.buyAddText, info.buyAddPrice, User.ClientNo);
                                    arrPhoneUid[intLoop] = du.GetLastInsertExecuteSN(query).ToString();

                                    if (string.IsNullOrEmpty(arrPhoneUid[intLoop]))
                                    {
                                        result = -1;
                                        break;
                                    }
                                }

                                intLoop++;
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region order 정보 입력
                    orderNo = order.orderno;

                    #region orderno 생성
                    if (string.IsNullOrEmpty(orderNo))
                    {
                        string tmpcnt = string.Empty;

                        orderNo = DateTime.Now.ToString("yyyyMMddHHmmss");

                        using (DBUTIL du = new DBUTIL())
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                query = string.Concat("   SELECT COUNT(*) FROM orderlist WHERE orderno = '", orderNo, i, "'");
                                tmpcnt = du.CreateMySqlExecuteString(query);

                                if (tmpcnt.Equals("0"))
                                {
                                    orderNo = string.Concat(orderNo, i);
                                    break;
                                }
                            }
                        }
                    }
                    #endregion

                    query = string.Format("   INSERT INTO orderlist SET orderno = '{0}', companySn = {1}, buyCount = {2}, memo = '{3}', register = '{4}', section = '2', w_date = NOW(), clientID = {5} ",
                                                    orderNo, order.companyNo, phoneCount, order.memo, order.register, User.ClientNo);

                    using (DBUTIL du = new DBUTIL())
                    {
                        orderUid = du.GetLastInsertExecuteSN(query).ToString();
                        string barcode = string.Empty;

                        if (!string.IsNullOrEmpty(orderUid) && Convert.ToInt32(orderUid) > 0)
                        {
                            result = 1;
                            intLoop = 0;

                            foreach (phoneInfo info in phonelist)
                            {
                                if (!string.IsNullOrEmpty(info.uid) && !info.uid.Equals("0"))
                                {
                                    result = -1;
                                    break;
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(info.barcodeNo))
                                        info.barcodeNo = MakeBarcode();

                                    if (string.IsNullOrEmpty(info.buyAddPrice))
                                        info.buyAddPrice = "0";

                                    query = string.Format(" INSERT INTO phonelist SET p_uid = {0}, iemiNo = '{1}', uniqueKey = '{2}', phoneState = '{3}', buyPrice = {4}, barcode = '{5}', orderno = '{6}', addPriceText = '{7}', addPrice = {8}, clientID = {9} ",
                                                                    info.phoneModelUid, info.iemiNo, info.uniqueKey, info.phoneState, info.buyPrice, info.barcodeNo, orderNo, info.buyAddText, info.buyAddPrice, User.ClientNo);
                                    arrPhoneUid[intLoop] = du.GetLastInsertExecuteSN(query).ToString();
                                }
                                
                                intLoop++;
                            }
                        }
                    }

                    #endregion
                }

            }
            catch (Exception x)
            {
                IO_Util.LogWrite("GetCompanyList", x.ToString());
            }

            return result;
        }

        #endregion

        /// <summary>
        /// 한개의 상품 정보 가져오기
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public phoneInfo GetPhoneInfo(string uid)
        {
            phoneInfo pinfo = new phoneInfo();

            try
            {
                string query = string.Concat("    SELECT * FROM phonelist WHERE uid = ", uid);
                DataSet ds = new DataSet();
                string[] arrTmp = { "" };

                using (DBUTIL du = new DBUTIL())
                {
                    arrTmp = du.ExecuteDataArray(query);
                }

                if (arrTmp.Length.Equals(13))
                {
                    pinfo.uid = arrTmp[0];
                    pinfo.ordernoBuy = arrTmp[1];
                    pinfo.ordernoSell = arrTmp[2];
                    pinfo.phoneModelUid = arrTmp[3];
                    pinfo.iemiNo = arrTmp[4];
                    pinfo.uniqueKey = arrTmp[5];
                    pinfo.phoneState = arrTmp[6];
                    pinfo.buyPrice = arrTmp[7];
                    pinfo.sellPrice = arrTmp[8];
                    pinfo.barcodeNo = arrTmp[9];
                    pinfo.buyAddText = arrTmp[10];
                    pinfo.buyAddPrice = arrTmp[11];
                }                    
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("GetPhoneInfo", x.ToString());
            }

            return pinfo;
        }

        /// <summary>
        /// 재고 상품 가져오기
        /// </summary>
        /// <param name="arrData"></param>
        /// <param name="SearchType"></param>
        /// <returns></returns>
        public string[] GetPhoneInfoUid(string[] arrData, int SearchType)
        {
            string[] arrResult = { "" };

            try
            {
                string query = string.Format("    SELECT uid FROM phonelist WHERE clientID = {0} AND (sellorderno = '' OR sellorderno IS NULL) AND ", User.ClientNo);

                if (SearchType.Equals(0))
                {
                    // 바코드 검색
                    query = string.Concat(query, " barcode IN (");
                }
                else if (SearchType.Equals(1))
                { 
                    // 일련번호 검색
                    query = string.Concat(query, " uniqueKey IN (");
                }

                string tmp = string.Empty;

                for (int i = 0; i < arrData.Length; i++)
                {
                    tmp = string.Concat(tmp, ",'", arrData[i], "' ");
                }

                if (tmp.Length > 0)
                {
                    query = string.Concat(query, tmp.Substring(1), ")");

                    DataSet ds = new DataSet();
                    
                    using (DBUTIL du = new DBUTIL())
                    {
                        ds = du.ExecuteDataSet(query);
                    }

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        arrResult = new string[ds.Tables[0].Rows.Count];

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            arrResult[i] = ds.Tables[0].Rows[i][0].ToString();
                        }
                    }
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("GetPhoneInfoUid", x.ToString());
            }

            return arrResult;
        }

        #region 핸드폰 모델 관리
        public List<phoneModelInfo> GetPhoneModelList(string manufacturer, string agency)
        {
            List<phoneModelInfo> result = new List<phoneModelInfo>();

            try
            {
                DataSet ds = new DataSet();
                string query = "    SELECT * FROM phonemodellist WHERE 1 ";

                if (Convert.ToInt16(manufacturer) > 0)
                    query = string.Concat(query, " AND Manufacturer = ", manufacturer);

                if (Convert.ToInt16(agency) > 0)
                    query = string.Concat(query, " AND agency = ", agency);

                using (DBUTIL du = new DBUTIL())
                {
                    ds = du.ExecuteDataSet(query);
                }

                int cnt = ds.Tables[0].Rows.Count;

                if (cnt > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        result.Add(new phoneModelInfo()
                        {
                            uid = ds.Tables[0].Rows[i]["uid"].ToString(),
                            phoneName = ds.Tables[0].Rows[i]["phoneName"].ToString(),
                            modelName = ds.Tables[0].Rows[i]["modelName"].ToString(),
                            agency = GetAgencyName(ds.Tables[0].Rows[i]["agency"].ToString()),
                            Manufacturer = GetMakerName(ds.Tables[0].Rows[i]["Manufacturer"].ToString()),
                            
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
        
        public phoneModelInfo GetPhoneModel(string no)
        {
            phoneModelInfo result = new phoneModelInfo();

            try
            {
                string query = string.Empty;
                string[] arrTmp = { "" };

                query = string.Concat(" SELECT * FROM phonemodellist WHERE uid = ", no);
                                            
                using (DBUTIL du = new DBUTIL())
                {
                    arrTmp = du.ExecuteDataArray(query);
                }

                if (arrTmp.Length.Equals(6))
                {
                    result.uid = arrTmp[0];
                    result.phoneName = arrTmp[1];
                    result.modelName = arrTmp[2];
                    result.Manufacturer = arrTmp[3];
                    result.agency = arrTmp[4];
                    result.color = arrTmp[5];                    
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("GetCompanyList", x.ToString());
            }

            return result;
        }

        public int DeletePhoneModel(string uid)
        {
            int result = 0;

            try
            {
                string query = string.Empty;

                query = string.Concat(" DELETE FROM phonemodellist WHERE uid = ", uid);

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

        public int SetModelInfo(phoneModelInfo info)
        {
            int result = 0;

            try
            {
                string query = string.Empty;

                query = string.Format(" phoneName = '{0}', modelName = '{1}', Manufacturer = {2}, `agency` = {3}, `color` = {4} ",
                                            info.phoneName, info.modelName, info.Manufacturer, info.agency, info.color);

                if (info.uid.Equals("0"))
                    query = string.Concat(" INSERT INTO phonemodellist SET ", query);
                else
                    query = string.Concat(" UPDATE phonemodellist SET ", query, " WHERE uid = ", info.uid);

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

        public string GetMakerName(string no)
        {
            string result = string.Empty;

            try
            {
                XDocument xmlDoc = XDocument.Load("Maker.xml");

                var makers = from maker in xmlDoc.Descendants("Maker")
                             where (string)maker.Element("No") == no
                             select new
                             {
                                 No = maker.Element("No").Value,
                                 MakerName = maker.Element("MakerName").Value
                             };

                result = makers.ElementAt(0).MakerName;
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("GetCompanyList", x.ToString());
            }
            return result;
        }
        
        public string GetAgencyName(string agency)
        {
            switch (agency)
            {
                case "1":
                    return "SK";
                case "2":
                    return "KT";
                case "3":
                    return "LG";
                default:
                    return "Err";
            }
        }

        public List<Maker> GetMakerList()
        {
            List<Maker> list = new List<Maker>();

            try
            {
                XDocument xmlDoc = XDocument.Load("Maker.xml");

                var makers = from maker in xmlDoc.Descendants("Maker")                             
                             select new
                             {
                                 No = maker.Element("No").Value,
                                 MakerName = maker.Element("MakerName").Value
                             };

                foreach (var Maker in makers)
                {
                    list.Add(new Maker()
                    {
                        uid = Maker.No,
                        MakerName = Maker.MakerName
                    });
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("GetCompanyList", x.ToString());
            }

            return list;
        }
        #endregion

        #region 입고/출고 원장 출력 관리

        public DataSet GetPhoneInfoByOrderNo(string orderno, string section)
        {
            DataSet ds = new DataSet();

            try
            {
                string query = string.Format("  SELECT b.PhoneName, b.modelName, a.uniqueKey, a.iemiNo, a.phoneState, IFNULL(a.buyPrice, 0) AS buyPrice, IFNULL(a.sellPrice, 0) AS sellPrice " +
                                    "   FROM phonelist a LEFT JOIN phonemodellist b ON a.p_uid = b.uid WHERE a.clientID = {0} AND ", User.ClientNo);

                if (section.Equals("2"))
                    query = string.Concat(query, " orderno = '", orderno, "' ");
                else
                    query = string.Concat(query, " Sellorderno = '", orderno, "' ");

                using (DBUTIL du = new DBUTIL())
                {
                    ds = du.ExecuteDataSet(query);
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("Phone.GetPhoneInfoByOrderNo", x.ToString());
            }

            return ds;
        }
        #endregion
    }

    /// <summary>
    /// 제조사 클래스
    /// </summary>
    public class Maker
    {
        public string uid { get; set; }

        public string MakerName { get; set; }
    }

    /// <summary>
    /// 핸드폰 모델 클래스
    /// </summary>
    public class phoneModelInfo
    {
        /// <summary>
        /// 일련번호
        /// </summary>
        public string uid { get; set; }

        /// <summary>
        /// 폰이름
        /// </summary>
        public string phoneName { get; set; }

        /// <summary>
        /// 모델명
        /// </summary>
        public string modelName { get; set; }

        /// <summary>
        /// 통신사
        /// </summary>
        public string agency { get; set; }

        /// <summary>
        /// 제조사
        /// </summary>
        public string Manufacturer { get; set; }
        
        /// <summary>
        ///  색상
        /// </summary>
        public string color { get; set; }
    }

    /// <summary>
    /// 주문서 클래스
    /// </summary>
    public class OrderInfo
    {
        /// <summary>
        /// 일련번호
        /// </summary>
        public string uid { get; set; }

        /// <summary>
        /// 2 : 매입, 1 : 매도
        /// </summary>
        public string section { get; set; }
        /// <summary>
        /// 주문번호
        /// </summary>
        public string orderno { get; set; }

        public string companyNo { get; set; }

        public string companyName { get; set; }

        public string count { get; set; }

        public string memo { get; set; }

        public string register { get; set; }

        public string date { get; set; }
    }

    /// <summary>
    /// 상품 클래스
    /// </summary>
    public class phoneInfo
    {
        /// <summary>
        /// 일련번호
        /// </summary>
        public string uid { get; set; }

        /// <summary>
        /// 주문번호(구매)
        /// </summary>
        public string ordernoBuy { get; set; }

        /// <summary>
        /// 주문번호(판매)
        /// </summary>
        public string ordernoSell { get; set; }

        /// <summary>
        /// phonemodellist.uid
        /// </summary>
        public string phoneModelUid { get; set; }

        /// <summary>
        /// 일련번호
        /// </summary>
        public string uniqueKey { get; set; }

        /// <summary>
        /// IEMI No
        /// </summary>
        public string iemiNo {get; set;}

        /// <summary>
        /// 검수상태
        /// </summary>
        public string phoneState { get; set; }

        /// <summary>
        /// 매입금액
        /// </summary>
        public string buyPrice { get; set; }

        /// <summary>
        /// 추가금액 설명
        /// </summary>
        public string buyAddText { get; set; }

        /// <summary>
        /// 추가금액
        /// </summary>
        public string buyAddPrice { get; set; }

        /// <summary>
        /// 매도금액
        /// </summary>
        public string sellPrice { get; set; }

        /// <summary>
        /// 등록자
        /// </summary>
        public string register { get; set; }

        /// <summary>
        /// 바코드 번호
        /// </summary>
        public string barcodeNo { get; set; }
        /// <summary>
        /// 등록일자
        /// </summary>
        public string w_Date { get; set; }
    }

    /// <summary>
    /// 상품 재고 클래스
    /// </summary>
    public class stockInfo
    {
        /// <summary>
        /// 일련번호
        /// </summary>
        public string uid { get; set; }

        /// <summary>
        /// 주문번호(구매)
        /// </summary>
        public string ordernoBuy { get; set; }

        /// <summary>
        /// phonemodellist.uid
        /// </summary>
        public string phoneModelUid { get; set; }

        /// <summary>
        /// 핸드폰 이름
        /// </summary>
        public string phoneName { get; set; }

        /// <summary>
        /// 모델명
        /// </summary>
        public string modelName { get; set; }

        /// <summary>
        /// 일련번호
        /// </summary>
        public string uniqueKey { get; set; }

        /// <summary>
        /// IEMI No
        /// </summary>
        public string iemiNo { get; set; }

        /// <summary>
        /// 검수상태
        /// </summary>
        public string phoneState { get; set; }

        /// <summary>
        /// 매입금액
        /// </summary>
        public string buyPrice { get; set; }

        /// <summary>
        /// 등록자
        /// </summary>
        public string register { get; set; }

        /// <summary>
        /// 바코드 번호
        /// </summary>
        public string barcodeNo { get; set; }

        /// <summary>
        /// 제조사
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// 통신사
        /// </summary>
        public string Agency { get; set; }

        /// <summary>
        /// 매입기업
        /// </summary>
        public string buyCompany { get; set; }

        /// <summary>
        /// 등록일자
        /// </summary>
        public string w_Date { get; set; }
    }

    public class PhoneTotalInfo
    {
        /// <summary>
        /// 일련번호(phonelist.uid)
        /// </summary>
        public string uid { get; set; }

        /// <summary>
        /// 주문번호(구매)
        /// </summary>
        public string ordernoBuy { get; set; }

        /// <summary>
        /// 주문번호(판매)
        /// </summary>
        public string ordernoSell { get; set; }

        /// <summary>
        /// phonemodellist.uid
        /// </summary>
        public string phoneModelUid { get; set; }

        /// <summary>
        /// 핸드폰 이름
        /// </summary>
        public string phoneName { get; set; }

        /// <summary>
        /// 모델명
        /// </summary>
        public string modelName { get; set; }

        /// <summary>
        /// 일련번호
        /// </summary>
        public string uniqueKey { get; set; }

        /// <summary>
        /// IEMI No
        /// </summary>
        public string iemiNo { get; set; }

        /// <summary>
        /// 검수상태
        /// </summary>
        public string phoneState { get; set; }

        /// <summary>
        /// 바코드 번호
        /// </summary>
        public string barcodeNo { get; set; }

        /// <summary>
        /// 제조사
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// 통신사
        /// </summary>
        public string Agency { get; set; }

        /// <summary>
        /// 매입금액
        /// </summary>
        public string buyPrice { get; set; }

        /// <summary>
        /// 판매금액
        /// </summary>
        public string sellPrice { get; set; }

        /// <summary>
        /// 등록자
        /// </summary>
        public string register { get; set; }
        
        /// <summary>
        /// 매입 업체 번호
        /// </summary>
        public string buyCompany { get; set; }

        /// <summary>
        /// 판매 업체 번호
        /// </summary>
        public string sellCompany { get; set; }

        /// <summary>
        /// 구매 업체명
        /// </summary>
        public string buyCompanyName { get; set; }

        /// <summary>
        /// 판매 업체명
        /// </summary>
        public string sellCompanyName { get; set; }

        /// <summary>
        /// 구매일자
        /// </summary>
        public string buyDate { get; set; }

        /// <summary>
        /// 판매일자
        /// </summary>
        public string sellDate { get; set; }
    }
}
