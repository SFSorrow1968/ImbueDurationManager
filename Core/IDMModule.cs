using System;
using ImbueDurationManager.Configuration;
using ThunderRoad;
using UnityEngine;

namespace ImbueDurationManager.Core
{
    public class IDMModule : ThunderScript
    {
        public override void ScriptEnable()
        {
            base.ScriptEnable();

            try
            {
                IDMLog.Info("Imbue Duration Manager v" + IDMModOptions.VERSION + " enabled.", true);
                IDMTelemetry.Initialize();
                IDMModOptionSync.Instance.Initialize();
                IDMManager.Instance.Initialize();
            }
            catch (Exception ex)
            {
                IDMLog.Error("ScriptEnable failed: " + ex.Message);
            }
        }

        public override void ScriptUpdate()
        {
            base.ScriptUpdate();

            try
            {
                IDMModOptionSync.Instance.Update();
                IDMManager.Instance.Update();
                IDMTelemetry.Update(Time.unscaledTime);
            }
            catch (Exception ex)
            {
                IDMLog.Error("ScriptUpdate failed: " + ex.Message);
            }
        }

        public override void ScriptDisable()
        {
            try
            {
                IDMManager.Instance.Shutdown();
                IDMModOptionSync.Instance.Shutdown();
                IDMTelemetry.Shutdown();
                IDMLog.Info("Imbue Duration Manager disabled.", true);
            }
            catch (Exception ex)
            {
                IDMLog.Error("ScriptDisable failed: " + ex.Message);
            }

            base.ScriptDisable();
        }
    }
}
