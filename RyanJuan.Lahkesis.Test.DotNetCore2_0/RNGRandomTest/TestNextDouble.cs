using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0
{
    public partial class TestRNGRandom
    {
        [TestMethod]
        public void TestNextDouble()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                double result = RNGRandom.Default.NextDouble();
                Assert.IsTrue(result < 1.0D);
                Assert.IsTrue(result >= 0.0D);
                //Console.WriteLine(result);
            }
        }
    }
}
