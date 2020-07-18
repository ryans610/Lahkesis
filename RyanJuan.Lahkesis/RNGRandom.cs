using System;
using System.Security.Cryptography;

namespace RyanJuan.Lahkesis
{
#if ZH_HANT
    /// <summary>
    /// 一個執行緒安全、避免模數偏差 (modulo bias) 且安全處理極端狀況，
    /// 基於 <see cref="RNGCryptoServiceProvider"/> 的隨機數產生器。
    /// 繼承 <see cref="Random"/> 並實作處置模式 (Dispose Pattern)。
    /// </summary>
#else
    /// <summary>
    /// A random number generator base on <see cref="RNGCryptoServiceProvider"/>
    /// that is thread-safe, modulo bias avoided and edge cases are safely handled.
    /// Inherit from <see cref="Random"/> and implement dispose pattern.
    /// </summary>
#endif
    public class RNGRandom : Random, IDisposable
    {
        private static readonly RNGCryptoServiceProvider s_defaultRNGProvider =
            new RNGCryptoServiceProvider();

#if ZH_HANT
        /// <summary>
        /// 取得預設 <see cref="RNGRandom"/> 執行個體的參考。
        /// </summary>
#else
        /// <summary>
        /// Gets a reference to the default <see cref="RNGRandom"/> instance.
        /// </summary>
#endif
        public static RNGRandom Default { get; } = new RNGRandom();

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
#endif
        public RNGRandom()
            : this(s_defaultRNGProvider)
        {

        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rngCryptoServiceProvider"></param>
#endif
        public RNGRandom(
            RNGCryptoServiceProvider rngCryptoServiceProvider)
        {
            _rngProvider = rngCryptoServiceProvider;
        }

        private RNGCryptoServiceProvider _rngProvider;

        private bool _disposed = false;

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
#endif
        public void Dispose()
        {
            if (ReferenceEquals(this, Default))
            {
                //do not dispose RNGRandom.Default
                return;
            }
            Dispose(true);
            GC.SuppressFinalize(this);
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
#endif
        public override void NextBytes(
            byte[] buffer)
        {
            ThrowIfObjectDisposed();
            if (buffer is null)
            {
                throw Error.ArgumentNull(nameof(buffer));
            }
            if (buffer.Length == 0)
            {
                return;
            }
            FillByteArray(buffer);
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
#endif
        public override double NextDouble()
        {
            ThrowIfObjectDisposed();
            var buffer = new byte[8];
            long int64;
            do
            {
                FillByteArray(buffer);
                int64 = BitConverter.ToInt64(buffer, 0);
            } while (int64 == long.MaxValue);
            return (double)(int64 & long.MaxValue) / long.MaxValue;
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
#endif
        public override int Next(
            int minValue,
            int maxValue)
        {
            ThrowIfObjectDisposed();
            if (minValue == maxValue)
            {
                return minValue;
            }
            if (minValue > maxValue)
            {
                throw Error.ArgumentOutOfRange(
                    nameof(minValue),
                    minValue,
                    Error.Message.MinValueBiggerThanMaxValue);
            }
            if (minValue >= 0)
            {
                return GenerateInt32WithMaxValue(maxValue - minValue) + minValue;
            }
            uint umin = unchecked((uint)minValue);
            uint result = LahkesisExtensions.GenerateUInt32WithRangeInternal(
                random: this,
                maxValue: unchecked((uint)(maxValue - minValue))) + umin;
            return unchecked((int)result);
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
#endif
        public override int Next(
            int maxValue)
        {
            ThrowIfObjectDisposed();
            if (maxValue == 0)
            {
                return 0;
            }
            if (maxValue < 0)
            {
                throw Error.ArgumentOutOfRange(
                    nameof(maxValue),
                    maxValue,
                    Error.Message.MaxValueSmallerThanZero);
            }
            return GenerateInt32WithMaxValue(maxValue);
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
#endif
        public override int Next()
        {
            ThrowIfObjectDisposed();
            return GenerateInt32();
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
#endif
        protected virtual void Dispose(
            bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (!ReferenceEquals(_rngProvider, s_defaultRNGProvider))
            {
                //do not dispose default rng provider
                _rngProvider?.Dispose();
            }
            _rngProvider = null;
            _disposed = true;
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
#endif
        protected override double Sample()
        {
            ThrowIfObjectDisposed();
            return NextDouble();
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
#endif
        protected virtual void FillByteArray(byte[] buffer)
        {
            ThrowIfObjectDisposed();
            _rngProvider.GetBytes(buffer);
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
#endif
        protected virtual int GenerateInt32()
        {
            ThrowIfObjectDisposed();
            var buffer = new byte[4];
            int result;
            do
            {
                FillByteArray(buffer);
                result = BitConverter.ToInt32(buffer, 0);
            } while (result == int.MinValue || result == int.MaxValue);
            return result < 0 ?
                result + int.MaxValue :
                result;
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
#endif
        protected virtual int GenerateInt32WithMaxValue(
            int maxValue)
        {
            ThrowIfObjectDisposed();
            int noModuloBias = int.MaxValue - int.MaxValue % maxValue;
            int result;
            do
            {
                result = GenerateInt32();
            } while (result >= noModuloBias);
            return result % maxValue;
        }

        private void ThrowIfObjectDisposed()
        {
            if (_disposed)
            {
                Error.ThrowObjectDisposed(this.GetType().FullName);
            }
        }

        /// <summary>
        /// finalizer
        /// </summary>
        ~RNGRandom()
        {
            Dispose(false);
        }
    }
}
