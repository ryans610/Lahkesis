using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0.LahkesisExtensionsTest
{
    public partial class TestLahkesisExtensions
    {
        private static readonly byte s_byteMaxValue = 224;
        private static readonly byte s_byteMinValue = 3;

        [TestMethod]
        public void TestNextByte()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                byte result = RNGRandom.Default.NextByte();
                Assert.IsTrue(result < byte.MaxValue);
                Assert.IsTrue(result >= 0);
                //Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void TestNextByteWithMaxValue()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                byte result = RNGRandom.Default.NextByte(s_byteMaxValue);
                Assert.IsTrue(result < s_byteMaxValue);
                Assert.IsTrue(result >= 0);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(RNGRandom.Default.NextByte(0), 0);
        }

        [TestMethod]
        public void TestNextByteWithRange()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                byte result = RNGRandom.Default.NextByte(s_byteMinValue, s_byteMaxValue);
                Assert.IsTrue(result < s_byteMaxValue);
                Assert.IsTrue(result >= s_byteMinValue);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(
                RNGRandom.Default.NextByte(s_byteMinValue, s_byteMinValue),
                s_byteMinValue);
            try
            {
                RNGRandom.Default.NextByte(s_byteMaxValue, s_byteMinValue);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_byteMaxValue);
            }
        }
    }
}
