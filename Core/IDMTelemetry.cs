using System.Collections.Generic;
using ImbueDurationManager.Configuration;
using UnityEngine;

namespace ImbueDurationManager.Core
{
    internal static class IDMTelemetry
    {
        private const float CorrectionLogIntervalSeconds = 0.8f;

        private static readonly Dictionary<int, float> correctionLogGate = new Dictionary<int, float>();

        private static float nextSummaryTime;
        private static int cycles;
        private static int itemsScanned;
        private static int imbuesScanned;
        private static int adjustedUp;
        private static int adjustedDown;
        private static int unchanged;

        public static void Initialize()
        {
            correctionLogGate.Clear();
            nextSummaryTime = 0f;
            ResetTrackingCounters();
        }

        public static void Shutdown()
        {
            correctionLogGate.Clear();
            nextSummaryTime = 0f;
            ResetTrackingCounters();
        }

        public static void ResetTrackingCounters()
        {
            cycles = 0;
            itemsScanned = 0;
            imbuesScanned = 0;
            adjustedUp = 0;
            adjustedDown = 0;
            unchanged = 0;
        }

        public static void RecordCycle(int cycleItems, int cycleImbues, int cycleAdjustedUp, int cycleAdjustedDown, int cycleUnchanged, int trackedCount, bool nativeInfinite)
        {
            cycles++;
            itemsScanned += cycleItems;
            imbuesScanned += cycleImbues;
            adjustedUp += cycleAdjustedUp;
            adjustedDown += cycleAdjustedDown;
            unchanged += cycleUnchanged;

            if (IDMLog.VerboseEnabled)
            {
                IDMLog.Info(
                    "cycle scannedItems=" + cycleItems +
                    " scannedImbues=" + cycleImbues +
                    " adjustedUp=" + cycleAdjustedUp +
                    " adjustedDown=" + cycleAdjustedDown +
                    " unchanged=" + cycleUnchanged +
                    " tracked=" + trackedCount +
                    " nativeInfinite=" + nativeInfinite,
                    verboseOnly: true);
            }
        }

        public static void Update(float now)
        {
            float interval = Mathf.Clamp(IDMModOptions.SummaryIntervalSeconds, 1f, 60f);
            if (now < nextSummaryTime)
            {
                return;
            }
            nextSummaryTime = now + interval;

            if (!IDMLog.DiagnosticsEnabled)
            {
                return;
            }

            if (cycles == 0)
            {
                return;
            }

            IDMLog.Info(
                "summary interval=" + interval.ToString("0.0") + "s" +
                " cycles=" + cycles +
                " scannedItems=" + itemsScanned +
                " scannedImbues=" + imbuesScanned +
                " adjustedUp=" + adjustedUp +
                " adjustedDown=" + adjustedDown +
                " unchanged=" + unchanged);

            ResetTrackingCounters();
        }

        public static void DumpState(string reason, int trackedCount, bool nativeInfinite, string sourceOfTruthSummary)
        {
            IDMLog.Info(
                "dump reason=" + reason +
                " tracked=" + trackedCount +
                " nativeInfinite=" + nativeInfinite +
                " sourceOfTruth={" + sourceOfTruthSummary + "}" +
                " counters={cycles=" + cycles + ",items=" + itemsScanned + ",imbues=" + imbuesScanned + ",up=" + adjustedUp + ",down=" + adjustedDown + ",same=" + unchanged + "}",
                true);
        }

        public static bool ShouldLogCorrection(int imbueId, float now)
        {
            if (correctionLogGate.TryGetValue(imbueId, out float nextAllowed) && now < nextAllowed)
            {
                return false;
            }

            correctionLogGate[imbueId] = now + CorrectionLogIntervalSeconds;
            return true;
        }
    }
}
