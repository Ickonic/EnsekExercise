using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekService
{
    public class Utility
    {
        public static bool IsValidMeterReading(string meterReading)
        {
            int acualReading;
            bool result = int.TryParse(meterReading, out acualReading);

            if (result)
            {
                if (acualReading < 1 || acualReading > 99999) return false;
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
