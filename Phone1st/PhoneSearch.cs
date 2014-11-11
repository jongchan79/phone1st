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
    public partial class PhoneSearch : Form
    {
        Phone phoneBiz = new Phone();
        public Forms.BuyMain frm;
        public Forms.phoneUC frmUc;
        public string selectType = string.Empty;

        public PhoneSearch()
        {
            InitializeComponent();
        }

        private void PhoneSearch_Load(object sender, EventArgs e)
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

            this.cbAgency.SelectedIndexChanged += new EventHandler(cbAgency_SelectedIndexChanged);
            this.cbMaker.SelectedIndexChanged += new EventHandler(cbMaker_SelectedIndexChanged);

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
            List<phoneModelInfo> info = phoneBiz.GetPhoneModelList(cbMaker.SelectedValue.ToString(), cbAgency.SelectedIndex.ToString());

            phoneList.AutoGenerateColumns = true;
            phoneList.AllowUserToAddRows = false;
            phoneList.ReadOnly = false;

            if (!phoneList.Columns.Count.Equals(7))
            {
                DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
                doWork.HeaderText = "선택";
                doWork.FalseValue = "0";
                doWork.TrueValue = "1";
                doWork.Name = "chk";
                doWork.Width = 50;
                phoneList.Columns.Insert(0, doWork);
            }

            phoneList.DataSource = info;

            // 685
            phoneList.Columns[1].HeaderText = "번호";
            phoneList.Columns[1].ReadOnly = true;
            phoneList.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            phoneList.Columns[1].Width = 55;
            phoneList.Columns[2].HeaderText = "핸드폰명";
            phoneList.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            phoneList.Columns[2].Width = 200;
            phoneList.Columns[3].HeaderText = "모델명";
            phoneList.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            phoneList.Columns[3].Width = 90;
            phoneList.Columns[4].HeaderText = "통신사";
            phoneList.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            phoneList.Columns[4].Width = 120;
            phoneList.Columns[5].HeaderText = "제조사";
            phoneList.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            phoneList.Columns[5].Width = 125;
            phoneList.Columns[6].Visible = false;

            phoneList.ClearSelection();
            phoneList.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.AliceBlue;
            phoneList.RowsDefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            phoneList.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LemonChiffon;
            phoneList.RowHeadersVisible = false;
            phoneList.AllowUserToResizeColumns = false;
            phoneList.AllowUserToResizeRows = false;
            phoneList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rows_with_checked_column = new List<DataGridViewRow>();
            string checkedRow = string.Empty;
            int selectedCount = 0;

            foreach (DataGridViewRow row in phoneList.Rows)
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

            if (selectType.Equals("1"))
            {
                checkedRow = checkedRow.Substring(1);
                frm.SetValue(checkedRow);
            }
            else
            {
                if (selectedCount.Equals(1))
                {
                    frmUc.SetPhone(checkedRow.Substring(1));
                }
                else
                {
                    MessageBox.Show("하나의 핸드폰만 선택해주세요.");
                    return;
                }
            }

            this.Close();            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
