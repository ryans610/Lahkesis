using System;

using JetBrains.Annotations;

namespace RyanJuan.Lahkesis
{
    public static partial class LahkesisExtensions
    {
#if ZH_HANT
        /// <inheritdoc
        ///     cref="NextDouble(Random, double, double)"
        ///     path="/*[not(self::returns)]"/>
#else
        /// <inheritdoc
        ///     cref="NextDouble(Random, double, double)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// A single-precision floating point number that is greater than or equal to
        /// <paramref name="minValue"/> and less than <paramref name="maxValue"/>;
        /// that is, the range of return values includes
        /// <paramref name="minValue"/> but not <paramref name="maxValue"/>.
        /// If <paramref name="minValue"/> equals <paramref name="maxValue"/>,
        /// <paramref name="minValue"/> is returned.
        /// </returns>
#endif
        [PublicAPI]
        public static float NextSingle(
            this Random random,
            float minValue,
            float maxValue)
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
            float result;
            do
            {
                result = (float)random.NextDouble(minValue, maxValue);
                // ReSharper disable once CompareOfFloatsByEqualityOperator
            } while (result == maxValue);
            return result;
        }

#if ZH_HANT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
#else
        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
        /// </summary>
        /// <param name="random"></param>
        /// <returns>
        /// A single-precision floating point number that is greater than or equal to 0.0, and less than 1.0.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random"/> is null.
        /// </exception>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static float NextSingle(
            this Random random)
        {
            Error.ThrowIfArgumentNull(random, nameof(random));
            return (float)random.NextDouble();
        }
    }
}
