using System;

namespace Api.Tests.Component
{
    public static class TestRandom
    {
        #region Private Fields

        private static readonly Random random = new Random();

        #endregion Private Fields

        #region Public Properties

        public static bool Boolean => Convert.ToBoolean(random.Next(0, 1));
        public static double Double => random.NextDouble();
        public static int Integer => random.Next();
        public static string String => Guid.NewGuid().ToString("N");

        #endregion Public Properties
    }
}
