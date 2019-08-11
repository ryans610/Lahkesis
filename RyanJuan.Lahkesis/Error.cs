using System;

namespace RyanJuan.Lahkesis
{
    internal static class Error
    {
        public static class Message
        {
            public static string MinValueBiggerThanMaxValue { get; } =
#if ZH_HANT
                    "minValue 必須小於或等於 maxValue。";
#else
                    "minValue must be smaller than or equal to maxValue.";
#endif

            public static string MaxValueSmallerThanZero { get; } =
#if ZH_HANT
                    "maxValue 必須大於 0。";
#else
                    "maxValue must be greater than 0.";
#endif

            public static string SizeSmallerThanZero { get; } =
#if ZH_HANT
                    "size 必須大於 0。";
#else
                    "size must be greater than 0.";
#endif
        }

        public static ArgumentNullException ArgumentNull(
            string name)
        {
            return new ArgumentNullException(name);
        }

        public static ArgumentOutOfRangeException ArgumentOutOfRange(
            string name,
            object value,
            string message)
        {
            return new ArgumentOutOfRangeException(name, value, message);
        }
    }
}
