using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Global
{
    internal class clsFormat
    {
        public static string DateFormat(DateTime dt)
        {
            return dt.ToString("dd-MMM-yyyy");
        }
    }
}
