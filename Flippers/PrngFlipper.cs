namespace FairFlip.Flippers;

/// <summary>
/// System.Random — the naive approach. Uses a PRNG seeded (well, since .NET 6)
/// but still not cryptographic. Included for comparison, not production use.
/// </summary>
public sealed class PrngFlipper : IFlipper
{
    public string Name => "prng";
    public string Description => "System.Random (non-cryptographic, for comparison)";

    private readonly Random _rng = new();

    public bool Flip() => _rng.Next(2) == 0;
}
