using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Chess
{
    public class PgnWriter
    {
        public static async Task Write(PgnInfo pgnInfo, string filePath)
        {
            string pgnText = "";

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            var sb = new StringBuilder(pgnText);
            sb.AppendLine($"[Site \"{pgnInfo.Site}\"]");
            sb.AppendLine($"[Date \"{pgnInfo.Date.ToString("dd-MM-yyyy")}\"]");
            sb.AppendLine($"[White \"{pgnInfo.WhitePlayer}\"]");
            sb.AppendLine($"[Black \"{pgnInfo.BlackPLayer}\"]");
            sb.AppendLine($"[Result \"{pgnInfo.Result}\"]");
            sb.AppendLine();
            sb.AppendLine();

            int index = 0;

            var movesByIndex = GetMovesByIndex(pgnInfo.Moves);

            foreach(var moves in movesByIndex)
            {
                sb.AppendLine($"{index+1}. {moves.white} {moves.black}");
                index++;
            }

            sb.Append($"{pgnInfo.Result}");

            string pgnData = sb.ToString();

            await File.WriteAllTextAsync(filePath, pgnData);
        }

        private static (string white, string black)[] GetMovesByIndex(string[] moves)
        {
            (string white, string black)[] movesByIndex;

            if ((moves.Length / 2) % 2 == 0)
                movesByIndex = new (string white, string black)[moves.Length/2];
            else
                movesByIndex = new (string white, string black)[(moves.Length / 2)+1];

            int index = 0;
            
            for (int i = 0; i < moves.Length-1; i++)
            {
                (string white, string black) move = ("", "");

                move = (moves[i], moves[i + 1]);

                if (i % 2 == 1)
                    index++;
                else
                    movesByIndex[index] = move;

            }



            return movesByIndex;

        }
    }
}
