using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ChessLogic;

namespace ChessLogic
{
    public static class PieceManager
    {



        static string[] DefaultPieceOrder = new string[16]
        {
        "P","P","P","P", "P","P","P","P",
        "R","N","B","K","Q","B","N","R"
        };
        static Dictionary<string, Type> pieceLibrary = new Dictionary<string, Type>()
        {
            {"P",typeof(Pawn) },
            {"R",typeof(Rook) },
            {"N",typeof(Knight) },
            {"B",typeof(Bishop) },
            {"K",typeof(King) },
            {"Q",typeof (Quinn) }
        };


        static public List<ChessPiece> GenrateChessPieces(ChessPieceColor teamColor, string[] pieceOrder = null)
        {
            if (pieceOrder == null)
                pieceOrder = DefaultPieceOrder;
            var newList = new List<ChessPiece>();
            for (int i = 0; i < pieceOrder.Length; i++)
            {
                ChessPiece piece = Activator.CreateInstance(pieceLibrary[pieceOrder[i]]) as ChessPiece;
                piece.Setup(teamColor);
                newList.Add(piece);
            }
            return newList;
        }
        static public void PlacePices(int pawnRow, int picesRow, List<ChessPiece> pices, ChessBoard board)
        {
            for (int i = 0; i < 8; i++)
            {
                pices[i].location = board.cells[pawnRow, i].SetPiece(pices[i]);
                pices[i + 8].location = board.cells[picesRow, i].SetPiece(pices[i + 8]);   
            }
        }


    }
}
