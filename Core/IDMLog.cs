using ImbueDurationManager.Configuration;
using UnityEngine;

namespace ImbueDurationManager.Core
{
    internal enum IDMLogLevel
    {
        Off = 0,
        Basic = 1,
        Diagnostics = 2,
        Verbose = 3,
    }

    internal static class IDMLog
    {
        private const string Prefix = "[IDM] ";

        public static bool DiagnosticsEnabled => GetCurrentLevel() >= IDMLogLevel.Diagnostics;
        public static bool VerboseEnabled => GetCurrentLevel() >= IDMLogLevel.Verbose;

        public static void Info(string message, bool verboseOnly = false)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            IDMLogLevel level = GetCurrentLevel();
            if (level == IDMLogLevel.Off)
            {
                return;
            }

            if (verboseOnly && level < IDMLogLevel.Verbose)
            {
                return;
            }

            Debug.Log(Prefix + message);
        }

        public static void Warn(string message, bool verboseOnly = false)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            IDMLogLevel level = GetCurrentLevel();
            if (level == IDMLogLevel.Off)
            {
                return;
            }

            if (verboseOnly && level < IDMLogLevel.Verbose)
            {
                return;
            }

            Debug.LogWarning(Prefix + message);
        }

        public static void Error(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            Debug.LogError(Prefix + message);
        }

        private static IDMLogLevel GetCurrentLevel()
        {
            string configured = IDMModOptions.LogLevel;
            if (string.Equals(configured, "Off", System.StringComparison.OrdinalIgnoreCase))
            {
                return IDMLogLevel.Off;
            }
            if (string.Equals(configured, "Verbose", System.StringComparison.OrdinalIgnoreCase))
            {
                return IDMLogLevel.Verbose;
            }
            if (string.Equals(configured, "Diagnostics", System.StringComparison.OrdinalIgnoreCase))
            {
                return IDMLogLevel.Diagnostics;
            }
            return IDMLogLevel.Basic;
        }
    }
}
