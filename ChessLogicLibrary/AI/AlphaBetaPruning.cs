using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAlgorithm
{

    /// <summary>
    /// The alpha beta pruning Algorithm generic.
    /// </summary>
    public class AlphaBetaPruning<T> where T : class, IStateAI
    {
        int MAX = int.MaxValue;
        int MIN = int.MinValue;
    
        /// <summary>
        /// Minis the max.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="alpha">The alpha.</param>
        /// <param name="beta">The beta.</param>
        /// <param name="maximizingPlayer">If true, maximizing player.</param>
        /// <returns>A          (T,int) .</returns>
        (T,int) MiniMax(T position, int depth, int alpha, int beta, bool maximizingPlayer)
        {
            
            if (position.GameOver())
                return (position, position.Value());
            if (depth <= 0)
            {
                int newDepth = position.DepthCalcul();
                if (newDepth > 0)
                  return  MiniMax(position, newDepth, alpha, beta, maximizingPlayer);
                return (position, position.Value());

            }
            if (maximizingPlayer)
            {
                var maxEval =MIN;
                T next= null;
                foreach( var child in position.Childs()) 
                {
                 
                    next = (T)child;
                    var eval = MiniMax((T)child, depth - 1, alpha, beta, false).Item2;
                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <=alpha)
                        break;
                }
                
                return (next, maxEval); 
            }
            else
            {
                var minEval = MAX;
                T next = null;
                foreach (var child in position.Childs())
                {
                    next = (T)child;

                    var eval = MiniMax((T)child, depth - 1, alpha, beta, true).Item2;
                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                        break;
                }
                return (next, minEval);
            }
        }
        /// <summary>
        /// Bests the next.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="maximizingPlayer">If true, maximizing player.</param>
        /// <param name="next">The next.</param>
        public void BestNext(T position, int depth,  bool maximizingPlayer,out T next ) 
        {
            var childs = position.Childs();
            next = (childs is not null) ? (T)childs.MaxBy(x => MiniMax((T)x, depth, MIN, MAX, maximizingPlayer).Item2) : null;
        }
    }
}
