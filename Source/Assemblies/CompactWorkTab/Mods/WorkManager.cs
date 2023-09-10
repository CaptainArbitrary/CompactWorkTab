using Verse;

namespace CompactWorkTab.Mods
{
    internal static class WorkManager
    {
        private const string PackageId = "LordKuper.WorkManager";

        // Unfortunately WorkManager hard-codes its icon size as a private const int = 16, so we also have to hard-code
        // it. See (deep breath) https://github.com/LordKuper/rimworld-workmanager/blob/9f70c6d8824527f20bd00371b48153c7a5968781/Source/Patches/PawnColumnWorkerWorkPriorityPatch.cs#L21
        private const int HeaderIconSize = 16 + (int)GenUI.GapTiny;

        private static readonly bool ModIsActive;

        static WorkManager()
        {
            ModIsActive = ModsConfig.IsActive(PackageId);
        }

        public static int RectYOffset => ModIsActive ? HeaderIconSize : 0;
    }
}
