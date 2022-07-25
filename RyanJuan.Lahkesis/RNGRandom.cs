using System;
using System.Security.Cryptography;
// ReSharper disable RedundantDefaultMemberInitializer

namespace RyanJuan.Lahkesis
{
#if ZH_HANT
    /// <summary>
    /// 一個執行緒安全、避免模數偏差 (modulo bias) 且安全處理極端狀況，
    /// 基於 <see cref="RandomNumberGenerator"/> 的隨機數產生器。
    /// 預設使用 <see cref="RNGCryptoServiceProvider"/> 作為
    /// <see cref="RandomNumberGenerator"/> 的實作。
    /// 繼承 <see cref="Random"/> 並實作處置模式 (Dispose Pattern)。
    /// </summary>
#else
    /// <summary>
    /// A random number generator base on <see cref="RandomNumberGenerator"/>
    /// that is thread-safe, avoid modulo bias and safely handle the edge cases.
    /// Using <see cref="RNGCryptoServiceProvider"/> as default implementation of
    /// <see cref="RandomNumberGenerator"/>.
    /// Inherit from <see cref="Random"/> and implement dispose pattern.
    /// </summary>
#endif
    // ReSharper disable once InconsistentNaming
    public class RNGRandom : Random, IDisposable
    {
        // ReSharper disable once InconsistentNaming
        private static readonly RandomNumberGenerator s_defaultRNGProvider =
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
        /// <summary>
        /// 一個執行緒安全、避免模數偏差 (modulo bias) 且安全處理極端狀況，
        /// 基於 <see cref="RandomNumberGenerator"/> 的隨機數產生器。
        /// 使用預設的 <see cref="RandomNumberGenerator"/> 實作。
        /// </summary>
#else
    /// <summary>
    /// A random number generator base on <see cref="RandomNumberGenerator"/>
    /// that is thread-safe, avoid modulo bias and safely handle the edge cases.
    /// Using the default implementation of <see cref="RandomNumberGenerator"/>.
    /// </summary>
#endif
        public RNGRandom()
            : this(s_defaultRNGProvider)
        {

        }

#if ZH_HANT
        /// <summary>
        /// 一個執行緒安全、避免模數偏差 (modulo bias) 且安全處理極端狀況，
        /// 基於 <see cref="RandomNumberGenerator"/> 的隨機數產生器。
        /// </summary>
        /// <param name="randomNumberGenerator">
        /// 要使用的 <see cref="RandomNumberGenerator"/> 實例。
        /// </param>
#else
        /// <summary>
        /// A random number generator base on <see cref="RandomNumberGenerator"/>
        /// that is thread-safe, avoid modulo bias and safely handle the edge cases.
        /// </summary>
        /// <param name="randomNumberGenerator">
        /// The <see cref="RandomNumberGenerator"/> instance to be used.
        /// </param>
#endif
        public RNGRandom(RandomNumberGenerator randomNumberGenerator)
        {
            Error.ThrowIfArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));
            _rngProvider = randomNumberGenerator;
        }

        private RandomNumberGenerator _rngProvider;

        private bool _disposed = false;

        /// <inheritdoc/>
        public void Dispose()
        {
            if (ReferenceEquals(this, Default))
            {
                // do not dispose RNGRandom.Default
                return;
            }
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public override void NextBytes(byte[] buffer)
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public override int Next()
        {
            ThrowIfObjectDisposed();
            return GenerateInt32();
        }

        /// <summary>
        /// The real dispose method of dispose pattern.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
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

        /// <inheritdoc/>
        protected override double Sample()
        {
            ThrowIfObjectDisposed();
            return NextDouble();
        }

        /// <summary>
        /// Use <see cref="RandomNumberGenerator"/> to fill
        /// <paramref name="buffer"/> with randomly generated bytes.
        /// Used as internal mechanism for <see cref="RNGRandom"/>.
        /// </summary>
        /// <param name="buffer">
        /// The byte array to be fill.
        /// </param>
        protected virtual void FillByteArray(byte[] buffer)
        {
            ThrowIfObjectDisposed();
            _rngProvider.GetBytes(buffer);
        }

        /// <summary>
        /// Use <see cref="RandomNumberGenerator"/> to
        /// create a non-negative random integer.
        /// Used as internal mechanism for <see cref="RNGRandom"/>.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is greater than or equal to 0
        /// and less than <see cref="int.MaxValue"/>.
        /// </returns>
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

        /// <summary>
        /// Use <see cref="RandomNumberGenerator"/> to
        /// create a non-negative random integer
        /// that is less than the specified maximum.
        /// Used as internal mechanism for <see cref="RNGRandom"/>.
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns>
        /// A 32-bit signed integer that is greater than or equal to 0,
        /// and less than <paramref name="maxValue"/>.
        /// </returns>
        protected virtual int GenerateInt32WithMaxValue(int maxValue)
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
