using UnityEngine;
using Verse;

namespace CompactWorkTab
{
    public class ModSettings : Verse.ModSettings
    {
        public static bool UseScrollWheel = true;
        public static bool DrawLabelsVertically = true;
        public static bool DrawInclinedLabels = false;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref UseScrollWheel, "UseScrollWheel", true);
            Scribe_Values.Look(ref DrawLabelsVertically, "DrawLabelsVertically", true);
            Scribe_Values.Look(ref DrawInclinedLabels, "DrawInclinedLabels", false);

            base.ExposeData();
        }

        public void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);
            listing.CheckboxLabeled("Draw column labels vertically", ref DrawLabelsVertically);
            listing.CheckboxLabeled("Use scroll wheel to change work priorities", ref UseScrollWheel);

            listing.CheckboxLabeled("Draw labels at a 60° angle", ref DrawInclinedLabels);

            Text.Font = GameFont.Tiny;
            GUI.color = Color.gray;
            listing.Label("This is an EXPERIMENTAL feature. Please report bugs at https://dsc.gg/CaptainArbitrary.");
            GUI.color = Color.white;
            Text.Font = GameFont.Small;

            listing.End();
        }
    }
}
