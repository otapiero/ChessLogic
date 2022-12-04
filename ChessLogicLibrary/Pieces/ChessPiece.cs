using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public abstract class ChessPiece : IChessPieceType
    {

        // Enum : To define a color for each piece.
        public ChessPieceColor Color { get; set; }

        // stores piece type or kind (Rook, Bishop, Knight, Queen, King, or Pawn) 
        // Note : they're concrete classes sharing the same interface

        // a flag to distinguish the playable pieces from the elementated ones
        public BoardLocation location { get; set; }

        public ChessPiece() { }

        public ChessPiece(ChessPieceColor color)
        {
            Color = color;
        }

        public ChessPiece( BoardLocation location)
        {
            this.location = location;
        }
        public abstract int[][] GetMoveTemplates();

        public virtual  IEnumerable<Move> GetValidMoves(ChessBoard board)
        {
            return null;
        }
        internal void Setup(ChessPieceColor teamColor)
        {
           this.Color = teamColor;
        }

        public ChessPiece DeepCopy()
        {
            ChessPiece newPiece = (ChessPiece) this.MemberwiseClone();

            return newPiece;
         }
    }
}
