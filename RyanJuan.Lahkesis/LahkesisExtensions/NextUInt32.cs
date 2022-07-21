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
        [NonNegativeValue]
        public static uint NextUInt32(
            this Random random,
            uint minValue,
            uint maxValue)
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
            return GenerateUInt32WithRangeInternal(random, maxValue, minValue);
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random"/> is null.
        /// </exception>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static uint NextUInt32(
            this Random random,
            uint maxValue)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
            if (maxValue == 0U)
            {
                return 0U;
            }
            return GenerateUInt32WithRangeInternal(random, maxValue);
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
        public static uint NextUInt32(
            this Random random)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
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
