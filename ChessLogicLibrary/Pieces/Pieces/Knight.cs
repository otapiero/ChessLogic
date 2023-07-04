using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public sealed class Knight : ChessPiece
    {

        private readonly static int[][] MoveTemplates = new int[][]
        {
            new[] {1,2},
            new[] {1,-2},
            new[] {-1,2},
            new[] {-1,-2},
            new[] {2,1},
            new[] {2,-1},
            new[] {-2,1},
            new[] {-2,-1}
        };
        /// <summary>
        /// Gets all valid moves for a Knight on the given ChessBoard.
        /// </summary>
        /// <param name="board">The ChessBoard on which the piece is located.</param>
        /// <returns>An IEnumerable of valid Move objects.</returns>
        public override IEnumerable<Move> GetValidMoves(ChessBoard board)
        {
            try
            {
                var result = board.moveUtilities.GetMoves(board, this, 1, MoveTemplates);
                return result;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the move templates.
        /// </summary>
        /// <returns>An array of int.</returns>
        public override int[][] GetMoveTemplates()
        {
            return MoveTemplates;
        }
    }
}
