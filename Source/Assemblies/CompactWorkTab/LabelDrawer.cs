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
            // Determine the pivot point for the rotation. This is adjusted slightly
            // based on the center of the rect and some additional space defined by 'GenUI.GapTiny'.
            Vector2 pivotPoint = new Vector2(rect.center.x + rect.width / 2 + GenUI.GapTiny, rect.center.y - GenUI.GapTiny);

            // Define a small rectangle around the pivot point for visualization.
            // This is useful if you'd like to visually debug or mark the pivot point.
            Rect pivotRect = new Rect(0f, 0f, 4f, 4f) { center = pivotPoint };

            // Store the current transformation matrix of the GUI to restore it later.
            Matrix4x4 originalMatrix = GUI.matrix;

            // Reset the GUI matrix to the identity matrix to start fresh.
            GUI.matrix = Matrix4x4.identity;

            // Rotate the GUI around the 'pivotPoint' by -60 degrees.
            GUIUtility.RotateAroundPivot(-60f, pivotPoint);

            // Multiply the original matrix by the new transformation to apply the rotation.
            GUI.matrix = originalMatrix * GUI.matrix;

            // Define a rectangle for the label, placing it centered on the pivot rectangle.
            Rect labelRect = new Rect(0f, 0f, rect.height, rect.width) { center = pivotRect.center };

            // Backup the current GUI properties to restore them after drawing the label.
            Color originalColor = GUI.color;
            TextAnchor originalAnchor = Text.Anchor;
            GameFont originalFont = Text.Font;
            bool originalWordWrap = Text.WordWrap;

            // Set the properties for the inclined label.
            GUI.color = new Color(.8f, .8f, .8f);
            Text.Anchor = TextAnchor.LowerLeft;
            Text.Font = GameFont.Small;
            Text.WordWrap = false;

            // Draw the inclined label.
            Widgets.Label(labelRect, label);

            // Restore the original GUI properties.
            Text.WordWrap = originalWordWrap;
            Text.Font = originalFont;
            GUI.color = originalColor;
            Text.Anchor = originalAnchor;

            // Reset the GUI matrix to its original state for subsequent GUI operations.
            GUI.matrix = originalMatrix;
        }
    }
}
