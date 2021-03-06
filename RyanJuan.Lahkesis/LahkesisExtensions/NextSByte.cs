﻿using System;

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
        public static sbyte NextSByte(
            this Random random,
            sbyte minValue,
            sbyte maxValue)
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
            return (sbyte)random.Next(minValue, maxValue);
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
        public static sbyte NextSByte(
            this Random random,
            sbyte maxValue)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
            if (maxValue == 0)
            {
                return 0;
            }
            if (maxValue < 0)
            {
                throw Error.ArgumentOutOfRange(
                    nameof(maxValue),
                    maxValue,
                    Error.Message.MaxValueSmallerThanZero);
            }
            return (sbyte)random.Next(maxValue);
        }

#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
#endif
        public static sbyte NextSByte(
            this Random random)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
            return (sbyte)random.Next(sbyte.MaxValue);
        }
    }
}
