using System;

namespace RyanJuan.Lahkesis
{
    public static partial class LahkesisExtensions
    {
#if ZH_HANT
        /// <summary>
        /// 傳回指定範圍內的隨機整數。
        /// </summary>
        /// <param name="random"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
#else
        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="minValue">
        /// The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number returned.
        /// <paramref name="maxValue"/> must be greater than
        /// or equal to <paramref name="minValue"/>.
        /// </param>
        /// <returns></returns>
#endif
        public static int NextInt32(
            this Random random,
            int minValue,
            int maxValue)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
            return random.Next(minValue, maxValue);
        }

#if ZH_HANT
        /// <summary>
        /// 傳回小於指定之最大值的非負值隨機整數。
        /// </summary>
        /// <param name="random"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
#else
        /// <summary>
        /// Returns a non-negative random integer that is
        /// less than the specified maximum.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number
        /// to be generated.
        /// <paramref name="maxValue"/> must be greater than or equal to 0.
        /// </param>
        /// <returns></returns>
#endif
        public static int NextInt32(
            this Random random,
            int maxValue)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
            return random.Next(maxValue);
        }

#if ZH_HANT
        /// <summary>
        /// 傳回非負值的隨機整數。
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
#else
        /// <summary>
        /// Returns a non-negative random integer.
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
#endif
        public static int NextInt32(
            this Random random)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
            return random.Next();
        }
    }
}
