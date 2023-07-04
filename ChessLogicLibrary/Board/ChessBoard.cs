using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIAlgorithm;
using ChessLogicLibrary;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChessLogic
{
    public sealed class ChessBoard
    {
        // Initiate a private new ChessPiece array
        private BoardLocation[,] cells = new BoardLocation[8, 8];
        public List<ChessPiece> whitePices { get; set; }
        public List<ChessPiece> blackPices { get; set; }
        private ChessPiece whiteKing;
        private ChessPiece blackKing;

        public ChessPieceColor turnColor { get; private set; } = ChessPieceColor.White;
        public const int SIZE = 8;

        public List<ChessPiece>[] Pices { get; set; }

        public bool GameOver { get; set; } = false;

        PieceManager pieceManager;
        public ChessMoveUtilities moveUtilities;
        public ChessBoard()
        {
            // callback the initiation process
            pieceManager = new PieceManager();
            moveUtilities = new ChessMoveUtilities();
            Initiate();
        }
        private ChessBoard(ChessBoard board)
        {
            // callback the initiation process
            pieceManager = board.pieceManager;
            moveUtilities = board.moveUtilities;
            InitiateCopy(board);
        }
        public BoardLocation this[int row, int column]
        {
            // This indexer allows you to access the chess pieces on the board by their
            // row and column coordinates, using array-like syntax.
            // For example, board[1,2] returns the ChessPiece object at row 1, column 2.
            get
            {
                if (row >= SIZE || column >= SIZE)
                    throw new ArgumentOutOfRangeException("index out of board size");
               return this.cells[row,column]; 
            }
            set { this.cells[row,column] = value; }
        }
        public ChessBoard DeepCopy()
        {
            ChessBoard newBoard = new ChessBoard(this);
            return newBoard;    
        }
        // initiation process (To initiate the ChessPiece compenents)
        private void Initiate()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    this[i,j] = new BoardLocation(i,j);
                }
            }
            whitePices = pieceManager.GenrateChessPieces(ChessPieceColor.White);
            blackPices = pieceManager.GenrateChessPieces(ChessPieceColor.Black);
            pieceManager.PlacePices(1,0,whitePices,this);
            pieceManager.PlacePices(6,7,blackPices,this);
            blackKing = blackPices[11];
            Pices = new[] { whitePices, blackPices };
        }
        private void InitiateCopy(ChessBoard board)
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    this[i, j] = new BoardLocation(i, j);
                }
            }
            this.blackPices = board.blackPices.ConvertAll(piece => piece.DeepCopy());
            this.whitePices = board.whitePices.ConvertAll(piece => piece.DeepCopy());
            for (int i = 0; i < board.blackPices.Count; i++)
            {
                this.blackPices[i].location =
                    this[board.blackPices[i].location.Row, board.blackPices[i].location.Column].SetPiece(this.blackPices[i]);
                
            }
            for (int i = 0; i < board.whitePices.Count; i++)
            {
                this.whitePices[i].location =
                    this[board.whitePices[i].location.Row, board.whitePices[i].location.Column].SetPiece(this.whitePices[i]);

            }
            this.blackKing = this.blackPices.First(x => x is King);
            this.whiteKing = this.whitePices.First(x => x is King);

            Pices = new[] { this.whitePices, this.blackPices };
        }
        // Dummy : To get the on-board pieces (playable peices)
        public IEnumerable<ChessPiece> GetAvailablePieces() { 
        var result = new List<ChessPiece>();
            return result;
        }

        // Dummy : To get the open-squares that can accept new ChessPiece 
        public IEnumerable<Move> GetAvailablePositions()
        {
            if (moveUtilities.IsTreaten(this, Pices[1 - (int)turnColor].First(x => x is King).location, turnColor))
                GameOver = true;

            IEnumerable<Move> result = new List<Move>();
            Pices[(int)turnColor].ForEach(x => result= result.Union(x.GetValidMoves(this)));
            return result ;
        }

        public ChessPiece GetKingOfSameColor(ChessPieceColor color)
        {
            if (color == ChessPieceColor.White)
                return whiteKing;
            else
                return blackKing;
        }

        public ChessPiece GetKingOfOpositeColor(ChessPieceColor color)
        {
            if (color == ChessPieceColor.White)
                return blackKing;
            else
                return whiteKing;
        }
        public ChessBoard ExecuteMove(Move move)
        {
            var startingLocation = this[move.startingLocation.Row, move.startingLocation.Column];
            var endingLocation = this[move.endingLocation.Row, move.endingLocation.Column];
            if (!endingLocation.IsEmpty())
            {
                if (endingLocation.piece is King)
                    this.GameOver = true;

                Pices[(int)endingLocation.piece.Color].RemoveAll(x => x == endingLocation.piece);
            }
            if (move.IsPromotion)
            { }
            var piece = startingLocation.piece;
            startingLocation.piece = null;
            piece.location = endingLocation.SetPiece(piece);
            turnColor =(ChessPieceColor) ((int) ChessPieceColor.White + ChessPieceColor.Black - turnColor);
            return this;
        }

        public ChessPiece GetPieceAtSquare(int targetSquare)
        {
            int x = targetSquare % 8;
            var y = targetSquare / 8;
            return cells[x, y].piece;
        }
    }
}
