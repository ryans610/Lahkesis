namespace RyanJuan.Lahkesis;

public static partial class LahkesisExtensions
{
#if ZH_HANT
    /// <inheritdoc
    ///     cref="NextInt32(Random, int, int)"
    ///     path="/*[not(self::returns)]"/>
    /// <returns>
    /// 32 位元不帶正負號的整數大於或等於 <paramref name="minValue"/>，
    /// 並且小於 <paramref name="maxValue"/>；
    /// 也就是說，傳回值的範圍包含 <paramref name="minValue"/>
    /// 但不包含 <paramref name="maxValue"/>。
    /// 如果 <paramref name="minValue"/> 等於 <paramref name="maxValue"/>，
    /// 會傳回 <paramref name="minValue"/>。
    /// </returns>
#else
    /// <inheritdoc
    ///     cref="NextInt32(Random, int, int)"
    ///     path="/*[not(self::returns)]"/>
    /// <returns>
    /// A 32-bit unsigned integer greater than or equal to
    /// <paramref name="minValue"/> and less than <paramref name="maxValue"/>;
    /// that is, the range of return values includes
    /// <paramref name="minValue"/> but not <paramref name="maxValue"/>.
    /// If <paramref name="minValue"/> equals <paramref name="maxValue"/>,
    /// <paramref name="minValue"/> is returned.
    /// </returns>
#endif
    [PublicAPI]
    [NonNegativeValue]
    public static uint NextUInt32(
        this Random random,
        uint minValue,
        uint maxValue)
    {
        Error.ThrowIfArgumentNull(random, nameof(random));
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

        return GenerateUInt32WithRangeInternal(random, maxValue, minValue);
    }

#if ZH_HANT
    /// <inheritdoc
    ///     cref="NextInt32(Random, int)"
    ///     path="/*[not(self::returns)][not(@cref='T:System.ArgumentOutOfRangeException')]"/>
    /// <returns>
    /// 32 位元不帶正負號的整數大於或等於 0，並且小於 <paramref name="maxValue"/>；
    /// 也就是說，傳回值的範圍通常包含 0 但不包含 <paramref name="maxValue"/>。
    /// 然而，如果 <paramref name="maxValue"/> 等於 0，
    /// 則會傳回 <paramref name="maxValue"/>。
    /// </returns>
#else
    /// <inheritdoc
    ///     cref="NextInt32(Random, int)"
    ///     path="/*[not(self::returns)][not(@cref='T:System.ArgumentOutOfRangeException')]"/>
    /// <returns>
    /// A 32-bit unsigned integer that is greater than or equal to 0,
    /// and less than <paramref name="maxValue"/>;
    /// that is, the range of return values ordinarily includes 0
    /// but not <paramref name="maxValue"/>.
    /// However, if <paramref name="maxValue"/> equals 0,
    /// <paramref name="maxValue"/> is returned.
    /// </returns>
#endif
    [PublicAPI]
    [NonNegativeValue]
    public static uint NextUInt32(
        this Random random,
        uint maxValue)
    {
        Error.ThrowIfArgumentNull(random, nameof(random));
        if (maxValue == 0U)
        {
            return 0U;
        }

        return GenerateUInt32WithRangeInternal(random, maxValue);
    }

#if ZH_HANT
    /// <inheritdoc
    ///     cref="NextInt32(Random)"
    ///     path="/*[not(self::returns)]"/>
    /// <returns>
    /// 大於或等於 0 且小於 <see cref="uint.MaxValue"/> 的 32 位元不帶正負號整數。
    /// </returns>
#else
    /// <inheritdoc
    ///     cref="NextInt32(Random)"
    ///     path="/*[not(self::returns)]"/>
    /// <returns>
    /// A 32-bit unsigned integer that is greater than or equal to 0
    /// and less than <see cref="uint.MaxValue"/>.
    /// </returns>
#endif
    [PublicAPI]
    [NonNegativeValue]
    public static uint NextUInt32(
        this Random random)
    {
        Error.ThrowIfArgumentNull(random, nameof(random));
        return GenerateUInt32Internal(random);
    }

    internal static uint GenerateUInt32Internal(Random random)
    {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        var buffer = BufferPool.Rent(4);
#else
        var buffer = new byte[4];
#endif
        uint result;
        try
        {
            do
            {
                random.NextBytes(buffer);
                result = BitConverter.ToUInt32(buffer, 0);
            } while (result == uint.MaxValue);
        }
        finally
        {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            BufferPool.Return(buffer);
#endif
        }

        return result;
    }

    internal static uint GenerateUInt32WithRangeInternal(
        Random random,
        uint maxValue,
        uint minValue = uint.MinValue)
    {
        uint range = maxValue - minValue;
        uint noModuloBias = uint.MaxValue - uint.MaxValue % range;
        uint result;
        do
        {
            result = GenerateUInt32Internal(random);
        } while (result >= noModuloBias);

        return result % range + minValue;
    }
}
