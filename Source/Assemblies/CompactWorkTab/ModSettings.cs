using UnityEngine;
using Verse;

namespace CompactWorkTab
{
    public class ModSettings : Verse.ModSettings
    {
        public static bool UseScrollWheel = true;
        public static HeaderOrientation HeaderOrientation = HeaderOrientation.Inclined;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref UseScrollWheel, "UseScrollWheel", true);
            Scribe_Values.Look(ref HeaderOrientation, "HeaderOrientation", HeaderOrientation.Inclined);

            base.ExposeData();
        }

        public void DoSettingsWindowContents(Rect inRect)
        {
            Rect leftColumn = new Rect(inRect) { width = inRect.width / 3f };
            Rect middleColumn = new Rect(inRect) { width = inRect.width / 3f, x = leftColumn.xMax };
            Rect rightColumn = new Rect(inRect) { width = inRect.width / 3f, x = middleColumn.xMax };

            Rect firstRow = new Rect(inRect) { height = GenUI.ListSpacing };
            Widgets.CheckboxLabeled(firstRow, "Use scroll wheel to change work priorities:", ref UseScrollWheel);

            Rect secondRow = new Rect(inRect) { y = firstRow.yMax, height = GenUI.ListSpacing };
            // Second row intentionally left blank

            Rect thirdRow = new Rect(inRect) { y = secondRow.yMax, height = GenUI.ListSpacing };
            DoRadioButtonAndTexture(leftColumn, thirdRow, "Inclined Headers", Textures.InclinedTexture, HeaderOrientation.Inclined);
            DoRadioButtonAndTexture(middleColumn, thirdRow, "Vertical Headers", Textures.VerticalTexture, HeaderOrientation.Vertical);
            DoRadioButtonAndTexture(rightColumn, thirdRow, "Horizontal Headers", Textures.HorizontalTexture, HeaderOrientation.Horizontal);
        }

        private void DoRadioButtonAndTexture(Rect column, Rect row, string label, Texture texture, HeaderOrientation orientation)
        {
            // Radio Button
            Vector2 labelSize = Text.CalcSize(label);
            Rect radioButtonRect = new Rect(column.x, row.y, column.width, row.height)
            {
                width = labelSize.x + Widgets.RadioButOnTex.width,
                x = column.center.x - (labelSize.x + Widgets.RadioButOnTex.width) / 2f
            };
            if (Widgets.RadioButtonLabeled(radioButtonRect, label, HeaderOrientation == orientation))
                HeaderOrientation = orientation;

            // Texture
            Rect textureRow = new Rect(column) { y = row.yMax, height = texture.height + GenUI.Gap * 2f };
            Rect textureRect = new Rect(textureRow)
            {
                width = texture.width,
                height = texture.height,
                center = textureRow.center
            };
            if (Event.current.type == EventType.MouseDown && textureRect.Contains(Event.current.mousePosition))
            {
                HeaderOrientation = orientation;
                Event.current.Use();
            }
            GUI.DrawTexture(textureRect, texture, ScaleMode.ScaleToFit);

            // Draw selection box if this orientation is selected
            if (HeaderOrientation == orientation)
                Widgets.DrawBox(textureRect);
        }
    }
}
