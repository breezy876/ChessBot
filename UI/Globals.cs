using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    internal static class Globals
    {
        public static string AppPath => Path.GetDirectoryName(Path.Combine(Application.ExecutablePath));
    }
}
