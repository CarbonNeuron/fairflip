using System.Security.Cryptography;

namespace FairFlip.Flippers;

/// <summary>
/// Demonstrates modulo bias. Generates a random byte and uses % 2.
/// With 256 values this happens to be unbiased (256 is even), but the pattern
/// is dangerous with odd moduli. Included to show the anti-pattern.
/// </summary>
public sealed class ModuloFlipper : IFlipper
{
    public string Name => "modulo";
    public string Description => "CSPRNG byte % 2 (unbiased here, but dangerous pattern)";

    public bool Flip()
    {
        Span<byte> buf = stackalloc byte[1];
        RandomNumberGenerator.Fill(buf);
        return buf[0] % 2 == 0;
    }
}
