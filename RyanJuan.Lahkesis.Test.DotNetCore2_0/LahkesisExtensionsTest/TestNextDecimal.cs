using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0.LahkesisExtensionsTest
{
    public partial class TestLahkesisExtensions
    {
        private static readonly decimal s_decimalMaxValue = 10.657484226548M;
        private static readonly decimal s_decimalMinValue = -4.5496851635144M;

        [TestMethod]
        public void TestNextDecimal()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                decimal result = RNGRandom.Default.NextDecimal();
                Assert.IsTrue(result < 1.0M);
                Assert.IsTrue(result >= 0.0M);
                //Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void TestNextDecimalWithRange()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                decimal result = RNGRandom.Default.NextDecimal(s_decimalMinValue, s_decimalMaxValue);
                Assert.IsTrue(result < s_decimalMaxValue);
                Assert.IsTrue(result >= s_decimalMinValue);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(
                RNGRandom.Default.NextDecimal(s_decimalMinValue, s_decimalMinValue),
                s_decimalMinValue);
            try
            {
                RNGRandom.Default.NextDecimal(s_decimalMaxValue, s_decimalMinValue);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_decimalMaxValue);
            }
        }
    }
}
