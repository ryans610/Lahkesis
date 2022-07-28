namespace RyanJuan.Lahkesis;

public static partial class LahkesisExtensions
{
#if ZH_HANT
    /// <inheritdoc
    ///     cref="NextInt32(Random, int, int)"
    ///     path="/*[not(self::returns)]"/>
    /// <returns>
    /// 16 位元帶正負號的整數大於或等於 <paramref name="minValue"/>，
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
    /// A 16-bit signed integer greater than or equal to
    /// <paramref name="minValue"/> and less than <paramref name="maxValue"/>;
    /// that is, the range of return values includes
    /// <paramref name="minValue"/> but not <paramref name="maxValue"/>.
    /// If <paramref name="minValue"/> equals <paramref name="maxValue"/>,
    /// <paramref name="minValue"/> is returned.
    /// </returns>
#endif
    [PublicAPI]
    public static short NextInt16(
        this Random random,
        short minValue,
        short maxValue)
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

        return (short)random.Next(minValue, maxValue);
    }

#if ZH_HANT
    /// <inheritdoc
    ///     cref="NextInt32(Random, int)"
    ///     path="/*[not(self::returns)]"/>
    /// <returns>
    /// 16 位元帶正負號的整數大於或等於 0，並且小於 <paramref name="maxValue"/>；
    /// 也就是說，傳回值的範圍通常包含 0 但不包含 <paramref name="maxValue"/>。
    /// 然而，如果 <paramref name="maxValue"/> 等於 0，
    /// 則會傳回 <paramref name="maxValue"/>。
    /// </returns>
#else
    /// <inheritdoc
    ///     cref="NextInt32(Random, int)"
    ///     path="/*[not(self::returns)]"/>
    /// <returns>
    /// A 16-bit signed integer that is greater than or equal to 0,
    /// and less than <paramref name="maxValue"/>;
    /// that is, the range of return values ordinarily includes 0
    /// but not <paramref name="maxValue"/>.
    /// However, if <paramref name="maxValue"/> equals 0,
    /// <paramref name="maxValue"/> is returned.
    /// </returns>
#endif
    [PublicAPI]
    [NonNegativeValue]
    public static short NextInt16(
        this Random random,
        [ValueRange(0, short.MaxValue)] short maxValue)
    {
        Error.ThrowIfArgumentNull(random, nameof(random));
        if (maxValue == 0)
        {
            return 0;
        }
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        // Not everyone using ReSharper
        if (maxValue < 0)
        {
            throw Error.ArgumentOutOfRange(
                nameof(maxValue),
                maxValue,
                Error.Message.MaxValueSmallerThanZero);
        }

        return (short)random.Next(maxValue);
    }

#if ZH_HANT
    /// <inheritdoc
    ///     cref="NextInt32(Random)"
    ///     path="/*[not(self::returns)]"/>
    /// <returns>
    /// 大於或等於 0 且小於 <see cref="short.MaxValue"/> 的 16 位元帶正負號整數。
    /// </returns>
#else
    /// <inheritdoc
    ///     cref="NextInt32(Random)"
    ///     path="/*[not(self::returns)]"/>
    /// <returns>
    /// A 16-bit signed integer that is greater than or equal to 0
    /// and less than <see cref="short.MaxValue"/>.
    /// </returns>
#endif
    [PublicAPI]
    [NonNegativeValue]
    public static short NextInt16(
        this Random random)
    {
        Error.ThrowIfArgumentNull(random, nameof(random));
        return (short)random.Next(short.MaxValue);
    }
}
