using System.Drawing.Drawing2D;
using UnityEngine;
using Verse;

namespace CompactWorkTab
{
    public static class LabelDrawer
    {
        public static void DrawVerticalLabel(Rect rect, string label)
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

        public static void DrawInclinedLabel(Rect rect, string label)
        {
            Vector2 pivotPoint = new Vector2(rect.center.x + rect.width/2 + GenUI.GapTiny, rect.center.y - GenUI.GapTiny);

            Rect pivotRect = new Rect(0f, 0f, 4f, 4f) { center = pivotPoint };

            Matrix4x4 originalMatrix = GUI.matrix;
            GUI.matrix = Matrix4x4.identity;
            GUIUtility.RotateAroundPivot(-60f, pivotPoint);
            GUI.matrix = originalMatrix * GUI.matrix;

            Rect labelRect = new Rect(0f, 0f, rect.height, rect.width) { center = pivotRect.center };

            Color color = GUI.color;
            TextAnchor anchor = Text.Anchor;
            GameFont font = Text.Font;
            bool wordWrap = Text.WordWrap;

            GUI.color = new Color(.8f, .8f, .8f);
            Text.Anchor = TextAnchor.LowerLeft;
            Text.Font = GameFont.Small;
            Text.WordWrap = false;

            Widgets.Label(labelRect, label);

            Text.WordWrap = wordWrap;
            Text.Font = font;
            GUI.color = color;
            Text.Anchor = anchor;

            GUI.matrix = originalMatrix;
        }
    }
}
