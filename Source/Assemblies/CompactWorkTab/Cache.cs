using CompactWorkTab.Mods;
using RimWorld;
using UnityEngine;
using Verse;

namespace CompactWorkTab
{
    public static class Cache
    {
        public static int MinPriority = Constants.MinPriority;
        public static int MaxPriority = Constants.MaxPriority;
        public static int DefPriority = Constants.DefPriority;

        public static int MinHeaderHeight;

        public static void Recache(PawnTable table)
        {
            MinPriority = ExternalModManager.MinPriority;
            MaxPriority = ExternalModManager.MaxPriority;
            DefPriority = ExternalModManager.DefPriority;

            float minHeaderHeightAsFloat = 0f;
            foreach (PawnColumnDef column in table.Columns)
            {
                if (column.workerClass != typeof(PawnColumnWorker_WorkPriority)) continue;
                string l = column.workType.labelShort.CapitalizeFirst();
                Vector2 s = Text.CalcSize(l);
                s.x += GenUI.GapTiny * 2;
                if (s.x > minHeaderHeightAsFloat)
                {
                    minHeaderHeightAsFloat = s.x;
                }
            }
            MinHeaderHeight = Mathf.CeilToInt(minHeaderHeightAsFloat + GenUI.GapTiny);
        }
    }
}
