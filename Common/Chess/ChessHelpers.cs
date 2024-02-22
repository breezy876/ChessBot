using Chess;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Common.Chess
{

    public enum Player { White, Black }

    public static class ChessHelpers
    {

        static ChessBoard board;

        static ChessHelpers()
        {
            board = new ChessBoard();

        }
        public static void ResetBoard()
        {
            board = new ChessBoard();
        }

        public static bool IsValidMove(string san)
        {
            bool isValid = false;

            try
            {
               isValid = board.IsValidMove(san);
            }
            catch (Exception ex)
            {
                return false;
            }
            return isValid;
        }

        public static string ToAscii() => board.ToAscii();


        public static void MakeMove(string san)
        {
            try
            {
  
                board.Move(san);
            }
            catch (Exception ex)
            {

            }
        }

        public static List<string> MakeAllMoves(List<string> moves)
        {
            var longMoves = new List<string>();
            int index = 1;

            foreach(var moveSan in moves)
            {
                try
                {
                    string moveLong = GetLong(moveSan, GetMoveNoPlayer(index));
                
                    if (string.IsNullOrEmpty(moveLong))
                        continue;
                    else
                    {
                        board.Move(moveSan);
                        longMoves.Add(moveLong);
                    }
                }
                catch (Exception ex)
                {

                }
                index++;
            }
            return longMoves;
        }

        public static string GetLong(string san, Player player)
        {
            Move move = null;
            san = san.Replace("+", "").Replace("#", "");

            if (san.Contains("="))
            {
                string pieceChar = san.Substring(san.Length - 1, 1).ToUpper();
                san = san.Substring(0, san.Length - 2);
                move = board.ParseFromSan(san);
                return move == null ? "" : $"{board.ParseFromSan(san).OriginalPosition}{board.ParseFromSan(san).NewPosition}={pieceChar}";
            }

            if (san.Equals("O-O"))
                return player == Player.White ? "e1g1" : "e8g8";

            else if (san.Equals("O-O-O"))
                return player == Player.White ? "e1c1" : "e8c8";

            
            try
            {
                move = board.ParseFromSan(san);
                return move == null ? "" : $"{board.ParseFromSan(san).OriginalPosition}{board.ParseFromSan(san).NewPosition}";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static Move ParseMove(string san, Player player)
        {
            Move move = null;
            try
            {
                move = board.ParseFromSan(san);
            }

            catch (Exception ex)
            {
                return null;
            }
            return move;
        }

        public static Player GetOpponent(Player player) => player == Player.White ? Player.Black : Player.White;

        public static bool IsTurn(Player player) => player == board.Turn.ToEnum();


        public static Player GetMoveNoPlayer(int moveNo) => moveNo % 2 == 0 ? Player.Black : Player.White;

    }
}