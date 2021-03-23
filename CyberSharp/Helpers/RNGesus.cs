using System;

namespace CyberSharp.Helpers
{
    public static class RNGesus        //this should be the only meme class/variable name in the project, at least I hope so, forgive me please
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
