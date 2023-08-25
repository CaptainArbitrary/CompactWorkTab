using System;
using System.Reflection;
using Verse;

namespace CompactWorkTab.Mods
{
    internal static class PriorityMaster
    {
        private const string PackageId = "lauriichen.PriorityMod";
        private const string ModTypeName = "PriorityMod.Core.PriorityMaster";
        private const string SettingsFieldName = "settings";
        private const string GetMaxPriorityMethodName = "GetMaxPriority";
        private const string GetDefPriorityMethodName = "GetDefPriority";

        private static object _modSettings;

        private static object PriorityMasterModSettings
        {
            get
            {
                if (_modSettings != null || !ModsConfig.IsActive(PackageId)) return _modSettings;
                Type modType = GenTypes.GetTypeInAnyAssembly(ModTypeName);
                _modSettings = modType?.GetField(SettingsFieldName).GetValue(LoadedModManager.GetMod(modType));
                return _modSettings;
            }
        }

        public static int? MaxPriority
        {
            get
            {
                MethodInfo method = PriorityMasterModSettings?.GetType().GetMethod(GetMaxPriorityMethodName);
                int? value = (int?)method?.Invoke(PriorityMasterModSettings, null);
                return value;
            }
        }

        public static int? DefaultPriority
        {
            get
            {
                MethodInfo method = PriorityMasterModSettings?.GetType().GetMethod(GetDefPriorityMethodName);
                int? value = (int?)method?.Invoke(PriorityMasterModSettings, null);
                return value;
            }
        }
    }
}
