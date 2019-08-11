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
        /// <param name="size"></param>
        /// <returns></returns>
#endif
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
            var array = new byte[size];
            random.NextBytes(array);
            return array;
        }
    }
}
