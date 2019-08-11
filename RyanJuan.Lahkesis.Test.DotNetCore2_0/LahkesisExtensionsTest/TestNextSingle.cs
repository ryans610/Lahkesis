using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0.LahkesisExtensionsTest
{
    public partial class TestLahkesisExtensions
    {
        private static readonly float s_floatMaxValue = 0.55333F;
        private static readonly float s_floatMinValue = 0.441F;

        [TestMethod]
        public void TestNextSingle()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                float result = RNGRandom.Default.NextSingle();
                Assert.IsTrue(result < 1.0F);
                Assert.IsTrue(result >= 0.0F);
                //Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void TestNextSingleWithRange()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                float result = RNGRandom.Default.NextSingle(s_floatMinValue, s_floatMaxValue);
                Assert.IsTrue(result < s_floatMaxValue);
                Assert.IsTrue(result >= s_floatMinValue);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(
                RNGRandom.Default.NextSingle(s_floatMinValue, s_floatMinValue),
                s_floatMinValue);
            try
            {
                RNGRandom.Default.NextSingle(s_floatMaxValue, s_floatMinValue);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_floatMaxValue);
            }
        }
    }
}
