using System;
using System.Security.Cryptography;

// ReSharper disable RedundantDefaultMemberInitializer

namespace RyanJuan.Lahkesis;

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
    private static readonly RandomNumberGenerator s_defaultRandomNumberGenerator =
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        new RandomNumberGeneratorStaticAdapter();
#else
        new RNGCryptoServiceProvider();
#endif

    private static readonly Random s_defaultRandomProvider =
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        new StaticExtendedRNGRandom();
#else
        new RNGRandomImplement(s_defaultRandomNumberGenerator);
#endif

#if ZH_HANT
    /// <summary>
    /// 取得預設 <see cref="RNGRandom"/> 執行個體的參考。
    /// </summary>
#else
    /// <summary>
    /// Gets a reference to the default <see cref="RNGRandom"/> instance.
    /// </summary>
#endif
    public static RNGRandom Default { get; } = new();

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
        : this(s_defaultRandomProvider)
    { }

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
        : this(new RNGRandomImplement(randomNumberGenerator))
    { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="randomProvider"></param>
    protected RNGRandom(Random randomProvider)
    {
        Error.ThrowIfArgumentNull(randomProvider, nameof(randomProvider));
        _randomProvider = randomProvider;
    }

    private readonly Random _randomProvider;
    private volatile bool _disposed = false;

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
        _randomProvider.NextBytes(buffer);
    }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    /// <inheritdoc/>
    public override void NextBytes(Span<byte> buffer)
    {
        _randomProvider.NextBytes(buffer);
    }
#endif

    /// <inheritdoc/>
    public override double NextDouble()
    {
        return _randomProvider.NextDouble();
    }

#if NET6_0_OR_GREATER
    /// <inheritdoc/>
    public override float NextSingle()
    {
        return _randomProvider.NextSingle();
    }
#endif

    /// <inheritdoc/>
    public override int Next(int minValue, int maxValue)
    {
        return _randomProvider.Next(minValue, maxValue);
    }

    /// <inheritdoc/>
    public override int Next(int maxValue)
    {
        return _randomProvider.Next(maxValue);
    }

    /// <inheritdoc/>
    public override int Next()
    {
        return _randomProvider.Next();
    }

#if NET6_0_OR_GREATER
    /// <inheritdoc/>
    public override long NextInt64(long minValue, long maxValue)
    {
        return _randomProvider.NextInt64(minValue, maxValue);
    }

    /// <inheritdoc/>
    public override long NextInt64(long maxValue)
    {
        return _randomProvider.NextInt64(maxValue);
    }

    /// <inheritdoc/>
    public override long NextInt64()
    {
        return _randomProvider.NextInt64();
    }
#endif

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
        _disposed = true;
        if (!ReferenceEquals(_randomProvider, s_defaultRandomProvider))
        {
            //do not dispose default random provider
            (_randomProvider as IDisposable)?.Dispose();
        }
    }

    /// <inheritdoc/>
    protected override double Sample()
    {
        return _randomProvider.NextDouble();
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

    /// <summary>
    /// 
    /// </summary>
    // ReSharper disable once InconsistentNaming
    protected class RNGRandomImplement : Random, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="randomNumberGenerator"></param>
        public RNGRandomImplement(RandomNumberGenerator randomNumberGenerator)
        {
            Error.ThrowIfArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));
            _randomNumberGenerator = randomNumberGenerator;
        }

        private readonly RandomNumberGenerator _randomNumberGenerator;
        private volatile bool _disposed = false;

        /// <inheritdoc/>
        public void Dispose()
        {
            if (ReferenceEquals(this, s_defaultRandomProvider))
            {
                // do not dispose default random provider
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

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        /// <inheritdoc/>
        public override void NextBytes(Span<byte> buffer)
        {
            ThrowIfObjectDisposed();
            if (buffer.Length == 0)
            {
                return;
            }
            FillByteSpan(buffer);
        }
#endif

        /// <inheritdoc/>
        public override double NextDouble()
        {
            ThrowIfObjectDisposed();
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            var buffer = LahkesisExtensions.BufferPool.Rent(8);
#else
            var buffer = new byte[8];
#endif
            long int64;
            try
            {
                do
                {
                    FillByteArray(buffer);
                    int64 = BitConverter.ToInt64(buffer, 0);
                } while (int64 == long.MaxValue);
            }
            finally
            {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                LahkesisExtensions.BufferPool.Return(buffer);
#endif
            }
            return (double)(int64 & long.MaxValue) / long.MaxValue;
        }

#if NET6_0_OR_GREATER
        /// <inheritdoc/>
        public override float NextSingle()
        {
            return (float)NextDouble();
        }
#endif

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
        public override int Next(int maxValue)
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
            _disposed = true;
            if (!ReferenceEquals(_randomNumberGenerator, s_defaultRandomNumberGenerator))
            {
                //do not dispose default random number generator
                _randomNumberGenerator?.Dispose();
            }
        }

        /// <inheritdoc/>
        protected override double Sample()
        {
            ThrowIfObjectDisposed();
            return NextDouble();
        }

        private void FillByteArray(byte[] buffer)
        {
            _randomNumberGenerator.GetBytes(buffer);
        }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        private void FillByteSpan(Span<byte> buffer)
        {
            _randomNumberGenerator.GetBytes(buffer);
        }
#endif

        private int GenerateInt32()
        {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            var buffer = LahkesisExtensions.BufferPool.Rent(4);
#else
            var buffer = new byte[4];
#endif
            int result;
            try
            {
                do
                {
                    FillByteArray(buffer);
                    result = BitConverter.ToInt32(buffer, 0);
                } while (result is int.MinValue or int.MaxValue);
            }
            finally
            {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                LahkesisExtensions.BufferPool.Return(buffer);
#endif
            }
            return result < 0 ?
                result + int.MaxValue :
                result;
        }

        private int GenerateInt32WithMaxValue(int maxValue)
        {
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
        ~RNGRandomImplement()
        {
            Dispose(false);
        }
    }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    // ReSharper disable once InconsistentNaming
    private class StaticExtendedRNGRandom : RNGRandomImplement
    {
        public StaticExtendedRNGRandom()
            : base(s_defaultRandomNumberGenerator is RandomNumberGeneratorStaticAdapter
                ? s_defaultRandomNumberGenerator
                : new RandomNumberGeneratorStaticAdapter())
        { }

        public override int Next()
        {
            return RandomNumberGenerator.GetInt32(int.MaxValue);
        }

        public override int Next(int maxValue)
        {
            return RandomNumberGenerator.GetInt32(maxValue);
        }

        public override int Next(int minValue, int maxValue)
        {
            return RandomNumberGenerator.GetInt32(minValue, maxValue);
        }

        public override void NextBytes(byte[] buffer)
        {
            RandomNumberGenerator.Fill(buffer);
        }

        public override void NextBytes(Span<byte> buffer)
        {
            RandomNumberGenerator.Fill(buffer);
        }
    }
#endif
}
