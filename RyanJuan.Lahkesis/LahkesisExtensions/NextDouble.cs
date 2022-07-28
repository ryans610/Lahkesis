namespace RyanJuan.Lahkesis;

public static partial class LahkesisExtensions
{
#if ZH_HANT
    /// <summary>
    /// 傳回指定範圍內的隨機浮點數。
    /// </summary>
    /// <param name="random"></param>
    /// <param name="minValue">
    /// 傳回亂數的內含下限 (Inclusive Lower Bound)。
    /// </param>
    /// <param name="maxValue">
    /// 傳回亂數的獨佔上限。
    /// <paramref name="maxValue"/> 必須大於或等於 <paramref name="minValue"/>。
    /// </param>
    /// <returns>
    /// 雙精確度浮點數大於或等於 <paramref name="minValue"/>，
    /// 並且小於 <paramref name="maxValue"/>；
    /// 也就是說，傳回值的範圍包含 <paramref name="minValue"/>
    /// 但不包含 <paramref name="maxValue"/>。
    /// 如果 <paramref name="minValue"/> 等於 <paramref name="maxValue"/>，
    /// 會傳回 <paramref name="minValue"/>。
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="random"/> 的值為 null。
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="minValue"/> 大於 <paramref name="maxValue"/>。
    /// </exception>
#else
    /// <summary>
    /// Returns a random floating-point number that is within a specified range.
    /// </summary>
    /// <param name="random"></param>
    /// <param name="minValue">
    /// The inclusive lower bound of the random number returned.
    /// </param>
    /// <param name="maxValue">
    /// The exclusive upper bound of the random number returned.
    /// <paramref name="maxValue"/> must be greater than or equal to
    /// <paramref name="minValue"/>.
    /// </param>
    /// <returns>
    /// A double-precision floating point number that is greater than or equal to
    /// <paramref name="minValue"/> and less than <paramref name="maxValue"/>;
    /// that is, the range of return values includes
    /// <paramref name="minValue"/> but not <paramref name="maxValue"/>.
    /// If <paramref name="minValue"/> equals <paramref name="maxValue"/>,
    /// <paramref name="minValue"/> is returned.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="random"/> is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="minValue"/> is greater than
    /// <paramref name="maxValue"/>.
    /// </exception>
#endif
    [PublicAPI]
    public static double NextDouble(
        this Random random,
        double minValue,
        double maxValue)
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

        return (maxValue - minValue) * random.NextDouble() + minValue;
    }
}
