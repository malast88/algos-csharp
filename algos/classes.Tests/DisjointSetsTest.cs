using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace classes.Tests
{
    [TestClass]
    public class DisjointSetsTest
    {
        [TestMethod]
        public void DisjoinSetsBaseScenarioTest()
        {
            var djs = new DisjointSets(4);
            Assert.AreEqual(0, djs.SetsCount);
            var setId = djs.CreateSet(0);
            Assert.AreEqual(0, setId);
            Assert.AreEqual(1, djs.SetsCount);
            djs.AddToSet(setId, 1);
            Assert.AreEqual(setId, djs.GetIncludingSetId(1));
            var setId1 = djs.CreateSet(2);
            Assert.AreEqual(2, djs.SetsCount);
            djs.Union(setId, setId1);
            Assert.AreEqual(1, djs.SetsCount);
            Assert.AreEqual(setId, djs.GetIncludingSetId(2));
        }

        [TestMethod]
        public void DisjoinSetsPerformanceTest()
        {
            #region Arrange
            var numElements = 1000000;
            var djs = new DisjointSets(numElements);
            #endregion
            #region Act
            var firstRoot = 0;
            var secondRoot = numElements / 2;
            djs.CreateSet(firstRoot);
            djs.CreateSet(secondRoot);
            for (int i=0;i<numElements;i++)
            {
                if (i != firstRoot && i != secondRoot)
                {
                    if (i < secondRoot)
                    {
                        djs.AddToSet(firstRoot, i);
                    }
                    else
                    {
                        djs.AddToSet(secondRoot, i);
                    }
                }
            }
            djs.Union(firstRoot, secondRoot);
            #endregion
            #region Assert
            Assert.AreEqual(1, djs.SetsCount);
            for (int i = 0; i < numElements; i++)
            {
                Assert.AreEqual(0, djs.GetIncludingSetId(i));
            }
            #endregion
        }
    }
}
