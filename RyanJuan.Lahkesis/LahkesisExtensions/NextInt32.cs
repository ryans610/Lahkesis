using System;

using JetBrains.Annotations;

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
        /// <paramref name="maxValue"/> must be greater than or equal to
        /// <paramref name="minValue"/>.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to
        /// <paramref name="minValue"/> and less than <paramref name="maxValue"/>;
        /// that is, the range of return values includes
        /// <paramref name="minValue"/> but not <paramref name="maxValue"/>.
        /// If <paramref name="minValue"/> equals <paramref name="maxValue"/>,
        /// <paramref name="minValue"/> is returned.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="minValue"/> is greater than
        /// <paramref name="maxValue"/>.
        /// </exception>
#endif
        [PublicAPI]
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
        /// Returns a non-negative random integer
        /// that is less than the specified maximum.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number to be generated.
        /// <paramref name="maxValue"/> must be greater than or equal to 0.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer that is greater than or equal to 0,
        /// and less than <paramref name="maxValue"/>;
        /// that is, the range of return values ordinarily includes 0
        /// but not <paramref name="maxValue"/>.
        /// However, if <paramref name="maxValue"/> equals 0,
        /// <paramref name="maxValue"/> is returned.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxValue"/> is less than 0.
        /// </exception>
#endif
        [PublicAPI]
        [NonNegativeValue]
        public static int NextInt32(
            this Random random,
            [ValueRange(0,int.MaxValue-1)]int maxValue)
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
        /// <returns>
        /// A 32-bit signed integer that is greater than or equal to 0
        /// and less than <see cref="Int32.MaxValue"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random"/> is null.
        /// </exception>
#endif
        [PublicAPI]
        [NonNegativeValue]
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
