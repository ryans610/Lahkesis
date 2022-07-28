namespace RyanJuan.Lahkesis;

public static partial class LahkesisExtensions
{
    /// <inheritdoc cref="GetBytes"/>
    [Obsolete(
        "Extension method NextByteArray(int) is deprecated, please use GetBytes(int) instead.",
        false)]
    [PublicAPI]
    public static byte[] NextByteArray(
        this Random random,
        int size)
    {
        return random.GetBytes(size);
    }
}
