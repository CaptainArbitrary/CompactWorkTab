using System.Reflection;
using HarmonyLib;
using Verse;

namespace CompactWorkTab
{
    public class CompactWorkTab : Mod
    {
        public CompactWorkTab(ModContentPack content) : base(content)
        {
            Harmony harmony = new Harmony(Constants.HarmonyId);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
