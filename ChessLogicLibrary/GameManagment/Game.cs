using ChessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogicLibrary;

public class Game
{
    ChessBoard board;
    AIAlgorithm.AlphaBetaPruning<ChessState> alphaBetaPruning;

    int depht = 4;
    static Dictionary<Type, string> pieceLibraryWhite = new Dictionary<Type, string>()
    {
        {typeof(Pawn) ,"P"},
        {typeof(Rook),"R" },
        {typeof(Knight),"N" },
        {typeof(Bishop),"B" },
        {typeof(King),"K" },
        {typeof (Queen) ,"Q"}
    };
    static Dictionary<Type, string> pieceLibraryBlack = new Dictionary<Type, string>()
    {
        {typeof(Pawn) ,"p"},
        {typeof(Rook),"r" },
        {typeof(Knight),"n" },
        {typeof(Bishop),"b" },
        {typeof(King),"k" },
        {typeof (Queen) ,"q"}
    };
    public Game() 
    {
        board = new ChessBoard();
        alphaBetaPruning = new AIAlgorithm.AlphaBetaPruning<ChessState>();
    }
    public void AIMove() 
    {
        alphaBetaPruning.BestNext(new ChessState(board), depht, board.turnColor == ChessPieceColor.White, out ChessState next);
        board.ExecuteMove(next.move);
    }
    public void PlayerMove(Move move)
    {
        board.ExecuteMove(move); 
    }
    static int count = 0;
    private string EmptyStr()
    {
        if(count%2==0)
        {
            count++;
            return "#";
        }
        else
        {
            count++;
            return "*";
        }
       
    }
    public string[,] BoardToString()
    {
        string[,] boardString = new string[8,9];
        for (int i = 0; i < ChessBoard.SIZE; i++)
        {
            for (int j = 0; j < ChessBoard.SIZE; j+=2)
            {
                boardString[i, j] = EmptyStr();
                boardString[i, j + 1] = EmptyStr();


                if (board[i,j].IsEmpty())
                {

                }
                else if(board[i, j].piece.Color == ChessPieceColor.White)
                {
                    boardString[i, j] = pieceLibraryWhite[board[i, j].piece.GetType()];
                }
                else
                    boardString[i, j] = pieceLibraryBlack[board[i, j].piece.GetType()];

                if (board[i, j+1].IsEmpty())
                {


                }
                else if (board[i, j + 1].piece.Color == ChessPieceColor.White)
                {
                    boardString[i, j + 1] = pieceLibraryWhite[board[i, j + 1].piece.GetType()];
                }
                else
                    boardString[i, j + 1] = pieceLibraryBlack[board[i, j + 1].piece.GetType()];
            }
            boardString[i, ChessBoard.SIZE] = "\n";
            count--;
        }
        return boardString;
    }
    
}

