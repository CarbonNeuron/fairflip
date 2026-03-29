using System.Security.Cryptography;

int count = 1;
if (args.Length > 0 && (!int.TryParse(args[0], out count) || count < 1))
{
    Console.Error.WriteLine("Usage: coinflip [count]");
    return 1;
}

// Rejection sampling on a single byte to guarantee exactly 50/50.
// Values 0-127 → Heads, 128-255 → Tails.
// Both ranges have exactly 128 values out of 256 — no bias whatsoever.
// Using RandomNumberGenerator (CSPRNG backed by OS entropy:
// getrandom(2) on Linux, BCryptGenRandom on Windows).

int heads = 0, tails = 0;
Span<byte> buf = stackalloc byte[1];

for (int i = 0; i < count; i++)
{
    RandomNumberGenerator.Fill(buf);
    bool isHeads = buf[0] < 128;

    if (isHeads) heads++;
    else tails++;

    if (count <= 100)
        Console.WriteLine($"  Flip {i + 1}: {(isHeads ? "🪙 Heads" : "🪙 Tails")}");
}

Console.WriteLine();
Console.WriteLine($"Results: {heads} Heads / {tails} Tails ({count} flips)");
if (count > 1)
    Console.WriteLine($"Ratio:   {(double)heads / count:P2} Heads / {(double)tails / count:P2} Tails");

return 0;
