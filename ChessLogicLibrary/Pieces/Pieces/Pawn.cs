﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public sealed class Pawn : ChessPiece
    {
      

        private bool inOriginalSquer { get; set; } = true;

        public override int[][] GetMoveTemplates()
        {
            return new int[][] { new[]{ 1,-1}, new int[] { 1, 1 }};
        }

        public override IEnumerable<Move> GetValidMoves(ChessBoard board)
        {
            try
            {
                int direction = (Color == ChessPieceColor.White) ? 1 : -1;
                var res = new List<Move>();
                // if it is not promotion
                if (BoardLocation.IsInRange(location.Row + 2 * direction))
                {

                    if (board[location.Row + direction, location.Column].IsEmpty())
                    {
                        res.Add(new Move(this.location, board[location.Row + direction, location.Column]));
                        if (inOriginalSquer && board[location.Row + 2 * direction, location.Column].IsEmpty())
                            res.Add(new Move(this.location, board[location.Row + 2 * direction, location.Column]));

                    }
                    if (BoardLocation.IsInRange(location.Column + 1) &&
                        !board[location.Row + direction, location.Column + 1].IsEmpty() &&
                        board[location.Row + direction, location.Column + 1].piece.Color != Color)
                    {
                        res.Add(new Move(this.location, board[location.Row + direction, location.Column + 1]));
                    }
                    if (BoardLocation.IsInRange(location.Column - 1) &&
                        !board[location.Row + direction, location.Column - 1].IsEmpty() &&
                        board[location.Row + direction, location.Column - 1].piece.Color != Color)
                    {
                        res.Add(new Move(this.location, board[location.Row + direction, location.Column - 1]));
                    }
                }
                else if (BoardLocation.IsInRange(location.Row + direction))
                {
                    if (board[location.Row + direction, location.Column].IsEmpty())
                    {
                        res.Add(new Move(this.location, board[location.Row + direction, location.Column]));

                        // Promot()
                    }
                    if (BoardLocation.IsInRange(location.Column + 1) &&
                        !board[location.Row + direction, location.Column + 1].IsEmpty() &&
                        board[location.Row + direction, location.Column + 1].piece.Color != Color)
                    {
                        res.Add(new Move(this.location, board[location.Row + direction, location.Column + 1]));
                        // Promot()

                    }
                    if (BoardLocation.IsInRange(location.Column - 1) &&
                        !board[location.Row + direction, location.Column - 1].IsEmpty() &&
                        board[location.Row + direction, location.Column - 1].piece.Color != Color)
                    {
                        res.Add(new Move(this.location, board[location.Row + direction, location.Column - 1]));
                        // Promot()

                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
          
        }
    }
}
