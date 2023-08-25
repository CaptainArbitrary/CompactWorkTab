using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;

namespace CompactWorkTab
{
    [HarmonyPatch(typeof(PawnTable), "RecacheIfDirty")]
    public class PawnTable_RecacheIfDirty
    {
        private static readonly FieldInfo DirtyField = AccessTools.Field(typeof(PawnTable), "dirty");
        private static readonly FieldInfo CachedColumnWidthsField = AccessTools.Field(typeof(PawnTable), "cachedColumnWidths");
        private static readonly FieldInfo CachedSizeField = AccessTools.Field(typeof(PawnTable), "cachedSize");

        static PawnTable_RecacheIfDirty()
        {
            if (DirtyField == null) throw new NullReferenceException("PawnTable.dirty not found.");
            if (CachedSizeField == null) throw new NullReferenceException("PawnTable.cachedSize not found.");
            if (CachedColumnWidthsField == null) throw new NullReferenceException("PawnTable.cachedColumnWidths not found.");
        }

        private static void Prefix(PawnTable __instance, ref bool __state, PawnTableDef ___def)
        {
            __state = (bool)DirtyField.GetValue(__instance) && ___def == PawnTableDefOf.Work;
        }

        private static void Postfix(PawnTable __instance, bool __state)
        {
            if (__state) Cache.Recache(__instance);
        }
    }
}
