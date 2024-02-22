
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared
{
    public class ChessHelpers
    {


        //public static Player GetOpponent(Player player)
        //{
        //    return player == Player.White ? Player.Black : Player.White;
        //}

        //public static string GetLastMoveSAN(ChessGame game)
        //{
        //    var lastMove = game.Moves.ToArray().Last();
        //    var moveSAN = lastMove.GetSAN(game);

        //    return moveSAN;
        //}

        //public static (string san, DetailedMove move) ApplyMove(ChessGame game, string moveSAN, Player player)
        //{
        //    DetailedMove move = null;
        //    try
        //    {
                
        //        move = ChessGame.ParseSAN(game, moveSAN, player);
        //        if (move == null)
        //            return (null, null);

        //        if (move != null && game.IsValidMove(move))
        //            game.ApplyMove(move, false);

        //    }
        //    catch(Exception ex)
        //    {
        //        return (null, null); 
        //    }

        //    return (ChessGame.GetLong(move).ToLower(), move); 
        //}
    }
}
