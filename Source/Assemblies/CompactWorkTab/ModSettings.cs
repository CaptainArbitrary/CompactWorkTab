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

            Rect thirdRow = new Rect(inRect) { y = secondRow.yMax, height = GenUI.ListSpacing };

            string leftRadioButtonLabel = "Inclined Headers";
            Vector2 leftRadioButtonLabelSize = Text.CalcSize(leftRadioButtonLabel);
            Rect leftRadioButtonRect = new Rect(leftColumn.x, thirdRow.y, leftColumn.width, thirdRow.height);
            leftRadioButtonRect.width = leftRadioButtonLabelSize.x + Widgets.RadioButOnTex.width;
            leftRadioButtonRect.x = leftColumn.center.x - leftRadioButtonRect.width / 2f;
            if (Widgets.RadioButtonLabeled(leftRadioButtonRect,"Inclined Headers", HeaderOrientation == HeaderOrientation.Inclined)) HeaderOrientation = HeaderOrientation.Inclined;

            string middleRadioButtonLabel = "Vertical Headers";
            Vector2 middleRadioButtonLabelSize = Text.CalcSize(middleRadioButtonLabel);
            Rect middleRadioButtonRect = new Rect(middleColumn.x, thirdRow.y, middleColumn.width, thirdRow.height);
            middleRadioButtonRect.width = middleRadioButtonLabelSize.x + Widgets.RadioButOnTex.width;
            middleRadioButtonRect.x = middleColumn.center.x - middleRadioButtonRect.width / 2f;
            if (Widgets.RadioButtonLabeled(middleRadioButtonRect,"Vertical Headers", HeaderOrientation == HeaderOrientation.Vertical)) HeaderOrientation = HeaderOrientation.Vertical;

            string rightRadioButtonLabel = "Horizontal Headers";
            Vector2 rightRadioButtonLabelSize = Text.CalcSize(rightRadioButtonLabel);
            Rect rightRadioButtonRect = new Rect(rightColumn.x, thirdRow.y, rightColumn.width, thirdRow.height);
            rightRadioButtonRect.width = rightRadioButtonLabelSize.x + Widgets.RadioButOnTex.width;
            rightRadioButtonRect.x = rightColumn.center.x - rightRadioButtonRect.width / 2f;
            if (Widgets.RadioButtonLabeled(rightRadioButtonRect,"Horizontal Headers", HeaderOrientation == HeaderOrientation.Horizontal)) HeaderOrientation = HeaderOrientation.Horizontal;

            Rect fourthRow = new Rect(inRect) { y = thirdRow.yMax, height = Textures.InclinedTexture.height + GenUI.Gap * 2f};

            Rect leftPictureRect = new Rect(fourthRow) { width = inRect.width / 3f };
            Rect leftTexRect = new Rect(leftPictureRect)
            {
                width = Textures.InclinedTexture.width,
                height = Textures.InclinedTexture.height,
                center = leftPictureRect.center
            };
            if (Event.current.type == EventType.MouseDown && leftTexRect.Contains(Event.current.mousePosition))
            {
                HeaderOrientation = HeaderOrientation.Inclined;
                Event.current.Use();
            }
            GUI.DrawTexture(leftTexRect, Textures.InclinedTexture, ScaleMode.ScaleToFit);
            if (HeaderOrientation == HeaderOrientation.Inclined) Widgets.DrawBox(leftTexRect);

            Rect middlePictureRect = new Rect(fourthRow) { width = inRect.width / 3f, x = leftPictureRect.xMax };
            Rect middleTexRect = new Rect(middlePictureRect)
            {
                width = Textures.VerticalTexture.width,
                height = Textures.VerticalTexture.height,
                center = middlePictureRect.center
            };
            if (Event.current.type == EventType.MouseDown && middleTexRect.Contains(Event.current.mousePosition))
            {
                HeaderOrientation = HeaderOrientation.Vertical;
                Event.current.Use();
            }
            GUI.DrawTexture(middleTexRect, Textures.VerticalTexture, ScaleMode.ScaleToFit);
            if (HeaderOrientation == HeaderOrientation.Vertical) Widgets.DrawBox(middleTexRect);

            Rect rightPictureRect = new Rect(fourthRow) { width = inRect.width / 3f, x = middlePictureRect.xMax };
            Rect rightTexRect = new Rect(rightPictureRect)
            {
                width = Textures.HorizontalTexture.width,
                height = Textures.HorizontalTexture.height,
                center = rightPictureRect.center
            };
            if (Event.current.type == EventType.MouseDown && rightTexRect.Contains(Event.current.mousePosition))
            {
                HeaderOrientation = HeaderOrientation.Horizontal;
                Event.current.Use();
            }
            GUI.DrawTexture(rightTexRect, Textures.HorizontalTexture, ScaleMode.ScaleToFit);
            if (HeaderOrientation == HeaderOrientation.Horizontal) Widgets.DrawBox(rightTexRect);
        }
    }
}
