using classes.Primes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace classes.Tests
{
    [TestClass]
    public class PrimesUtilsTest
    {
        [TestMethod]
        public void PrimesUtilsShouldGetAllPrimes()
        {
            #region Arrange
            var pu = new PrimesUtils(1000000);
            #endregion
            #region Act
            var primes = pu.GetSievePrimes();
            #endregion
            #region Assert
            Assert.AreEqual(78498, primes.Count);
            Assert.AreEqual(2, primes[0]);
            Assert.AreEqual(999983, primes[78497]);
            #endregion
        }

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

        [TestMethod]
        public void PrimesUtildShouldCheckIfNumberIsPrime()
        {
            var pu = new PrimesUtils();
            Assert.AreEqual(true, pu.IsPrime(41));
            Assert.AreEqual(false, pu.IsPrime(42));
            Assert.AreEqual(true, pu.IsPrime(953));
            Assert.AreEqual(true, pu.IsPrime(997651));
            Assert.AreEqual(true, pu.IsPrime(999715711));
            Assert.AreEqual(true, pu.IsPrime(999973156643));
            Assert.AreEqual(false, pu.IsPrime(1122004669633));
        }
    }
}
