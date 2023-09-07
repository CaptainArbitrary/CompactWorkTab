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
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);

            listing.CheckboxLabeled("Use scroll wheel to change work priorities", ref UseScrollWheel);

            listing.Label("Header Orientation:");
            if (listing.RadioButton("Inclined", HeaderOrientation == HeaderOrientation.Inclined)) { HeaderOrientation = HeaderOrientation.Inclined; };
            if (listing.RadioButton("Vertical", HeaderOrientation == HeaderOrientation.Vertical)) { HeaderOrientation = HeaderOrientation.Vertical; };
            if (listing.RadioButton("Horizontal (RimWorld default)", HeaderOrientation == HeaderOrientation.Horizontal)) { HeaderOrientation = HeaderOrientation.Horizontal; };

            listing.End();
        }
    }
}
