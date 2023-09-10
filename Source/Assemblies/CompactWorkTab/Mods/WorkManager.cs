using Verse;

// Unfortunately WorkManager hard-codes its icon size as a private const int = 16, so we also have to hard-code it. See:
// https://github.com/LordKuper/rimworld-workmanager/blob/9f70c6d8824527f20bd00371b48153c7a5968781/Source/Patches/PawnColumnWorkerWorkPriorityPatch.cs#L21

namespace CompactWorkTab.Mods
{
    internal static class WorkManager
    {
        private const string PackageId = "LordKuper.WorkManager";
        private static bool? _modIsActive;
        private const int HeaderIconSize = 16 + (int)GenUI.GapTiny;

        public static int RectYOffset
        {
            get
            {
                if (_modIsActive == null) _modIsActive = ModsConfig.IsActive(PackageId);
                return _modIsActive.Value ? HeaderIconSize : 0;
            }
        }
    }
}
