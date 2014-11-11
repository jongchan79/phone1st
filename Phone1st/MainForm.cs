using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Phone1stBiz.Business;

namespace Phone1st
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            DialogResult dlgResult = login.ShowDialog();

            if (dlgResult == DialogResult.OK)
            {
                //Forms.BuyMain uc = new Forms.BuyMain();
                //uc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

                //MainPanel.Controls.Clear();
                //MainPanel.AutoScroll = true;
                //MainPanel.Controls.Add(uc);

                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.Close();
            }

            //User.LoginID = "spazio79";
            //User.ClientNo = "1";
            //Forms.SalesMain uc = new Forms.SalesMain();
            //uc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            //MainPanel.Controls.Clear();
            //MainPanel.AutoScroll = true;
            //MainPanel.Controls.Add(uc);
            //this.WindowState = FormWindowState.Maximized;
        }

        private void Main1_Sub2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Menu2_Sub1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Phone1stBiz.Business.User.LoginID))
            {
                Phone1stBiz.IO_Util.Alert("세션이 만료되었습니다.\r\n다시 로그인 해주세요.");
                Application.Exit();
            }

            Forms.BuyMain uc = new Forms.BuyMain();
            uc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            MainPanel.Controls.Clear();
            MainPanel.AutoScroll = true;
            MainPanel.Controls.Add(uc);            
        }

        private void Menu2_Sub2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Phone1stBiz.Business.User.LoginID))
            {
                Phone1stBiz.IO_Util.Alert("세션이 만료되었습니다.\r\n다시 로그인 해주세요.");
                Application.Exit();
            }

            Forms.SellMain uc = new Forms.SellMain();
            uc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            MainPanel.Controls.Clear();
            MainPanel.AutoScroll = true;
            MainPanel.Controls.Add(uc);   
        }


        private void Menu2_Sub3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Phone1stBiz.Business.User.LoginID))
            {
                Phone1stBiz.IO_Util.Alert("세션이 만료되었습니다.\r\n다시 로그인 해주세요.");
                Application.Exit();
            }

            Forms.StockMain uc = new Forms.StockMain();
            uc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            MainPanel.Controls.Clear();
            MainPanel.AutoScroll = true;
            MainPanel.Controls.Add(uc);   
        }

        private void Menu2_Sub4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Phone1stBiz.Business.User.LoginID))
            {
                Phone1stBiz.IO_Util.Alert("세션이 만료되었습니다.\r\n다시 로그인 해주세요.");
                Application.Exit();
            }

            Forms.SalesMain uc = new Forms.SalesMain();
            uc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            MainPanel.Controls.Clear();
            MainPanel.AutoScroll = true;
            MainPanel.Controls.Add(uc);   
        }

        private void Menu3_Sub1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Phone1stBiz.Business.User.LoginID))
            {
                Phone1stBiz.IO_Util.Alert("세션이 만료되었습니다.\r\n다시 로그인 해주세요.");
                Application.Exit();
            }

            Forms.CompanyMain uc = new Forms.CompanyMain();
            uc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            MainPanel.Controls.Clear();
            MainPanel.AutoScroll = true;
            MainPanel.Controls.Add(uc);   
        }

        private void Menu3_Sub2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Phone1stBiz.Business.User.LoginID))
            {
                Phone1stBiz.IO_Util.Alert("세션이 만료되었습니다.\r\n다시 로그인 해주세요.");
                Application.Exit();
            }

            Forms.PhoneModel uc = new Forms.PhoneModel();
            uc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            MainPanel.Controls.Clear();
            MainPanel.AutoScroll = true;
            MainPanel.Controls.Add(uc);   
        }

        private void Menu3_Sub3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Phone1stBiz.Business.User.LoginID))
            {
                Phone1stBiz.IO_Util.Alert("세션이 만료되었습니다.\r\n다시 로그인 해주세요.");
                Application.Exit();
            }

            string url = "https://selfsolve.apple.com/agreementWarrantyDynamic.do";
            System.Diagnostics.Process.Start(url);
        }
    }
}
