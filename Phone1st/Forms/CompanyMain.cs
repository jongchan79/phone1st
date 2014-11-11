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
    public partial class CompanyMain : UserControl
    {
        int section = 1;
        int result = 0;
        
        Company companyBiz = new Company();

        public CompanyMain()
        {
            InitializeComponent();
        }

        private void CompanyMain_Load(object sender, EventArgs e)
        {
            GetData();
            SetEmpty();
            SetModify(false);
        }

        #region seller Manager

        private void btnNew_Click(object sender, EventArgs e)
        {
            SetEmpty();
            SetModify(false);
            SellerList.ClearSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCompany.Text.Length < 2)
            {
                lblError.Text += "* 회사명을 입력해주세요.\r\n";
                return;
            }
            else if (txtContact.Text.Length < 2)
            {
                lblError.Text += "* 담당명을 입력해주세요.\r\n";
                return;
            }
            else if (txtCompany.Text.Length < 2)
            {
                lblError.Text += "* 핸드폰 번호를 입력해주세요.\r\n";
                return;
            }
            else
            {
                CompanyInfo cinfo = new CompanyInfo();

                if (lblCode.Visible)
                    cinfo.uid = lblCode.Text;
                else
                    cinfo.uid = "0";

                cinfo.section = section;
                cinfo.CompanyName = txtCompany.Text;
                cinfo.ContactName = txtContact.Text;
                cinfo.hp = txtHp.Text;
                cinfo.tel = txtTel.Text;
                cinfo.addr = txtAddr.Text;

                cinfo.bankName = txtBank.Text;
                cinfo.bankNo = txtBankNo.Text;
                cinfo.bankOwner = txtBankOwner.Text;
                cinfo.memo = txtMemo.Text;
                cinfo.register = User.LoginID;

                result = companyBiz.SetCompanyInfo(cinfo);

                if (result.Equals(1))
                {
                    IO_Util.Alert("정상적으로 입력되었습니다.");
                    GetData();
                    SetEmpty();
                    SetModify(false);
                }
                else
                    IO_Util.Alert("디비 작업 중 오류가 발생했습니다.\r 잠시 후 다시 시도 해주세요.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblCode.Text))
            {
                result = companyBiz.Delete(lblCode.Text);

                if (result > 0)
                {
                    IO_Util.Alert("삭제되었습니다.");
                    GetData();
                    SetEmpty();
                    SetModify(false);
                }
                else
                    IO_Util.Alert("디비 작업 중 오류가 발생했습니다.\r 잠시 후 다시 시도 해주세요.");
            }
            else
                IO_Util.Alert("삭제할 업체를 선택해 주세요");
        }
                
        private void SellerList_Click(object sender, EventArgs e)
        {
            try
            {
                int index = SellerList.CurrentCell.RowIndex;

                CompanyInfo cinfo = companyBiz.GetCompanyInfo(SellerList[0, index].Value.ToString());

                if (cinfo != null)
                {
                    SetCompanyData(cinfo);
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("CompanyMain.SellerList_Click", x.ToString());
            }
        }
        #endregion

        #region buyer Manager
        private void btnNew2_Click(object sender, EventArgs e)
        {
            SetModify(false);
            SetEmpty();
            buyerList.ClearSelection();
        }

        private void btnSave2_Click(object sender, EventArgs e)
        {
            if (txtCompany2.Text.Length < 2)
            {
                lblError2.Text += "* 회사명을 입력해주세요.\r\n";
                return;
            }
            else if (txtContact2.Text.Length < 2)
            {
                lblError2.Text += "* 담당명을 입력해주세요.\r\n";
                return;
            }
            else if (txtCompany2.Text.Length < 2)
            {
                lblError2.Text += "* 핸드폰 번호를 입력해주세요.\r\n";
                return;
            }
            else
            {
                CompanyInfo cinfo = new CompanyInfo();

                if (lblCode2.Visible)
                    cinfo.uid = lblCode.Text;
                else
                    cinfo.uid = "0";

                cinfo.section = section;
                cinfo.CompanyName = txtCompany2.Text;
                cinfo.ContactName = txtContact2.Text;
                cinfo.hp = txtHp2.Text;
                cinfo.tel = txtTel2.Text;
                cinfo.addr = txtAddr2.Text;

                cinfo.bankName = txtBank2.Text;
                cinfo.bankNo = txtBankNo2.Text;
                cinfo.bankOwner = txtBankOwner2.Text;
                cinfo.memo = txtMemo2.Text;
                cinfo.register = User.LoginID;

                result = companyBiz.SetCompanyInfo(cinfo);

                if (result.Equals(1))
                {
                    IO_Util.Alert("정상적으로 입력되었습니다.");
                    GetData();
                    SetEmpty();
                    SetModify(false);
                }
                else
                    IO_Util.Alert("디비 작업 중 오류가 발생했습니다.\r 잠시 후 다시 시도 해주세요.");
            }
        }

        private void btnDel2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblCode2.Text))
            {
                result = companyBiz.Delete(lblCode2.Text);

                if (result > 0)
                {
                    IO_Util.Alert("삭제되었습니다.");
                    GetData();
                    SetEmpty();
                    SetModify(false);
                }
                else
                    IO_Util.Alert("디비 작업 중 오류가 발생했습니다.\r 잠시 후 다시 시도 해주세요.");
            }
            else
                IO_Util.Alert("삭제할 업체를 선택해 주세요");
        }
        
        private void buyerList_Click(object sender, EventArgs e)
        {
            int index = buyerList.CurrentCell.RowIndex;

            CompanyInfo cinfo = companyBiz.GetCompanyInfo(buyerList[0, index].Value.ToString());

            if (cinfo != null)
            {
                SetCompanyData(cinfo);
            }
        }

        #endregion

        #region Common
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
             section = tabControl1.SelectedIndex +1;
             GetData();
             SetEmpty();
             SetModify(false);
        }
        
        private void GetData()
        {
            List<CompanyInfo> info = companyBiz.GetCompanyList(section);

            if (info.Count() > 0)
            {
                if (section.Equals(1))
                {
                    SellerList.DataSource = info;

                    SellerList.Columns[0].HeaderText = "번호";
                    SellerList.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    SellerList.Columns[1].Visible = false;
                    SellerList.Columns[2].HeaderText = "회사명";
                    SellerList.Columns[3].HeaderText = "담당자";
                    SellerList.Columns[4].HeaderText = "핸드폰";
                    SellerList.Columns[5].HeaderText = "연락처";
                    SellerList.Columns[6].Visible = false;
                    SellerList.Columns[7].Visible = false;
                    SellerList.Columns[8].Visible = false;
                    SellerList.Columns[9].Visible = false;
                    SellerList.Columns[10].HeaderText = "메모";
                    SellerList.Columns[11].Visible = false;
                    SellerList.Columns[12].HeaderText = "등록자";
                    SellerList.Columns[13].HeaderText = "등록일";

                    SellerList.ClearSelection();
                    SellerList.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.AliceBlue;
                    SellerList.RowsDefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
                    SellerList.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LemonChiffon;
                    SellerList.RowHeadersVisible = false;
                    SellerList.ReadOnly = true;
                    SellerList.AllowUserToResizeColumns = false;
                    SellerList.AllowUserToResizeRows = false;
                    SellerList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
                else if (section.Equals(2))
                {
                    buyerList.DataSource = info;

                    buyerList.Columns[0].HeaderText = "번호";
                    buyerList.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    buyerList.Columns[1].Visible = false;
                    buyerList.Columns[2].HeaderText = "회사명";
                    buyerList.Columns[3].HeaderText = "담당자";
                    buyerList.Columns[4].HeaderText = "핸드폰";
                    buyerList.Columns[5].HeaderText = "연락처";
                    buyerList.Columns[6].Visible = false;
                    buyerList.Columns[7].Visible = false;
                    buyerList.Columns[8].Visible = false;
                    buyerList.Columns[9].Visible = false;
                    buyerList.Columns[10].HeaderText = "메모";
                    buyerList.Columns[11].Visible = false;
                    buyerList.Columns[12].HeaderText = "등록자";
                    buyerList.Columns[13].HeaderText = "등록일";

                    buyerList.ClearSelection();
                    buyerList.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.AliceBlue;
                    buyerList.RowsDefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
                    buyerList.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LemonChiffon;
                    buyerList.RowHeadersVisible = false;
                    buyerList.ReadOnly = true;
                    buyerList.AllowUserToResizeColumns = false;
                    buyerList.AllowUserToResizeRows = false;
                    buyerList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
            }
        }

        private void SetModify(bool chk)
        {
            if (section.Equals(1))
            {
                lblCode.Visible = chk;
                lblCodeName.Visible = chk;
                lblDate.Visible = chk;
                lblRegister.Visible = chk;
                lblTitle1.Visible = chk;
                lblTitle2.Visible = chk;

                hidLine1.Visible = chk;
                hidLine2.Visible = chk;
                hidLine3.Visible = chk;
                hidLine4.Visible = chk;
                hidLine5.Visible = chk;
                hidLine6.Visible = chk;
                hidLine7.Visible = chk;
                hidLine8.Visible = chk;
                hidLine9.Visible = chk;
            }
            else
            {
                lblCode2.Visible = chk;
                lblCodeName2.Visible = chk;
                lblDate2.Visible = chk;
                lblRegister2.Visible = chk;
                lblTitle11.Visible = chk;
                lblTitle12.Visible = chk;

                hidLine21.Visible = chk;
                hidLine22.Visible = chk;
                hidLine23.Visible = chk;
                hidLine24.Visible = chk;
                hidLine25.Visible = chk;
                hidLine26.Visible = chk;
                hidLine27.Visible = chk;
                hidLine28.Visible = chk;
                hidLine29.Visible = chk;
            }
        }
        
        private void SetEmpty()
        {
            if (section.Equals(1))
            {
                lblError.Text = "";
                lblCode.Text = "";
                lblDate.Text = "";
                lblRegister.Text = "";

                txtCompany.Text = "";
                txtContact.Text = "";
                txtHp.Text = "";
                txtTel.Text = "";
                txtAddr.Text = "";

                txtBank.Text = "";
                txtBankOwner.Text = "";
                txtBankNo.Text = "";

                txtMemo.Text = "";
            }
            else
            {
                lblError2.Text = "";
                lblCode2.Text = "";
                lblDate2.Text = "";
                lblRegister2.Text = "";

                txtCompany2.Text = "";
                txtContact2.Text = "";
                txtHp2.Text = "";
                txtTel2.Text = "";
                txtAddr2.Text = "";

                txtBank2.Text = "";
                txtBankOwner2.Text = "";
                txtBankNo2.Text = "";

                txtMemo2.Text = "";            
            }
        }

        private void SetCompanyData(CompanyInfo cinfo)
        {
            SetModify(true);

            if (section.Equals(1))
            {
                lblCode.Text = cinfo.uid.ToString();
                txtCompany.Text = cinfo.CompanyName;
                txtContact.Text = cinfo.ContactName;
                txtHp.Text = cinfo.hp;
                txtTel.Text = cinfo.tel;
                txtAddr.Text = cinfo.addr;
                txtBank.Text = cinfo.bankName;
                txtBankNo.Text = cinfo.bankNo;
                txtBankOwner.Text = cinfo.bankOwner;
                txtMemo.Text = cinfo.memo;

                lblRegister.Text = cinfo.register;
                lblDate.Text = cinfo.w_Date;
            }
            else
            {
                lblCode2.Text = cinfo.uid.ToString();
                txtCompany2.Text = cinfo.CompanyName;
                txtContact2.Text = cinfo.ContactName;
                txtHp2.Text = cinfo.hp;
                txtTel2.Text = cinfo.tel;
                txtAddr2.Text = cinfo.addr;
                txtBank2.Text = cinfo.bankName;
                txtBankNo2.Text = cinfo.bankNo;
                txtBankOwner2.Text = cinfo.bankOwner;
                txtMemo2.Text = cinfo.memo;

                lblRegister2.Text = cinfo.register;
                lblDate2.Text = cinfo.w_Date;
            }
        }
        #endregion

    }
}
