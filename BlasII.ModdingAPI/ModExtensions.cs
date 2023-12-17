using System;

namespace BlasII.ModdingAPI
{
    internal static class ModExtensions
    {
        public static string CleanStackTrace(this Exception exception)
        {
            var split = exception.StackTrace?.Split(Environment.NewLine);
            return split == null || split.Length < 2 ? exception.StackTrace : string.Join(Environment.NewLine, split[..^2]);
        }
    }
}
