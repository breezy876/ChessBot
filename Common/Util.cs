using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Util
    {

        public static void WaitUntilThenExecute(Func<bool> cond, Action action)
        {
            while (!cond())
            {
                Thread.Sleep(20);
            }
            action();
        }
    }
}
