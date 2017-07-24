using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using classes.Graph;
using System.Linq;

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
    }
}
