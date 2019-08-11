using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0.LahkesisExtensionsTest
{
    public partial class TestLahkesisExtensions
    {
        private static readonly byte s_byteArraySize = 30;

        [TestMethod]
        public void TestNextByteArray()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                byte[] result = RNGRandom.Default.NextByteArray(s_byteArraySize);
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Length, s_byteArraySize);
                //Console.WriteLine(result);
            }
        }
    }
}
