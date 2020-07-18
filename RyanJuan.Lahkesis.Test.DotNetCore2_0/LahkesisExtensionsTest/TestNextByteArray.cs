using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0.LahkesisExtensionsTest
{
    public partial class TestLahkesisExtensions
    {
        private const byte ByteArraySize = 30;

        [TestMethod]
        public void TestNextByteArray()
        {
            var random = new FakeRandom(byteArrayFiller: x =>
            {
                for(byte i = 0; i < x.Length; i += 1)
                {
                    x[i] = (byte)(i * 2);
                }
            });

            byte[] result = random.NextByteArray(ByteArraySize);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Length, ByteArraySize);
            for(byte i = 0; i < ByteArraySize; i += 1)
            {
                Assert.AreEqual(result[i], (byte)(i * 2));
            }
        }
    }
}
