using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0.LahkesisExtensionsTest
{
    public partial class TestLahkesisExtensions
    {
        private static readonly uint s_uintMaxValue = 4632U;
        private static readonly uint s_uintMinValue = 4000U;

        [TestMethod]
        public void TestNextUInt32()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                uint result = RNGRandom.Default.NextUInt32();
                Assert.IsTrue(result < uint.MaxValue);
                Assert.IsTrue(result >= 0U);
                //Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void TestNextUInt32WithMaxValue()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                uint result = RNGRandom.Default.NextUInt32(s_uintMaxValue);
                Assert.IsTrue(result < s_uintMaxValue);
                Assert.IsTrue(result >= 0U);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(RNGRandom.Default.NextUInt32(0U), 0U);
        }

        [TestMethod]
        public void TestNextUInt32WithRange()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                uint result = RNGRandom.Default.NextUInt32(s_uintMinValue, s_uintMaxValue);
                Assert.IsTrue(result < s_uintMaxValue);
                Assert.IsTrue(result >= s_uintMinValue);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(
                RNGRandom.Default.NextUInt32(s_uintMinValue, s_uintMinValue),
                s_uintMinValue);
            try
            {
                RNGRandom.Default.NextUInt32(s_uintMaxValue, s_uintMinValue);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_uintMaxValue);
            }
        }
    }
}
