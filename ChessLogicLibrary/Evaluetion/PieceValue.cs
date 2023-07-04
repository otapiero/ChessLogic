using ChessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogicLibrary.Evaluetion
{
    public class PieceValue
    {
        public static Dictionary<Type, int> pieceValue = new Dictionary<Type, int>()
        {
          {typeof(Pawn) ,1},
          {typeof(Rook),3 },
          {typeof(Knight),5 },
          {typeof(Bishop),5 },
          {typeof(King),0 },
          {typeof (Queen) ,9}

        };
    }
}
