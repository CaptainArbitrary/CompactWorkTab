using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace CompactWorkTab
{
    public class CompactWorkTab : Mod
    {
        private readonly ModSettings settings;
        private readonly string settingsCategory;

        public CompactWorkTab(ModContentPack content) : base(content)
        {
            settings = GetSettings<ModSettings>();
            settingsCategory = content.Name;

            Harmony harmony = new Harmony(content.PackageId);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override string SettingsCategory()
        {
            return settingsCategory;
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            settings.DoSettingsWindowContents(inRect);
            base.DoSettingsWindowContents(inRect);
        }
    }
}
