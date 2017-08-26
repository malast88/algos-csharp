using System;
using System.Collections.Generic;
using System.Numerics;

namespace classes.Digits
{
    /// <summary>
    /// Class wich represents decimal positive integer internally as List of digits.
    /// Used in scenarios with high usage of number's digits structure
    /// (for example, polyndrome checking, reverse of digits etc).
    /// </summary>
    public class Numeral
    {
        #region Constructors

        public Numeral(ulong n)
        {
            Digits = GetDigits(n);
        }

        public Numeral(BigInteger n)
        {
            Digits = GetDigits(n);
        }

        #endregion

        #region Public

        public List<byte> Digits { get; set; }

        public BigInteger GetBigInteger()
        {
            BigInteger result = new BigInteger(0);
            BigInteger multiplier = new BigInteger(1);
            for (int i = 0; i < Digits.Count; i++)
            {
                result += Digits[i] * multiplier;
                multiplier *= 10;
            }
            return result;
        }

        public ulong GetULong()
        {
            ulong result = 0;
            ulong multiplier = 1;
            checked
            {
                for (int i = 0; i < Digits.Count; i++)
                {
                    result += Digits[i] * multiplier;
                    multiplier *= 10;
                }
            }
            return result;
        }

        public bool IsPalyndrome()
        {
            for (int i = 0; i < Digits.Count / 2; i++)
            {
                if (Digits[i] != Digits[Digits.Count - 1 - i])
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Static utils

        public static List<byte> GetDigits(ulong n)
        {
            int capacity = 8;
            if (n >= 1000000000000000)
            {
                capacity = 32;
            }
            else if (n >= 1000000000)
            {
                capacity = 16;
            }
            var result = new List<byte>(capacity);
            while (n > 0)
            {
                result.Add((byte)(n % 10));
                n /= 10;
            }
            return result;
        }

        public static List<byte> GetDigits(BigInteger n)
        {
            var result = new List<byte>();
            while (n > 0)
            {
                result.Add((byte)(n % 10));
                n /= 10;
            }
            return result;
        }

        public static void Sum(Numeral n1, Numeral n2, Numeral result)
        {
            SumDigits(n1.Digits, n2.Digits, result.Digits);
        }

        public static void SumDigits(List<byte> digits1, List<byte> digits2, List<byte> result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = 0;
            }

            int currIndex = 0;
            var maxIndex = Math.Max(digits1.Count, digits2.Count);
            while (currIndex < maxIndex)
            {
                if (result.Count == currIndex)
                {
                    result.Add(0);
                }

                int sum = 0;
                if (currIndex < digits1.Count)
                {
                    sum += digits1[currIndex];
                }
                if (currIndex < digits2.Count)
                {
                    sum += digits2[currIndex];
                }
                if (sum < 10)
                {
                    result[currIndex] += (byte)sum;
                    if (result[currIndex] >= 10)
                    {
                        result[currIndex] = (byte)(result[currIndex] % 10);
                        if (result.Count == currIndex + 1)
                        {
                            result.Add(0);
                        }
                        result[currIndex + 1] = 1;
                    }
                }
                else
                {
                    result[currIndex] += (byte)(sum % 10);
                    if (result.Count == currIndex + 1)
                    {
                        result.Add(0);
                    }
                    result[currIndex + 1] = 1;
                }

                currIndex++;
            }
        }

        #endregion
    }
}
