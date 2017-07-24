using classes.Primes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace classes.Tests
{
    [TestClass]
    public class PrimesUtilsTest
    {
        [TestMethod]
        public void PrimesUtilsShouldCalculatePrimeDividors()
        {
            #region Arrange
            var n = 1008;
            var pu = new PrimesUtils(10000);
            #endregion
            #region Act
            var primeDividors = pu.GetPrimeDividors(n);
            #endregion
            #region Assert
            Assert.AreEqual(3, primeDividors.Count);
            Assert.AreEqual(4, primeDividors[2]);
            Assert.AreEqual(2, primeDividors[3]);
            Assert.AreEqual(1, primeDividors[7]);
            #endregion
        }
        [TestMethod]
        public void PrimesUtilsShouldCalculatePrimeDividorsList()
        {
            #region Arrange
            var n = 1008;
            var pu = new PrimesUtils(10000);
            #endregion
            #region Act
            var primeDividors = pu.GetPrimeDividorsList(n);
            #endregion
            #region Assert
            Assert.AreEqual(7, primeDividors.Count);
            Assert.AreEqual(2, primeDividors[0]);
            Assert.AreEqual(3, primeDividors[4]);
            Assert.AreEqual(7, primeDividors[6]);
            #endregion
        }
        [TestMethod]
        public void PrimesUtilsShouldCalculateAllDividors()
        {
            #region Arrange
            var n = 72;
            var pu = new PrimesUtils(10000);
            #endregion
            #region Act
            var allDividors = pu.GetAllDividors(n);
            #endregion
            #region Assert
            Assert.AreEqual(11, allDividors.Count);
            #endregion
        }
    }
}
