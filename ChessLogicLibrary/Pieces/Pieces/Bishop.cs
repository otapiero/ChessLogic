using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public sealed class Bishop : ChessPiece
    {

        private readonly static int[][] MoveTemplates = new int[][]
        {
             new [] { 1, -1 },
             new [] { 1, 1 },
             new [] { -1, -1 },
             new [] { -1, 1 }
        };


        /// <summary>
        /// Gets the valid moves.
        /// </summary>
        /// <param name="board">The board.</param>
        /// <returns>A list of Moves.</returns>
        public override IEnumerable<Move> GetValidMoves(ChessBoard board)
        {
            try
            {
                var result = board.moveUtilities.GetMoves(board, this, 7, MoveTemplates);
                return result;
            }
            catch(Exception ex)
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
