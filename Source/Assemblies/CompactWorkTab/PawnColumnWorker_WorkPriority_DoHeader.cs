using System;
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
            Action<Rect, string> drawLabelAction;

            switch (ModSettings.HeaderOrientation)
            {
                case HeaderOrientation.Inclined:
                    drawLabelAction = LabelDrawer.DrawInclinedLabel;
                    break;
                case HeaderOrientation.Vertical:
                    drawLabelAction = LabelDrawer.DrawVerticalLabel;
                    break;
                case HeaderOrientation.Horizontal:
                    return true;
                default:
                    return true;
            }
            if (table.def != PawnTableDefOf.Work) return true;

            MouseoverSounds.DoRegion(rect);

            if (table.SortingBy == __instance.def)
            {
                Texture2D tex = table.SortingDescending ? Textures.SortingDescendingIcon : Textures.SortingIcon;
                GUI.DrawTexture(new Rect(rect.xMax - tex.width - 1f, rect.yMax - tex.height - 1f, tex.width, tex.height), tex);
            }

            if (Mouse.IsOver(rect))
            {
                if (ModSettings.HeaderOrientation != HeaderOrientation.Inclined) Widgets.DrawHighlight(rect);
                string headerTip = __instance.GetHeaderTip(table);
                if (!headerTip.NullOrEmpty()) TooltipHandler.TipRegion(rect, headerTip);
            }

            if (Widgets.ButtonInvisible(rect)) __instance.HeaderClicked(rect, table);

            string label = __instance.def.workType.labelShort.CapitalizeFirst();
            drawLabelAction(rect, label);

            return false;
        }
    }
}
