using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace ChessLogic.Tests
{
    [TestClass()]
    public class PieceManagerTests
    {
        [TestMethod()]
        public void GenrateChessPiecesTest()
        {
             Dictionary<string, Type> pieceLibrary = new Dictionary<string, Type>()
            {
            {"P",typeof(Pawn) },
            {"R",typeof(Rook) },
            {"N",typeof(Knight) },
            {"B",typeof(Bishop) },
            {"K",typeof(King) },
            {"Q",typeof (Quinn) }
            };
            var pieceOrder = new string[] { "P", "R", "N", "B", "K", "Q" };
            var lisOfPices = PieceManager.GenrateChessPieces(ChessPieceColor.White);
            Assert.AreEqual(lisOfPices.Count, 16, "Wrong count of pices");

            lisOfPices = PieceManager.GenrateChessPieces(ChessPieceColor.Black, pieceOrder);

            for (int i = 0; i < lisOfPices.Count; i++)
            {
                Assert.AreEqual(lisOfPices[i].GetType(), pieceLibrary[pieceOrder[i]], $"wrong piece type at {i}");
            }
        }

        [TestMethod()]
        public void PlacePicesTest()
        {
            var cheesBoard = new ChessBoard();
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j <8; j++)
                {
                    Assert.AreEqual(cheesBoard.cells[i, j].piece, cheesBoard.whitePices[ i * 8 +j],
                        $"wrong white pice in place {i},{j}");
                    Assert.AreEqual(cheesBoard.cells[6+i, j].piece, cheesBoard.blackPices[ i * 8 + j],
                        $"wrong black pice in place {i},{j}");

                }
            }
        }
    }
}