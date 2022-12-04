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
      
        public override IEnumerable<Move> GetValidMoves(ChessBoard board)
        {
            var result = ChessMoveUtilities.GetMoves(board, this, 1, MoveTemplates);
            return result;
        }

        public override int[][] GetMoveTemplates()
        {
            return MoveTemplates;
        }
    }
}
