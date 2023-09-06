using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace CompactWorkTab
{
    [HarmonyPatch(typeof(PawnColumnWorker_WorkPriority), nameof(PawnColumnWorker_WorkPriority.DoHeader))]
    public class PawnColumnWorker_WorkPriority_DoHeader
    {
        private static bool Prefix(PawnColumnWorker_WorkPriority __instance, Rect rect, PawnTable table)
        {
            if (!ModSettings.DrawLabelsVertically) return true;

            MouseoverSounds.DoRegion(rect);

            if (table.SortingBy == __instance.def)
            {
                Texture2D tex = table.SortingDescending ? Textures.SortingDescendingIcon : Textures.SortingIcon;
                GUI.DrawTexture(new Rect(rect.xMax - tex.width - 1f, rect.yMax - tex.height - 1f, tex.width, tex.height), tex);
            }

            if (Mouse.IsOver(rect))
            {
                if (!ModSettings.DrawInclinedLabels) Widgets.DrawHighlight(rect);
                string headerTip = __instance.GetHeaderTip(table);
                if (!headerTip.NullOrEmpty()) TooltipHandler.TipRegion(rect, headerTip);
            }

            if (Widgets.ButtonInvisible(rect)) __instance.HeaderClicked(rect, table);

            string label = __instance.def.workType.labelShort.CapitalizeFirst();
            if (ModSettings.DrawInclinedLabels)
                LabelDrawer.DrawInclinedLabel(rect, label);
            else
                LabelDrawer.DrawVerticalLabel(rect, label);
            return false;
        }
    }
}
