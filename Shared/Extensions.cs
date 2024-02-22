using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class Extensions
    {

        public static bool IsEmpty(this byte[] arr)
        {
            return arr.All(b => b == (byte)0);
        }
        public static byte[] ToByteArray(this string str)
        {
            return str.Select(c => (byte) c).ToArray();
        }

        public static byte[] ExcludeNull(this byte[] arr)
        {
            return arr.Where(c => c != 0).ToArray();
        }

        public static char[] ToCharArray(this byte[] arr)
        {
            return arr.Select(b => (char) b).ToArray();
        }
    }
}
