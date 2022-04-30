using System;

namespace CyberSharp.Helpers
{
    public static class RNGhelper
    {
        public static readonly Random RNG = new Random();       

        public static decimal GenerateInitBalance(decimal lowerBound, decimal upperBound)
        {
            if (lowerBound > upperBound)
            {
                throw new ArgumentException("Lower bound cannot be greater than upper bound");
            }

            return (decimal) RNG.NextDouble()*(upperBound - lowerBound) + lowerBound;
        }
    }
}
