using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    /// <summary>
    /// The queen.
    /// </summary>
    public sealed class Queen : ChessPiece
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

        public override IEnumerable<Move> GetValidMoves(ChessBoard board)
        {
            try
            {
                var result = board.moveUtilities.GetMoves(board, this, ChessBoard.SIZE, MoveTemplates);
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
