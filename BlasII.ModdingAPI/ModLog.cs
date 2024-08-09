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

    private static void LogInternal(object message, LogLevel level, Assembly assembly)
    {
        string name = _modNames.TryGetValue(assembly, out string modName) ? modName : UNKNOWN_MOD;
        MelonLogger.Msg(_colorMapping[level], name, message.ToString());
    }

    /// <summary>
    /// Logs an information message
    /// </summary>
    public static void Info(object message) => LogInternal(message, LogLevel.Info, Assembly.GetCallingAssembly());
    /// <summary>
    /// Logs an information message through the specified mod
    /// </summary>
    public static void Info(object message, BlasIIMod mod) => LogInternal(message, LogLevel.Info, mod.GetType().Assembly);

    /// <summary>
    /// Logs a warning message
    /// </summary>
    public static void Warn(object message) => LogInternal(message, LogLevel.Warning, Assembly.GetCallingAssembly());
    /// <summary>
    /// Logs a warning message through the specified mod
    /// </summary>
    public static void Warn(object message, BlasIIMod mod) => LogInternal(message, LogLevel.Warning, mod.GetType().Assembly);

    /// <summary>
    /// Logs an error message
    /// </summary>
    public static void Error(object message) => LogInternal(message, LogLevel.Error, Assembly.GetCallingAssembly());
    /// <summary>
    /// Logs an error message through the specified mod
    /// </summary>
    public static void Error(object message, BlasIIMod mod) => LogInternal(message, LogLevel.Error, mod.GetType().Assembly);

    /// <summary>
    /// Logs a fatal error message
    /// </summary>
    public static void Fatal(object message) => LogInternal(message, LogLevel.Fatal, Assembly.GetCallingAssembly());
    /// <summary>
    /// Logs a fatal error message through the specified mod
    /// </summary>
    public static void Fatal(object message, BlasIIMod mod) => LogInternal(message, LogLevel.Fatal, mod.GetType().Assembly);

    /// <summary>
    /// Logs a debug message
    /// </summary>
    public static void Debug(object message) => LogInternal(message, LogLevel.Debug, Assembly.GetCallingAssembly());
    /// <summary>
    /// Logs a debug message through the specified mod
    /// </summary>
    public static void Debug(object message, BlasIIMod mod) => LogInternal(message, LogLevel.Debug, mod.GetType().Assembly);

    private enum LogLevel
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

    public static void LogSpecial(string modName, string message)
    {
        int length = message.Length;
        var sb = new StringBuilder();
        for (int i = 0; i < length; i++)
            sb.Append('-');
        string line = sb.ToString();

        LogCustom(modName, string.Empty, Color.White);
        LogCustom(modName, line, Color.White);
        LogCustom(modName, message, Color.White);
        LogCustom(modName, line, Color.White);
        LogCustom(modName, string.Empty, Color.White);
    }
}