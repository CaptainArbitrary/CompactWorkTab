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
            switch (ModSettings.HeaderOrientation)
            {
                case HeaderOrientation.Horizontal:
                    return true;
                case HeaderOrientation.Vertical:
                    return true;
                case HeaderOrientation.Inclined:
                    __result = Mathf.CeilToInt(Cache.MinHeaderHeight * Mathf.Sqrt(3f) / 2f) / 2;
                    return false;
                default:
                    return true;
            }
        }
    }
}
