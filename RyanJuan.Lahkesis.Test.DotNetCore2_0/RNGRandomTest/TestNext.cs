using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0
{
    public partial class TestRNGRandom
    {
        private static readonly int s_intMaxValuePositive = 2444;
        private static readonly int s_intMinValuePositive = 433;
        private static readonly int s_intMaxValueNegative = -1223;
        private static readonly int s_intMinValueNegative = -355541;

        [TestMethod]
        public void TestNext()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                int result = RNGRandom.Default.Next();
                Assert.IsTrue(result < int.MaxValue);
                Assert.IsTrue(result >= 0);
                //Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void TestNextWithMaxValue()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                int result = RNGRandom.Default.Next(s_intMaxValuePositive);
                Assert.IsTrue(result < s_intMaxValuePositive);
                Assert.IsTrue(result >= 0);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(RNGRandom.Default.Next(0), 0);
            //negative
            try
            {
                RNGRandom.Default.Next(s_intMaxValueNegative);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMaxValue);
                Assert.AreEqual(aoorException.ActualValue, s_intMaxValueNegative);
            }
        }

        [TestMethod]
        public void TestNextWithRangePositive()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                int result = RNGRandom.Default.Next(s_intMinValuePositive, s_intMaxValuePositive);
                Assert.IsTrue(result < s_intMaxValuePositive);
                Assert.IsTrue(result >= s_intMinValuePositive);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(
                RNGRandom.Default.Next(s_intMinValuePositive, s_intMinValuePositive),
                s_intMinValuePositive);
            try
            {
                RNGRandom.Default.Next(s_intMaxValuePositive, s_intMinValuePositive);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_intMaxValuePositive);
            }
        }

        [TestMethod]
        public void TestNextWithRangeNegative()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                int result = RNGRandom.Default.Next(s_intMinValueNegative, s_intMaxValueNegative);
                Assert.IsTrue(result < s_intMaxValueNegative);
                Assert.IsTrue(result >= s_intMinValueNegative);
                //Console.WriteLine(result);
            }
            Assert.AreEqual(
                RNGRandom.Default.Next(s_intMinValueNegative, s_intMinValueNegative),
                s_intMinValueNegative);
            try
            {
                RNGRandom.Default.Next(s_intMaxValueNegative, s_intMinValueNegative);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_intMaxValueNegative);
            }
        }

        [TestMethod]
        public void TestNextWithRange()
        {
            for (int i = 0; i < s_testRepeatCount; i++)
            {
                int result = RNGRandom.Default.Next(s_intMinValueNegative, s_intMaxValuePositive);
                Assert.IsTrue(result < s_intMaxValuePositive);
                Assert.IsTrue(result >= s_intMinValueNegative);
                //Console.WriteLine(result);
            }
            try
            {
                RNGRandom.Default.Next(s_intMaxValuePositive, s_intMinValueNegative);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                var aoorException = exception as ArgumentOutOfRangeException;
                Assert.IsNotNull(aoorException);
                Assert.AreEqual(aoorException.ParamName, s_parameterNameMinValue);
                Assert.AreEqual(aoorException.ActualValue, s_intMaxValuePositive);
            }
        }
    }
}
