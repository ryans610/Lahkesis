using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Lahkesis;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0
{
    [TestClass]
    public partial class TestRNGRandom
    {
        private static readonly int s_testRepeatCount = 100000;
        private static readonly string s_parameterNameMaxValue = "maxValue";
        private static readonly string s_parameterNameMinValue = "minValue";
    }
}
