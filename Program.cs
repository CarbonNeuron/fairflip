using FairFlip;

// Parse args: [--strategy name] [--list] [count]
string? strategyName = null;
bool listMode = false;
int count = 1;

for (int i = 0; i < args.Length; i++)
{
    switch (args[i])
    {
        case "--strategy" or "-s":
            if (i + 1 < args.Length) strategyName = args[++i];
            else { Console.Error.WriteLine("--strategy requires a name"); return 1; }
            break;
        case "--list" or "-l":
            listMode = true;
            break;
        default:
            if (!int.TryParse(args[i], out count) || count < 1)
            {
                Console.Error.WriteLine($"Usage: fairflip [--strategy name] [--list] [count]");
                return 1;
            }
            break;
    }
}

// List available strategies
if (listMode)
{
    Console.WriteLine("Available strategies:\n");
    foreach (var f in FlipperRegistry.All)
        Console.WriteLine($"  {f.Name,-12} {f.Description}");
    return 0;
}

// Resolve strategy
var flipper = strategyName is not null
    ? FlipperRegistry.Get(strategyName)
    : FlipperRegistry.Default;

if (flipper is null)
{
    Console.Error.WriteLine($"Unknown strategy: {strategyName}");
    Console.Error.WriteLine("Use --list to see available strategies.");
    return 1;
}

if (strategyName is not null || count > 1)
    Console.WriteLine($"Strategy: {flipper.Name} — {flipper.Description}\n");

// Flip
int heads = 0, tails = 0;

for (int i = 0; i < count; i++)
{
    bool isHeads = flipper.Flip();
    if (isHeads) heads++; else tails++;

    if (count <= 100)
        Console.WriteLine($"  Flip {i + 1}: {(isHeads ? "🪙 Heads" : "🪙 Tails")}");
}

Console.WriteLine();
Console.WriteLine($"Results: {heads} Heads / {tails} Tails ({count} flips)");
if (count > 1)
    Console.WriteLine($"Ratio:   {(double)heads / count:P2} Heads / {(double)tails / count:P2} Tails");

return 0;
