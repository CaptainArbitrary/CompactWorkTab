using HarmonyLib;
using RimWorld;
using Verse;

namespace CompactWorkTab.Patches
{
    [HarmonyPatch(typeof(PawnColumnWorker_WorkPriority), nameof(PawnColumnWorker_WorkPriority.GetMinHeaderHeight))]
    public class PawnColumnWorker_WorkPriority_GetMinHeaderHeight
    {
        public static void Postfix(PawnColumnWorker_WorkPriority __instance, ref int __result,  PawnTable table)
        {
            if (table.def != PawnTableDefOf.Work) return;
            if (Cache.MinHeaderHeight == 0) Cache.Recache(table);
            __result = Cache.MinHeaderHeight;
        }
    }
}
