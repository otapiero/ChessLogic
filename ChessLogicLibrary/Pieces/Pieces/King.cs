using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public sealed class King : ChessPiece
    {

        private readonly static int[][] MoveTemplates = new int[][]
        {
             new [] { 1, -1 },
             new [] { 1, 0 },
             new [] { 1, 1 },
             new [] { 0, -1 },
             new [] { 0, 1 },
             new [] { -1, -1 },
             new [] { -1, 0 },
             new [] { -1, 1 },
           
        };


        public  bool IsAMove(BoardLocation newCordinates)
        {

            if (newCordinates.Row == location.Row)
                if (newCordinates.Column == location.Column+1|| newCordinates.Column == location.Column - 1)
                    return true;
            if (newCordinates.Column == location.Column)
                if (newCordinates.Row == location.Row + 1 || newCordinates.Row == location.Row - 1)
                    return true;
            return false;
        }

        /// <summary>
        /// Nexts the moves.
        /// </summary>
        /// <returns>A list of Mooves.</returns>
        public override IEnumerable<Move> GetValidMoves(ChessBoard board)
        {
            try
            {
                List<Move> result = board.moveUtilities.GetMoves(board, this, 1, MoveTemplates) as List<Move>;
                // add promotiens moves


                return result;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
            

        }

        public override int[][] GetMoveTemplates()
        {
            return MoveTemplates;
        }
    }
}
