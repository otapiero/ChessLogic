using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic;

/// <summary>
/// A move.
/// </summary>
public sealed class Move
{
    /// <summary>
    /// Gets or sets the ending location.
    /// </summary>
    public BoardLocation endingLocation { get; set; }
    /// <summary>
    /// Gets the piece.
    /// </summary>
    public BoardLocation startingLocation { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether is capture.
    /// </summary>
    public bool isCapture { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether is check.
    /// </summary>
    public bool isCheck { get; set; }=false;

    /// <summary>
    /// Gets or sets a value indicating whether is promotion.
    /// </summary>
    public bool IsPromotion { get; set; }=false;
    /// <summary>
    /// Initializes a new instance of the <see cref="Move"/> class.
    /// </summary>
    /// <param name="piece">The piece.</param>
    /// <param name="endingLocation">The to.</param>
    public Move(BoardLocation startingLocation, BoardLocation endingLocation, bool isCapture = false, bool isCheck = false)
    {
        this.startingLocation = startingLocation ?? throw new ArgumentNullException("startingLocation");
        this.endingLocation = endingLocation ?? throw new ArgumentNullException("endingLocation");
        this.isCapture = isCapture;
        this.isCheck = isCheck;
    }
}

