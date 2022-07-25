using System;

using JetBrains.Annotations;

namespace RyanJuan.Lahkesis
{
    public static partial class LahkesisExtensions
    {
#if ZH_HANT
        /// <inheritdoc
        ///     cref="NextInt32(Random, int, int)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// 64 位元不帶正負號的整數大於或等於 <paramref name="minValue"/>，
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
        /// A 64-bit unsigned integer greater than or equal to
        /// <paramref name="minValue"/> and less than <paramref name="maxValue"/>;
        /// that is, the range of return values includes
        /// <paramref name="minValue"/> but not <paramref name="maxValue"/>.
        /// If <paramref name="minValue"/> equals <paramref name="maxValue"/>,
        /// <paramref name="minValue"/> is returned.
        /// </returns>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static ulong NextUInt64(
            this Random random,
            ulong minValue,
            ulong maxValue)
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
            return GenerateUInt64WithRangeInternal(random, maxValue, minValue);
        }

#if ZH_HANT
        /// <inheritdoc
        ///     cref="NextUInt32(Random, uint)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// 64 位元不帶正負號的整數大於或等於 0，並且小於 <paramref name="maxValue"/>；
        /// 也就是說，傳回值的範圍通常包含 0 但不包含 <paramref name="maxValue"/>。
        /// 然而，如果 <paramref name="maxValue"/> 等於 0，
        /// 則會傳回 <paramref name="maxValue"/>。
        /// </returns>
#else
        /// <inheritdoc
        ///     cref="NextUInt32(Random, uint)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// A 64-bit unsigned integer that is greater than or equal to 0,
        /// and less than <paramref name="maxValue"/>;
        /// that is, the range of return values ordinarily includes 0
        /// but not <paramref name="maxValue"/>.
        /// However, if <paramref name="maxValue"/> equals 0,
        /// <paramref name="maxValue"/> is returned.
        /// </returns>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static ulong NextUInt64(
            this Random random,
            ulong maxValue)
        {
            Error.ThrowIfArgumentNull(random, nameof(random));
            if (maxValue == 0UL)
            {
                return 0UL;
            }
            return GenerateUInt64WithRangeInternal(random, maxValue);
        }

#if ZH_HANT
        /// <inheritdoc
        ///     cref="NextInt32(Random)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// 大於或等於 0 且小於 <see cref="ulong.MaxValue"/> 的 64 位元不帶正負號整數。
        /// </returns>
#else
        /// <inheritdoc
        ///     cref="NextInt32(Random)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// A 64-bit unsigned integer that is greater than or equal to 0
        /// and less than <see cref="ulong.MaxValue"/>.
        /// </returns>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static ulong NextUInt64(
            this Random random)
        {
            Error.ThrowIfArgumentNull(random, nameof(random));
            return GenerateUInt64Internal(random);
        }

        internal static ulong GenerateUInt64Internal(
            Random random)
        {
            var buffer = new byte[8];
            ulong result;
            do
            {
                random.NextBytes(buffer);
                result = BitConverter.ToUInt64(buffer, 0);
            } while (result == ulong.MaxValue);
            return result;
        }

        internal static ulong GenerateUInt64WithRangeInternal(
            Random random,
            ulong maxValue,
            ulong minValue = ulong.MinValue)
        {
            ulong range = maxValue - minValue;
            ulong noModuloBias = ulong.MaxValue - ulong.MaxValue % range;
            ulong result;
            do
            {
                result = GenerateUInt64Internal(random);
            } while (result >= noModuloBias);
            return result % range + minValue;
        }
    }
}
