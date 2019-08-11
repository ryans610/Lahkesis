using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0
{
    public partial class TestRNGRandom
    {
        [TestMethod]
        public void TestNextBytes()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                var buffer = new byte[10];
                RNGRandom.Default.NextBytes(buffer);
                Assert.AreEqual(buffer.Length, 10);
            }
        }
    }
}
