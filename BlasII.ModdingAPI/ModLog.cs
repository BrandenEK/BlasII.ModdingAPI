using MelonLoader;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace BlasII.ModdingAPI;

/// <summary>
/// Useful methods for logging data
/// </summary>
public static class ModLog
{
    private static readonly Dictionary<Assembly, string> _modNames = new();
    private const string UNKNOWN_MOD = "Unknown mod";

    /// <summary>
    /// Registers a new mod to be able to log, based on its assembly
    /// </summary>
    internal static void Register(BlasIIMod mod)
    {
        if (mod == null)
            return;

        _modNames.Add(mod.GetType().Assembly, mod.Name);
    }

    internal static void Log(object message, string name, Color color)
    {
        MelonLogger.Msg(color, name, message.ToString());
    }

    internal static void Log(object message, Assembly assembly, Color color)
    {
        string name = _modNames.TryGetValue(assembly, out string modName) ? modName : UNKNOWN_MOD;
        Log(message, name, color);
    }

    internal static void Log(object message, string name, LogLevel level) =>
        Log(message, name, _colorMapping[level]);

    internal static void Log(object message, Assembly assembly, LogLevel level) =>
        Log(message, assembly, _colorMapping[level]);

    /// <summary>
    /// Logs an information message
    /// </summary>
    public static void Info(object message) => Log(message, Assembly.GetCallingAssembly(), LogLevel.Info);
    /// <summary>
    /// Logs an information message through the specified mod
    /// </summary>
    public static void Info(object message, BlasIIMod mod) => Log(message, mod.Name, LogLevel.Info);

    /// <summary>
    /// Logs a warning message
    /// </summary>
    public static void Warn(object message) => Log(message, Assembly.GetCallingAssembly(), LogLevel.Warning);
    /// <summary>
    /// Logs a warning message through the specified mod
    /// </summary>
    public static void Warn(object message, BlasIIMod mod) => Log(message, mod.Name, LogLevel.Warning);

    /// <summary>
    /// Logs an error message
    /// </summary>
    public static void Error(object message) => Log(message, Assembly.GetCallingAssembly(), LogLevel.Error);
    /// <summary>
    /// Logs an error message through the specified mod
    /// </summary>
    public static void Error(object message, BlasIIMod mod) => Log(message, mod.Name, LogLevel.Error);

    /// <summary>
    /// Logs a fatal error message
    /// </summary>
    public static void Fatal(object message) => Log(message, Assembly.GetCallingAssembly(), LogLevel.Fatal);
    /// <summary>
    /// Logs a fatal error message through the specified mod
    /// </summary>
    public static void Fatal(object message, BlasIIMod mod) => Log(message, mod.Name, LogLevel.Fatal);

    /// <summary>
    /// Logs a debug message
    /// </summary>
    public static void Debug(object message) => Log(message, Assembly.GetCallingAssembly(), LogLevel.Debug);
    /// <summary>
    /// Logs a debug message through the specified mod
    /// </summary>
    public static void Debug(object message, BlasIIMod mod) => Log(message, mod.Name, LogLevel.Debug);

    /// <summary>
    /// Logs a custom message
    /// </summary>
    public static void Custom(object message, Color color) => Log(message, Assembly.GetCallingAssembly(), color);
    /// <summary>
    /// Logs a custom message through the specified mod
    /// </summary>
    public static void Custom(object message, Color color, BlasIIMod mod) => Log(message, mod.Name, color);

    internal enum LogLevel
    {
        Info,
        Warning,
        Error,
        Fatal,
        Debug,
    }

    private static readonly Dictionary<LogLevel, Color> _colorMapping = new()
    {
        {  LogLevel.Info, Color.White },
        {  LogLevel.Warning, Color.Yellow },
        {  LogLevel.Error, Color.Red },
        {  LogLevel.Fatal, Color.DarkRed },
        {  LogLevel.Debug, Color.Gray },
    };
}