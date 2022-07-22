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
#else
        /// <inheritdoc
        ///     cref="NextInt32(Random, int, int)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// A 8-bit unsigned integer greater than or equal to
        /// <paramref name="minValue"/> and less than <paramref name="maxValue"/>;
        /// that is, the range of return values includes
        /// <paramref name="minValue"/> but not <paramref name="maxValue"/>.
        /// If <paramref name="minValue"/> equals <paramref name="maxValue"/>,
        /// <paramref name="minValue"/> is returned.
        /// </returns>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static byte NextByte(
            this Random random,
            byte minValue,
            byte maxValue)
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
            return (byte)random.Next(minValue, maxValue);
        }

#if ZH_HANT
        /// <inheritdoc
        ///     cref="NextUInt32(Random, uint)"
        ///     path="/*[not(self::returns)]"/>
#else
        /// <inheritdoc
        ///     cref="NextUInt32(Random, uint)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// A 8-bit unsigned integer that is greater than or equal to 0,
        /// and less than <paramref name="maxValue"/>;
        /// that is, the range of return values ordinarily includes 0
        /// but not <paramref name="maxValue"/>.
        /// However, if <paramref name="maxValue"/> equals 0,
        /// <paramref name="maxValue"/> is returned.
        /// </returns>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static byte NextByte(
            this Random random,
            byte maxValue)
        {
            Error.ThrowIfArgumentNull(random, nameof(random));
            if (maxValue == 0)
            {
                return 0;
            }
            return (byte)random.Next(maxValue);
        }

#if ZH_HANT
        /// <inheritdoc
        ///     cref="NextInt32(Random)"
        ///     path="/*[not(self::returns)]"/>
#else
        /// <inheritdoc
        ///     cref="NextInt32(Random)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// A 8-bit unsigned integer that is greater than or equal to 0
        /// and less than <see cref="byte.MaxValue"/>.
        /// </returns>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static byte NextByte(
            this Random random)
        {
            Error.ThrowIfArgumentNull(random, nameof(random));
            return (byte)random.Next(byte.MaxValue);
        }
    }
}
