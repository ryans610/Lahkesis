using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0
{
    public partial class TestRNGRandom
    {
        private static readonly short s_shortMaxValuePositive = 4567;
        private static readonly short s_shortMinValuePositive = 1333;
        private static readonly short s_shortMaxValueNegative = -10000;
        private static readonly short s_shortMinValueNegative = -30155;

        [TestMethod]
        public void TestNextInt16()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                short result = RNGRandom.Default.NextInt16();
                Assert.IsTrue(result < short.MaxValue);
                Assert.IsTrue(result >= 0);
                //Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void TestNextInt16WithMaxValue()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                short result = RNGRandom.Default.NextInt16(s_shortMaxValuePositive);
                Assert.IsTrue(result < s_shortMaxValuePositive);
                Assert.IsTrue(result >= 0);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(RNGRandom.Default.NextInt16(0), 0);
            //negative
            try
            {
                RNGRandom.Default.NextInt16(s_shortMaxValueNegative);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMaxValue);
                Assert.AreEqual(aoorException.ActualValue, s_shortMaxValueNegative);
            }
        }

        [TestMethod]
        public void TestNextInt16WithRangePositive()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                short result = RNGRandom.Default.NextInt16(s_shortMinValuePositive, s_shortMaxValuePositive);
                Assert.IsTrue(result < s_shortMaxValuePositive);
                Assert.IsTrue(result >= s_shortMinValuePositive);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(
                RNGRandom.Default.NextInt16(s_shortMinValuePositive, s_shortMinValuePositive),
                s_shortMinValuePositive);
            try
            {
                RNGRandom.Default.NextInt16(s_shortMaxValuePositive, s_shortMinValuePositive);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_shortMaxValuePositive);
            }
        }

        [TestMethod]
        public void TestNextInt16WithRangeNegative()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                short result = RNGRandom.Default.NextInt16(s_shortMinValueNegative, s_shortMaxValueNegative);
                Assert.IsTrue(result < s_shortMaxValueNegative);
                Assert.IsTrue(result >= s_shortMinValueNegative);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(
                RNGRandom.Default.NextInt16(s_shortMinValueNegative, s_shortMinValueNegative),
                s_shortMinValueNegative);
            try
            {
                RNGRandom.Default.NextInt16(s_shortMaxValueNegative, s_shortMinValueNegative);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_shortMaxValueNegative);
            }
        }

        [TestMethod]
        public void TestNextInt16WithRange()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                short result = RNGRandom.Default.NextInt16(s_shortMinValueNegative, s_shortMaxValuePositive);
                Assert.IsTrue(result < s_shortMaxValuePositive);
                Assert.IsTrue(result >= s_shortMinValueNegative);
                //Console.WriteLine(result);
            }
            try
            {
                RNGRandom.Default.NextInt16(s_shortMaxValuePositive, s_shortMinValueNegative);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_shortMaxValuePositive);
            }
        }
    }
}
