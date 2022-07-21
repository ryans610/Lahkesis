using System;

using JetBrains.Annotations;

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
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random"/> is null.
        /// </exception>
#endif
        [PublicAPI]
        public static float NextSingle(
            this Random random,
            float minValue,
            float maxValue)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
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
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random"/> is null.
        /// </exception>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static float NextSingle(
            this Random random)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
            return (float)random.NextDouble();
        }
    }
}
