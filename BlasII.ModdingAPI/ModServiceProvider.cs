
namespace BlasII.ModdingAPI;

/// <summary>
/// Allows a mod to register custom services
/// </summary>
public class ModServiceProvider
{
    internal ModServiceProvider(BlasIIMod mod)
    {
        RegisteringMod = mod;
    }

    /// <summary>
    /// The mod that is registering this service
    /// </summary>
    public BlasIIMod RegisteringMod { get; }
}
