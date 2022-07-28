namespace RyanJuan.Lahkesis;

public static partial class LahkesisExtensions
{
#if ZH_HANT
    /// <inheritdoc
    ///     cref="NextDouble(Random,double,double)"
    ///     path="/*[not(self::returns)]"/>
    /// <returns>
    /// 十進位浮點數大於或等於 <paramref name="minValue"/>，
    /// 並且小於 <paramref name="maxValue"/>；
    /// 也就是說，傳回值的範圍包含 <paramref name="minValue"/>
    /// 但不包含 <paramref name="maxValue"/>。
    /// 如果 <paramref name="minValue"/> 等於 <paramref name="maxValue"/>，
    /// 會傳回 <paramref name="minValue"/>。
    /// </returns>
#else
    /// <inheritdoc
    ///     cref="NextDouble(Random,double,double)"
    ///     path="/*[not(self::returns)]"/>
    /// <returns>
    /// A decimal floating point number that is greater than or equal to
    /// <paramref name="minValue"/> and less than <paramref name="maxValue"/>;
    /// that is, the range of return values includes
    /// <paramref name="minValue"/> but not <paramref name="maxValue"/>.
    /// If <paramref name="minValue"/> equals <paramref name="maxValue"/>,
    /// <paramref name="minValue"/> is returned.
    /// </returns>
#endif
    [PublicAPI]
    public static decimal NextDecimal(
        this Random random,
        decimal minValue,
        decimal maxValue)
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

        decimal result;
        do
        {
            result = (maxValue - minValue) * GenerateDecimalInternal(random) + minValue;
        } while (result == maxValue);

        return result;
    }

#if ZH_HANT
    /// <inheritdoc
    ///     cref="NextSingle(Random)"
    ///     path="/*[not(self::returns)]"/>
    /// <returns>
    /// 十進位浮點數大於或等於 0.0，且小於 1.0。
    /// </returns>
#else
    /// <inheritdoc
    ///     cref="NextSingle(Random)"
    ///     path="/*[not(self::returns)]"/>
    /// <returns>
    /// A decimal floating point number that is greater than or equal to 0.0, and less than 1.0.
    /// </returns>
#endif
    [PublicAPI]
    [NonNegativeValue]
    public static decimal NextDecimal(
        this Random random)
    {
        Error.ThrowIfArgumentNull(random, nameof(random));
        return GenerateDecimalInternal(random);
    }

    internal static decimal GenerateDecimalInternal(
        Random random)
    {
        string decimals = string.Join(
            string.Empty,
            Enumerable.Range(0, 29).Select(_ => random.Next(10)));
        decimal result = decimal.Parse($"0.{decimals}");
        //trim zero
        return result / 1.000000000000000000000000000000000M;
    }
}
