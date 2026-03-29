namespace FairFlip;

/// <summary>
/// A coin flip strategy. Implement this to register a new flip algorithm.
/// </summary>
public interface IFlipper
{
    /// <summary>Short name used on the CLI (e.g. "csprng", "prng").</summary>
    string Name { get; }

    /// <summary>One-line description of the algorithm.</summary>
    string Description { get; }

    /// <summary>Flip a coin. Returns true for heads, false for tails.</summary>
    bool Flip();
}
