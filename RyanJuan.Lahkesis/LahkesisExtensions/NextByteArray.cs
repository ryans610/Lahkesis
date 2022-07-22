using System;

using JetBrains.Annotations;

namespace RyanJuan.Lahkesis
{
    public static partial class LahkesisExtensions
    {
#if NETFRAMEWORK && !NET46_OR_GREATER
        private static readonly byte[] s_emptyByteArray = new byte[0];
#endif

#if ZH_HANT
#else
        /// <summary>
        /// Returns a byte array with specified length that is fill with random value.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="size">
        /// The length of the array to be generated.
        /// </param>
        /// <returns>
        /// A byte array with the length of <paramref name="size"/>,
        /// which every value is randomly generated.
        /// </returns>
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
            Error.ThrowIfArgumentNull(random, nameof(random));
            if (size < 0)
            {
                throw Error.ArgumentOutOfRange(
                    nameof(size),
                    size,
                    Error.Message.SizeSmallerThanZero);
            }
            if (size == 0)
            {
#if NETFRAMEWORK && !NET46_OR_GREATER
                return s_emptyByteArray;
#else
                return Array.Empty<byte>();
#endif
            }
            var array = new byte[size];
            random.NextBytes(array);
            return array;
        }
    }
}
