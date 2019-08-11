using System;

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
        public static long NextInt64(
            this Random random,
            long minValue,
            long maxValue)
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
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
#endif
        public static long NextInt64(
            this Random random,
            long maxValue)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
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
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
#endif
        public static long NextInt64(
            this Random random)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
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
