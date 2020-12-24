
using System;

namespace Tools
{
    public static class RandomExt
    {
        public static int GetRandomValue(this Random random, int maxValue)
        {
            return random.Next(0, maxValue);
        }
    }
}