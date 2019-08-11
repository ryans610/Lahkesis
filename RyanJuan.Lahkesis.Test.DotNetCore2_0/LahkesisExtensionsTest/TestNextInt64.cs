using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0
{
    public partial class TestRNGRandom
    {
        private static readonly long s_longMaxValuePositive = 223344556677889900L;
        private static readonly long s_longMinValuePositive = 1L;
        private static readonly long s_longMaxValueNegative = -123456789123456789L;
        private static readonly long s_longMinValueNegative = -369258147987654321L;

        [TestMethod]
        public void TestNextInt64()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                long result = RNGRandom.Default.NextInt64();
                Assert.IsTrue(result < long.MaxValue);
                Assert.IsTrue(result >= 0L);
                //Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void TestNextInt64WithMaxValue()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                long result = RNGRandom.Default.NextInt64(s_longMaxValuePositive);
                Assert.IsTrue(result < s_longMaxValuePositive);
                Assert.IsTrue(result >= 0L);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(RNGRandom.Default.NextInt64(0L), 0L);
            //negative
            try
            {
                RNGRandom.Default.NextInt64(s_longMaxValueNegative);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMaxValue);
                Assert.AreEqual(aoorException.ActualValue, s_longMaxValueNegative);
            }
        }

        [TestMethod]
        public void TestNextInt64WithRangePositive()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                long result = RNGRandom.Default.NextInt64(s_longMinValuePositive, s_longMaxValuePositive);
                Assert.IsTrue(result < s_longMaxValuePositive);
                Assert.IsTrue(result >= s_longMinValuePositive);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(
                RNGRandom.Default.NextInt64(s_longMinValuePositive, s_longMinValuePositive),
                s_longMinValuePositive);
            try
            {
                RNGRandom.Default.NextInt64(s_longMaxValuePositive, s_longMinValuePositive);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_longMaxValuePositive);
            }
        }

        [TestMethod]
        public void TestNextInt64WithRangeNegative()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                long result = RNGRandom.Default.NextInt64(s_longMinValueNegative, s_longMaxValueNegative);
                Assert.IsTrue(result < s_longMaxValueNegative);
                Assert.IsTrue(result >= s_longMinValueNegative);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(
                RNGRandom.Default.NextInt64(s_longMinValueNegative, s_longMinValueNegative),
                s_longMinValueNegative);
            try
            {
                RNGRandom.Default.NextInt64(s_longMaxValueNegative, s_longMinValueNegative);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_longMaxValueNegative);
            }
        }

        [TestMethod]
        public void TestNextInt64WithRange()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                long result = RNGRandom.Default.NextInt64(s_longMinValueNegative, s_longMaxValuePositive);
                Assert.IsTrue(result < s_longMaxValuePositive);
                Assert.IsTrue(result >= s_longMinValueNegative);
                //Console.WriteLine(result);
            }
            try
            {
                RNGRandom.Default.NextInt64(s_longMaxValuePositive, s_longMinValueNegative);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_longMaxValuePositive);
            }
        }
    }
}
