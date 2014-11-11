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
    public partial class PhoneModel : UserControl
    {
        int result = 0;
        string modifyNo = "0";

        Phone phoneBiz = new Phone();

        public PhoneModel()
        {
            InitializeComponent();
        }

        private void PhoneModel_Load(object sender, EventArgs e)
        {
            try
            {
                cbAgency.Items.Insert(0, "통신사");
                cbAgency.Items.Insert(1, "SK");
                cbAgency.Items.Insert(2, "KT");
                cbAgency.Items.Insert(3, "LG");
                cbAgency.SelectedIndex = 0;

                cbAgencyInsert.Items.Insert(0, "통신사");
                cbAgencyInsert.Items.Insert(1, "SK");
                cbAgencyInsert.Items.Insert(2, "KT");
                cbAgencyInsert.Items.Insert(3, "LG");
                cbAgencyInsert.SelectedIndex = 0;

                List<Maker> makerlist = new List<Maker>();
                makerlist = phoneBiz.GetMakerList();

                cbMaker.DataSource = makerlist;
                cbMaker.ValueMember = "uid";
                cbMaker.DisplayMember = "MakerName";
                cbMaker.SelectedValue = "0";

                List<Maker> makerlist2 = new List<Maker>();
                makerlist2 = phoneBiz.GetMakerList();

                cbMakerInsert.DataSource = makerlist2;
                cbMakerInsert.ValueMember = "uid";
                cbMakerInsert.DisplayMember = "MakerName";

                this.cbMaker.SelectedIndexChanged += new System.EventHandler(cbMaker_SelectedIndexChanged);
                this.cbAgency.SelectedIndexChanged += new System.EventHandler(cbAgency_SelectedIndexChanged);
                GetData();
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("PhoneModel", x.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetEmpty();
        }

        private void SetEmpty()
        {
            txtModel.Text = "";
            txtName.Text = "";
            cbMakerInsert.SelectedIndex = 1;
            cbAgencyInsert.SelectedIndex = 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                phoneModelInfo info = new phoneModelInfo();

                if (string.IsNullOrEmpty(txtName.Text))
                {
                    lblError.Text += "* 핸드폰 이름을 입력해주세요.\r\n";
                    return;
                }

                if (string.IsNullOrEmpty(txtModel.Text))
                {
                    lblError.Text += "* 핸드폰 모델명을 입력해주세요.\r\n";
                    return;
                }

                if (cbAgencyInsert.SelectedIndex.Equals(0))
                {
                    lblError.Text += "* 통신사를 선택해주세요.\r\n";
                    return;
                }

                info.phoneName = txtName.Text;
                info.modelName = txtModel.Text;
                info.Manufacturer = cbMakerInsert.SelectedValue.ToString();
                info.agency = cbAgencyInsert.SelectedIndex.ToString();
                info.uid = modifyNo;
                info.color = "1";

                result = phoneBiz.SetModelInfo(info);

                if (result.Equals(1))
                {
                    IO_Util.Alert("정상적으로 입력되었습니다.");
                    SetEmpty();
                    GetData();
                    modifyNo = "0";
                }
                else
                    IO_Util.Alert("디비 작업 중 오류가 발생했습니다.\r 잠시 후 다시 시도 해주세요.");
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("PhoneModel", x.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (modifyNo.Equals("0"))
                    IO_Util.Alert("삭제할 모델을 선택해주세요.");
                else
                {
                    result = phoneBiz.DeletePhoneModel(modifyNo);

                    if (result.Equals(1))
                    {
                        IO_Util.Alert("정상적으로 삭제되었습니다.");
                        SetEmpty();
                        GetData();
                    }
                    else
                        IO_Util.Alert("디비 작업 중 오류가 발생했습니다.\r 잠시 후 다시 시도 해주세요.");
                }

                modifyNo = "0";
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("PhoneModel", x.ToString());
            }
        }

        #region 기타 메서드
        private void GetData()
        {
            try
            {
                List<phoneModelInfo> info = phoneBiz.GetPhoneModelList(cbMaker.SelectedValue.ToString(), (object.Equals(cbAgency.SelectedIndex, null) ? "0" : cbAgency.SelectedIndex.ToString()));

                phoneList.AutoGenerateColumns = true;
                phoneList.AllowUserToAddRows = false;
                phoneList.ReadOnly = true;

                phoneList.DataSource = info;
                phoneList.Columns[5].Visible = false;

                phoneList.Columns[0].HeaderText = "번호";
                phoneList.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                phoneList.Columns[1].HeaderText = "핸드폰명";
                phoneList.Columns[2].HeaderText = "모델명";
                phoneList.Columns[3].HeaderText = "통신사";
                phoneList.Columns[4].HeaderText = "제조사";

                phoneList.ClearSelection();
                phoneList.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.AliceBlue;
                phoneList.RowsDefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
                phoneList.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LemonChiffon;
                phoneList.RowHeadersVisible = false;
                phoneList.ReadOnly = true;
                phoneList.AllowUserToResizeColumns = false;
                phoneList.AllowUserToResizeRows = false;
                phoneList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                lblError.Text = "";
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("PhoneModel", x.ToString());
            }
        }
        #endregion

        private void cbMaker_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void cbAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void phoneList_Click(object sender, EventArgs e)
        {
            try
            {
                int index = phoneList.CurrentCell.RowIndex;
                modifyNo = phoneList[0, index].Value.ToString();
                phoneModelInfo cinfo = phoneBiz.GetPhoneModel(modifyNo);

                if (cinfo != null)
                {
                    SetPhoneModel(cinfo);
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("phonelist", x.ToString());
            }
        }

        private void SetPhoneModel(phoneModelInfo info)
        {
            modifyNo = info.uid;
            txtName.Text = info.phoneName;
            txtModel.Text = info.modelName;
            cbMakerInsert.SelectedValue = info.Manufacturer;
            cbAgencyInsert.SelectedIndex = Convert.ToInt16(info.agency);
        }
    }
}
