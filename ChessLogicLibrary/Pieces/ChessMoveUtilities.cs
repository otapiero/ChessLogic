using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChessLogic
{
    static public  class ChessMoveUtilities
    {
        private readonly static int[][] ChecksMoveTemplates = new int[][]
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

        private static bool IsValid(ChessBoard board, BoardLocation currentLocation, int deltaRow, int deltaCol, out BoardLocation location)
        {
            location = null;
            BoardLocation current = currentLocation;
            var newRow = current.Row + deltaRow;
            if (!BoardLocation.IsInRange(newRow)) return false;

            var newCol = current.Column + deltaCol;
            if (!BoardLocation.IsInRange(newCol)) return false;

            location = board.cells[newRow, newCol];

            return true;
        }
        /// <summary>
        /// check if the path batween  from to to is empty (used to chek checks move)
        /// </summary>
        /// <param name="board">The board.</param>
        /// <param name="from">The from.</param>
        /// <param name="to">The to.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>A bool.</returns>
        private static bool EmptyPathForward(ChessBoard board, BoardLocation from, BoardLocation to, int[] direction)
        {
            if (to.Column != from.Column && to.Row != from.Row)
                return false;
            int range = to.Column == from.Column ? Math.Abs(from.Column - to.Column) : Math.Abs(from.Row - to.Row);
            for (int i = 1; i < range; i++)
            {
                if (!board.cells[from.Row + i * direction[0], from.Column + i * direction[0]].IsEmpty())
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// check if the path batween from to to is empty (used to chek checks move) in diagonal
        /// </summary>
        /// <param name="board">The board.</param>
        /// <param name="from">The from.</param>
        /// <param name="to">The to.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>A bool.</returns>
        private static bool EmptyPathDiagonal(ChessBoard board, BoardLocation from, BoardLocation to, int[] direction)
        {

            if (Math.Abs(to.Column - from.Column) != Math.Abs(to.Row - to.Row))
                return false;
            for (int i = 1; i < Math.Abs(to.Column - from.Column); i++)
            {
                if (!board.cells[from.Row + i * direction[0], from.Column + i * direction[0]].IsEmpty())
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// find if a location is treaten
        /// </summary>
        /// <param name="board">The board.</param>
        /// <param name="location">The location.</param>
        /// <param name="color">The color.</param>
        /// <returns>A bool.</returns>
        public static bool IsTreaten(ChessBoard board, BoardLocation location, ChessPieceColor colorTreater)
        {
            foreach (var dir in ChecksMoveTemplates)
            {
                for (var radius = 1; radius < 8; radius++)
                {
                    var deltaX = radius * dir[0];
                    var deltaY = radius * dir[1];
                    if (!IsValid(board, location, deltaX, deltaY, out BoardLocation newLocation))
                    {
                        break;
                    }

                    if (!newLocation.IsEmpty())
                    {
                        if (newLocation.piece.Color != colorTreater && newLocation.piece is not Pawn notPawn &&
                            newLocation.piece.GetMoveTemplates().Contains(dir))
                            return true;
                        else
                            break;
                    }                  
                }
            }
            foreach (var dir in new Pawn().GetMoveTemplates())
            {
                if(IsValid(board, location, dir[1], dir[2],out BoardLocation newLocation) && 
                    (!newLocation.IsEmpty()&&newLocation.piece is Pawn))           
                {
                    return true;  
                }
            }
            

            return false;
        }

        //internal static bool IsInCheck(Board board, ChessPiece piece, IEnumerable<int[]> mults)

        /// <summary>
        /// Gets the moves.
        /// </summary>
        /// <param name="board">The board.</param>
        /// <param name="piece">The piece.</param>
        /// <param name="range">The range.</param>
        /// <param name="mults">The mults.</param>
        /// <returns>A list of Moves.</returns>
        public static IEnumerable<Move> GetMoves(ChessBoard board, ChessPiece piece, int range, IEnumerable<int[]> mults)
        {
            if (board == null) throw new ArgumentNullException("board");
            if (piece == null) throw new ArgumentNullException("piece");
            if (range < 1) throw new ArgumentOutOfRangeException("range");
            if (mults == null || !mults.Any()) throw new ArgumentException("mults");
            bool IsCheck;

            var ret = new List<Move>();
            // list of the possible move acording the paterns in mults
            foreach (var mult in mults)
            {
                for (var radius = 1; radius <= range; radius++)
                {

                    var deltaX = radius * mult[0];
                    var deltaY = radius * mult[1];
                    if (!IsValid(board, piece.location, deltaX, deltaY, out BoardLocation newLocation))
                    {
                        break;
                    }
                    if (newLocation.IsEmpty())
                        ret.Add(new Move(piece.location, newLocation));
                    else if (newLocation.piece.Color != piece.Color)
                    {
                        ret.Add(new Move(piece.location, newLocation, true));
                        break;
                    }
                    else
                        break;

                }
            }
            return ret;
        }
    }
}
