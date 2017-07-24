using System.Collections.Generic;

namespace classes.Graph
{
    public class GraphEdgeWeightComparer : IComparer<GraphEdge>
    {
        public int Compare(GraphEdge x, GraphEdge y)
        {
            return x.Weight.CompareTo(y.Weight);
        }
    }
}
