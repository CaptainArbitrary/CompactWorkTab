using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace CompactWorkTab
{
    [HarmonyPatch(typeof(PawnColumnWorker_WorkPriority), nameof(PawnColumnWorker_WorkPriority.DoCell))]
    public class PawnColumnWorker_WorkPriority_DoCell
    {
        public static void Postfix(PawnColumnWorker_WorkPriority __instance, Rect rect, Pawn pawn, PawnTable table)
        {
            if (!ModSettings.UseScrollWheel) return;

            if (!Mouse.IsOver(rect) || pawn.WorkTypeIsDisabled(__instance.def.workType)) return;

            if (Event.current.type == EventType.ScrollWheel)
            {
                bool workTypeWasActive = pawn.workSettings.WorkIsActive(__instance.def.workType);
                int newPriority;

                if (Event.current.delta.y > 0)
                {
                    if (Find.PlaySettings.useWorkPriorities)
                    {
                        newPriority = pawn.workSettings.GetPriority(__instance.def.workType) + 1;
                        if (newPriority > Cache.MaxPriority) newPriority = 0;
                    }
                    else
                    {
                        newPriority = pawn.workSettings.GetPriority(__instance.def.workType) != Cache.MinPriority
                            ? Cache.MinPriority
                            : Cache.DefPriority;
                    }

                    pawn.workSettings.SetPriority(__instance.def.workType, newPriority);
                    SoundDefOf.DragSlider.PlayOneShotOnCamera();
                }

                if (Event.current.delta.y < 0)
                {
                    if (Find.PlaySettings.useWorkPriorities)
                    {
                        newPriority = pawn.workSettings.GetPriority(__instance.def.workType) - 1;
                        if (newPriority < Cache.MinPriority) newPriority = Cache.MaxPriority;
                    }
                    else
                    {
                        newPriority = pawn.workSettings.GetPriority(__instance.def.workType) != Cache.MinPriority
                            ? Cache.MinPriority
                            : Cache.DefPriority;
                    }

                    pawn.workSettings.SetPriority(__instance.def.workType, newPriority);
                    SoundDefOf.DragSlider.PlayOneShotOnCamera();
                }

                if (!workTypeWasActive && pawn.workSettings.WorkIsActive(__instance.def.workType) &&
                    __instance.def.workType.relevantSkills.Any() && pawn.skills.AverageOfRelevantSkillsFor(__instance.def.workType) <= 2f)
                    SoundDefOf.Crunch.PlayOneShotOnCamera();

                Event.current.Use();
            }
        }
    }
}
