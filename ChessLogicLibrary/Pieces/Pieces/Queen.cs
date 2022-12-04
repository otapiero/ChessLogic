using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public sealed class Quinn : ChessPiece
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
            var result=  ChessMoveUtilities.GetMoves(board, this, ChessBoard.SIZE, MoveTemplates);
            return result;
        }

        public override int[][] GetMoveTemplates()
        {
            return MoveTemplates; 
        }
    }
}
