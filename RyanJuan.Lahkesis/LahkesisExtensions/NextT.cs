using System;
using System.Globalization;

namespace RyanJuan.Lahkesis
{
    public static partial class LahkesisExtensions
    {
#if ZH_HANT
#else
#endif
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
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <typeparamref name="TType"/> is not numeric type or <see cref="string"/>.
        /// </exception>
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
                TypeCode.String => (TType)(object)random.NextDecimal().ToString(CultureInfo.InvariantCulture),
                _ => throw new InvalidOperationException(Error.Message.NextTInvalidType),
            };
        }

#if ZH_HANT
#else
#endif
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
                TypeCode.String => (TType)(object)random.NextDecimal((decimal)(object)minValue, (decimal)(object)maxValue).ToString(CultureInfo.InvariantCulture),
                _ => throw new InvalidOperationException(Error.Message.NextTInvalidType),
            };
        }
    }
}
