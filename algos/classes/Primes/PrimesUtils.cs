using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace classes.Primes
{
    /// <summary>
    /// Simple implementation of some prime numbers stuff
    /// </summary>
    public class PrimesUtils
    {
        /// <summary>
        /// Ordered list of primes
        /// </summary>
        List<int> _primes;
        /// <summary>
        /// Hash set of primes - allows quickly check if number is prime
        /// </summary>
        HashSet<int> _primesHashSet;

        int _maxN;

        static long ModularExp(int a, int b, int n)
        {
            long d = 1;
            int k = 0;
            while ((b >> k) > 0) k++;

            for (int i = k - 1; i >= 0; i--)
            {
                d = d * d % n;
                if (((b >> i) & 1) > 0) d = d * a % n;
            }

            return d;
        }
        // Implementation on inner loop of Miller-Rabin test
        static bool MillerRabinWitness(long n, long s, long d, long a)
        {
            long x = 0;
            if (a <= int.MaxValue && d <= int.MaxValue && n <= int.MaxValue)
            {
                x = ModularExp((int)a, (int)d, (int)n);
            }
            else
            {
                x = (long)BigInteger.ModPow(a, d, n);
            }
            if (x == 1 || x == n - 1)
            {
                return true;
            }

            while (s > 0)
            {
                if (x <= int.MaxValue && n <= int.MaxValue)
                {
                    x = ModularExp((int)x, 2, (int)n);
                }
                else
                {
                    x = (long)BigInteger.ModPow(x, 2, n);
                }

                if (x == 1)
                {
                    return false;
                }
                if (x == n - 1)
                {
                    return true;
                }
                s--;
            }
            return false;
        }

        /// <summary>
        /// Deterministic version of Miller-Rabin test
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static bool IsPrimeMillerRabinDeterministic(long n)
        {
            if (((!((n & 1) == 1)) && n != 2) || (n < 2) || (n % 3 == 0 && n != 3))
                return false;
            if (n <= 3)
                return true;

            long d = n - 1;
            long s = 0;
            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            if (n < 2047)
            {
                return MillerRabinWitness(n, s, d, 2);
            }
            if (n < 1373653)
            {
                return MillerRabinWitness(n, s, d, 2) && MillerRabinWitness(n, s, d, 3);
            }
            if (n < 9080191)
            {
                return MillerRabinWitness(n, s, d, 31) && MillerRabinWitness(n, s, d, 73);
            }
            if (n < 25326001)
            {
                return MillerRabinWitness(n, s, d, 2) && MillerRabinWitness(n, s, d, 3) && MillerRabinWitness(n, s, d, 5);
            }
            if (n < 3215031751)
            {
                return MillerRabinWitness(n, s, d, 2) && MillerRabinWitness(n, s, d, 3) && MillerRabinWitness(n, s, d, 5) && MillerRabinWitness(n, s, d, 7);
            }
            if (n < 4759123141)
            {
                return MillerRabinWitness(n, s, d, 2) && MillerRabinWitness(n, s, d, 7) && MillerRabinWitness(n, s, d, 61);
            }
            if (n < 1122004669633)
            {
                return MillerRabinWitness(n, s, d, 2) && MillerRabinWitness(n, s, d, 13) && MillerRabinWitness(n, s, d, 23) && MillerRabinWitness(n, s, d, 1662803);
            }
            if (n < 2152302898747)
            {
                return MillerRabinWitness(n, s, d, 2) && MillerRabinWitness(n, s, d, 3) && MillerRabinWitness(n, s, d, 5) && MillerRabinWitness(n, s, d, 7) && MillerRabinWitness(n, s, d, 11);
            }
            if (n < 3474749660383)
            {
                return MillerRabinWitness(n, s, d, 2) && MillerRabinWitness(n, s, d, 3) && MillerRabinWitness(n, s, d, 5) && MillerRabinWitness(n, s, d, 7) && MillerRabinWitness(n, s, d, 11) && MillerRabinWitness(n, s, d, 13);
            }
            if (n < 341550071728321)
            {
                return MillerRabinWitness(n, s, d, 2) && MillerRabinWitness(n, s, d, 3) && MillerRabinWitness(n, s, d, 5) && MillerRabinWitness(n, s, d, 7) && MillerRabinWitness(n, s, d, 11) && MillerRabinWitness(n, s, d, 13) && MillerRabinWitness(n, s, d, 17);
            }
            throw new NotImplementedException("cannot perform deterministic check for n >= 341550071728321");
        }

        /// <summary>
        /// Constructor which initializes util class with maximum poossible
        /// value of prime number. At one calculates all primes under maxN
        /// </summary>
        /// <param name="maxN">Maximum possible value of prime number</param>
        public PrimesUtils(int maxN)
        {
            _maxN = maxN;
            _primes = new List<int>();
            _primesHashSet = new HashSet<int>();

            // uses stupid Sieve of Eratosthenes
            // TODO - consider caching primes under 1000 or even 100000
            var eratosfen = new BitArray(maxN + 1);
            for (int i = 2; i <= maxN; i++)
            {
                if (eratosfen[i])
                {
                    continue;
                }
                _primes.Add(i);
                _primesHashSet.Add(i);

                var curr = i;
                while (curr <= maxN)
                {
                    eratosfen[curr] = true;
                    curr += i;
                }
            }
        }

        public List<int> GetPrimes()
        {
            return _primes;
        }

        public bool IsPrime(long n)
        {
            if (n <= int.MaxValue)
            {
                int nInt = (int)n;
                if (nInt < _maxN)
                {
                    return _primesHashSet.Contains(nInt);
                }
            }
            return IsPrimeMillerRabinDeterministic(n);
        }

        /// <summary>
        /// Get prime dividors of number
        /// </summary>
        /// <param name="n">Number to get prime dividors</param>
        /// <returns>Key - prime dividor, value - count if it</returns>
        public SortedDictionary<int, int> GetPrimeDividors(int n)
        {
            var currRemain = n;
            var currPrimeIndex = 0;
            var dividors = new SortedDictionary<int, int>();
            while (currRemain > 1)
            {
                var currPrime = 0;
                if (_primesHashSet.Contains(currRemain))
                {
                    currPrime = currRemain;
                }
                else
                {
                    currPrime = _primes[currPrimeIndex];
                }


                if (currRemain % currPrime == 0)
                {
                    currRemain = currRemain / currPrime;


                    if (!dividors.ContainsKey(currPrime))
                    {
                        dividors.Add(currPrime, 1);
                    }
                    else
                    {
                        dividors[currPrime]++;
                    }


                }
                else
                {
                    currPrimeIndex++;
                }
            }

            return dividors;
        }

        public List<int> GetPrimeDividorsList(int n)
        {
            var result = new List<int>();

            var primeDividors = GetPrimeDividors(n);
            foreach (var currDividor in primeDividors.Keys)
            {
                var countCurrDividor = primeDividors[currDividor];
                for (int i = 0; i < countCurrDividor; i++)
                {
                    result.Add(currDividor);
                }
            }

            return result;
        }

        /// <summary>
        /// Get all dividors of number
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public List<int> GetAllDividors(int n)
        {
            List<int> result = new List<int>();

            var primeDividors = GetPrimeDividors(n);
            foreach (var prime in primeDividors)
            {
                var currMultiplier = 1;
                var countMultipliers = prime.Value;
                var prevDividorsCount = result.Count;
                for (int i = 1; i <= countMultipliers; i++)
                {
                    currMultiplier *= prime.Key;
                    result.Add(currMultiplier);
                    for (int j = 0; j < prevDividorsCount; j++)
                    {
                        result.Add(result[j] * currMultiplier);
                    }
                }
            }

            return result;
        }
    }
}
