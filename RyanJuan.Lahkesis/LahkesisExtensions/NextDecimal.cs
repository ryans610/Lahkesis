using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

namespace RyanJuan.Lahkesis
{
    public static partial class LahkesisExtensions
    {
#if ZH_HANT
        /// <inheritdoc
        ///     cref="NextDouble(Random,double,double)"
        ///     path="/*[not(self::returns)]"/>
#else
        /// <inheritdoc
        ///     cref="NextDouble(Random,double,double)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// A decimal floating point number that is greater than or equal to
        /// <paramref name="minValue"/> and less than <paramref name="maxValue"/>;
        /// that is, the range of return values includes
        /// <paramref name="minValue"/> but not <paramref name="maxValue"/>.
        /// If <paramref name="minValue"/> equals <paramref name="maxValue"/>,
        /// <paramref name="minValue"/> is returned.
        /// </returns>
#endif
        [PublicAPI]
        public static decimal NextDecimal(
            this Random random,
            decimal minValue,
            decimal maxValue)
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
            decimal result;
            do
            {
                result = (maxValue - minValue) * GenerateDecimalInternal(random) + minValue;
            } while (result == maxValue);
            return result;
        }

#if ZH_HANT
        /// <inheritdoc
        ///     cref="NextSingle(Random)"
        ///     path="/*[not(self::returns)]"/>
#else
        /// <inheritdoc
        ///     cref="NextSingle(Random)"
        ///     path="/*[not(self::returns)]"/>
        /// <returns>
        /// A decimal floating point number that is greater than or equal to 0.0, and less than 1.0.
        /// </returns>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static decimal NextDecimal(
            this Random random)
        {
            Error.ThrowIfArgumentNull(random, nameof(random));
            return GenerateDecimalInternal(random);
        }

        internal static decimal GenerateDecimalInternal(
            Random random)
        {
            string decimals = string.Join(
                string.Empty,
                Enumerable.Range(0, 29).Select(x => random.Next(10)));
            decimal result = decimal.Parse($"0.{decimals}");
            //trim zero
            return result / 1.000000000000000000000000000000000M;
        }
    }
}
