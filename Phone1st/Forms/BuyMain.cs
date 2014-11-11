using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Drawing.Printing;
using System.Net;
using System.Net.Sockets;


using Phone1stBiz.Business;
using Phone1stBiz;

namespace Phone1st.Forms
{
    public partial class BuyMain : UserControl
    {
        Phone phoneBiz = new Phone();
        Company companyBiz = new Company();
        public string selectedNo = string.Empty;
        public string uid = string.Empty;
        public string orderno = string.Empty;
        public int section = 2;
        int index = 0;

        public string GetSection
        {
            get
            {
                return section.ToString();
            }
        }

        public BuyMain()
        {
            InitializeComponent();            
        }

        private void BuyMain_Load(object sender, EventArgs e)
        {
            List<CompanyInfo> list = companyBiz.GetCompanyDropDown(section);

            cbBuyCompany.DataSource = list;
            cbBuyCompany.DisplayMember = "CompanyName";
            cbBuyCompany.ValueMember = "uid";
            cbBuyCompany.SelectedIndex = 0;
            
            cbSearch.DataSource = list;
            cbSearch.BindingContext = new System.Windows.Forms.BindingContext();
            cbSearch.DisplayMember = "CompanyName";
            cbSearch.ValueMember = "uid";
            cbSearch.SelectedIndex = 0;

            this.cbBuyCompany.SelectedIndexChanged += new System.EventHandler(this.cbBuyCompany_SelectedIndexChanged);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PhoneSearch search = new PhoneSearch();
            search.frm = this;
            search.selectType = "1";
            search.Show();            
        }

        public void SetValue(string str)
        {
            if (str.Length > 0)
            {
                string[] arrPhone = str.Split(',');
                
                if (arrPhone.Length > 0)
                {
                    int i = 0;
                    int nowCnt = panel1.Controls.Count;
                        
                    for (i = 0; i < arrPhone.Length; i++)
                    {
                        phoneUC uc = new phoneUC();

                        uc.SellBuy = section.ToString();
                        uc.phoneModelNo = arrPhone[i];
                        panel1.Controls.Add(uc);
                        uc.Name = "uc" + (nowCnt + i).ToString();
                        
                        uc.Top = i * 300 + panel1.Height;
                        IO_Util.LogWrite("test", uc.Top.ToString());
                    }

                    panel1.Height += (i * 300);
                    this.Height += (i * 300);
                    groupBox3.Height += (i * 300);
                }
            }
            else
                IO_Util.Alert("다시 시도해주세요.");
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Phone1stBiz.Business.User.LoginID))
            {
                IO_Util.Alert("세션이 만료되었습니다.\r\n다시 로그인 해주세요.");
                Application.Exit();
            }

            string barcode = string.Empty;
            string tmp = string.Empty;
            List<phoneInfo> pinfolist = new List<phoneInfo>();
            OrderInfo oinfo = new OrderInfo();

            if (cbBuyCompany.SelectedValue.ToString().Equals("0"))
            {
                IO_Util.Alert("매입업체를 선택해주세요.");
                return;
            }

            oinfo.companyNo = cbBuyCompany.SelectedValue.ToString();
            oinfo.orderno = orderno;
            oinfo.memo = txtMemo.Text;
            oinfo.uid = uid;
            oinfo.section = section.ToString();
            oinfo.register = User.LoginID;

            if (panel1.Controls.Count <= 0)
            {
                IO_Util.Alert("입고할 상품을 입력해주세요.");
                return;
            }

            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                phoneUC uc = (phoneUC)panel1.Controls[i];

