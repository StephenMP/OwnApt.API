using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Tests.Component
{
    public static class TestRandom
    {
        private static readonly Random random = new Random();
        public static string String => Guid.NewGuid().ToString("N");
        public static int Integer => random.Next();
        public static double Double => random.NextDouble();
    }
}
