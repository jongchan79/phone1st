using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Phone1stBiz.Business;
using Phone1stBiz;

namespace Phone1st.Forms
{
    public partial class SellMain : UserControl
    {
        Phone phoneBiz = new Phone();
        Company companyBiz = new Company();
        public string selectedNo = string.Empty;
        public string uid = string.Empty;
        public string orderno = string.Empty;
        public int section = 1;

        public string GetSection
        {
            get
            {
                return section.ToString();
            }
        }

        public SellMain()
        {
            InitializeComponent();
        }

        public void SetPanelHeight()
        {
            panel1.Height -= 300;
            this.Height -= 300;
        }

        private void SellMain_Load(object sender, EventArgs e)
        {
            List<CompanyInfo> list = companyBiz.GetCompanyDropDown(section);

            cbCompany.DataSource = list;
            cbCompany.DisplayMember = "CompanyName";
            cbCompany.ValueMember = "uid";

            cbSearch.DataSource = list;
            cbSearch.BindingContext = new System.Windows.Forms.BindingContext();
            cbSearch.DisplayMember = "CompanyName";
            cbSearch.ValueMember = "uid";

            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetOrderList();
            SetDefault();
        }

        public void GetOrderList()
        {
            if (string.IsNullOrEmpty(Phone1stBiz.Business.User.LoginID))
            {
                IO_Util.Alert("세션이 만료되었습니다.\r\n다시 로그인 해주세요.");
                Application.Exit();
            }

            List<OrderInfo> olist = new List<OrderInfo>();
            olist = phoneBiz.GetOrderList(txtSdate.Text, txtEdate.Text, cbSearch.SelectedValue.ToString(), section.ToString());

            grdOrder.DataSource = olist;
            grdOrder.ClearSelection();

            // 990
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
        }

        /// <summary>
        /// 매수 업체 리스트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedValue == null)
                companyUC1.Visible = false;
            else
                SetCompanyInfo();
        }

        /// <summary>
        /// 선택된 상품들 바인딩
        /// </summary>
        /// <param name="selectedNo">,로 붙인 데이터들</param>
        public void SetSelectedPhone(string selectedNo)
        {
            string[] arrPhoneUid = { "" };

            arrPhoneUid = selectedNo.Split(new string[]  { "," } , StringSplitOptions.RemoveEmptyEntries);
            
            if (arrPhoneUid.Length > 0)
            {
                int i = 0;
                int nowCnt = panel1.Controls.Count;

                for (i = 0; i < arrPhoneUid.Length; i++)
                {
                    phoneUC uc = new phoneUC();

                    uc.SellBuy = section.ToString();
                    uc.phoneUid = arrPhoneUid[i];
                    uc.SetPhoneInfo();
                    panel1.Controls.Add(uc);
                    uc.Name = "uc" + (nowCnt + i).ToString();

                    uc.Top = i * 300 + panel1.Height;
                }

                panel1.Height += (i * 300);
                this.Height += (i * 300);
                groupBox3.Height += (i * 300);
            }
        }

        /// <summary>
        /// 선택된 회사 정보 보여주기
        /// </summary>
        private void SetCompanyInfo()
        {
            try
            {
                companyUC1.Visible = true;
                companyUC1.companyNo = cbCompany.SelectedValue.ToString();
                companyUC1.companySection = "1";
                companyUC1.SetCompanyInfo();                
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("SellMain.SetCompanyInfo", x.ToString());
            }
        }

        /// <summary>
        /// 재고 조회 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPhoneSearch_Click(object sender, EventArgs e)
        {
            StockList search = new StockList();
            search.frm = this;            
            search.Show();      
        }

        /// <summary>
        /// 초기화 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDefault_Click(object sender, EventArgs e)
        {
            SetDefault();
        }

        private void SetDefault()
        {            
            cbCompany.SelectedValue = "0";
            txtMemo.Text = "";
            panel1.Controls.Clear();
            panel1.Height = 0;
            this.Height = 640;
            groupBox3.Height = 280;

            uid = "";
            orderno = "";

            companyUC1.Visible = false;
        }

        /// <summary>
        /// 출고 처리 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            string barcode = string.Empty;
            string tmp = string.Empty;
            List<phoneInfo> pinfolist = new List<phoneInfo>();
            OrderInfo oinfo = new OrderInfo();

            if (cbCompany.SelectedValue.ToString().Equals("0"))
            {
                IO_Util.Alert("매도업체를 선택해주세요.");
                return;
            }

            if (panel1.Controls.Count.Equals(0))
            {
                IO_Util.Alert("매도할 상품을 한 개이상 추가해주세요.");
                return;            
            }

            oinfo.companyNo = cbCompany.SelectedValue.ToString();
            oinfo.orderno = orderno;
            oinfo.memo = txtMemo.Text;
            oinfo.uid = uid;
            oinfo.section = section.ToString();
            oinfo.register = User.LoginID;

            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                phoneUC uc = (phoneUC)panel1.Controls[i];

                if (!string.IsNullOrEmpty(uc.phoneModelNo))
                {
                    TextBox txt1 = (TextBox)uc.Controls["txtSellPrice"];

                    if (string.IsNullOrEmpty(txt1.Text))
                    {                        
                        txt1.Text = "0";
                        return;
                    }

                    pinfolist.Add(new phoneInfo()
                    {
                        uid = uc.phoneUid,
                        ordernoSell = orderno,
                        phoneModelUid = uc.phoneModelNo,
                        sellPrice = txt1.Text,
                        barcodeNo = uc.barCode
                    });

                    tmp = string.Concat(tmp, ",", uc.phoneUid);
                }
            }

            string[] arr = tmp.Substring(1).Split(',');

            int result = phoneBiz.SetSellAll(pinfolist, oinfo, ref uid, ref orderno);

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
                
                GetOrderList();
                SetDefault();

                IO_Util.Alert("정상적으로 입력되었습니다.");
            }
            else
                IO_Util.Alert("실패했습니다. 다시 시도해주세요.");
        }

        private void SetOrderInfo(string localuid)
        {
            string[] arrPhoneUid = { "" };
            OrderInfo oinfo = phoneBiz.GetOrderInfo(localuid, section.ToString(), ref arrPhoneUid);

            cbCompany.SelectedValue = oinfo.companyNo;
            txtMemo.Text = oinfo.memo;

            if (oinfo != null)
            {
                orderno = oinfo.orderno;
                uid = oinfo.uid;
                panel1.Controls.Clear();
                panel1.Height = 0;

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

        private void btnOrderDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(orderno))
            {
                int result = phoneBiz.DeleteSellOrder(orderno);

                if (result < 0)
                    IO_Util.Alert("실패했습니다. 다시 시도해 주세요.");
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
                int index = grdOrder.CurrentCell.RowIndex;

                uid = grdOrder[0, index].Value.ToString();
                SetOrderInfo(uid);
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("SellMain.grdOrder_SelectionChanged", x.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
    }
}
