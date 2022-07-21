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
        public static ulong NextUInt64(
            this Random random,
            ulong minValue,
            ulong maxValue)
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
            return GenerateUInt64WithRangeInternal(random, maxValue, minValue);
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
        public static ulong NextUInt64(
            this Random random,
            ulong maxValue)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
            if (maxValue == 0UL)
            {
                return 0UL;
            }
            return GenerateUInt64WithRangeInternal(random, maxValue);
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
        public static ulong NextUInt64(
            this Random random)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
            return GenerateUInt64Internal(random);
        }

        internal static ulong GenerateUInt64Internal(
            Random random)
        {
            var buffer = new byte[8];
            ulong result;
            do
            {
                random.NextBytes(buffer);
                result = BitConverter.ToUInt64(buffer, 0);
            } while (result == ulong.MaxValue);
            return result;
        }

        internal static ulong GenerateUInt64WithRangeInternal(
            Random random,
            ulong maxValue,
            ulong minValue = ulong.MinValue)
        {
            ulong range = maxValue - minValue;
            ulong noModuloBias = ulong.MaxValue - ulong.MaxValue % range;
            ulong result;
            do
            {
                result = GenerateUInt64Internal(random);
            } while (result >= noModuloBias);
            return result % range + minValue;
        }
    }
}
