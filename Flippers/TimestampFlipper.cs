namespace FairFlip.Flippers;

/// <summary>
/// The worst possible approach: uses current timestamp ticks % 2.
/// Highly biased depending on call frequency and timer resolution.
/// Included as a cautionary tale.
/// </summary>
public sealed class TimestampFlipper : IFlipper
{
    public string Name => "timestamp";
    public string Description => "DateTime.UtcNow.Ticks % 2 (terrible, don't use this)";

    public bool Flip() => DateTime.UtcNow.Ticks % 2 == 0;
}
