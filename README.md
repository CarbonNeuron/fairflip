# 🪙 fairflip

A cryptographically fair coin flipper. Zero bias, OS-entropy backed, native compiled.

## Why?

`Math.Random() % 2` is not a coin flip. It's a biased approximation seeded by time. This uses your operating system's cryptographic random number generator (`getrandom(2)` on Linux, `BCryptGenRandom` on Windows, `SecRandomCopyBytes` on macOS) to produce results that are **provably exactly 50/50**.

## How it works

1. Request one cryptographically random byte from the OS entropy pool via `System.Security.Cryptography.RandomNumberGenerator`
2. If byte < 128 → **Heads**. If byte ≥ 128 → **Tails**.
3. 128 values map to each outcome out of 256 possible. **Exactly 50/50. No bias.**

That's it. No modulo bias. No PRNG seeds. No time-based entropy. Just raw OS randomness split perfectly in half.

## Usage

```bash
# Single flip
fairflip

# Multiple flips
fairflip 10

# Stress test (suppresses individual results >100)
fairflip 1000000
```

## Output

```
$ fairflip 5
  Flip 1: 🪙 Heads
  Flip 2: 🪙 Tails
  Flip 3: 🪙 Heads
  Flip 4: 🪙 Heads
  Flip 5: 🪙 Tails

Results: 3 Heads / 2 Tails (5 flips)
Ratio:   60.00 % Heads / 40.00 % Tails
```

## Install

Grab a pre-built native binary from [Releases](https://github.com/CarbonNeuron/fairflip/releases) (no .NET runtime required), or build from source:

```bash
dotnet publish -c Release -o out
```

Requires .NET 10 SDK for building. The output is a single native executable — no runtime dependencies.

## Platforms

CI builds native AOT binaries for:
- **Linux** x64
- **Windows** x64
- **macOS** x64
- **macOS** ARM64 (Apple Silicon)

## License

MIT
