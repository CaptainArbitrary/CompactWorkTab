using HarmonyLib;
using RimWorld;

namespace CompactWorkTab
{
    [HarmonyPatch(typeof(PawnTable), "CalculateHeaderHeight")]
    public class PawnTable_CalculateHeaderHeight
    {
        static bool Prefix(PawnTable __instance, ref float __result)
        {
            if (!ModSettings.DrawLabelsVertically) return true;

            if (Cache.MinHeaderHeight == default) Cache.Recache(__instance);
            __result = Cache.MinHeaderHeight;
            return false;
        }
    }
}
