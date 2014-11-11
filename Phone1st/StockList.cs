using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Phone1stBiz.Business;
using Phone1stBiz;

namespace Phone1st
{
    public partial class StockList : Form
    {
        public Forms.SellMain frm;

        Phone phoneBiz = new Phone();

        public StockList()
        {
			InitializeComponent();

            txtCode1.Focus();
        }

        #region 공통 사용
        private void StockList_Load(object sender, EventArgs e)
        {
            cbType.SelectedIndex = 0;            
        }
        
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex.Equals(1))
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

                this.cbAgency.SelectedIndexChanged += new System.EventHandler(this.cbAgency_SelectedIndexChanged);
                this.cbMaker.SelectedIndexChanged += new System.EventHandler(this.cbMaker_SelectedIndexChanged);

                GetList();
            }
        }
        #endregion

        #region 재고 검색 탭에서 사용
        private void GetList()
        {
            List<stockInfo> list = phoneBiz.GetStockList(cbMaker.SelectedValue.ToString(), cbAgency.SelectedIndex.ToString(), "");

            grdStockList.AutoGenerateColumns = true;
            grdStockList.AllowUserToAddRows = false;
            grdStockList.ReadOnly = false;

            if (!grdStockList.Columns.Count.Equals(13))
            {
                DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
                doWork.HeaderText = "선택";
                doWork.FalseValue = "0";
                doWork.TrueValue = "1";
                doWork.Name = "chk";
                doWork.Width = 80;
                grdStockList.Columns.Insert(0, doWork);
            }

            grdStockList.DataSource = list;
            grdStockList.ClearSelection();

            grdStockList.Columns[1].HeaderText = "번호";
            grdStockList.Columns[1].ReadOnly = true;
            grdStockList.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdStockList.Columns[1].Width = 60;

            grdStockList.Columns[2].Visible = false;
            grdStockList.Columns[3].Visible = false;

            grdStockList.Columns[4].HeaderText = "핸드폰명";
            grdStockList.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdStockList.Columns[4].Width = 200;

            grdStockList.Columns[5].HeaderText = "모델명";
            grdStockList.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdStockList.Columns[5].Width = 90;

            grdStockList.Columns[6].Visible = false;
            grdStockList.Columns[7].Visible = false;

            grdStockList.Columns[8].HeaderText = "검수상태";
            grdStockList.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            grdStockList.Columns[9].HeaderText = "구매 가격";
            grdStockList.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            grdStockList.Columns[10].Visible = false;
            grdStockList.Columns[11].Visible = false;
            grdStockList.Columns[12].Visible = false;
            grdStockList.Columns[13].Visible = false;

            grdStockList.Columns[14].HeaderText = "구매업체";
            grdStockList.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdStockList.Columns[14].Width = 120;

            grdStockList.Columns[15].HeaderText = "구매일";
            grdStockList.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdStockList.Columns[15].Width = 120;

            grdStockList.ClearSelection();
            grdStockList.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.AliceBlue;
            grdStockList.RowsDefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            grdStockList.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LemonChiffon;
            grdStockList.RowHeadersVisible = false;
            grdStockList.AllowUserToResizeColumns = false;
            grdStockList.AllowUserToResizeRows = false;
            grdStockList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void cbAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetList();
        }

        private void cbMaker_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetList();
        }

        #endregion

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = tabControl1.Controls[0].Controls.Count;

            // 바코드 읽기 선택
            for (int i = 0; i < count; i++)
            {
                if (tabControl1.Controls[0].Controls[i].Name.Contains("txtCode"))
                {
                    ((TextBox)tabControl1.Controls[0].Controls[i]).TextChanged += new EventHandler(BarcodeRead_TextChanged);
                    ((TextBox)tabControl1.Controls[0].Controls[i]).Text = "";
                }
            }
        }

        private void BarcodeRead_TextChanged(object sender, EventArgs e)
        {
            if (cbType.SelectedIndex.Equals(0))
            {
                #region 바코드 읽기일때 처리
                if (((TextBox)sender).Text.Length.Equals(12))
                {
                    int textBoxNo = Convert.ToInt16(((TextBox)sender).Name.Replace("txtCode", ""));

                    if (!textBoxNo.Equals(10))
                        ((TextBox)tabControl1.Controls[0].Controls.Find(string.Concat("txtCode", textBoxNo + 1), false)[0]).Focus();
                    else
                        btnInput.Focus();
                }
                #endregion
            }          
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            string oneData = string.Empty;
            string data = string.Empty;
            string result = string.Empty;

            int count = tabControl1.Controls[0].Controls.Count;
            int chkCount = 0;

            for (int i = 0; i < count; i++)
            {
                if (tabControl1.Controls[0].Controls[i].Name.Contains("txtCode"))
                {
                    oneData = ((TextBox)tabControl1.Controls[0].Controls[i]).Text;

                    if (!string.IsNullOrEmpty(oneData))
                    {
                        // 재고가 존재하는지 체크
                        data = phoneBiz.CheckStock(oneData, cbType.SelectedIndex);

                        if (!string.IsNullOrEmpty(data))
                        {
                            result = string.Concat(result, ",", data);
                            chkCount++;
                        }
                        else
                        {
                            IO_Util.Alert("재고가 존재하지 않는 상품이 있습니다.");
                            ((TextBox)tabControl1.Controls[0].Controls[i]).Focus();
                            return;
                        }
                    }
                }
            }

            if (chkCount.Equals(0))
            {
                IO_Util.Alert("하나 이상의 데이터를 입력해주세요.");
                return;
            }

            frm.SetSelectedPhone(result.Substring(1));

            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rows_with_checked_column = new List<DataGridViewRow>();
            string checkedRow = string.Empty;
            int selectedCount = 0;

            foreach (DataGridViewRow row in grdStockList.Rows)
            {
                if (!object.Equals(row.Cells[0].Value, null) && row.Cells[0].Value.ToString().Equals("1"))
                {
                    checkedRow = string.Concat(checkedRow, ",", row.Cells[1].Value);
                    selectedCount++;
                }
            }

            if (string.IsNullOrEmpty(checkedRow))
            {
                MessageBox.Show("한 개이상 선택해주세요.");
                return;
            }

            frm.SetSelectedPhone(checkedRow.Substring(1));

            this.Close();
        }
    }
}
