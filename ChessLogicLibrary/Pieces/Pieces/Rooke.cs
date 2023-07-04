using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    /// <summary>
    /// The rooke.
    /// </summary>
    public sealed class Rook : ChessPiece
    {

        private readonly static int[][] MoveTemplates = new int[][]
        {
             new [] { 1, 0 },
             new [] { 0, -1 },
             new [] { 0, 1 },
              new [] { -1, 0 }
        };

        /// <summary>
        ///  the location.
        /// </summary>

        public override int[][] GetMoveTemplates()
        {
            return MoveTemplates;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rook"/> class.
        /// </summary>
        /// <param name="x">The x.</param>



        public override IEnumerable<Move> GetValidMoves(ChessBoard board)
        {
            try
            {
                var result = board.moveUtilities.GetMoves(board, this, 7, MoveTemplates);

                return result;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
          
        }
    }
}
