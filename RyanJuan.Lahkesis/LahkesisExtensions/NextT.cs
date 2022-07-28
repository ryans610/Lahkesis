using System.Globalization;

namespace RyanJuan.Lahkesis;

public static partial class LahkesisExtensions
{
#if ZH_HANT
    /// <summary>
    /// 傳回隨機數。
    /// 對於整數型別，內含下限為 0，獨佔上限為該型別的最大值。
    /// 對於浮點數型別和 <see cref="string"/>，內含下限為 0.0，獨佔上限為 1.0。
    /// <typeparamref name="TType"/> 必須是數值型別或 <see cref="string"/>。
    /// 如果 <typeparamref name="TType"/> 是 <see cref="string"/> 型別，
    /// 會產生 <see cref="decimal"/> 型別亂數後再轉為字串。
    /// </summary>
    /// <typeparam name="TType">
    /// 產生亂數的型別。
    /// </typeparam>
    /// <param name="random"></param>
    /// <returns>
    /// 型別 <typeparamref name="TType"/> 的值。
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// <typeparamref name="TType"/> 並非數值型別或 <see cref="string"/>。
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="random"/> 的值為 null。
    /// </exception>
#else
    /// <summary>
    /// Returns a random number.
    /// For integer type, the inclusive lower bound is 0,
    /// and the exclusive upper bound is the maxValue of the type.
    /// For floating-point number type,
    /// <see cref="decimal"/> and <see cref="string"/>,
    /// the inclusive lower bound is 0.0, and the exclusive upper bound is 1.0.
    /// <typeparamref name="TType"/> must be numeric type
    /// or <see cref="string"/>.
    /// If <typeparamref name="TType"/> is <see cref="string"/>,
    /// the value will be generate as <see cref="decimal"/>
    /// and than convert to <see cref="string"/>.
    /// </summary>
    /// <typeparam name="TType">
    /// The type of value to generate.
    /// </typeparam>
    /// <param name="random"></param>
    /// <returns>
    /// A value of type <typeparamref name="TType"/>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// <typeparamref name="TType"/> is not numeric type or <see cref="string"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="random"/> is null.
    /// </exception>
#endif
    public static TType Next<TType>(
        this Random random)
        where TType : struct
    {
        Error.ThrowIfArgumentNull(random, nameof(random));
        // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
        return Type.GetTypeCode(typeof(TType)) switch
        {
            TypeCode.Byte => (TType)(object)random.NextByte(),
            TypeCode.SByte => (TType)(object)random.NextSByte(),
            TypeCode.Int16 => (TType)(object)random.NextInt16(),
            TypeCode.UInt16 => (TType)(object)random.NextUInt16(),
            TypeCode.Int32 => (TType)(object)random.NextInt32(),
            TypeCode.UInt32 => (TType)(object)random.NextUInt32(),
            TypeCode.Int64 => (TType)(object)random.NextInt64(),
            TypeCode.UInt64 => (TType)(object)random.NextUInt64(),
            TypeCode.Single => (TType)(object)random.NextSingle(),
            TypeCode.Double => (TType)(object)random.NextDouble(),
            TypeCode.Decimal => (TType)(object)random.NextDecimal(),
            TypeCode.String => (TType)(object)random
                .NextDecimal()
                .ToString(CultureInfo.InvariantCulture),
            _ => throw new InvalidOperationException(Error.Message.NextTInvalidType),
        };
    }

#if ZH_HANT
    /// <summary>
    /// 傳回指定範圍內的隨機數。
    /// <typeparamref name="TType"/> 必須是數值型別或 <see cref="string"/>。
    /// 如果 <typeparamref name="TType"/> 是 <see cref="string"/> 型別，
    /// 會產生 <see cref="decimal"/> 型別亂數後再轉為字串。
    /// </summary>
    /// <typeparam name="TType">
    /// 產生亂數的型別。
    /// </typeparam>
    /// <param name="random"></param>
    /// <param name="minValue">
    /// 傳回亂數的內含下限 (Inclusive Lower Bound)。
    /// </param>
    /// <param name="maxValue">
    /// 傳回亂數的獨佔上限。
    /// <paramref name="maxValue"/> 必須大於或等於 <paramref name="minValue"/>。
    /// </param>
    /// <returns>
    /// 型別 <typeparamref name="TType"/> 的值且大於或等於
    /// <paramref name="minValue"/>，並且小於 <paramref name="maxValue"/>；
    /// 也就是說，傳回值的範圍包含 <paramref name="minValue"/>
    /// 但不包含 <paramref name="maxValue"/>。
    /// 如果 <paramref name="minValue"/> 等於 <paramref name="maxValue"/>，
    /// 會傳回 <paramref name="minValue"/>。
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// <typeparamref name="TType"/> 並非數值型別或 <see cref="string"/>。
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="random"/> 的值為 null。
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="minValue"/> 大於 <paramref name="maxValue"/>。
    /// </exception>
#else
    /// <summary>
    /// Returns a random number that is within a specified range.
    /// <typeparamref name="TType"/> must be numeric type
    /// or <see cref="string"/>.
    /// If <typeparamref name="TType"/> is <see cref="string"/>,
    /// the value will be generate as <see cref="decimal"/>
    /// and than convert to <see cref="string"/>.
    /// </summary>
    /// <typeparam name="TType">
    /// The type of value to generate.
    /// </typeparam>
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
    /// A value of type <typeparamref name="TType"/>
    /// which greater than or equal to <paramref name="minValue"/>
    /// and less than <paramref name="maxValue"/>;
    /// that is, the range of return values includes
    /// <paramref name="minValue"/> but not <paramref name="maxValue"/>.
    /// If <paramref name="minValue"/> equals <paramref name="maxValue"/>,
    /// <paramref name="minValue"/> is returned.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// <typeparamref name="TType"/> is not numeric type or <see cref="string"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="random"/> is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="minValue"/> is greater than
    /// <paramref name="maxValue"/>.
    /// </exception>
#endif
    public static TType Next<TType>(
        this Random random,
        TType minValue,
        TType maxValue)
        where TType : struct
    {
        Error.ThrowIfArgumentNull(random, nameof(random));
        // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
        return Type.GetTypeCode(typeof(TType)) switch
        {
            TypeCode.Byte => (TType)(object)random.NextByte((byte)(object)minValue, (byte)(object)maxValue),
            TypeCode.SByte => (TType)(object)random.NextSByte((sbyte)(object)minValue, (sbyte)(object)maxValue),
            TypeCode.Int16 => (TType)(object)random.NextInt16((short)(object)minValue, (short)(object)maxValue),
            TypeCode.UInt16 => (TType)(object)random.NextUInt16((ushort)(object)minValue, (ushort)(object)maxValue),
            TypeCode.Int32 => (TType)(object)random.NextInt32((int)(object)minValue, (int)(object)maxValue),
            TypeCode.UInt32 => (TType)(object)random.NextUInt32((uint)(object)minValue, (uint)(object)maxValue),
            TypeCode.Int64 => (TType)(object)random.NextInt64((long)(object)minValue, (long)(object)maxValue),
            TypeCode.UInt64 => (TType)(object)random.NextUInt64((ulong)(object)minValue, (ulong)(object)maxValue),
            TypeCode.Single => (TType)(object)random.NextSingle((float)(object)minValue, (float)(object)maxValue),
            TypeCode.Double => (TType)(object)random.NextDouble((double)(object)minValue, (double)(object)maxValue),
            TypeCode.Decimal => (TType)(object)random.NextDecimal((decimal)(object)minValue, (decimal)(object)maxValue),
            TypeCode.String => (TType)(object)random
                .NextDecimal(decimal.Parse((string)(object)minValue), decimal.Parse((string)(object)maxValue))
                .ToString(CultureInfo.InvariantCulture),
            _ => throw new InvalidOperationException(Error.Message.NextTInvalidType),
        };
    }
}
