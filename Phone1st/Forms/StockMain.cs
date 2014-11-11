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
    public partial class StockMain : UserControl
    {
        Phone phoneBiz = new Phone();
        Company companyBiz = new Company();

        public StockMain()
        {
            InitializeComponent();
        }

        private void StockMain_Load(object sender, EventArgs e)
        {
            cbAgency.Items.Insert(0, "통신사");
            cbAgency.Items.Insert(1, "SK");
            cbAgency.Items.Insert(2, "KT");
            cbAgency.Items.Insert(3, "LG");
            cbAgency.SelectedIndex = 0;

            List<Maker> makerlist = new List<Maker>();
            makerlist = phoneBiz.GetMakerList();

            cbMaker.DataSource = makerlist;
            cbMaker.ValueMember = "uid";
            cbMaker.DisplayMember = "MakerName";

            List<CompanyInfo> list = companyBiz.GetCompanyDropDown(2);

            cbCompany.DataSource = list;
            cbCompany.DisplayMember = "CompanyName";
            cbCompany.ValueMember = "uid";
            cbCompany.SelectedIndex = 0;
            
            this.cbAgency.SelectedIndexChanged += new EventHandler(cbAgency_SelectedIndexChanged);
            this.cbMaker.SelectedIndexChanged += new EventHandler(cbMaker_SelectedIndexChanged);
            this.cbCompany.SelectedIndexChanged += new EventHandler(cbCompany_SelectedIndexChanged);

            GetData();
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void cbMaker_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void cbAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            if (string.IsNullOrEmpty(Phone1stBiz.Business.User.LoginID))
            {
                IO_Util.Alert("세션이 만료되었습니다.\r\n다시 로그인 해주세요.");
                Application.Exit();
            }

            List<stockInfo> list = phoneBiz.GetStockList(cbMaker.SelectedValue.ToString(), cbAgency.SelectedIndex.ToString(), cbCompany.SelectedValue.ToString());

            phoneList.AutoGenerateColumns = true;
            phoneList.AllowUserToAddRows = false;
            phoneList.ReadOnly = false;

            phoneList.AutoGenerateColumns = true;
            phoneList.AllowUserToAddRows = false;
            phoneList.ReadOnly = false;
            phoneList.DataSource = list;

            // 1020
            phoneList.Columns[0].HeaderText = "번호";
            phoneList.Columns[0].ReadOnly = true;
            phoneList.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            phoneList.Columns[0].Width = 60;

            phoneList.Columns[1].HeaderText = "주문번호";
            phoneList.Columns[1].ReadOnly = true;
            phoneList.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            phoneList.Columns[1].Width = 120;
            
            phoneList.Columns[2].Visible = false;

            phoneList.Columns[3].HeaderText = "핸드폰명";
            phoneList.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            phoneList.Columns[3].Width = 200;

            phoneList.Columns[4].HeaderText = "모델명";
            phoneList.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            phoneList.Columns[4].Width = 90;

            phoneList.Columns[5].HeaderText = "일련번호";
            phoneList.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            phoneList.Columns[5].Width = 120;
            
            phoneList.Columns[6].HeaderText = "IEMI";
            phoneList.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            phoneList.Columns[6].Width = 90;

            phoneList.Columns[7].HeaderText = "검수상태";
            phoneList.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            phoneList.Columns[8].HeaderText = "구매 가격";
            phoneList.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            phoneList.Columns[9].Visible = false;
            phoneList.Columns[10].Visible = false;

            phoneList.Columns[11].HeaderText = "제조사";
            phoneList.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            phoneList.Columns[11].Width = 80;

            phoneList.Columns[12].HeaderText = "통신사";
            phoneList.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            phoneList.Columns[12].Width = 80;

            phoneList.Columns[13].HeaderText = "구매업체";
            phoneList.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            phoneList.Columns[13].Width = 120;

            phoneList.Columns[14].HeaderText = "구매일";
            phoneList.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            phoneList.Columns[14].Width = 150;

            phoneList.ClearSelection();
            phoneList.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.AliceBlue;
            phoneList.RowsDefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            phoneList.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LemonChiffon;
            phoneList.RowHeadersVisible = false;
            phoneList.ReadOnly = true;
            phoneList.AllowUserToResizeColumns = false;
            phoneList.AllowUserToResizeRows = false;
            phoneList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            lblTotalCount.Text = string.Concat("총 수량 : ", list.Count().ToString());
            lbltotalMoney.Text = string.Concat("금액 : ", list.AsEnumerable().Sum(o => Convert.ToInt32(o.buyPrice.Replace(",", ""))).ToString("###,###"));
        }
    }
}
