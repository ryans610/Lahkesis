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
#else
        /// <inheritdoc
        ///     cref="NextInt32(Random, int, int)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// A 32-bit unsigned integer greater than or equal to
        /// <paramref name="minValue"/> and less than <paramref name="maxValue"/>;
        /// that is, the range of return values includes
        /// <paramref name="minValue"/> but not <paramref name="maxValue"/>.
        /// If <paramref name="minValue"/> equals <paramref name="maxValue"/>,
        /// <paramref name="minValue"/> is returned.
        /// </returns>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static uint NextUInt32(
            this Random random,
            uint minValue,
            uint maxValue)
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
            return GenerateUInt32WithRangeInternal(random, maxValue, minValue);
        }

#if ZH_HANT
        /// <inheritdoc
        ///     cref="NextInt32(Random, int)"
        ///     path="/*[not(self::returns)][not(@cref='T:System.ArgumentOutOfRangeException')]"/>
#else
        /// <inheritdoc
        ///     cref="NextInt32(Random, int)"
        ///     path="/*[not(self::returns)][not(@cref='T:System.ArgumentOutOfRangeException')]"/>
        /// <returns>
        /// A 32-bit unsigned integer that is greater than or equal to 0,
        /// and less than <paramref name="maxValue"/>;
        /// that is, the range of return values ordinarily includes 0
        /// but not <paramref name="maxValue"/>.
        /// However, if <paramref name="maxValue"/> equals 0,
        /// <paramref name="maxValue"/> is returned.
        /// </returns>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static uint NextUInt32(
            this Random random,
            uint maxValue)
        {
            Error.ThrowIfArgumentNull(random, nameof(random));
            if (maxValue == 0U)
            {
                return 0U;
            }
            return GenerateUInt32WithRangeInternal(random, maxValue);
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
        /// A 32-bit unsigned integer that is greater than or equal to 0
        /// and less than <see cref="uint.MaxValue"/>.
        /// </returns>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static uint NextUInt32(
            this Random random)
        {
            Error.ThrowIfArgumentNull(random, nameof(random));
            return GenerateUInt32Internal(random);
        }

        internal static uint GenerateUInt32Internal(
            Random random)
        {
            var buffer = new byte[4];
            uint result;
            do
            {
                random.NextBytes(buffer);
                result = BitConverter.ToUInt32(buffer, 0);
            } while (result == uint.MaxValue);
            return result;
        }

        internal static uint GenerateUInt32WithRangeInternal(
            Random random,
            uint maxValue,
            uint minValue = uint.MinValue)
        {
            uint range = maxValue - minValue;
            uint noModuloBias = uint.MaxValue - uint.MaxValue % range;
            uint result;
            do
            {
                result = GenerateUInt32Internal(random);
            } while (result >= noModuloBias);
            return result % range + minValue;
        }
    }
}
