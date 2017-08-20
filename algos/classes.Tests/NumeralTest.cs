using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using classes.Digits;
using System.Collections.Generic;

namespace classes.Tests
{
    [TestClass]
    public class NumeralTest
    {
        [TestMethod]
        public void NumeralShouldGetDigits()
        {
            #region Arrange
            var ulongNum = 29382440;
            var bigIntegerNum = new BigInteger(43653452598);
            #endregion
            #region Act
            var ulongDigits = Numeral.GetDigits(ulongNum);
            var bigIntegerDigits = Numeral.GetDigits(bigIntegerNum);
            #endregion
            #region Assert
            Assert.AreEqual(8, ulongDigits.Count);
            Assert.AreEqual(0, ulongDigits[0]);
            Assert.AreEqual(4, ulongDigits[1]);
            Assert.AreEqual(4, ulongDigits[2]);
            Assert.AreEqual(2, ulongDigits[3]);
            Assert.AreEqual(2, ulongDigits[7]);

            Assert.AreEqual(11, bigIntegerDigits.Count);
            Assert.AreEqual(8, bigIntegerDigits[0]);
            Assert.AreEqual(9, bigIntegerDigits[1]);
            Assert.AreEqual(6, bigIntegerDigits[8]);
            Assert.AreEqual(3, bigIntegerDigits[9]);
            Assert.AreEqual(4, bigIntegerDigits[10]);
            #endregion
        }

        [TestMethod]
        public void NumeralShouldSumDigits()
        {
            #region Arrange
            var digits1 = new List<List<byte>>();
            var digits2 = new List<List<byte>>();
            var sums = new List<List<byte>>();
            var results = new List<List<byte>>();

            digits1.Add(new List<byte>() { 0, 0, 0, 1 });
            digits2.Add(new List<byte>() { 2, 4, 1 });
            sums.Add(new List<byte>());
            results.Add(new List<byte>() { 2, 4, 1, 1 });

            digits1.Add(new List<byte>() { 8, 7, 5, 6 });
            digits2.Add(new List<byte>() { 7, 9 });
            sums.Add(new List<byte>());
            results.Add(new List<byte>() { 5, 7, 6, 6 });

            digits1.Add(new List<byte>() { 9, 9, 9 });
            digits2.Add(new List<byte>() { 9, 9, 9 });
            sums.Add(new List<byte>());
            results.Add(new List<byte>() { 8, 9, 9, 1 });

            digits1.Add(new List<byte>() { 9, 9, 9 });
            digits2.Add(new List<byte>() { 9, 9 });
            sums.Add(new List<byte>());
            results.Add(new List<byte>() { 8, 9, 0, 1 });
            #endregion
            #region Act
            for (int i=0;i<digits1.Count;i++)
            {
                Numeral.SumDigits(digits1[i], digits2[i], sums[i]);
            }
            #endregion
            #region Assert
            for (int i = 0; i < digits1.Count; i++)
            {
                Assert.AreEqual(string.Join(":", results[i]), string.Join(":", sums[i]));
            }
            #endregion
        }

        [TestMethod]
        public void NumeralShouldRetainValue()
        {
            #region Arrange
            var n1 = new BigInteger(234235);
            var n2 = 478365;
            #endregion
            #region Act
            var num1 = new Numeral(n1);
            var num2 = new Numeral(n2);
            var nn1 = num1.GetBigInteger();
            var nn2 = num2.GetBigInteger();
            #endregion
            #region Assert
            Assert.AreEqual(n1, nn1);
            Assert.AreEqual(n2, nn2);
            #endregion
        }

        [TestMethod]
        public void NumeralShouldCheckIfPalyndrome()
        {
            #region Arrange
            #endregion
            #region Act
            #endregion
            #region Assert
            Assert.AreEqual(true, new Numeral(4578754).IsPalyndrome());
            Assert.AreEqual(true, new Numeral(44766744).IsPalyndrome());
            Assert.AreEqual(false, new Numeral(3940573458).IsPalyndrome());
            #endregion
        }
    }
}
