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
        /// 
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="random"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static TType Next<TType>(
            this Random random)
            where TType : struct
        {
            Error.ThrowIfArgumentNull(nameof(random), random);
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
        /// 
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="random"></param>
        /// <param name="maxValue"></param>
        /// <param name="minValue"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static TType Next<TType>(
            this Random random,
            TType maxValue,
            TType minValue)
            where TType : struct
        {
            Error.ThrowIfArgumentNull(nameof(random), random);
            return Type.GetTypeCode(typeof(TType)) switch
            {
                TypeCode.Byte => (TType)(object)random.NextByte((byte)(object)maxValue, (byte)(object)minValue),
                TypeCode.SByte => (TType)(object)random.NextSByte((sbyte)(object)maxValue, (sbyte)(object)minValue),
                TypeCode.Int16 => (TType)(object)random.NextInt16((short)(object)maxValue, (short)(object)minValue),
                TypeCode.UInt16 => (TType)(object)random.NextUInt16((ushort)(object)maxValue, (ushort)(object)minValue),
                TypeCode.Int32 => (TType)(object)random.NextInt32((int)(object)maxValue, (int)(object)minValue),
                TypeCode.UInt32 => (TType)(object)random.NextUInt32((uint)(object)maxValue, (uint)(object)minValue),
                TypeCode.Int64 => (TType)(object)random.NextInt64((long)(object)maxValue, (long)(object)minValue),
                TypeCode.UInt64 => (TType)(object)random.NextUInt64((ulong)(object)maxValue, (ulong)(object)minValue),
                TypeCode.Single => (TType)(object)random.NextSingle((float)(object)maxValue, (float)(object)minValue),
                TypeCode.Double => (TType)(object)random.NextDouble((double)(object)maxValue, (double)(object)minValue),
                TypeCode.Decimal => (TType)(object)random.NextDecimal((decimal)(object)maxValue, (decimal)(object)minValue),
                TypeCode.String => (TType)(object)random.NextDecimal((decimal)(object)maxValue, (decimal)(object)minValue).ToString(CultureInfo.InvariantCulture),
                _ => throw new InvalidOperationException(Error.Message.NextTInvalidType),
            };
        }
    }
}
