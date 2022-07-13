using HarmonyLib;
using System.Linq;

namespace Eatmaps
{
    [HarmonyPatch(typeof(SlimeDiet), "RefreshEatMap")]
    class EatMapPatch
    {
        static void Postfix(SlimeDiet __instance, SlimeDefinitions definitions, SlimeDefinition definition)
        {
            if (definition.IdentifiableId != ModIds.DELICIOUS_SLIME)
            {
                __instance.EatMap.RemoveAll((x) => x.eats == ModIds.DELICIOUS_SLIME);
                __instance.EatMap.Add(new SlimeDiet.EatMapEntry()
                {
                    producesId = Identifiable.Id.GINGER_VEGGIE,
                    eats = ModIds.DELICIOUS_SLIME,
                    minDrive = 3
                });
            }
        }
    }
}