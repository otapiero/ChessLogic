using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public interface IChessPieceType
    {
        BoardLocation location { get; set; }
        //ChessPieceType Type { get; }
        IEnumerable<Move> GetValidMoves(ChessBoard board);
    }
}
