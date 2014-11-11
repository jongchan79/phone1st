using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;

using Phone1stBiz.Business;
using Phone1stBiz;

namespace Phone1st.Forms
{
    public partial class phoneUC : UserControl
    {
        public string phoneModelNo = string.Empty;
        public string barCode = string.Empty;
        /// <summary>
        /// 수정시 사용
        /// </summary>
        public string phoneUid = string.Empty;

        public string SellBuy = "1";

        private Socket m_ClientSocket;

        public phoneUC()
        {
            InitializeComponent();                        
        }

        private void phoneUC_Load(object sender, EventArgs e)
        {
            if (SellBuy.Equals("1"))
            { 
                // sell 등록일 때
                txtSerialNo.Enabled = false;
                txtModel.Enabled = false;
                txtIemi.Enabled = false;                
                txtPrice.Enabled = false;
                txtState.Enabled = false;
                txtSellPrice.Enabled = true;
                btnPrint.Visible = false;
                lblAddName.Visible = true;
                txtAddName.Visible = false;
                txtAddPrice.Enabled = false;
            }
            else if (SellBuy.Equals("2"))
            { 
                // buy 등록일 때
                txtPrice.Enabled = true;
                txtSellPrice.Enabled = false;
                txtAddPrice.Enabled = true;
                lblAddName.Visible = false;
                txtAddName.Visible = true;
                txtAddPrice.Enabled = true;
            }

            if (!string.IsNullOrEmpty(phoneModelNo))
            {
                Phone phoneBiz = new Phone();
                phoneModelInfo pinfo = new phoneModelInfo();

                pinfo = phoneBiz.GetPhoneModel(phoneModelNo);
                txtModel.Text = string.Concat(pinfo.phoneName, "(", pinfo.modelName, ")");

                if (pinfo.phoneName.IndexOf("IP") > -1)
                {
                    btnWarrent.Visible = true;
                    btnWarrent.Text = "리퍼조회";
                }
                else
                    btnWarrent.Visible = false;
            }
        }

        public void SetPhoneInfo()
        {
            try
            {
                if (!string.IsNullOrEmpty(phoneUid))
                {
                    Phone phoneBiz = new Phone();
                    phoneInfo pinfo = phoneBiz.GetPhoneInfo(phoneUid);

                    if (!string.IsNullOrEmpty(pinfo.uid))
                    {
                        phoneModelInfo pminfo = new phoneModelInfo();
                        pminfo = phoneBiz.GetPhoneModel(pinfo.phoneModelUid);

                        txtModel.Text = string.Concat(pminfo.phoneName, "(", pminfo.modelName, ")");
                        txtIemi.Text = pinfo.iemiNo;
                        txtPrice.Text = pinfo.buyPrice;
                        txtSellPrice.Text = pinfo.sellPrice;
                        txtSerialNo.Text = pinfo.uniqueKey;
                        phoneModelNo = pinfo.phoneModelUid;
                        barCode = pinfo.barcodeNo;
                        txtState.Text = pinfo.phoneState;
                        txtAddName.Text = pinfo.buyAddText;
                        lblAddName.Text = pinfo.buyAddText;
                        txtAddPrice.Text = pinfo.buyAddPrice;

                        if (!string.IsNullOrEmpty(pinfo.ordernoSell) && SellBuy.Equals("2"))
                            btnDelete.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                IO_Util.LogWrite("phoneUC.SetPhoneInfo", ex.ToString());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSerialNo.Text))
            {
                IO_Util.Alert("일련번호를 입력해주세요");
                txtSerialNo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtIemi.Text))
            {
                IO_Util.Alert("IEMI를 입력해주세요");
                txtIemi.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPrice.Text))
            {
                IO_Util.Alert("금액를 입력해주세요");
                txtPrice.Focus();
                return;
            }

            OrderInfo info = new OrderInfo();

            // 부모창의 Combox 컨트롤 비활성화
            ComboBox cb = (this.Parent.Parent as GroupBox).Controls["cbBuyCompany"] as ComboBox;

            if (cb.SelectedValue.ToString().Equals("0"))
            {
                IO_Util.Alert("업체를 입력해주세요");
                cb.Focus();
                return;
            }

            // cb.Enabled = false;

            TextBox tbox = (this.Parent.Parent as GroupBox).Controls["txtMemo"] as TextBox;
            companyUC cuc = (this.Parent.Parent.Parent as UserControl).Controls["companyUC1"] as companyUC;

            info.companyNo = cb.SelectedValue.ToString();
            info.orderno = (this.Parent.Parent.Parent as BuyMain).orderno;
            info.uid = (this.Parent.Parent.Parent as BuyMain).uid;
            info.memo = tbox.Text;
            info.section = "2";

            phoneInfo pinfo = new phoneInfo();
            Phone phoneBiz = new Phone();

            #region 바코드 출력 로직
            // 한개만 입력하는 곳에서는 MakeBarcode 바로 사용해도 됨
            barCode = phoneBiz.MakeBarcode();
            #endregion

