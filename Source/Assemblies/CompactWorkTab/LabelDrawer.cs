using UnityEngine;
using Verse;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace CompactWorkTab
{
    public static class LabelDrawer
    {
        public delegate (Rect transformedRect, Matrix4x4 transformationMatrix) LabelDrawerDelegate(Rect rect, string label);

        public static (Rect transformedRect, Matrix4x4 transformationMatrix) DrawVerticalLabel(Rect rect, string label)
        {
            // Store the current transformation matrix of the GUI to restore it later.
            Matrix4x4 originalMatrix = GUI.matrix;

            // Retrieve the topmost clipping rectangle in local GUI coordinates.
            Rect topRect = GUIClip.GetTopRect();

            // Reset the GUI matrix to the identity matrix.
            GUI.matrix = Matrix4x4.identity;

            // Calculate the unclipped position of the current UI element in screen-space coordinates.
            Vector2 unclippedPosition = GUIClip.Unclip(Vector2.zero);

            // Restore the original matrix for subsequent operations.
            Matrix4x4 transformationMatrix = originalMatrix;

            // Create a translation matrix to shift the pivot point to 'unclippedPosition'.
            transformationMatrix *= Matrix4x4.TRS(unclippedPosition, Quaternion.identity, Vector3.one);

            // Create a rotation matrix for a 90-degree counter-clockwise rotation.
            transformationMatrix *= Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -90f), Vector3.one);

            // Create another translation matrix to shift the pivot back from 'unclippedPosition'.
            transformationMatrix *= Matrix4x4.TRS(new Vector2(-rect.yMax - unclippedPosition.x, rect.xMin - unclippedPosition.y), Quaternion.identity, Vector3.one);

            // Apply the transformations.
            GUI.matrix = transformationMatrix;

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

            return (labelRect, transformationMatrix);
        }

        public static (Rect transformedRect, Matrix4x4 transformationMatrix) DrawInclinedLabel(Rect rect, string label)
        {
            // Calculate the size of the label
            Vector2 labelSize = Text.CalcSize(label);

            // Create a rectangle for the rotated label centered on the original rectangle
            Rect rotatedRect = new Rect(0f, 0f, rect.height, labelSize.y) { center = rect.center };

            // Let's label the corners of rotatedRect. The top left corner is a. The top right corner is b.
            // The bottom left corner is c. The bottom right corner is d. Our goal is to make c' match the target
            // position after the 60-degree rotation, where the target position is (rect.center.x, rect.yMax).

            Vector2 center = rotatedRect.center;
            float theta = Mathf.Deg2Rad * 60; // Convert 60 degrees to radians

            // Coordinates of point c relative to the center of the rotatedRect
            Vector2 cRelative = new Vector2(-rotatedRect.width / 2, -rotatedRect.height / 2);

            // Calculate where point c would land after a 60-degree rotation
            Vector2 cPrime = new Vector2(
                Mathf.Cos(theta) * cRelative.x - Mathf.Sin(theta) * cRelative.y + center.x,
                Mathf.Sin(theta) * cRelative.x + Mathf.Cos(theta) * cRelative.y + center.y
            );

            // Calculate the required horizontal offset to make c' match the target position
            float xOffset = rect.xMax - cPrime.x;

            // Apply the offset to the rotatedRect
            rotatedRect.x += xOffset;

            // Backup the original GUI matrix
            Matrix4x4 originalMatrix = GUI.matrix;

            // Reset the GUI matrix to identity (no transformations)
            GUI.matrix = Matrix4x4.identity;

            // Set the pivot point for rotation to the center of the rotated rectangle
            Vector2 pivotPoint = GUIClip.Unclip(rotatedRect.center);

            // Restore the original matrix for subsequent operations
            Matrix4x4 transformationMatrix = originalMatrix;

            // Translate the matrix so the pivot point becomes the new origin
            transformationMatrix *= Matrix4x4.TRS(pivotPoint, Quaternion.identity, Vector3.one);

            // Rotate the matrix by -60 degrees around the new origin (pivotPoint)
            transformationMatrix *= Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -60f), Vector3.one);

            // Translate the matrix back to its original position
            transformationMatrix *= Matrix4x4.TRS(-pivotPoint, Quaternion.identity, Vector3.one);

            // Apply the transformation
            GUI.matrix = transformationMatrix;

            // Backup the current GUI properties
            Color originalColor = GUI.color;
            TextAnchor originalAnchor = Text.Anchor;
            GameFont originalFont = Text.Font;
            bool originalWordWrap = Text.WordWrap;

            // Set GUI properties for the rotated label drawing
            GUI.color = new Color(.8f, .8f, .8f);
            Text.Anchor = TextAnchor.MiddleLeft;
            Text.Font = GameFont.Small;
            Text.WordWrap = false;

            // Draw the label in the rotated space
            Widgets.Label(rotatedRect, label);

            // Underscore the label
            Vector2 bottomRight = new Vector2(rotatedRect.xMax, rotatedRect.yMax);
            Vector2 bottomLeft = new Vector2(rotatedRect.xMin, rotatedRect.yMax);
            Widgets.DrawLine(bottomRight, bottomLeft, new Color(1f, 1f, 1f, 0.2f), 1f);

            // Restore the original GUI properties
            Text.WordWrap = originalWordWrap;
            Text.Font = originalFont;
            GUI.color = originalColor;
            Text.Anchor = originalAnchor;

            // Reset the GUI matrix to its original state
            GUI.matrix = originalMatrix;

            return (rotatedRect, transformationMatrix);
        }
    }
}
