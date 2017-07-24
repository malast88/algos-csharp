using System.Collections;
using System.Collections.Generic;

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

        /// <summary>
        /// Constructor which initializes util class with maximum poossible
        /// value of prime number. At one calculates all primes under maxN
        /// </summary>
        /// <param name="maxN">Maximum possible value of prime number</param>
        public PrimesUtils(int maxN)
        {
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

        public bool IsPrime(int n)
        {
            return _primesHashSet.Contains(n);
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
                for (int i=0;i<countCurrDividor;i++)
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
                for (int i=1;i<=countMultipliers;i++)
                {
                    currMultiplier *= prime.Key;
                    result.Add(currMultiplier);
                    for (int j=0;j<prevDividorsCount;j++)
                    {
                        result.Add(result[j] * currMultiplier);
                    }
                }
            }

            return result;
        }
    }
}
