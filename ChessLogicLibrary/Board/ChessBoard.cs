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
        public BoardLocation[,] cells = new BoardLocation[8, 8];
        public List<ChessPiece> whitePices { get; set; }
        public List<ChessPiece> blackPices { get; set; }
        private ChessPiece whiteKing;
        private ChessPiece blackKing;

        public ChessPieceColor turnColor { get; private set; } = ChessPieceColor.White;
        public const int SIZE = 8;

        public List<ChessPiece>[] Pices { get; set; }

        public bool GameOver { get; set; } = false;
        public ChessBoard()
        {
            // callback the initiation process
            Initiate();
        }
        private ChessBoard(ChessBoard board)
        {
            // callback the initiation process
            InitiateCopy(board);
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
                    cells[i,j] = new BoardLocation(i,j);
                }
            }
            whitePices = PieceManager.GenrateChessPieces(ChessPieceColor.White);
            blackPices = PieceManager.GenrateChessPieces(ChessPieceColor.Black);
            PieceManager.PlacePices(1,0,whitePices,this);
            PieceManager.PlacePices(6,7,blackPices,this);
            blackKing = blackPices[11];
            Pices = new[] { whitePices, blackPices };
        }
        private void InitiateCopy(ChessBoard board)
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    cells[i, j] = new BoardLocation(i, j);
                }
            }
            this.blackPices = board.blackPices.ConvertAll(piece => piece.DeepCopy());
            this.whitePices = board.whitePices.ConvertAll(piece => piece.DeepCopy());
            for (int i = 0; i < board.blackPices.Count; i++)
            {
                this.blackPices[i].location =
                    this.cells[board.blackPices[i].location.Row, board.blackPices[i].location.Column].SetPiece(this.blackPices[i]);
                
            }
            for (int i = 0; i < board.whitePices.Count; i++)
            {
                this.whitePices[i].location =
                    this.cells[board.whitePices[i].location.Row, board.whitePices[i].location.Column].SetPiece(this.whitePices[i]);

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
            if (ChessMoveUtilities.IsTreaten(this, Pices[1 - (int)turnColor].First(x => x is King).location, turnColor))
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
            var startingLocation = cells[move.startingLocation.Row, move.startingLocation.Column];
            var endingLocation = cells[move.endingLocation.Row, move.endingLocation.Column];
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
    }
}
