using UnityEngine;
using Verse;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace CompactWorkTab
{
    [HotSwappable]
    public static class LabelDrawer
    {
        public static void DrawVerticalLabel(Rect rect, string label)
        {
            // Store the current transformation matrix of the GUI to restore it later.
            Matrix4x4 originalMatrix = GUI.matrix;

            // Reset the GUI matrix to the identity matrix.
            GUI.matrix = Matrix4x4.identity;

            // Calculate the unclipped position of the current UI element in screen-space coordinates.
            Vector2 unclippedPosition = GUIClip.Unclip(Vector2.zero);

            // Retrieve the topmost clipping rectangle in local GUI coordinates.
            Rect topRect = GUIClip.GetTopRect();

            // Restore the original matrix for subsequent operations.
            GUI.matrix = originalMatrix;

            // Create a translation matrix to shift the pivot point to 'unclippedPosition'.
            Matrix4x4 translationToPivot = Matrix4x4.TRS(unclippedPosition, Quaternion.identity, Vector3.one);

            // Create a rotation matrix for a 90-degree counter-clockwise rotation.
            Matrix4x4 rotation = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -90f), Vector3.one);

            // Create another translation matrix to shift the pivot back from 'unclippedPosition'.
            Matrix4x4 translationFromPivot = Matrix4x4.TRS(new Vector2(-rect.yMax - unclippedPosition.x, rect.xMin - unclippedPosition.y), Quaternion.identity, Vector3.one);

            // Apply the transformations.
            GUI.matrix *= translationToPivot * rotation * translationFromPivot;

            // Calculate the necessary clipping values based on the rect and topRect.
            float leftClip = Mathf.Min(rect.xMin, 0);
            float rightClip = Mathf.Max(rect.xMax - topRect.width, 0);
            float topClip = Mathf.Min(rect.yMin, 0);
            float bottomClip = Mathf.Max(rect.yMax - topRect.height, 0);

            // Define the clipping rectangle.
            Rect clipRect = new Rect(bottomClip, -leftClip, rect.height + topClip - bottomClip, rect.width + leftClip - rightClip);

            // Begin the custom GUI clipping.
            GUI.BeginClip(clipRect);

            // Define the rectangle for the label.
            Rect labelRect = new Rect(-bottomClip + GenUI.GapTiny, leftClip, rect.height, rect.width + GenUI.GapTiny);

            // Backup the current GUI properties.
            Color originalColor = GUI.color;
            TextAnchor originalAnchor = Text.Anchor;
            GameFont originalFont = Text.Font;

            // Set the properties for the label.
            GUI.color = new Color(.8f, .8f, .8f);
            Text.Anchor = TextAnchor.MiddleLeft;
            Text.Font = GameFont.Small;

            // Draw the label.
            Widgets.Label(labelRect, label);

            // Restore the original GUI properties.
            Text.Font = originalFont;
            GUI.color = originalColor;
            Text.Anchor = originalAnchor;

            // End the custom GUI clipping.
            GUI.EndClip();

            // Restore the original transformation matrix for subsequent GUI operations.
            GUI.matrix = originalMatrix;
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
