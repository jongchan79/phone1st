using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace Phone1st
{
    public partial class usbForm : Form
    {
        public usbForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] arrtmp = { "ipconfig", "aaaaa", "12", "23", "43", "3", "45" };


            Array.Clear(arrtmp, 0, 2);
            //ManagementObjectCollection collection;
            //using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))
            //    collection = searcher.Get();

            //foreach (var device in collection)
            //{
            //    Phone1stBiz.IO_Util.LogWrite("test", (string)device.GetPropertyValue("DeviceID"));
            //    Phone1stBiz.IO_Util.LogWrite("test", (string)device.GetPropertyValue("PNPDeviceID"));
            //    Phone1stBiz.IO_Util.LogWrite("test", (string)device.GetPropertyValue("Description"));
            //    Phone1stBiz.IO_Util.LogWrite("test", device.ToString());
                
            //}

            //collection.Dispose();
        }
    }
}
