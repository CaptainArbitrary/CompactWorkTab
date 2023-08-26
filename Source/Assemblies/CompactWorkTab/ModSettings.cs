using UnityEngine;
using Verse;

namespace CompactWorkTab
{
    public class ModSettings : Verse.ModSettings
    {
        public static bool UseScrollWheel = true;
        public static bool DrawLabelsVertically = true;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref UseScrollWheel, "UseScrollWheel", true);
            Scribe_Values.Look(ref DrawLabelsVertically, "DrawLabelsVertically", true);

            base.ExposeData();
        }

        public void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);
            listing.CheckboxLabeled("Draw column labels vertically", ref DrawLabelsVertically);
            listing.CheckboxLabeled("Use scroll wheel to change work priorities", ref UseScrollWheel);
            listing.End();
        }
    }
}
