using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace CompactWorkTab
{
    public class CompactWorkTab : Mod
    {
        private readonly ModSettings _settings;

        public CompactWorkTab(ModContentPack content) : base(content)
        {
            _settings = GetSettings<ModSettings>();

            Harmony harmony = new Harmony(content.PackageId);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override string SettingsCategory()
        {
            return Content.Name;
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            _settings.DoSettingsWindowContents(inRect);
            base.DoSettingsWindowContents(inRect);
        }
    }
}
