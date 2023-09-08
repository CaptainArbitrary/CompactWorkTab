using System;
using System.ComponentModel;
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
            if (table.def != PawnTableDefOf.Work) return true;

            LabelDrawer.LabelDrawerDelegate drawLabelDelegate;

            switch (ModSettings.HeaderOrientation)
            {
                case HeaderOrientation.Inclined:
                    drawLabelDelegate = LabelDrawer.DrawInclinedLabel;
                    break;
                case HeaderOrientation.Vertical:
                    drawLabelDelegate = LabelDrawer.DrawVerticalLabel;
                    break;
                case HeaderOrientation.Horizontal:
                    return true;
                default:
                    throw new InvalidEnumArgumentException(nameof(ModSettings.HeaderOrientation), (int)ModSettings.HeaderOrientation, typeof(HeaderOrientation));
            }

            MouseoverSounds.DoRegion(rect);

            if (table.SortingBy == __instance.def)
            {
                Texture2D tex = table.SortingDescending ? Textures.SortingDescendingIcon : Textures.SortingIcon;
                Rect sortingTexRect;

                switch (ModSettings.HeaderOrientation)
                {
                    case HeaderOrientation.Inclined:
                        sortingTexRect = new Rect(rect.center.x - tex.width / 2f, rect.yMax - tex.height, tex.width, tex.height);
                        break;
                    case HeaderOrientation.Vertical:
                    case HeaderOrientation.Horizontal:
                    default:
                        sortingTexRect = new Rect(rect.xMax - tex.width - 1f, rect.yMax - tex.height - 1f, tex.width, tex.height);
                        break;
                }

                GUI.DrawTexture(sortingTexRect, tex);
            }

            if (Mouse.IsOver(rect))
            {
                if (ModSettings.HeaderOrientation != HeaderOrientation.Inclined) Widgets.DrawHighlight(rect);
                string headerTip = __instance.GetHeaderTip(table);
                if (!headerTip.NullOrEmpty()) TooltipHandler.TipRegion(rect, headerTip);
            }

            if (Widgets.ButtonInvisible(rect)) __instance.HeaderClicked(rect, table);

            string label = __instance.def.workType.labelShort.CapitalizeFirst();
            drawLabelDelegate(rect, label);

            return false;
        }
    }
}
