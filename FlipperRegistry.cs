using System.Reflection;

namespace FairFlip;

/// <summary>
/// Auto-discovers and registers all IFlipper implementations in the assembly.
/// </summary>
public static class FlipperRegistry
{
    private static readonly Dictionary<string, IFlipper> _flippers = [];

    static FlipperRegistry()
    {
        // Find all concrete IFlipper implementations in this assembly
        var types = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.GetInterfaces().Contains(typeof(IFlipper)));

        foreach (var type in types)
        {
            if (Activator.CreateInstance(type) is IFlipper flipper)
                _flippers[flipper.Name] = flipper;
        }
    }

    public static IFlipper? Get(string name) =>
        _flippers.GetValueOrDefault(name);

    public static IFlipper Default =>
        _flippers["csprng"];

    public static IEnumerable<IFlipper> All =>
        _flippers.Values.OrderBy(f => f.Name);
}
