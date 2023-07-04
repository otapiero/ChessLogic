using AIAlgorithm;
using ChessLogic;
using ChessLogicLibrary.Evaluetion;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogicLibrary
{
    internal sealed record class ChessState : IStateAI
    {

        ChessBoard board;
        public Move move;

      
        public ChessState(ChessBoard board)
        {
            this.board = board;
        }
        public ChessState(ChessBoard board, Move move)
        {
           this.board = board.DeepCopy().ExecuteMove(move);
            this.move = move;
        }

        public bool GameOver()
        {
            return board.GameOver;
        }

        public IEnumerable<IStateAI> Childs()
        {
            var y = board.GetAvailablePositions();
           return from move in board.GetAvailablePositions()
                  select new ChessState(board, move);
        }

        public int Value()
        {

            return new BoardEvaluetion(board).Evaluete();
        }

        public int DepthCalcul()
        {
          /*  if (move.isCapture || move.isCheck) 
            {
                return 1;
            }*/
            return -1;
        }
    }
}
