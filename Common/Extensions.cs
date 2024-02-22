
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess;

namespace Common.Chess
{
    public static class Extensions
    {
        public static Player ToEnum(this PieceColor color) => (Player)color.Value - 1;

        public static string ToSpaced(this IEnumerable<string> strs) => String.Join(" ", strs);

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => enumerable == null || !enumerable.Any();

        public static string PathOnly(this string path)
        {
            return path.Substring(path.LastIndexOf("\\") + 1);
        }
    }
}
