using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Phone1stBiz.Business;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Phone1st
{
    public partial class frmLogin : Form
    {
        public string ID
        {
            get
            {
                return this.txtID.Text;
            }
        }

        public string Password
        {
            get
            {
                return this.txtPassword.Text;
            }
        }

        int TogMove;
        int MValX;
        int MValY;
        private Socket m_ClientSocket;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User();

                if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("아이디, 비밀번호를 입력하세요.");
                }
                else
                {
                    if (Phone1stBiz.Communication.Firewall.AuthorizeProgram("Phone1st.exe", Environment.CurrentDirectory))
                    {
                        string packetData = string.Concat("login|", txtID.Text, "|", txtPassword.Text);

                        m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("1.223.75.250"), 10000);
                        SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                        args.RemoteEndPoint = ipep;

                        byte[] szData = Encoding.Unicode.GetBytes(packetData);
                        args.SetBuffer(szData, 0, szData.Length);
                        m_ClientSocket.ConnectAsync(args);

                        Thread.Sleep(1000);

                        args = new SocketAsyncEventArgs();
                        szData = new byte[2];
                        args.SetBuffer(szData, 0, 2);
                        args.UserToken = m_ClientSocket;
                        args.Completed += new EventHandler<SocketAsyncEventArgs>(Receive_Completed);
                        m_ClientSocket.ReceiveAsync(args);
                    }
                    else
                        MessageBox.Show("방화벽 포트를 확인해주세요[10000]");

                    //szData = new byte[2];
                    //string result = string.Empty;

                    //while (string.IsNullOrEmpty(result))
                    //{
                    //    m_ClientSocket.Receive(szData, 0, szData.Length, SocketFlags.None);
                    //    Encoding.Unicode.GetString(szData);
                    //    result = Encoding.UTF7.GetString(szData, 0, szData.Length);
                    //}

                    //MessageBox.Show(result);

                    #region 이전 소스 백업
                    //string result = user.LoginProcess(txtID.Text, txtPassword.Text);
                    //if (!string.IsNullOrEmpty(result))
                    //{
                    //    if (result.Equals("-1"))
                    //    {
                    //        MessageBox.Show("사용기간이 종료되었습니다.");
                    //    }
                    //    else if (result.Equals("0"))
                    //    {
                    //        MessageBox.Show("일치하는 정보가 없습니다.");
                    //    }
                    //    else
                    //    {
                    //        User.LoginID = txtID.Text;
                    //        User.ClientNo = result;

                    //        this.DialogResult = DialogResult.OK;
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("일치하는 정보가 없습니다.");
                    //}
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "[ERROR]", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Receive_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket ClientSocket = (Socket)sender;

            if (ClientSocket.Connected && e.BytesTransferred > 0)
            {
                byte[] szData = e.Buffer;   // 데이터 수신
                string sData = Encoding.Unicode.GetString(szData);

                string result = sData.Replace("\0", "").Trim();

                if (!string.IsNullOrEmpty(result))
                {
                    if (result.Equals("-1"))
                    {
                        MessageBox.Show("사용기간이 종료되었습니다.");
                    }
                    else if (result.Equals("0"))
                    {
                        MessageBox.Show("일치하는 정보가 없습니다.");
                    }
                    else
                    {
                        User.LoginID = txtID.Text;
                        User.ClientNo = result;

                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    MessageBox.Show("일치하는 정보가 없습니다.");
                }
            }

            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Dispose();
        }


        private void frmLogin_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void frmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove.Equals(1))
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);
        }

        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1;
            MValX = e.X;
            MValY = e.Y;
        }

        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.pictureBox1_Click(null, null);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
