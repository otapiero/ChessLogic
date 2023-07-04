using ChessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogicLibrary.Evaluetion
{
    public class BoardEvaluetion
    {
        private ChessBoard chessBoard;

        private ChessMoveUtilities utilities = new ChessMoveUtilities();

        public BoardEvaluetion(ChessBoard board)
        {
            chessBoard = board;
        }

        public int Evaluete()
        {
            int eval = 0;
            eval += PieceValueEvaluetion();
            eval += TreatenCount();
            eval += CenterControlEvaluetion();
            return eval;
        }
        private int CenterControlEvaluetion()
        {
            int eval = 0;
            return eval;
        }

        private int PieceValueEvaluetion()
        {
            int eval = chessBoard.whitePices.Sum(x => PieceValue.pieceValue[x.GetType()]);
            eval -= chessBoard.blackPices.Sum(x => PieceValue.pieceValue[x.GetType()]);
            return eval;
        }

        private int TreatenCount()
        {
            int sum = 0;
            for (int i = 0; i < BoardLocation.SIZE; i++)
            {
                for (int j = 0; j < BoardLocation.SIZE; j++)
                {
                    var location = chessBoard[i, j];
                    if (utilities.IsTreaten(chessBoard, location, ChessPieceColor.Black))
                    {
                        sum--;
                    }
                    if (utilities.IsTreaten(chessBoard, location, ChessPieceColor.White))
                    {
                        sum++;
                    }
                }
            }
            return sum;

        }

        // Define score values for each piece type
        const int PawnScore = 10;
        const int KnightScore = 30;
        const int BishopScore = 30;
        const int RookScore = 50;
        const int QueenScore = 100;
        const int KingScore = 1000;

        // Define score values for mobility
        const int MobilityCentralization = 5;
        const int MobilityOpenLines = 10;

        // Evaluate the score of the current board position
        public int EvaluateBoard(ChessBoard board)
        {
            int score = 0;

            // Loop through each square on the board
            for (int i = 0; i < 64; i++)
            {
                // Get the piece at this square
                ChessPiece piece = board.GetPieceAtSquare(i);

                if (piece != null)
                {
                    // Add the score for the piece type
                    switch (piece.GetType().Name)
                    {
                        case nameof(Pawn):
                            score += PawnScore;
                            break;
                        case nameof(Knight):
                            score += KnightScore;
                            break;
                        case nameof(Bishop):
                            score += BishopScore;
                            break;
                        case nameof(Rook):
                            score += RookScore;
                            break;
                        case nameof(Queen):
                            score += QueenScore;
                            break;
                        case nameof(King):
                            score += KingScore;
                            break;
                    }

                    // Add the mobility score for the piece
                    if (piece is Knight)
                    {
                        // Add mobility score for knights based on the number of available moves
                        score += MobilityCentralization * CountAvailableMoves(board, piece, i, 2, 1);
                    }
                    else if (piece is Bishop)
                    {
                        // Add mobility score for bishops based on open diagonals
                        score += MobilityOpenLines * CountOpenDiagonals(board, i);
                    }
                    else if (piece is Rook)
                    {
                        // Add mobility score for rooks based on open files
                        score += MobilityOpenLines * CountOpenFiles(board, i);
                    }
                }
            }
            score = GetMiddle(board);
            return score;
        }

        // Helper function to count the number of open diagonals for a bishop
        private int CountOpenDiagonals(ChessBoard board, int square)
        {
            int count = 0;

            // Check each diagonal for open squares
            if (IsOpenDiagonal(board, square, -9)) count++;
            if (IsOpenDiagonal(board, square, -7)) count++;
            if (IsOpenDiagonal(board, square, 7)) count++;
            if (IsOpenDiagonal(board, square, 9)) count++;

            return count;
        }

        // Helper function to count the number of open files for a rook
        private int CountOpenFiles(ChessBoard board, int square)
        {
            int count = 0;

            // Check each file for open squares
            if (IsOpenFile(board, square, -8)) count++;
            if (IsOpenFile(board, square, -1)) count++;
            if (IsOpenFile(board, square, 1)) count++;
            if (IsOpenFile(board, square, 8)) count++;

            return count;
        }

        /// Helper function to count the number of available moves for a knight
        private int CountAvailableMoves(ChessBoard board, ChessPiece piece, int square, int deltaX, int deltaY)
        {
            // Calculate the position of each potential move and count the number that are valid
            int count = 0;
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    if (Math.Abs(i) + Math.Abs(j) == 3)
                    {
                        int targetSquare = square + i * deltaX + j * deltaY;
                        if (targetSquare >= 0 && targetSquare < 64 && (board.GetPieceAtSquare(targetSquare) == null || board.GetPieceAtSquare(targetSquare).Color != piece.Color))
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        // Helper function to check if a diagonal is open
        private bool IsOpenDiagonal(ChessBoard board, int square, int delta)
        {
            int targetSquare = square + delta;
            while (targetSquare >= 0 && targetSquare < 64)
            {
                if (board.GetPieceAtSquare(targetSquare) != null) return false;
                if (targetSquare % 8 == 7 || targetSquare % 8 == 0) break;
                targetSquare += delta;
            }
            return true;
        }
        // Helper function to check if a file is open
        private bool IsOpenFile(ChessBoard board, int square, int delta)
        {
            int targetSquare = square + delta;
            while (targetSquare >= 0 && targetSquare < 64)
            {
                if (board.GetPieceAtSquare(targetSquare) != null) return false;
                targetSquare += delta;
            }
            return true;
        }

        /// <summary>
        /// Calculates the number of cells threatened in the middle of the board for each piece.
        /// </summary>
        /// <param name="board">The chessboard to evaluate.</param>
        /// <returns>The number of threatened cells in the middle of the board for each piece.</returns>
        private int GetMiddle(ChessBoard board)
        {
            int count = 0;
            var middleLocations = new List<(int, int)> { (3, 3), (3, 4), (4, 3), (4, 4) };
            // Loop over all pieces on the board
            foreach (var location in middleLocations)
            {


                if (utilities.IsTreaten(board, new BoardLocation(location.Item1, location.Item2), colorTreater: ChessPieceColor.Black))
                { count++; }



                if (utilities.IsTreaten(board, new BoardLocation(location.Item1, location.Item2), colorTreater: ChessPieceColor.White))
                {
                    count--;
                }



            }



            return count * 1000;

        }
    }
}
