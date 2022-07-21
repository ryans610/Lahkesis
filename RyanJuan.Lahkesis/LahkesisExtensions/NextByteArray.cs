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
        /// <param name="size"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="size"/> is less than 0.
        /// </exception>
#endif
        [PublicAPI]
        public static byte[] NextByteArray(
            this Random random,
            int size)
        {
            if (random is null)
            {
                throw Error.ArgumentNull(nameof(random));
            }
            if (size < 0)
            {
                throw Error.ArgumentOutOfRange(
                    nameof(size),
                    size,
                    Error.Message.SizeSmallerThanZero);
            }
            if (size == 0)
            {
                return Array.Empty<byte>();
            }
            var array = new byte[size];
            random.NextBytes(array);
            return array;
        }
    }
}
