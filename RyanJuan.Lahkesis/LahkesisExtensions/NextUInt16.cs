﻿using System;

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
        /// 16 位元不帶正負號的整數大於或等於 <paramref name="minValue"/>，
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
        /// A 16-bit unsigned integer greater than or equal to
        /// <paramref name="minValue"/> and less than <paramref name="maxValue"/>;
        /// that is, the range of return values includes
        /// <paramref name="minValue"/> but not <paramref name="maxValue"/>.
        /// If <paramref name="minValue"/> equals <paramref name="maxValue"/>,
        /// <paramref name="minValue"/> is returned.
        /// </returns>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static ushort NextUInt16(
            this Random random,
            ushort minValue,
            ushort maxValue)
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
            return (ushort)random.Next(minValue, maxValue);
        }

#if ZH_HANT
        /// <inheritdoc
        ///     cref="NextUInt32(Random, uint)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// 16 位元不帶正負號的整數大於或等於 0，並且小於 <paramref name="maxValue"/>；
        /// 也就是說，傳回值的範圍通常包含 0 但不包含 <paramref name="maxValue"/>。
        /// 然而，如果 <paramref name="maxValue"/> 等於 0，
        /// 則會傳回 <paramref name="maxValue"/>。
        /// </returns>
#else
        /// <inheritdoc
        ///     cref="NextUInt32(Random, uint)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// A 16-bit unsigned integer that is greater than or equal to 0,
        /// and less than <paramref name="maxValue"/>;
        /// that is, the range of return values ordinarily includes 0
        /// but not <paramref name="maxValue"/>.
        /// However, if <paramref name="maxValue"/> equals 0,
        /// <paramref name="maxValue"/> is returned.
        /// </returns>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static ushort NextUInt16(
            this Random random,
            ushort maxValue)
        {
            Error.ThrowIfArgumentNull(random, nameof(random));
            if (maxValue == 0)
            {
                return 0;
            }
            return (ushort)random.Next(maxValue);
        }

#if ZH_HANT
        /// <inheritdoc
        ///     cref="NextInt32(Random)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// 大於或等於 0 且小於 <see cref="ushort.MaxValue"/> 的 16 位元不帶正負號整數。
        /// </returns>
#else
        /// <inheritdoc
        ///     cref="NextInt32(Random)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// A 16-bit unsigned integer that is greater than or equal to 0
        /// and less than <see cref="ushort.MaxValue"/>.
        /// </returns>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static ushort NextUInt16(
            this Random random)
        {
            Error.ThrowIfArgumentNull(random, nameof(random));
            return (ushort)random.Next(ushort.MaxValue);
        }
    }
}
