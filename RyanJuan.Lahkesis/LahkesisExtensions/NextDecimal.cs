using System;
using System.Collections.Generic;
using System.Linq;

namespace RyanJuan.Lahkesis
{
    public static partial class LahkesisExtensions
    {
#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
#endif
        public static decimal NextDecimal(
            this Random random,
            decimal minValue,
            decimal maxValue)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
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
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
#endif
        public static decimal NextDecimal(
            this Random random)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
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
