using System.Security.Cryptography;

// ReSharper disable ArrangeStaticMemberQualifier

namespace RyanJuan.Lahkesis.Resources;

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
internal class RandomNumberGeneratorStaticAdapter : RandomNumberGenerator
{
    public override void GetBytes(byte[] data)
    {
        RandomNumberGenerator.Fill(data);
    }

    public override void GetBytes(Span<byte> data)
    {
        RandomNumberGenerator.Fill(data);
    }
}
#endif
