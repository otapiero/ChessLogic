using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic.Tests
{
    [TestClass()]
    public class ChessPieceTests
    {
        [TestMethod()]
        public void ChessPieceTest()
        {
            var piece = new King();
        }

        [TestMethod()]
        public void GetValidMovesKnightTest()
        {
            var board = new ChessBoard();   

            Assert.AreEqual(2,board.blackPices.First(x=>x is Knight ).GetValidMoves(board).ToList().Count,"Fail");
        }
        [TestMethod()]
        public void GetValidMovesKingTest()
        {
            var board = new ChessBoard();

            Assert.AreEqual(0, board.blackPices.First(x => x is King).GetValidMoves(board).ToList().Count, "Fail");
        }
        [TestMethod()]
        public void GetValidMovesQuinnTest()
        {
            var board = new ChessBoard();

            Assert.AreEqual(0, board.blackPices.First(x => x is Quinn).GetValidMoves(board).ToList().Count, "Fail");
        }
        [TestMethod()]
        public void GetValidMovesPawnTest()
        {
            var board = new ChessBoard();
            foreach (var item in board.whitePices)
            {
                if (item is Pawn)
                    Assert.AreEqual(2, item.GetValidMoves(board).ToList().Count, $"Fail {item.location}");
            }

        }
        [TestMethod()]
        public void GetValidMovesRookTest()
        {
            var board = new ChessBoard();

            Assert.AreEqual(0, board.blackPices.First(x => x is Rook).GetValidMoves(board).ToList().Count, "Fail");
        }
        [TestMethod()]
        public void GetValidMovesBishopTest()
        {
            var board = new ChessBoard();

            Assert.AreEqual(0, board.blackPices.First(x => x is Bishop).GetValidMoves(board).ToList().Count, "Fail");
        }


    }
}