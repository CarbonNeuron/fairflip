using System.Security.Cryptography;

namespace FairFlip.Flippers;

/// <summary>
/// Gold standard. One random byte from OS CSPRNG, split at 128.
/// 128/256 values per side = exactly 50/50, zero bias.
/// </summary>
public sealed class CsprngFlipper : IFlipper
{
    public string Name => "csprng";
    public string Description => "OS cryptographic RNG, byte < 128 split (default, zero bias)";

    public bool Flip()
    {
        Span<byte> buf = stackalloc byte[1];
        RandomNumberGenerator.Fill(buf);
        return buf[0] < 128;
    }
}
