using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone1stBiz.Business
{
     public class BarCodePrint
    {
         public void PrintBarCode(string[] arrData, string CompanyName)
         {
             try
             {
                 if (Barcode.BXLLIB.ConnectPrinter("BIXOLON SLP-T403"))
                 {
                     Business.Phone phoneBiz = new Phone();
                     int MM2D = 8;// mm to dot
                     MM2D = Barcode.BXLLIB.GetPrinterResolution() < 300 ? 8 : 12;

                     int nPaper_Width = Convert.ToInt32(80 * MM2D);
                     int nPaper_Height = Convert.ToInt32(30 * MM2D);
                     int nMarginX = Convert.ToInt32(5 * MM2D);
                     int nMarginY = Convert.ToInt32(5 * MM2D);

                     int nSpeed = 2;
                     int nDensity = 20;


                     Barcode.BXLLIB.StartLabel();
                     Barcode.BXLLIB.SetConfigOfPrinter(nSpeed, nDensity, 10, false, 0, true);
                     Barcode.BXLLIB.SetPaper(nMarginX, nMarginY, nPaper_Width, nPaper_Height, Barcode.BXLLIB.GAP, 0, 16); // 4 inch (Width) * 6 inch (Hiehgt)            
                     Barcode.BXLLIB.PrintDirect("STd");

                     for (int i = 0; i < arrData.Length; i++)
                     {
                         phoneInfo pinfo = phoneBiz.GetPhoneInfo(arrData[i]);
                         phoneModelInfo pModelinfo = phoneBiz.GetPhoneModel(pinfo.phoneModelUid);

                         Barcode.BXLLIB.ClearBuffer();

                         Barcode.BXLLIB.PrintTrueFontLib(0, 0, "Arial", 8, 0, true, true, false, CompanyName);
                         Barcode.BXLLIB.PrintTrueFontLib(650, 0, "Arial", 8, 0, true, true, false, DateTime.Now.ToString("yyyy-MM-dd"));
                         Barcode.BXLLIB.PrintTrueFontLib(0, 40, "Arial", 8, 0, true, true, false, string.Concat(pModelinfo.phoneName, " ", pModelinfo.modelName));
                         Barcode.BXLLIB.PrintTrueFontLib(0, 80, "Arial", 8, 0, true, true, false, pinfo.uniqueKey);
                         Barcode.BXLLIB.PrintTrueFontLib(600, 80, "Arial", 8, 0, true, true, false, pinfo.iemiNo);
                         Barcode.BXLLIB.PrintTrueFontLib(0, 120, "Arial", 8, 0, true, true, false, pinfo.phoneState.Length > 18 ? pinfo.phoneState.Substring(0, 18) : pinfo.phoneState);
                         Barcode.BXLLIB.PrintTrueFontLib(600, 120, "Arial", 8, 0, true, true, false, Convert.ToInt32(pinfo.buyPrice).ToString("###,###"));

                         Barcode.BXLLIB.Print1DBarcode(110, 180, Barcode.BXLLIB.CODE39, 4, 8, 70, Barcode.BXLLIB.ROTATE_0, true, pinfo.barcodeNo);
                         Barcode.BXLLIB.Prints(1, 1);
                     }


                     //	Set the Label End
                     Barcode.BXLLIB.EndLabel();

                     //	Disconnect Printer Driver
                     Barcode.BXLLIB.DisconnectPrinter();
                 }
                 else
                 {
                     IO_Util.Alert("라벨 프린터를 연결해 주세요");
                     return;
                 }
             }
             catch (Exception x)
             {
                 IO_Util.Alert("라벨프린터가 없습니다.");
                 IO_Util.LogWrite("Phone1stBiz.BarCodePrint", x.ToString());
             }
         }

         public void PrintBarCode(string phoneUid, string CompanyName)
         {
             try
             {
                 if (Barcode.BXLLIB.ConnectPrinter("BIXOLON SLP-T403"))
                 {
                     Business.Phone phoneBiz = new Phone();
                     int MM2D = 8;// mm to dot
                     MM2D = Barcode.BXLLIB.GetPrinterResolution() < 300 ? 8 : 12;

                     int nPaper_Width = Convert.ToInt32(80 * MM2D);
                     int nPaper_Height = Convert.ToInt32(30 * MM2D);
                     int nMarginX = Convert.ToInt32(5 * MM2D);
                     int nMarginY = Convert.ToInt32(5 * MM2D);

                     int nSpeed = 2;
                     int nDensity = 20;


                     Barcode.BXLLIB.StartLabel();
                     Barcode.BXLLIB.SetConfigOfPrinter(nSpeed, nDensity, 10, false, 0, true);
                     Barcode.BXLLIB.SetPaper(nMarginX, nMarginY, nPaper_Width, nPaper_Height, Barcode.BXLLIB.GAP, 0, 16); // 4 inch (Width) * 6 inch (Hiehgt)            
                     Barcode.BXLLIB.PrintDirect("STd");

                     phoneInfo pinfo = phoneBiz.GetPhoneInfo(phoneUid);
                     phoneModelInfo pModelinfo = phoneBiz.GetPhoneModel(pinfo.phoneModelUid);

                     Barcode.BXLLIB.ClearBuffer();

                     Barcode.BXLLIB.PrintTrueFontLib(0, 0, "Arial", 8, 0, true, true, false, CompanyName);
                     Barcode.BXLLIB.PrintTrueFontLib(650, 0, "Arial", 8, 0, true, true, false, DateTime.Now.ToString("yyyy-MM-dd"));
                     Barcode.BXLLIB.PrintTrueFontLib(0, 40, "Arial", 8, 0, true, true, false, string.Concat(pModelinfo.phoneName, " ", pModelinfo.modelName));
                     Barcode.BXLLIB.PrintTrueFontLib(0, 80, "Arial", 8, 0, true, true, false, pinfo.uniqueKey);
                     Barcode.BXLLIB.PrintTrueFontLib(600, 80, "Arial", 8, 0, true, true, false, pinfo.iemiNo);
                     Barcode.BXLLIB.PrintTrueFontLib(0, 120, "Arial", 8, 0, true, true, false, pinfo.phoneState.Length > 18 ? pinfo.phoneState.Substring(0, 18) : pinfo.phoneState);
                     Barcode.BXLLIB.PrintTrueFontLib(600, 120, "Arial", 8, 0, true, true, false, Convert.ToInt32(pinfo.buyPrice).ToString("###,###"));
                     Barcode.BXLLIB.Print1DBarcode(110, 180, Barcode.BXLLIB.CODE39, 4, 8, 70, Barcode.BXLLIB.ROTATE_0, true, pinfo.barcodeNo);
                     Barcode.BXLLIB.Prints(1, 1);


                     //	Set the Label End
                     Barcode.BXLLIB.EndLabel();

                     //	Disconnect Printer Driver
                     Barcode.BXLLIB.DisconnectPrinter();
                 }
                 else
                 {
                     IO_Util.Alert("라벨 프린터를 연결해 주세요");
                     return;
                 }
             }
             catch (Exception x)
             {
                 IO_Util.Alert("라벨프린터가 없습니다.");
                 IO_Util.LogWrite("Phone1stBiz.BarCodePrint", x.ToString());
             }
         }
    }
}
