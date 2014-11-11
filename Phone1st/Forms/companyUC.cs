using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Phone1stBiz.Business;

namespace Phone1st.Forms
{
    public partial class companyUC : UserControl
    {
        public string companyNo = string.Empty;
        public string companyName = string.Empty;
        public string companySection = string.Empty;

        public companyUC()
        {
            InitializeComponent();
        }

        private void companyUC_Load(object sender, EventArgs e)
        {

        }

        public void SetCompanyInfo()
        {
            if (companySection.Equals("1"))
                phoneGroup.Text = "매도 업체";
            else
                phoneGroup.Text = "매입 업체";

            if (!string.IsNullOrEmpty(companyNo))
            {
                Company classCompany = new Company();
                CompanyInfo cinfo = classCompany.GetCompanyInfo(companyNo);

                companyName = cinfo.CompanyName;
                lblCompany.Text = cinfo.CompanyName;
                lblDamdang.Text = cinfo.ContactName;
                lblPhone.Text = cinfo.hp;
                lblTel.Text = cinfo.tel;
                lblAddr.Text = cinfo.addr;
            }        
        }
    }
}
