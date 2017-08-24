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
        /// Ordered list of primes.
        /// In constructor is filled only by numbers below sqrt(_maxN)
        /// due to performance improvements
        /// </summary>
        List<int> _primes;

        /// <summary>
        /// Indicates whether _primes contains all number possible from _sieve
        /// When creating _sieve, only numbers below sqrt(_maxN) are put
        /// into _primes list
        /// </summary>
        bool _primesListIsFull;

        /// <summary>
        /// Maximum number in _primes list
        /// </summary>
        int _primesMax;

        /// <summary>
        /// Upper boundary for sieve
        /// </summary>
        int _maxN;

        /// <summary>
        /// Eratosthenes sieve (stores only odd numbers)
        /// </summary>
        BitArray _sieve; 

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

        public PrimesUtils() : this(2) { }

        /// <summary>
        /// Constructor allows to specify up boundary for sieve
        /// (for numbers in sieve primarity checking based on sieve data
        /// without further calculations)
        /// </summary>
        /// <param name="maxN">Up boundary for sieve</param>
        public PrimesUtils(int maxN)
        {
            _maxN = maxN;
            _primes = new List<int>();
            _sieve = new BitArray(maxN/2 + 1);

            _primesListIsFull = false;
            int upBoundary = (int)Math.Floor(Math.Sqrt(maxN)) + 1;
            _primes.Add(2);
            _primesMax = 2;
            for (int i = 3; i <= upBoundary; i+=2)
            {
                if (_sieve[i/2])
                {
                    continue;
                }
                _primes.Add(i);
                _primesMax = i;

                var curr = i * i;
                while (curr <= maxN)
                {
                    if (curr % 2 == 1)
                    {
                        _sieve[curr / 2] = true;
                    }
                    curr += i;
                }
            }
        }

        /// <summary>
        /// Get complete list of primes from sieve
        /// </summary>
        /// <returns></returns>
        public List<int> GetSievePrimes()
        {
            if (!_primesListIsFull)
            {
                if (_primesMax == 2)
                {
                    _primes.Add(3);
                    _primesMax = 3;
                }
                for (int i=_primesMax+2;i<=_maxN;i+=2)
                {
                    if (!_sieve[i/2])
                    {
                        _primes.Add(i);
                    }
                }
                _primesListIsFull = true;
            }
            return _primes;
        }

        public bool IsPrime(long n)
        {
            if (n == 2)
            {
                return true;
            }
            if (n > 2 && n % 2 == 0)
            {
                return false;
            }
            if (n <= _maxN)
            {
                int nInt = (int)n;
                return !_sieve[nInt / 2];
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
                if (IsPrime(currRemain))
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