                if (!string.IsNullOrEmpty(uc.phoneModelNo))
                {
                    TextBox txt1 = (TextBox)uc.Controls["txtSerialNo"];
                    TextBox txt2 = (TextBox)uc.Controls["txtIemi"];
                    TextBox txt3 = (TextBox)uc.Controls["txtPrice"];
                    TextBox txt4 = (TextBox)uc.Controls["txtState"];
                    TextBox txt5 = (TextBox)uc.Controls["txtAddName"];
                    TextBox txt6 = (TextBox)uc.Controls["txtAddPrice"];

                    #region 바코드 출력 로직
                    barcode = uc.barCode;
                    #endregion

                    if (string.IsNullOrEmpty(txt1.Text))
                    {
                        IO_Util.Alert("일련번호를 입력해주세요");
                        txt1.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txt2.Text))
                    {
                        IO_Util.Alert("IEMI를 입력해주세요");
                        txt2.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txt3.Text))
                    {
                        IO_Util.Alert("매입금액을 입력해주세요");
                        txt3.Focus();
                        return;
                    }

                    pinfolist.Add(new phoneInfo()
                    {
                        uid = uc.phoneUid,
                        ordernoBuy = orderno,
                        phoneModelUid = uc.phoneModelNo,
                        uniqueKey = txt1.Text,
                        iemiNo = txt2.Text,
                        phoneState = txt4.Text,
                        buyPrice = txt3.Text,
                        barcodeNo = barcode,
                        buyAddText = txt5.Text,
                        buyAddPrice = txt6.Text
                    });

                    tmp = string.Concat(tmp, ",", uc.phoneUid);
                }                
            }

            tmp = tmp.Substring(1);
            string[] arr = tmp.Split(',');
            int result = phoneBiz.SetOrderAll(pinfolist, oinfo, ref orderno, ref uid, ref arr);

            if (result.Equals(1))
            {
                if (panel1.Controls.Count.Equals(arr.Length))
                {
                    for (int i = 0; i < panel1.Controls.Count; i++)
                    {
                        phoneUC uc = (phoneUC)panel1.Controls[i];

                        uc.SellBuy = section.ToString();
                        uc.phoneUid = arr[i];
                    }
                }
                                
                IO_Util.Alert("정상적으로 입력되었습니다.");

                GetOrderList();
                SetDefault();
            }
            else
                IO_Util.Alert("실패했습니다. 다시 시도해주세요.");
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetOrderList();
            SetDefault();
        }

        public void GetOrderList()
        {
            try
            {
                List<OrderInfo> olist = new List<OrderInfo>();
                olist = phoneBiz.GetOrderList(txtSdate.Text, txtEdate.Text, cbSearch.SelectedValue.ToString(), section.ToString());

                grdOrder.DataSource = olist;

                grdOrder.Columns[0].HeaderText = "번호";
                grdOrder.Columns[0].Width = 60;
                grdOrder.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grdOrder.Columns[1].Visible = false;
                grdOrder.Columns[2].HeaderText = "주문번호";
                grdOrder.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grdOrder.Columns[2].Width = 120;
                grdOrder.Columns[3].Visible = false;
                grdOrder.Columns[4].HeaderText = "회사명";
                grdOrder.Columns[4].Width = 165;
                grdOrder.Columns[5].HeaderText = "갯수";
                grdOrder.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grdOrder.Columns[5].Width = 70;
                grdOrder.Columns[6].HeaderText = "메모";
                grdOrder.Columns[6].Width = 340;
                grdOrder.Columns[7].HeaderText = "등록자";
                grdOrder.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grdOrder.Columns[7].Width = 120;
                grdOrder.Columns[8].HeaderText = "등록일";
                grdOrder.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grdOrder.Columns[8].Width = 150;

                grdOrder.ClearSelection();
                grdOrder.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.AliceBlue;
                grdOrder.RowsDefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
                grdOrder.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LemonChiffon;
                grdOrder.RowHeadersVisible = false;
                grdOrder.ReadOnly = true;
                grdOrder.AllowUserToResizeColumns = false;
                grdOrder.AllowUserToResizeRows = false;
                grdOrder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("BuyMain.GetOrderList", x.ToString());
            }
        }

        //public void SetPanelHeight()
        //{
        //    panel1.Height -= 300;
        //    this.Height = -300;
        //}

        private void SetCompanyInfo()
        {
            companyUC1.Visible = true;
            companyUC1.companyNo = cbBuyCompany.SelectedValue.ToString();
            companyUC1.companySection = "2";
            companyUC1.SetCompanyInfo();

        }

        private void cbBuyCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBuyCompany.SelectedValue == null)
                companyUC1.Visible = false;
            else
                SetCompanyInfo();
        }

        private void SetDefault()
        {            
            cbBuyCompany.SelectedValue = "0";
            txtMemo.Text = "";
            panel1.Controls.Clear();
            panel1.Height = 0;
            this.Height = 640;
            groupBox3.Height = 280;
            
            uid = "";
            orderno = "";

            companyUC1.Visible = false;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            SetDefault();
        }
        
        private Socket m_ClientSocket;

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(uid))
            {
                IO_Util.Alert("프린트할 매입 건을 선택해주세요");
                return;
            }

            int cnt = panel1.Controls.Count;
            string[] arrData = new string[cnt];

            for (int i = 0; i < cnt; i++)
            {
                phoneUC uc = (phoneUC)panel1.Controls[i];
                arrData[i] = uc.phoneUid;
            }

            // BarCodePrint barPrint = new BarCodePrint();
            // barPrint.PrintBarCode(arrData, companyUC1.companyName);

            #region 소켓 통신 작업
            if (Phone1stBiz.Communication.Firewall.AuthorizeProgram("Phone1st.exe", Environment.CurrentDirectory))
            {
                string packetData = string.Concat("barcode|", companyUC1.companyName, "|", string.Join("|", arrData));

                m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("1.223.75.250"), 10000);
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.RemoteEndPoint = ipep;

                byte[] szData = Encoding.Unicode.GetBytes(packetData);
                args.SetBuffer(szData, 0, szData.Length);
                m_ClientSocket.ConnectAsync(args);
            }
            #endregion

            IO_Util.Alert("바코드가 출력되었습니다.");
        }

        private void btnOrderDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(orderno))
            {
                int result = phoneBiz.DeleteBuyOrder(orderno);

                if (result < 0)
                    IO_Util.Alert("실패했습니다. 다시 시도해 주세요.");
                else if (result.Equals(99))
                {
                    IO_Util.Alert("이미 출고된 상품이 있어서 주문을 삭제할 수 없습니다.");
                }
                else
                {
                    IO_Util.Alert("삭제되었습니다.");
                    GetOrderList();
                    SetDefault();
                }
            }
            else
                IO_Util.Alert("삭제할 주문을 선택해주세요.");
        }

        private void grdOrder_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdOrder.Rows.Count > 0)
                {
                    index = grdOrder.CurrentCell.RowIndex;
                    string[] arrPhoneUid = { "" };

                    uid = grdOrder[0, index].Value.ToString();

                    OrderInfo oinfo = phoneBiz.GetOrderInfo(uid, section.ToString(), ref arrPhoneUid);
                    cbBuyCompany.SelectedValue = oinfo.companyNo;
                    txtMemo.Text = oinfo.memo;

                    if (oinfo != null)
                    {
                        orderno = oinfo.orderno;
                        uid = oinfo.uid;
                        panel1.Controls.Clear();
                        panel1.Height = 0;
                        //                    this.Height = 1000;

                        if (arrPhoneUid.Length > 0)
                        {
                            int i = 0;

                            for (i = 0; i < arrPhoneUid.Length; i++)
                            {
                                phoneUC uc = new phoneUC();

                                uc.SellBuy = section.ToString();
                                uc.phoneUid = arrPhoneUid[i];
                                uc.SetPhoneInfo();
                                panel1.Controls.Add(uc);
                                uc.Name = "uc" + i.ToString();

                                uc.Top = i * 300 + panel1.Height;
                            }

                            panel1.Height = (i * 300);
                            this.Height = 700 + (i * 300);
                            groupBox3.Height = 150 + (i * 300);
                        }

                        SetCompanyInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                IO_Util.LogWrite("buyMain.grdOrder_SelectionChanged", ex.ToString());
            }
        }

        private void btnPrint3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(orderno))
            {
                PrintForm print = new PrintForm();

                print.section = section.ToString();
                print.orderno = orderno;
                print.DisplaybuyPrice = false;

                print.Show();
            }
            else
                IO_Util.Alert("출력할 주문을 선택해주세요");
        }

        private void btnPrint2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(orderno))
            {
                PrintForm print = new PrintForm();

                print.section = section.ToString();
                print.orderno = orderno;
                print.DisplaybuyPrice = true;

                print.Show();
            }
            else
                IO_Util.Alert("출력할 주문을 선택해주세요");
        }
    }
}
