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
        public static ushort NextUInt16(
            this Random random,
            ushort minValue,
            ushort maxValue)
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
            return (ushort)random.Next(minValue, maxValue);
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
        public static ushort NextUInt16(
            this Random random,
            ushort maxValue)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
            if (maxValue == 0)
            {
                return 0;
            }
            return (ushort)random.Next(maxValue);
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
        public static ushort NextUInt16(
            this Random random)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
            return (ushort)random.Next(ushort.MaxValue);
        }
    }
}
