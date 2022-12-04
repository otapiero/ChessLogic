using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    /// <summary>
    /// The cordinates.
    /// </summary>
    public sealed class  BoardLocation
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
       public int Row { get; init; }
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
       public int Column { get; init; }
        public  static  int SIZE = 8;
        public ChessPiece piece;

        /// <summary>
        /// is in range.
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <returns>A bool.</returns>
        public static bool IsInRange(int pos)
        {
            return (pos >= 0) && (pos < SIZE);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BoardLocation"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public BoardLocation SetPiece(ChessPiece piece)
        {
            this.piece = piece;
            return this;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BoardLocation"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public BoardLocation(int x, int y)
        {
            if (IsInRange(x) && IsInRange(Column))
            {
                this.Row = x;
                this.Column = y;
            }
            else
                throw new ArgumentException("the index are out of range");
        }
        /// <summary>
        /// Are empty.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool IsEmpty()
        {
            return piece == null;
        }
        /// <summary>
        /// Indices the.
        /// </summary>
        /// <returns>An int.</returns>
        public int Index()
        {
            return this.Row + this.Column * SIZE;
        }

        /// <summary>
        /// Locations the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>A BoardLocation.</returns>
        static public BoardLocation Location(int index)
        {
            return new BoardLocation(index%SIZE, index/SIZE);
        }

        public override string? ToString()
        {
            return $"BoardLocation:({Row},{Column})\n";
        }
    }
}