            pinfo.uid = phoneUid;
            pinfo.ordernoBuy = info.orderno;
            pinfo.phoneModelUid = phoneModelNo;
            pinfo.uniqueKey = txtSerialNo.Text;
            pinfo.iemiNo = txtIemi.Text;
            pinfo.buyPrice = txtPrice.Text;
            pinfo.phoneState = txtState.Text;
            pinfo.barcodeNo = barCode;
            pinfo.buyAddText = txtAddName.Text;
            pinfo.buyAddPrice = txtAddPrice.Text;

            string orderno = info.orderno;
            string orderUid = info.uid;

            phoneBiz.SetOrderOne(pinfo, info, ref orderUid, ref orderno, ref phoneUid);

            if (!string.IsNullOrEmpty(orderno) && !string.IsNullOrEmpty(orderUid) && !string.IsNullOrEmpty(phoneUid))
            {
                (this.Parent.Parent.Parent as BuyMain).orderno = orderno;
                (this.Parent.Parent.Parent as BuyMain).uid = orderUid;

                //BarCodePrint barPrint = new BarCodePrint();
                //barPrint.PrintBarCode(phoneUid, cuc.companyName);

                #region 소켓 통신 작업
                if (Phone1stBiz.Communication.Firewall.AuthorizeProgram("Phone1st.exe", Environment.CurrentDirectory))
                {
                    string packetData = string.Concat("barcode|", cuc.companyName, "|", phoneUid);

                    m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("1.223.75.250"), 10000);
                    SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                    args.RemoteEndPoint = ipep;

                    byte[] szData = Encoding.Unicode.GetBytes(packetData);
                    args.SetBuffer(szData, 0, szData.Length);
                    m_ClientSocket.ConnectAsync(args);

                    IO_Util.Alert("바코드가 출력되었습니다.");
                }
                else
                    IO_Util.Alert("방화벽 포트를 확인해주세요[10000]");
                #endregion                
            }
            else
                IO_Util.Alert("다시 시도해주세요.");
        }
        
        private void txtModel_Click(object sender, EventArgs e)
        {
            PhoneSearch search = new PhoneSearch();
            search.frmUc = this;
            search.selectType = (this.Parent.Parent.Parent as BuyMain).GetSection;
            search.Show();
        }

        public void SetPhone(string no)
        {
            Phone phoneBiz = new Phone();
            phoneModelInfo pinfo = new phoneModelInfo();
            phoneModelNo = no;
            pinfo = phoneBiz.GetPhoneModel(phoneModelNo);
            txtModel.Text = string.Concat(pinfo.phoneName, "(", pinfo.modelName, ")");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                Phone phoneBiz = new Phone();

                if (SellBuy.Equals("2"))
                {
                    BuyMain main = this.Parent.Parent.Parent as BuyMain;

                    if (!string.IsNullOrEmpty(phoneUid) && !string.IsNullOrEmpty(main.uid))
                    {
                        result = phoneBiz.DeletePhoneOrder(phoneUid, main.orderno, SellBuy);
                        // main.GetOrderList();
                        
                    }
                    else
                        result = 1;
                }
                else
                {
                    SellMain main = this.Parent.Parent.Parent as SellMain;

                    if (!string.IsNullOrEmpty(phoneUid) && !string.IsNullOrEmpty(main.uid))
                    {
                        result = phoneBiz.DeletePhoneOrder(phoneUid, main.orderno, SellBuy);
                        // main.GetOrderList();
                    }
                    else
                        result = 1;
                }

                if (result.Equals(1))
                {
                    txtModel.Text = "";
                    txtSerialNo.Text = "";
                    txtIemi.Text = "";
                    txtPrice.Text = "";
                    txtState.Text = "";
                    phoneUid = "";

                    #region 삭제한 핸드폰 숨기기
                    Panel panelPhone = this.Parent as Panel;
                    int cnt = panelPhone.Controls.Count;
                    int rowCnt = 0;
                    panelPhone.Height = 0;

                    for (int i = 0; i < cnt; i++)
                    {
                        phoneUC uc = (phoneUC)panelPhone.Controls[i];
                        IO_Util.LogWrite("test", uc.Name + "/" + this.Name);
                        if (this.Name.Equals(uc.Name))
                        {
                            uc.Hide();
                            uc.phoneModelNo = string.Empty;
                        }
                        else if (uc.Visible)
                        {
                            uc.Top = rowCnt * 300 + panelPhone.Height;
                            rowCnt++;
                        }
                    }
                    #endregion

                    panelPhone.Height = (rowCnt * 300);
                    GroupBox grbox = (this.Parent.Parent as GroupBox);
                    grbox.Height = 150 + (rowCnt * 300);

                    IO_Util.Alert("삭제되었습니다.");
                }
                else if (result.Equals(99))
                    IO_Util.Alert("이미 출고된 상품입니다.");
                else
                    IO_Util.Alert("삭제 중 오류가 발생했습니다.");
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("phoneUC.btnDelete_Click", x.ToString());
            }
        }

        private void btnWarrent_Click(object sender, EventArgs e)
        {
            if (btnWarrent.Text.Equals("리퍼조회"))
            {
                string url = "https://selfsolve.apple.com/agreementWarrantyDynamic.do";
                System.Diagnostics.Process.Start(url);
            }
        }

    }
}
