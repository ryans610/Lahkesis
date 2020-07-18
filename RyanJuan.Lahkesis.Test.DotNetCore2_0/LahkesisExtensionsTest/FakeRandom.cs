using System;
using System.Collections.Generic;
using System.Text;

namespace RyanJuan.Lahkesis.Test.DotNetCore2_0.LahkesisExtensionsTest
{
    public class FakeRandom : Random
    {
        public FakeRandom(
            Func<int> int32Factory = null,
            Func<double> doubleFactory = null,
            Action<byte[]> byteArrayFiller = null)
        {
            _int32Factory = int32Factory ?? (() => base.Next());
            _doubleFactory = doubleFactory ?? (() => base.NextDouble());
            _byteArrayFiller = byteArrayFiller ?? (x => base.NextBytes(x));
        }

        private readonly Func<int> _int32Factory;
        private readonly Func<double> _doubleFactory;
        private readonly Action<byte[]> _byteArrayFiller;

        public override int Next() => _int32Factory();
        public override int Next(int maxValue) => _int32Factory();
        public override int Next(int minValue, int maxValue) => _int32Factory();
        public override double NextDouble() => _doubleFactory();
        protected override double Sample() => _doubleFactory();
        public override void NextBytes(byte[] buffer) => _byteArrayFiller(buffer);
    }
}
