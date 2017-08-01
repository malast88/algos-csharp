using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using classes.Graph;
using System.Linq;
using System.Collections.Generic;

namespace classes.Tests
{
    [TestClass]
    public class GraphUtilsTest
    {
        [TestMethod]
        public void GraphUtilsShouldCalculateKruskal()
        {
            #region Arrange
            var numNodes = 3;
            var edges = new GraphEdge[4];
            edges[0].Node1 = 0;
            edges[0].Node1 = 1;
            edges[0].Weight = 3;
            edges[1].Node1 = 1;
            edges[1].Node1 = 2;
            edges[1].Weight = 2;
            edges[2].Node1 = 0;
            edges[2].Node1 = 2;
            edges[2].Weight = 1;
            edges[3].Node1 = 0;
            edges[3].Node1 = 1;
            edges[3].Weight = 2;
            #endregion
            #region Act
            Array.Sort(edges, new GraphEdgeWeightComparer());
            var mst = GraphUtils.CalcKruskal(numNodes, edges);
            #endregion
            #region Assert
            Assert.AreEqual(3, mst.Select(index => edges[index]).Sum(e => e.Weight));
            #endregion
        }

        [TestMethod]
        public void GraphUtilsShouldCalculateDijkstraReachWeight()
        {
            #region Arrange
            var n = 4;
            Dictionary<int, int>[] edges = new Dictionary<int, int>[4];
            edges[0] = new Dictionary<int, int>();
            edges[0].Add(1, 24);
            edges[0].Add(3, 20);
            edges[0].Add(2, 3);
            edges[1] = new Dictionary<int, int>();
            edges[1].Add(0, 24);
            edges[2] = new Dictionary<int, int>();
            edges[2].Add(0, 3);
            edges[2].Add(3, 12);
            edges[3] = new Dictionary<int, int>();
            edges[3].Add(2, 12);
            edges[3].Add(0, 20);
            #endregion
            #region Act
            var reach = GraphUtils.DijkstraReachWeight(n, edges, 0);
            #endregion
            #region Assert
            Assert.AreEqual(24, reach[1]);
            Assert.AreEqual(3, reach[2]);
            Assert.AreEqual(15, reach[3]);
            #endregion
        }

        [TestMethod]
        public void GraphUtilsShouldCalculateDijkstraReachWeightNonUniqueEdges()
        {
            #region Arrange
            var n = 4;
            List<Tuple<int, int>>[] edges = new List<Tuple<int, int>>[4];
            edges[0] = new List<Tuple<int, int>>();
            edges[0].Add(new Tuple<int, int>(1, 24));
            edges[0].Add(new Tuple<int, int>(3, 20));
            edges[0].Add(new Tuple<int, int>(3, 29));
            edges[0].Add(new Tuple<int, int>(2, 3));
            edges[1] = new List<Tuple<int, int>>();
            edges[1].Add(new Tuple<int, int>(0, 24));
            edges[2] = new List<Tuple<int, int>>();
            edges[2].Add(new Tuple<int, int>(0, 3));
            edges[2].Add(new Tuple<int, int>(3, 12));
            edges[3] = new List<Tuple<int, int>>();
            edges[3].Add(new Tuple<int, int>(2, 12));
            edges[3].Add(new Tuple<int, int>(0, 29));
            edges[3].Add(new Tuple<int, int>(0, 20));
            #endregion
            #region Act
            var reach = GraphUtils.DijkstraReachWeight(n, edges, 0);
            #endregion
            #region Assert
            Assert.AreEqual(24, reach[1]);
            Assert.AreEqual(3, reach[2]);
            Assert.AreEqual(15, reach[3]);
            #endregion
        }
    }
}
