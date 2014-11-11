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

using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace Phone1st.Forms
{
    public partial class SalesMain : UserControl
    {        
        // Company companyBiz = new Company();
        List<PhoneTotalInfo> list = new List<PhoneTotalInfo>();

        public SalesMain()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Phone1stBiz.Business.User.LoginID))
                {
                    IO_Util.Alert("세션이 만료되었습니다.\r\n다시 로그인 해주세요.");
                    Application.Exit();
                }

                Phone phoneBiz = new Phone();
                string sdate = string.Empty;
                string edate = string.Empty;
                string dateGbn = string.Empty;

                if (!cbDateGbn.SelectedIndex.Equals(0))
                {
                    dateGbn = cbDateGbn.SelectedIndex.Equals(1) ? "c." : "e.";
                    sdate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    edate = dateTimePicker2.Value.ToString("yyyy-MM-dd");

                    if (string.IsNullOrEmpty(sdate) || string.IsNullOrEmpty(edate))
                    {
                        IO_Util.Alert("날짜를 선택해주세요");
                        return;
                    }
                }

                list = phoneBiz.TotalSellBuyList("", "", sdate, edate, dateGbn);
                phoneList.DataSource = list;

                phoneList.Columns[0].HeaderText = "번호";
                phoneList.Columns[0].Width = 60;
                phoneList.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                phoneList.Columns[1].Visible = false;
                phoneList.Columns[2].Visible = false;
                phoneList.Columns[3].Visible = false;
                phoneList.Columns[4].HeaderText = "폰이름";
                phoneList.Columns[4].Width = 120;
                phoneList.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                phoneList.Columns[5].HeaderText = "모델명";
                phoneList.Columns[5].Width = 120;
                phoneList.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                phoneList.Columns[6].HeaderText = "일련번호";
                phoneList.Columns[6].Width = 100;
                phoneList.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                phoneList.Columns[7].Visible = false;

                phoneList.Columns[8].HeaderText = "메모";
                phoneList.Columns[8].Width = 100;
                phoneList.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                phoneList.Columns[9].Visible = false;

                phoneList.Columns[10].HeaderText = "제조사";
                phoneList.Columns[10].Width = 100;
                phoneList.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                phoneList.Columns[11].HeaderText = "통신사";
                phoneList.Columns[11].Width = 100;
                phoneList.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                phoneList.Columns[12].HeaderText = "구매가격";
                phoneList.Columns[12].Width = 100;
                phoneList.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                phoneList.Columns[13].HeaderText = "판매가격";
                phoneList.Columns[13].Width = 100;
                phoneList.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                phoneList.Columns[14].Visible = false;
                phoneList.Columns[15].Visible = false;
                phoneList.Columns[16].Visible = false;

                phoneList.Columns[17].HeaderText = "매입업체";
                phoneList.Columns[17].Width = 150;
                phoneList.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                phoneList.Columns[18].HeaderText = "매도업체";
                phoneList.Columns[18].Width = 150;
                phoneList.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                phoneList.Columns[19].HeaderText = "매입일자";
                phoneList.Columns[19].Width = 150;
                phoneList.Columns[19].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                phoneList.Columns[20].HeaderText = "매도일자";
                phoneList.Columns[20].Width = 150;
                phoneList.Columns[20].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                phoneList.ClearSelection();
                phoneList.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.AliceBlue;
                phoneList.RowsDefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
                phoneList.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LemonChiffon;
                phoneList.RowHeadersVisible = false;
                phoneList.ReadOnly = true;
                phoneList.AllowUserToResizeColumns = false;
                phoneList.AllowUserToResizeRows = false;
                phoneList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                companyUC1.Visible = false;
                companyUC2.Visible = false;
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("SalesMain.btnSearch_Click", x.ToString());
            }
        }

        private void SalesMain_Load(object sender, EventArgs e)
        {
            cbDateGbn.Items.Insert(0, "선택");
            cbDateGbn.Items.Insert(1, "입고일자");
            cbDateGbn.Items.Insert(2, "출고일자");
            cbDateGbn.SelectedIndex = 0;            
        }

        private void phoneList_SelectionChanged(object sender, EventArgs e)
        {
            int index = phoneList.CurrentCell.RowIndex;

            companyUC1.Visible = true;
            companyUC1.companySection = "2";
            companyUC1.companyNo = list[index].buyCompany;
            companyUC1.SetCompanyInfo();

            companyUC2.Visible = false;

            if (!string.IsNullOrEmpty(list[index].sellCompany))
            {
                companyUC2.Visible = true;
                companyUC2.companySection = "1";
                companyUC2.companyNo = list[index].sellCompany;
                companyUC2.SetCompanyInfo();
            }
        }

        private void txtSerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtSerialNo.Text.Length < 3)
                    {
                        IO_Util.Alert("일련번호를 입력해주세요");
                    }
                    else
                    {
                        if (phoneList.RowCount.Equals(0))
                        {
                            IO_Util.Alert("검색을 먼저 해주세요.");
                        }
                        else
                        {
                            for (int i = 0; i < phoneList.RowCount; i++)
                            {
                                if (list[i].uniqueKey.Equals(txtSerialNo.Text))
                                {
                                    IO_Util.LogWrite("Test", phoneList.CurrentCell.RowIndex.ToString());
                                    phoneList.CurrentCell = phoneList[0, i];
                                    IO_Util.LogWrite("Test", phoneList.CurrentCell.RowIndex.ToString());
                                    //phoneList.CurrentRow.Selected = true;
                                    //phoneList.Rows[i].Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception x)
            {
                IO_Util.LogWrite("SalesMain.txtSerialno_Keydown", x.ToString());
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExportExcel(true);
        }

        private void ExportExcel(bool captions)
        {
            this.saveFileDialog1.FileName = "매출";
            this.saveFileDialog1.DefaultExt = "xls";
            this.saveFileDialog1.Filter = "Excel files (*.xls)|*.xls";
            // this.saveFileDialog1.InitialDirectory = "c:\\";

            DialogResult result = saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                int num = 0;
                object missingType = Type.Missing;

                Excel.Application objApp;
                Excel._Workbook objBook;
                Excel.Workbooks objBooks;
                Excel.Sheets objSheets;
                Excel._Worksheet objSheet;
                Excel.Range range;

                int cnt = 0;

                for (int i = 0; i < phoneList.ColumnCount; i++)
                {
                    if (phoneList.Rows[0].Cells[i].Visible)
                        cnt++;
                }

                string[] headers = new string[cnt];
                string[] columns = new string[cnt];

                try
                {
                    cnt = 0;

                    for (int c = 0; c < phoneList.ColumnCount; c++)
                    {
                        if (phoneList.Rows[0].Cells[c].Visible)
                        {
                            headers[cnt] = phoneList.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();
                            num = cnt + 65;
                            columns[cnt] = Convert.ToString((char)num);
                            cnt++;
                        }
                    }

                    objApp = new Excel.Application();
                    objBooks = objApp.Workbooks;
                    objBook = objBooks.Add(Missing.Value);
                    objSheets = objBook.Worksheets;
                    objSheet = (Excel._Worksheet)objSheets.get_Item(1);

                    if (captions)
                    {
                        cnt = 0;

                        for (int c = 0; c < phoneList.ColumnCount; c++)
                        {
                            if (phoneList.Rows[0].Cells[c].Visible)
                            {
                                range = objSheet.get_Range(columns[cnt] + "1", Missing.Value);
                                range.set_Value(Missing.Value, headers[cnt]);
                                cnt++;
                            }
                        }
                    }
                                        
                    for (int i = 0; i < phoneList.RowCount - 1; i++)
                    {
                        cnt = 0;

                        for (int j = 0; j < phoneList.ColumnCount; j++)
                        {
                            if (phoneList.Rows[0].Cells[j].Visible)
                            {
                                range = objSheet.get_Range(columns[cnt] + Convert.ToString(i + 2), Missing.Value);
                                range.set_Value(Missing.Value, phoneList.Rows[i].Cells[j].Value.ToString());
                                cnt++;
                            }
                        }
                    }

                    objApp.Visible = false;
                    objApp.UserControl = false;

                    objBook.SaveAs(@saveFileDialog1.FileName,
                              Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
                              missingType, missingType, missingType, missingType,
                              Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                              missingType, missingType, missingType, missingType, missingType);
                    objBook.Close(false, missingType, missingType);

                    Cursor.Current = Cursors.Default;

                    MessageBox.Show("저장되었습니다.");
                }
                catch (Exception theException)
                {
                    String errorMessage;
                    errorMessage = "Error: ";
                    errorMessage = String.Concat(errorMessage, theException.Message);
                    errorMessage = String.Concat(errorMessage, " Line: ");
                    errorMessage = String.Concat(errorMessage, theException.Source);

                    MessageBox.Show(errorMessage, "Error");
                }
            }
        } 
    }
}
