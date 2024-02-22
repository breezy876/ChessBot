using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Chess
{
    public class PgnInfo
    {
        public string Site { get; set; }
        public string WhitePlayer { get; set; }
        public string BlackPLayer { get; set; }

        public string RoundNo { get; set; }

        public DateTime Date { get; set; }

        public string Result { get; set; }

        public string[] Moves { get; set; }
    }
}
