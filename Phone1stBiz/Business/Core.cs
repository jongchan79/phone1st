using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone1stBiz.Business
{
    public class Core
    {
        public string QuotConv(string str)
        {
            if (str.Length > 0)
                return str.Replace("'", "''").Replace("‘", "&lsquo;").Replace("’", "&rsquo;").Replace("“", "&ldquo;").Replace("”", "&rdquo;").Replace("\r\n", "<br />").Replace("\n", "<br />");
            else
                return str;
        }
    }
}
