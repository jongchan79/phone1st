using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;


using Phone1stBiz.Business;

namespace Phone1st
{
    public partial class PrintForm : Form
    {
        public string orderno = string.Empty;
        public string section = string.Empty;
        public bool DisplaybuyPrice = true;

        DataSet ds = new DataSet();
        OrderInfo oinfo = new OrderInfo();
        CompanyInfo cinfo = new CompanyInfo();
        public PrintForm()
        {
            InitializeComponent();
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(orderno))
            {
                SetTable();
            }
        }

        public void SetTable()
        {
            printDocument1.PrintPage += new PrintPageEventHandler(PrintImage);

            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("PagerA4", 826, 1169);
            printDocument1.DefaultPageSettings.Landscape = false;

            printDocument1.Print();
            this.Close();
        }

        public void PrintImage(object o, PrintPageEventArgs e)
        {
            Phone phoneBiz = new Phone();
            Company companyBiz = new Company();

            oinfo = phoneBiz.GetOrderInfo(orderno);
            ds = phoneBiz.GetPhoneInfoByOrderNo(orderno, section);
            cinfo = companyBiz.GetCompanyInfo(oinfo.companyNo);
                        
            int y = 105;
            int limit = 0;
            int count = 0;
            int x = 15;
            int tempcount = 0;

            Font fnt_Bold = new Font("돋움", 9, FontStyle.Bold);
            Font fnt = new Font("돋움", 8, FontStyle.Regular);


            e.Graphics.DrawString(string.Concat(section.Equals("1") ? "매도" : "매입", " 상세 내역"), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 340, 70);

            e.Graphics.DrawString(string.Concat("전표 번호 : ", orderno), fnt, Brushes.Black, 30, 120);
            e.Graphics.DrawString(string.Concat("출력 일자 : ", DateTime.Now.ToString("yyy-MM-dd HH:mm:ss")), fnt, Brushes.Black, 580, 120);
            e.Graphics.DrawLine(Pens.Black, 45 - x, y + 35, 795 - x, y + 35);   // 상단의 가로

            e.Graphics.DrawLine(Pens.Black, 45 - x, y + 35, 45 - x, y + 60);
            e.Graphics.DrawString("업체명", fnt_Bold, Brushes.Black, 50 - x, y + 42);
            e.Graphics.DrawLine(Pens.Black, 115 - x, y + 35, 115 - x, y + 60);
            e.Graphics.DrawString(cinfo.CompanyName, fnt_Bold, Brushes.Black, 120 - x, y + 42);
            
            e.Graphics.DrawLine(Pens.Black, 245 - x, y + 35, 245 - x, y + 60);
            e.Graphics.DrawString("담당자명", fnt_Bold, Brushes.Black, 260 - x, y + 42);
            e.Graphics.DrawLine(Pens.Black, 337 - x, y + 35, 337 - x, y + 60);
            e.Graphics.DrawString(cinfo.ContactName, fnt_Bold, Brushes.Black, 340 - x, y + 42);

            e.Graphics.DrawLine(Pens.Black, 590 - x, y + 35, 590 - x, y + 60);
            e.Graphics.DrawString("연락처", fnt_Bold, Brushes.Black, 600 - x, y + 42);
            e.Graphics.DrawLine(Pens.Black, 655 - x, y + 35, 655 - x, y + 60);
            e.Graphics.DrawString(cinfo.hp, fnt_Bold, Brushes.Black, 660 - x, y + 42);


            e.Graphics.DrawLine(Pens.Black, 45 - x, y + 60, 795 - x, y + 60);   // 상단 테이블 제일 하단 라인
            e.Graphics.DrawLine(Pens.Black, 795 - x, y + 35, 795 - x, y + 60);  // 상단 테이블 제일 우측 세로 라인


            e.Graphics.DrawLine(Pens.Black, 45 - x, 187, 795 - x, 187);   // 하단 테이블 제일 상단 라인
            e.Graphics.DrawString("번호", fnt_Bold, Brushes.Black, 50 - x, 190);
            e.Graphics.DrawString("제품명", fnt_Bold, Brushes.Black, 120 - x, 190);
            e.Graphics.DrawString("모델명", fnt_Bold, Brushes.Black, 220 - x, 190);
            e.Graphics.DrawString("일련번호", fnt_Bold, Brushes.Black, 310 - x, 190);
            e.Graphics.DrawString("MEI번호", fnt_Bold, Brushes.Black, 410 - x, 190);

            if (section.Equals("2"))
            {
                // 매입장
                if (!DisplaybuyPrice)
                    e.Graphics.DrawString("색상 및 검수 상태", fnt_Bold, Brushes.Black, 600 - x, 190);
                else
                {
                    e.Graphics.DrawString("색상 및 검수 상태", fnt_Bold, Brushes.Black, 540 - x, 190);
                    e.Graphics.DrawString("매입금액", fnt_Bold, Brushes.Black, 720 - x, 190);
                }
            }
            else
            {
                // 매도장
                if (!DisplaybuyPrice)
                {
                    e.Graphics.DrawString("색상 및 검수 상태", fnt_Bold, Brushes.Black, 540 - x, 190);
                    e.Graphics.DrawString("매도금액", fnt_Bold, Brushes.Black, 720 - x, 190);                    
                }
                else
                {
                    e.Graphics.DrawString("색상 및 검수 상태", fnt_Bold, Brushes.Black, 510 - x, 190);
                    e.Graphics.DrawString("매도금액", fnt_Bold, Brushes.Black, 730 - x, 190);
                    e.Graphics.DrawString("매입금액", fnt_Bold, Brushes.Black, 650 - x, 190);                    
                }

            }

            int totalPrice = 0;
            int totalPrice1 = 0;
            int index = 0;
            StringBuilder tmp = new StringBuilder();
            tmp.Append(ds.Tables[0].Rows.Count.ToString());
            tmp.Append("\r\n");

            int buyPrice = 0;
            int sellPrice = 0;

            for (tempcount = 0; tempcount < ds.Tables[0].Rows.Count; tempcount = tempcount + 40)
            {
                y = 210;

                for (index = tempcount; index < tempcount + 40; index++)
                {
                    tmp.Append(index);
                    tmp.Append("/");
                    tmp.Append(tempcount);
                    tmp.Append("\r\n");


                    if (index < (ds.Tables[0].Rows.Count))
                    {
                        #region 내용 입력
                        e.Graphics.DrawString((index + 1).ToString(), fnt, Brushes.Black, 60 - x, y);
                        e.Graphics.DrawString(ds.Tables[0].Rows[index][0].ToString(), fnt, Brushes.Black, 90 - x, y);
                        e.Graphics.DrawString(ds.Tables[0].Rows[index][1].ToString(), fnt, Brushes.Black, 210 - x, y);
                        e.Graphics.DrawString(ds.Tables[0].Rows[index][2].ToString(), fnt, Brushes.Black, 290 - x, y);
                        e.Graphics.DrawString(ds.Tables[0].Rows[index][3].ToString(), fnt, Brushes.Black, 390 - x, y);
                        e.Graphics.DrawString(ds.Tables[0].Rows[index][4].ToString().Replace("\r\n", ""), fnt, Brushes.Black, 500 - x, y);

                        if (section.Equals("2"))
                        {
                            // 매입장
                            if (DisplaybuyPrice)
                            {
                                buyPrice = Convert.ToInt32(string.IsNullOrEmpty(ds.Tables[0].Rows[index][5].ToString()) ? "0" : ds.Tables[0].Rows[index][5].ToString());
                                totalPrice += buyPrice;
                                e.Graphics.DrawString(buyPrice.ToString("###,###"), fnt, Brushes.Black, 720 - x, y);
                            }
                        }
                        else
                        {
                            // 매도장
                            sellPrice = Convert.ToInt32(string.IsNullOrEmpty(ds.Tables[0].Rows[index][6].ToString()) ? "0" : ds.Tables[0].Rows[index][6].ToString());
                            totalPrice += sellPrice;

                            if (DisplaybuyPrice)
                            {
                                // 매입금액 표시
                                buyPrice = Convert.ToInt32(string.IsNullOrEmpty(ds.Tables[0].Rows[index][5].ToString()) ? "0" : ds.Tables[0].Rows[index][5].ToString());                                
                                totalPrice1 += buyPrice;
                                e.Graphics.DrawString(buyPrice.ToString("###,###"), fnt, Brushes.Black, 650 - x, y);

                                // 매도금액 표시
                                e.Graphics.DrawString(sellPrice.Equals(0) ? sellPrice.ToString() : sellPrice.ToString("###,###"), fnt, Brushes.Black, 730 - x, y);
                            }
                            else
                            {
                                // 매도금액                                
                                e.Graphics.DrawString(sellPrice.ToString("###,###"), fnt, Brushes.Black, 720 - x, y);
                            }
                        }
                        #endregion

                        e.Graphics.DrawLine(Pens.Black, 45 - x, y - 3, 795 - x, y - 3);

                        limit += 1;
                        count += 1;
                        y += 20;
                    }
                    else
                    {
                        break;
                    }
                }

                #region 합계 및 마감선 출력
                e.Graphics.DrawLine(Pens.Black, 45 - x, y - 2, 795 - x, y - 2); // 테이블의 마지막 가로 줄

                if (section.Equals("2"))
                {
                    // 매입장
                    if (DisplaybuyPrice)
                    {
                        e.Graphics.DrawString("합계", fnt, Brushes.Black, 52 - x, y);
                        e.Graphics.DrawString(totalPrice.ToString("###,###"), fnt_Bold, Brushes.Black, 720 - x, y + 2);
                        y += 20;

                        e.Graphics.DrawLine(Pens.Black, 45 - x, y - 2, 795 - x, y - 2); // 테이블의 마지막 가로 줄                    
                        e.Graphics.DrawLine(Pens.Black, 705 - x, 187, 705 - x, y - 2);  // 일곱번째 세로 줄

                    }
                }
                else
                {
                    // 매도장
                    if (DisplaybuyPrice)
                    {
                        e.Graphics.DrawString("합계", fnt, Brushes.Black, 52 - x, y);
                        e.Graphics.DrawString(totalPrice.ToString("###,###"), fnt_Bold, Brushes.Black, 730 - x, y + 2);
                        e.Graphics.DrawString(totalPrice1.ToString("###,###"), fnt_Bold, Brushes.Black, 650 - x, y + 2);

                        y += 20;

                        e.Graphics.DrawLine(Pens.Black, 645 - x, 187, 645 - x, y - 2);  // 일곱번째 세로 줄                    
                        e.Graphics.DrawLine(Pens.Black, 720 - x, 187, 720 - x, y - 2);  // 일곱번째 세로 줄

                    }
                    else
                    {
                        e.Graphics.DrawString("합계", fnt, Brushes.Black, 52 - x, y);
                        e.Graphics.DrawString(totalPrice.ToString("###,###"), fnt_Bold, Brushes.Black, 720 - x, y + 2);
                        y += 20;

                        e.Graphics.DrawLine(Pens.Black, 705 - x, 187, 705 - x, y - 2);  // 일곱번째 세로 줄
                    }
                }

                e.Graphics.DrawLine(Pens.Black, 795 - x, 187, 795 - x, y - 2); // 여덟번째 세로 줄            
                e.Graphics.DrawLine(Pens.Black, 45 - x, y - 2, 795 - x, y - 2); // 테이블의 마지막 가로 줄
                e.Graphics.DrawLine(Pens.Black, 45 - x, 187, 45 - x, y - 2); // 첫번째 세로 줄
                e.Graphics.DrawLine(Pens.Black, 85 - x, 187, 85 - x, y - 2); // 두번째 세로 줄
                e.Graphics.DrawLine(Pens.Black, 205 - x, 187, 205 - x, y - 2); // 세번째 세로 줄
                e.Graphics.DrawLine(Pens.Black, 285 - x, 187, 285 - x, y - 2); // 네번째 세로 줄
                e.Graphics.DrawLine(Pens.Black, 385 - x, 187, 385 - x, y - 2); // 다섯번째 세로 줄
                e.Graphics.DrawLine(Pens.Black, 500 - x, 187, 500 - x, y - 2); // 여섯번째 세로 줄
                #endregion

                if (limit == 40 && count < ds.Tables[0].Rows.Count)
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                }

                tmp.Append("===========\r\n");
                tmp.Append(limit);
                tmp.Append("/");
                tmp.Append(count);
                tmp.Append("/");
                tmp.Append(e.HasMorePages.ToString());
                tmp.Append("\r\n");

                limit = 0;
            }

            Phone1stBiz.IO_Util.LogWrite("test", tmp.ToString());
            fnt.Dispose();
            fnt_Bold.Dispose();
        }
    }
}

