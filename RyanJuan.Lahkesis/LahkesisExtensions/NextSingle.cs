namespace RyanJuan.Lahkesis;

public static partial class LahkesisExtensions
{
#if ZH_HANT
    /// <inheritdoc
    ///     cref="NextDouble(Random, double, double)"
    ///     path="/*[not(self::returns)]"/>
    /// <returns>
    /// 單精確度浮點數大於或等於 <paramref name="minValue"/>，
    /// 並且小於 <paramref name="maxValue"/>；
    /// 也就是說，傳回值的範圍包含 <paramref name="minValue"/>
    /// 但不包含 <paramref name="maxValue"/>。
    /// 如果 <paramref name="minValue"/> 等於 <paramref name="maxValue"/>，
    /// 會傳回 <paramref name="minValue"/>。
    /// </returns>
#else
    /// <inheritdoc
    ///     cref="NextDouble(Random, double, double)"
    ///     path="/*[not(self::returns)]"/>
    /// <returns>
    /// A single-precision floating point number that is greater than or equal to
    /// <paramref name="minValue"/> and less than <paramref name="maxValue"/>;
    /// that is, the range of return values includes
    /// <paramref name="minValue"/> but not <paramref name="maxValue"/>.
    /// If <paramref name="minValue"/> equals <paramref name="maxValue"/>,
    /// <paramref name="minValue"/> is returned.
    /// </returns>
#endif
    [PublicAPI]
    public static float NextSingle(
        this Random random,
        float minValue,
        float maxValue)
    {
        Error.ThrowIfArgumentNull(random, nameof(random));
        // ReSharper disable once CompareOfFloatsByEqualityOperator
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

        // ReSharper disable once JoinDeclarationAndInitializer
        float result;
#if NET6_0_OR_GREATER
        result = (maxValue - minValue) * random.NextSingle() + minValue;
#else
        do
        {
            result = (float)random.NextDouble(minValue, maxValue);
            // ReSharper disable once CompareOfFloatsByEqualityOperator
        } while (result == maxValue);
#endif
        return result;
    }

#if ZH_HANT
    /// <summary>
    /// 傳回大於或等於 0.0，且小於 1.0 的隨機浮點數。
    /// </summary>
    /// <param name="random"></param>
    /// <returns>
    /// 單精確度浮點數大於或等於 0.0，且小於 1.0。
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="random"/> 的值為 null。
    /// </exception>
#else
    /// <summary>
    /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
    /// </summary>
    /// <param name="random"></param>
    /// <returns>
    /// A single-precision floating point number that is greater than or equal to 0.0, and less than 1.0.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="random"/> is null.
    /// </exception>
#endif
#if NET6_0_OR_GREATER
    [Obsolete("Use native Random.NextSingle() instead.", false)]
#endif
    [PublicAPI]
    [NonNegativeValue]
    public static float NextSingle(
        this Random random)
    {
        Error.ThrowIfArgumentNull(random, nameof(random));
#if NET6_0_OR_GREATER
        return random.NextSingle();
#else
        return (float)random.NextDouble();
#endif
    }
}
