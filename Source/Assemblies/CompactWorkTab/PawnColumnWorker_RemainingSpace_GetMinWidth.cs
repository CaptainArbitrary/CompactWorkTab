using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace CompactWorkTab
{
    [HarmonyPatch(typeof(PawnColumnWorker_RemainingSpace), nameof(PawnColumnWorker_RemainingSpace.GetMinWidth))]
    public class PawnColumnWorker_RemainingSpace_GetMinWidth
    {
        private static bool Prefix(PawnTable table, PawnColumnWorker_RemainingSpace __instance, ref int __result)
        {
            if (!ModSettings.DrawInclinedLabels) return true;

            __result = Cache.MinHeaderHeight / 2;

            return false;
        }
    }
}
