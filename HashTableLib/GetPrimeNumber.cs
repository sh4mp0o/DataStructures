﻿namespace HashTableLib
{
    internal class GetPrimeNumber
    {
        private int _current;
        readonly int[] _primes = { 11, 29, 61, 127, 257, 523, 1087, 
            2213, 4519, 9619, 19717, 40009, 62851, 75431, 90523, 
            108631, 130363, 156437,  187751, 225307, 270371, 324449, 
            389357, 467237, 560689, 672827, 807403, 968897, 
            1162687, 1395263, 1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 
            4999559, 5999471, 7199369};
        readonly int[] primedivs = {
             11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
            1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591,
            17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437,
            187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263,
            1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369};
        public int Next()
        {
            if (_current < _primes.Length)
            {
                var value = _primes[_current];
                _current++;
                return value;
            }
            _current++;
            return (_current - _primes.Length) * _primes[_primes.Length - 1];
        }
        public int GetMin()
        {
            _current = 0;
            return _primes[_current];
        }
        public (int, int) GetPrimes(int size)
        {
            for(int i = 0;i< primedivs.Length;i++) 
            {
                if (primedivs[i] > size)
                    return (primedivs[i], primedivs[i + 1]);
            }
            return (size * 3 + 1, size + 11);
        }
    }
}
