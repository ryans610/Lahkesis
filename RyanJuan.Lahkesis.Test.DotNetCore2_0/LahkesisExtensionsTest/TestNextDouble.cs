using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0.LahkesisExtensionsTest
{
    public partial class TestLahkesisExtensions
    {
        private static readonly double s_doubleMaxValue = 35625.984651666D;
        private static readonly double s_doubleMinValue = -4888.6552D;

        [TestMethod]
        public void TestNextDoubleWithRange()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                double result = RNGRandom.Default.NextDouble(s_doubleMinValue, s_doubleMaxValue);
                Assert.IsTrue(result < s_doubleMaxValue);
                Assert.IsTrue(result >= s_doubleMinValue);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(
                RNGRandom.Default.NextDouble(s_doubleMinValue, s_doubleMinValue),
                s_doubleMinValue);
            try
            {
                RNGRandom.Default.NextDouble(s_doubleMaxValue, s_doubleMinValue);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_doubleMaxValue);
            }
        }
    }
}
