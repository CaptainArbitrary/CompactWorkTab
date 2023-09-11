using System.ComponentModel;
using CompactWorkTab.Mods;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace CompactWorkTab.Patches
{
    [HotSwappable]
    [HarmonyPatch(typeof(PawnColumnWorker_WorkPriority), nameof(PawnColumnWorker_WorkPriority.DoHeader))]
    public class PawnColumnWorker_WorkPriority_DoHeader
    {
        private static bool Prefix(PawnColumnWorker_WorkPriority __instance, Rect rect, PawnTable table)
        {
            if (table.def != PawnTableDefOf.Work) return true;

            rect.y -= ExternalModManager.RectYOffset;

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
                        sortingTexRect = new Rect(rect.xMax - tex.width - 1f, rect.yMax - tex.height - 1f, tex.width, tex.height);
                        break;
                    case HeaderOrientation.Horizontal:
                        return true;
                    default:
                        throw new InvalidEnumArgumentException(nameof(ModSettings.HeaderOrientation), (int)ModSettings.HeaderOrientation, typeof(HeaderOrientation));
                }

                GUI.DrawTexture(sortingTexRect, tex);
            }

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

            string label = __instance.def.workType.labelShort.CapitalizeFirst();
            Rect transformedRect;
            Matrix4x4 transformationMatrix;
            (transformedRect, transformationMatrix) = drawLabelDelegate(rect, label);

            Matrix4x4 originalMatrix = GUI.matrix;
            GUI.matrix = transformationMatrix;

            bool mouseIsOver = transformedRect.Contains(Event.current.mousePosition);

            if (Widgets.ButtonInvisible(transformedRect)) __instance.HeaderClicked(rect, table);

            if (mouseIsOver && ModSettings.HeaderOrientation == HeaderOrientation.Inclined) Widgets.DrawHighlight(transformedRect);

            GUI.matrix = originalMatrix;

            if (mouseIsOver && ModSettings.HeaderOrientation == HeaderOrientation.Vertical) Widgets.DrawHighlight(rect);

            MouseoverSounds.DoRegion(rect);

            if (mouseIsOver)
            {
                string headerTip = __instance.GetHeaderTip(table);
                TooltipHandler.TipRegion(new Rect(0f, 0f, UI.screenWidth, UI.screenHeight), headerTip);
            }

            return false;
        }
    }
}
