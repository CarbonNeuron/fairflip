using System.Security.Cryptography;

namespace FairFlip.Flippers;

/// <summary>
/// Uses only the least significant bit of a CSPRNG byte.
/// Equivalent to byte &amp; 1. Perfectly fair (bit is uniformly 0 or 1).
/// </summary>
public sealed class BitMaskFlipper : IFlipper
{
    public string Name => "bitmask";
    public string Description => "CSPRNG byte, least significant bit only (& 1)";

    public bool Flip()
    {
        Span<byte> buf = stackalloc byte[1];
        RandomNumberGenerator.Fill(buf);
        return (buf[0] & 1) == 0;
    }
}
