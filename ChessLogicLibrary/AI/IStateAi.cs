using System.Data.SqlTypes;

namespace AIAlgorithm
{
    public interface IStateAI 
    {
        internal IEnumerable<IStateAI> Childs();
        bool GameOver();
        int Value();
        int DepthCalcul();
    }
}
