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

            string label = __instance.def.workType.labelShort.CapitalizeFirst();
            Rect transformedRect;
            Matrix4x4 transformationMatrix;
            (transformedRect, transformationMatrix) = drawLabelDelegate(rect, label);

            Matrix4x4 originalMatrix = GUI.matrix;
            GUI.matrix = transformationMatrix;

            bool mouseIsOver = transformedRect.Contains(Event.current.mousePosition);

            if (mouseIsOver && ModSettings.HeaderOrientation == HeaderOrientation.Inclined) Widgets.DrawHighlight(transformedRect);

            GUI.matrix = originalMatrix;

            if (mouseIsOver && ModSettings.HeaderOrientation == HeaderOrientation.Vertical) Widgets.DrawHighlight(rect);

            return false;
        }
    }
}
