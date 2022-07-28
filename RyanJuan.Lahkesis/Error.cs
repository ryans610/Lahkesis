namespace RyanJuan.Lahkesis;

internal static class Error
{
    public static class Message
    {
        public const string MinValueBiggerThanMaxValue =
#if ZH_HANT
            "minValue 必須小於或等於 maxValue。";
#else
            "minValue must be smaller than or equal to maxValue.";
#endif

        public const string MaxValueSmallerThanZero =
#if ZH_HANT
            "maxValue 必須大於 0。";
#else
            "maxValue must be greater than 0.";
#endif

        public const string SizeSmallerThanZero =
#if ZH_HANT
            "size 必須大於 0。";
#else
            "size must be greater than 0.";
#endif

        public const string NotEnumType =
#if ZH_HANT
            "{0} 不是列舉型別。";
#else
            "{0} is not a enum type.";
#endif

        public const string NextTInvalidType =
#if ZH_HANT
            "TType 不是允許的型別。";
#else
            "TType is not a valid type.";
#endif
    }

    public static void ThrowIfArgumentNull<TValue>(
        TValue value,
        string name)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(value, name);
#else
        if (value is null)
        {
            throw new ArgumentNullException(name);
        }
#endif
    }

    public static ArgumentNullException ArgumentNull(string name)
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

    public static void ThrowIfTypeIsNotEnum(
        string name,
        Type type)
    {
        if (!type.IsEnum)
        {
            throw new InvalidOperationException(string.Format(Message.NotEnumType, name));
        }
    }

    public static void ThrowObjectDisposed(string name)
    {
        throw new ObjectDisposedException(name);
    }
}
