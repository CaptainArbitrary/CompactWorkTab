using HarmonyLib;
using RimWorld;

namespace CompactWorkTab
{
    [HarmonyPatch(typeof(PawnColumnWorker_WorkPriority), nameof(PawnColumnWorker_WorkPriority.GetMinHeaderHeight))]
    public class PawnColumnWorker_WorkPriority_GetMinHeaderHeight
    {
        static void Postfix(ref int __result, PawnTable table)
        {
            if (Cache.MinHeaderHeight == default) Cache.Recache(table);
            __result = Cache.MinHeaderHeight;
        }
    }
}
