using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0
{
    public partial class TestRNGRandom
    {
        private static readonly sbyte s_sbyteMaxValuePositive = 60;
        private static readonly sbyte s_sbyteMinValuePositive = 25;
        private static readonly sbyte s_sbyteMaxValueNegative = -3;
        private static readonly sbyte s_sbyteMinValueNegative = -100;

        [TestMethod]
        public void TestNextSByte()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                sbyte result = RNGRandom.Default.NextSByte();
                Assert.IsTrue(result < sbyte.MaxValue);
                Assert.IsTrue(result >= 0);
                //Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void TestNextSByteWithMaxValue()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                sbyte result = RNGRandom.Default.NextSByte(s_sbyteMaxValuePositive);
                Assert.IsTrue(result < s_sbyteMaxValuePositive);
                Assert.IsTrue(result >= 0);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(RNGRandom.Default.NextSByte(0), 0);
            //negative
            try
            {
                RNGRandom.Default.NextSByte(s_sbyteMaxValueNegative);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMaxValue);
                Assert.AreEqual(aoorException.ActualValue, s_sbyteMaxValueNegative);
            }
        }

        [TestMethod]
        public void TestNextSByteWithRangePositive()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                sbyte result = RNGRandom.Default.NextSByte(s_sbyteMinValuePositive, s_sbyteMaxValuePositive);
                Assert.IsTrue(result < s_sbyteMaxValuePositive);
                Assert.IsTrue(result >= s_sbyteMinValuePositive);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(
                RNGRandom.Default.NextSByte(s_sbyteMinValuePositive, s_sbyteMinValuePositive),
                s_sbyteMinValuePositive);
            try
            {
                RNGRandom.Default.NextSByte(s_sbyteMaxValuePositive, s_sbyteMinValuePositive);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_sbyteMaxValuePositive);
            }
        }

        [TestMethod]
        public void TestNextSByteWithRangeNegative()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                sbyte result = RNGRandom.Default.NextSByte(s_sbyteMinValueNegative, s_sbyteMaxValueNegative);
                Assert.IsTrue(result < s_sbyteMaxValueNegative);
                Assert.IsTrue(result >= s_sbyteMinValueNegative);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(
                RNGRandom.Default.NextSByte(s_sbyteMinValueNegative, s_sbyteMinValueNegative),
                s_sbyteMinValueNegative);
            try
            {
                RNGRandom.Default.NextSByte(s_sbyteMaxValueNegative, s_sbyteMinValueNegative);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_sbyteMaxValueNegative);
            }
        }

        [TestMethod]
        public void TestNextSByteWithRange()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                sbyte result = RNGRandom.Default.NextSByte(s_sbyteMinValueNegative, s_sbyteMaxValuePositive);
                Assert.IsTrue(result < s_sbyteMaxValuePositive);
                Assert.IsTrue(result >= s_sbyteMinValueNegative);
                //Console.WriteLine(result);
            }
            try
            {
                RNGRandom.Default.NextSByte(s_sbyteMaxValuePositive, s_sbyteMinValueNegative);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_sbyteMaxValuePositive);
            }
        }
    }
}
