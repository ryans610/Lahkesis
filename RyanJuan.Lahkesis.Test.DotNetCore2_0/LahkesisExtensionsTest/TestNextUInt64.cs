using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0.LahkesisExtensionsTest
{
    public partial class TestLahkesisExtensions
    {
        private static readonly ulong s_ulongMaxValue = 15323372036854775807UL;
        private static readonly ulong s_ulongMinValue = 155563354444UL;

        [TestMethod]
        public void TestNextUInt64()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                ulong result = RNGRandom.Default.NextUInt64();
                Assert.IsTrue(result < ulong.MaxValue);
                Assert.IsTrue(result >= 0UL);
                //Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void TestNextUInt64WithMaxValue()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                ulong result = RNGRandom.Default.NextUInt64(s_ulongMaxValue);
                Assert.IsTrue(result < s_ulongMaxValue);
                Assert.IsTrue(result >= 0UL);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(RNGRandom.Default.NextUInt64(0UL), 0UL);
        }

        [TestMethod]
        public void TestNextUInt64WithRange()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                ulong result = RNGRandom.Default.NextUInt64(s_ulongMinValue, s_ulongMaxValue);
                Assert.IsTrue(result < s_ulongMaxValue);
                Assert.IsTrue(result >= s_ulongMinValue);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(
                RNGRandom.Default.NextUInt64(s_ulongMinValue, s_ulongMinValue),
                s_ulongMinValue);
            try
            {
                RNGRandom.Default.NextUInt64(s_ulongMaxValue, s_ulongMinValue);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_ulongMaxValue);
            }
        }
    }
}
