using Common.Chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class LichessUtilities
    {

        public static long GetMilliSeconds(string time)
        {
            try
            {
                time = time.Replace("\n", "");
                if (time.Length > 5)
                    return (long)TimeSpan.Parse(time).TotalMilliseconds;
                else
                    return (long)TimeSpan.Parse($"00:{time}").TotalMilliseconds;
            }
            catch
            {
                return 0;
            }
        }

    }
}
