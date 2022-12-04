using ChessLogic;

namespace ChessLogic.Tests
{
    [TestClass]
    public class ChessBoardTest
    {

         
        [TestMethod]
        public void Creat()
        {
            ChessBoard chessBoard = new ChessBoard();

            for (int i = 0; i < ChessBoard.SIZE; i++)
            {
                for (int j = 0; j < ChessBoard.SIZE; j++)
                {
                    Assert.AreEqual(chessBoard.cells[i,j].Row, i, $"Error in location ({i},{j})");
                    Assert.AreEqual(chessBoard.cells[i, j].Column, j, $"Error in location ({i},{j})");
                }
            }
            var whitePices = chessBoard.whitePices;
            var blackPices = chessBoard.blackPices;
            Assert.AreEqual(whitePices.Count, 16, "Count of white pices are wrong");
            Assert.AreEqual(blackPices.Count, 16, "Count of black pices are wrong");
        }
        [TestMethod]
        public void GetAvailablePiecesTest()
        {
            ChessBoard chessBoard = new ChessBoard();
           var res = chessBoard.GetAvailablePositions().ToList();
            Assert.AreEqual(20, res.Count, $"are not equal :{res.Count}");

        }
        [TestMethod]
        public void BoardClone()
        {
            ChessBoard chessBoard = new ChessBoard();
            var secondBoard = chessBoard.DeepCopy();
        
        }
    }
}