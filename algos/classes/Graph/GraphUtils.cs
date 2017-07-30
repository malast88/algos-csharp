using System;
using System.Collections.Generic;

namespace classes.Graph
{
    public class GraphUtils
    {
        /// <summary>
        /// Calculates minimal spanning tree using Kruskal algo.
        /// Edges are required to be sorted by weigth.
        /// </summary>
        /// <param name="numNodes">Count of nodes</param>
        /// <param name="edges">Edges data</param>
        /// <returns>Array of indicies of edges in minimal spanning tree</returns>
        public static int[] CalcKruskal(int numNodes,
            GraphEdge[] edges)
        {
            int[] result = new int[numNodes - 1];

            var ds = new DisjointSets(numNodes);
            var connectedNodesCount = 0;
            var currResultEdge = 0;
            // for each edge try to include it in minimal spanning tree
            for (int i = 0; i < edges.Length; i++)
            {
                var set1 = ds.GetIncludingSetId(edges[i].Node1);
                var set2 = ds.GetIncludingSetId(edges[i].Node2);
                
                if (set1 == set2 && set1 != -1)
                {
                    // this edge will introduce cycle, skip it
                    // it already connects two nodes which are 
                    // already in the same region
                    continue;
                }
                else
                {
                    // edge connects two edges which are not in the same region
                    // so this edge can be included into MST
                    result[currResultEdge] = i;
                    currResultEdge++;

                    if (set1 != -1 && set2 != -1)
                    {
                        // edge connects two distinct regions - union them
                        ds.Union(set1, set2);
                    }
                    else if (set1 != -1 && set2 == -1)
                    {
                        // node 2 is single, add to gerion of node 1
                        ds.AddToSet(set1, edges[i].Node2);
                        connectedNodesCount += 1;
                    }
                    else if (set2 != -1 && set1 == -1)
                    {
                        // node 1 is single, add to gerion of node 2
                        ds.AddToSet(set2, edges[i].Node1);
                        connectedNodesCount += 1;
                    }
                    else
                    {
                        // both nodes are singe
                        // create region and add them into it
                        var newSet = ds.CreateSet(edges[i].Node1);
                        ds.AddToSet(newSet, edges[i].Node2);
                        connectedNodesCount += 2;
                    }

                    // if all nodes are connected and in the single region
                    // then MST is built
                    if (connectedNodesCount == numNodes && ds.SetsCount == 1)
                    {
                        return result;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates shorted reach weight in undirected graph from start node to others
        /// </summary>
        /// <param name="n">Overall nodes count</param>
        /// <param name="edges">Edges weights data (w>0). edges[i] is edges for i-th node, key - dest node, value - wight of edge</param>
        /// <param name="start">Start node</param>
        /// <returns>Array of reaches weights (result[i] is weight of reach from start to i-th node). 
        /// If node is unreachanble - value == -1
        /// </returns>
        public static int[] DijkstraReachWeight(int n, Dictionary<int, int>[] edges, int start)
        {
            var visitedNodes = new HashSet<int>();

            var result = new int[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = -1;
            }
            result[start] = 0;

            var nodesToReach = new SortedSet<Tuple<int, int>>();
            nodesToReach.Add(new Tuple<int, int>(0, start));

            while (nodesToReach.Count > 0)
            {
                var currNodeToReach = nodesToReach.Min;
                nodesToReach.Remove(currNodeToReach);
                var currNode = currNodeToReach.Item2;
                if (visitedNodes.Contains(currNode))
                {
                    continue;
                }
                visitedNodes.Add(currNode);
                foreach (var kvp in edges[currNode])
                {
                    var currEdge = kvp;
                    var currEdgeDestNode = currEdge.Key;
                    var currEdgeW = currEdge.Value;

                    if (visitedNodes.Contains(currEdgeDestNode))
                    {
                        continue;
                    }

                    if (result[currEdgeDestNode] == -1 || result[currEdgeDestNode] > result[currNode] + currEdgeW)
                    {
                        result[currEdgeDestNode] = result[currNode] + currEdgeW;
                    }

                    nodesToReach.Add(new Tuple<int, int>(result[currEdgeDestNode], currEdgeDestNode));
                }
            }

            return result;
        }
    }
}
