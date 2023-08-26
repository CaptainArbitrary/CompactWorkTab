using System.Reflection;
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
        private static readonly Texture2D SortingIcon = ContentFinder<Texture2D>.Get("UI/Icons/Sorting");
        private static readonly Texture2D SortingDescendingIcon = ContentFinder<Texture2D>.Get("UI/Icons/SortingDescending");

        static bool Prefix(PawnColumnWorker_WorkPriority __instance, Rect rect, PawnTable table)
        {
            if (!ModSettings.DrawLabelsVertically) return true;

            MouseoverSounds.DoRegion(rect);

            if (table.SortingBy == __instance.def)
            {
                Texture2D tex = table.SortingDescending ? SortingDescendingIcon : SortingIcon;
                GUI.DrawTexture(new Rect(rect.xMax - tex.width - 1f, rect.yMax - tex.height - 1f, tex.width, tex.height), tex);
            }

            if (Mouse.IsOver(rect))
            {
                Widgets.DrawHighlight(rect);
                string headerTip = __instance.GetHeaderTip(table);
                if (!headerTip.NullOrEmpty()) TooltipHandler.TipRegion(rect, headerTip);
            }
            if (Widgets.ButtonInvisible(rect)) __instance.HeaderClicked(rect, table);

            string label = __instance.def.workType.labelShort.CapitalizeFirst();
            DoVerticalLabel(rect, label);
            return false;
        }

        private static void DoVerticalLabel(Rect rect, string label)
        {
            Matrix4x4 matrix = GUI.matrix;
            GUI.matrix = Matrix4x4.identity;

            Vector2 unclippedPosition = GUIClip.Unclip(Vector2.zero);
            Rect topRect = GUIClip.GetTopRect();

            GUI.matrix = matrix;
            GUI.matrix *= Matrix4x4.TRS(unclippedPosition, Quaternion.Euler(0f, 0f, -90), Vector3.one);
            GUI.matrix *= Matrix4x4.TRS(new Vector2(-rect.yMax - unclippedPosition.x, rect.xMin - unclippedPosition.y), Quaternion.identity, Vector3.one);

            float leftClip = Mathf.Min(rect.xMin, 0);
            float rightClip = Mathf.Max(rect.xMax - topRect.width, 0);
            float topClip = Mathf.Min(rect.yMin, 0);
            float bottomClip = Mathf.Max(rect.yMax - topRect.height, 0);

            Rect clipRect = new Rect(bottomClip, -leftClip, rect.height + topClip - bottomClip, rect.width + leftClip - rightClip);
            GUI.BeginClip(clipRect);

            Rect labelRect = new Rect(-bottomClip, leftClip, rect.height, rect.width);

            labelRect.x += GenUI.GapTiny;
            labelRect.width += GenUI.GapTiny;

            Color color = GUI.color;
            TextAnchor anchor = Text.Anchor;
            GameFont font = Text.Font;

            GUI.color = new Color(.8f, .8f, .8f);
            Text.Anchor = TextAnchor.MiddleLeft;
            Text.Font = GameFont.Small;

            Widgets.Label(labelRect, label);

            Text.Font = font;
            GUI.color = color;
            Text.Anchor = anchor;

            GUI.EndClip();

            GUI.matrix = matrix;
        }
    }
}
