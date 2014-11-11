using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace Phone1st
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))
                collection = searcher.Get();
            string description = string.Empty;
            string usbinfo = string.Empty;

            foreach (var device in collection)
            {
                description = (string)device.GetPropertyValue("Description");
                usbinfo = "";

                if (description.IndexOf("apple", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    foreach (var prop in device.Properties)
                    {
                        usbinfo += prop.Name + ":" + prop.Value + "\r\n";
                    }

                    //usbinfo = (string)device.GetPropertyValue("DeviceID") + "\r\n" +
                    //(string)device.GetPropertyValue("PNPDeviceID") + "\r\n" +
                    //description + "\r\n" +
                    //device.ToString() + "\r\n";
                    Phone1stBiz.IO_Util.LogWrite("test", usbinfo);
                    textBox1.Text = usbinfo;
                }
                else if (description.IndexOf("samsung", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    foreach (var prop in device.Properties)
                    {
                        usbinfo += prop.Name + ":" + prop.Value + "\r\n";
                    }
                    // DeviceManagerLib.DeviceManager mag = new DeviceManagerLib.DeviceManager();

                    uint aa = 0;
                    // mag.

                    // mag.GetDeviceID
                    //usbinfo = (string)device.GetPropertyValue("DeviceID") + "\r\n" +
                    //(string)device.GetPropertyValue("PNPDeviceID") + "\r\n" +
                    //description + "\r\n" +
                    //device.ToString() + "\r\n";
                    // Phone1stBiz.IO_Util.LogWrite("test", usbinfo);
                    // textBox2.Text = usbinfo;                
                }
            }

            collection.Dispose();
        }

    }
}
