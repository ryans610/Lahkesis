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
        /// A 64-bit signed integer greater than or equal to
        /// <paramref name="minValue"/> and less than <paramref name="maxValue"/>;
        /// that is, the range of return values includes
        /// <paramref name="minValue"/> but not <paramref name="maxValue"/>.
        /// If <paramref name="minValue"/> equals <paramref name="maxValue"/>,
        /// <paramref name="minValue"/> is returned.
        /// </returns>
#endif
        [PublicAPI]
        public static long NextInt64(
            this Random random,
            long minValue,
            long maxValue)
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
            if (minValue >= 0L)
            {
                return GenerateInt64WithMaxValueInternal(random, maxValue - minValue) + minValue;
            }
            ulong umin = unchecked((ulong)minValue);
            ulong result = GenerateUInt64WithRangeInternal(
                random,
                maxValue: unchecked((ulong)(maxValue - minValue))) + umin;
            return unchecked((long)result);
        }

#if ZH_HANT
        /// <inheritdoc
        ///     cref="NextInt32(Random, int)"
        ///     path="/*[not(self::returns)]"/>
#else
        /// <inheritdoc
        ///     cref="NextInt32(Random, int)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// A 64-bit signed integer that is greater than or equal to 0,
        /// and less than <paramref name="maxValue"/>;
        /// that is, the range of return values ordinarily includes 0
        /// but not <paramref name="maxValue"/>.
        /// However, if <paramref name="maxValue"/> equals 0,
        /// <paramref name="maxValue"/> is returned.
        /// </returns>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static long NextInt64(
            this Random random,
            long maxValue)
        {
            Error.ThrowIfArgumentNull(random, nameof(random));
            if (maxValue == 0L)
            {
                return 0L;
            }
            if (maxValue < 0L)
            {
                throw Error.ArgumentOutOfRange(
                    nameof(maxValue),
                    maxValue,
                    Error.Message.MaxValueSmallerThanZero);
            }
            return GenerateInt64WithMaxValueInternal(random, maxValue);
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
        /// A 64-bit signed integer that is greater than or equal to 0
        /// and less than <see cref="long.MaxValue"/>.
        /// </returns>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static long NextInt64(
            this Random random)
        {
            Error.ThrowIfArgumentNull(random, nameof(random));
            return GenerateInt64Internal(random);
        }

        internal static long GenerateInt64Internal(
            Random random)
        {
            var buffer = new byte[8];
            long result;
            do
            {
                random.NextBytes(buffer);
                result = BitConverter.ToInt64(buffer, 0);
            } while (result == long.MinValue || result == long.MaxValue);
            return result < 0L ?
                result + long.MaxValue :
                result;
        }

        internal static long GenerateInt64WithMaxValueInternal(
            Random random,
            long maxValue)
        {
            long noModuloBias = long.MaxValue - long.MaxValue % maxValue;
            long result;
            do
            {
                result = GenerateInt64Internal(random);
            } while (result >= noModuloBias);
            return result % maxValue;
        }
    }
}
