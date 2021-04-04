using System;

namespace TBBProject.Core.Common
{
    public interface IRandomGenerator
    {
        int Generate();
        int Generate(int min, int max);
        int GenerateRerefenceNumber();
    }

    public class RandomGenerator : IRandomGenerator
    {
        public static readonly Random Random = new Random();

        public int Generate()
        {
            lock (Random)
            {
                return Random.Next();
            }
        }

        public int Generate(int min, int max)
        {
            lock (Random)
            {
                return Random.Next(min, max);
            }
        }

        public int GenerateRerefenceNumber()
        {
            lock (Random)
            {
                const int min = 100000000;
                const int max = 999999999;

                return Random.Next(min, max);
            }
        }
    }
}
