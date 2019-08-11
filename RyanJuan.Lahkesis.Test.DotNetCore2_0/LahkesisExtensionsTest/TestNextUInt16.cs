using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0.LahkesisExtensionsTest
{
    public partial class TestLahkesisExtensions
    {
        private static readonly ushort s_ushortMaxValue = 30005;
        private static readonly ushort s_ushortMinValue = 15550;

        [TestMethod]
        public void TestNextUInt16()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                ushort result = RNGRandom.Default.NextUInt16();
                Assert.IsTrue(result < ushort.MaxValue);
                Assert.IsTrue(result >= 0);
                //Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void TestNextUInt16WithMaxValue()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                ushort result = RNGRandom.Default.NextUInt16(s_ushortMaxValue);
                Assert.IsTrue(result < s_ushortMaxValue);
                Assert.IsTrue(result >= 0);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(RNGRandom.Default.NextUInt16(0), 0);
        }

        [TestMethod]
        public void TestNextUInt16WithRange()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                ushort result = RNGRandom.Default.NextUInt16(s_ushortMinValue, s_ushortMaxValue);
                Assert.IsTrue(result < s_ushortMaxValue);
                Assert.IsTrue(result >= s_ushortMinValue);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(
                RNGRandom.Default.NextUInt16(s_ushortMinValue, s_ushortMinValue),
                s_ushortMinValue);
            try
            {
                RNGRandom.Default.NextUInt16(s_ushortMaxValue, s_ushortMinValue);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_ushortMaxValue);
            }
        }
    }
}
