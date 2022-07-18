using HarmonyLib;
using System.Linq;

namespace Eatmaps
{
    [HarmonyPatch(typeof(SlimeDiet), "RefreshEatMap")]
    class EatMapPatch
    {
        static void Postfix(SlimeDiet __instance, SlimeDefinitions definitions, SlimeDefinition definition)
        {
            if (definition.IdentifiableId != ModIds.GINGER_SLIME && definition.IdentifiableId != ModIds.KOOKADOBA_SLIME && definition.IdentifiableId != ModIds.DELICIOUS_SLIME)
            {
                __instance.EatMap.RemoveAll((x) => x.eats == ModIds.GINGER_SLIME);
                __instance.EatMap.Add(new SlimeDiet.EatMapEntry()
                {
                    producesId = Identifiable.Id.GINGER_VEGGIE,
                    eats = ModIds.GINGER_SLIME,
                    minDrive = 3
                });
            }
            if (definition.IdentifiableId != ModIds.KOOKADOBA_SLIME && definition.IdentifiableId != ModIds.GINGER_SLIME && definition.IdentifiableId != ModIds.DELICIOUS_SLIME)
            {
                __instance.EatMap.RemoveAll((x) => x.eats == ModIds.KOOKADOBA_SLIME);
                __instance.EatMap.Add(new SlimeDiet.EatMapEntry()
                {
                    producesId = Identifiable.Id.KOOKADOBA_FRUIT,
                    eats = ModIds.KOOKADOBA_SLIME,
                    minDrive = 3
                });
            }
            if (definition.IdentifiableId != ModIds.DELICIOUS_SLIME && definition.IdentifiableId != ModIds.GINGER_SLIME && definition.IdentifiableId != ModIds.KOOKADOBA_SLIME)
            {
                __instance.EatMap.RemoveAll((x) => x.eats == ModIds.DELICIOUS_SLIME);
                __instance.EatMap.Add(new SlimeDiet.EatMapEntry()
                {
                    producesId = Identifiable.Id.KOOKADOBA_FRUIT,
                    eats = ModIds.DELICIOUS_SLIME,
                    minDrive = 3
                });
            }
            if (definition.IdentifiableId == ModIds.DELICIOUS_SLIME)
            {
                __instance.EatMap.RemoveAll((x) => x.eats == Identifiable.Id.KOOKADOBA_FRUIT);
                __instance.EatMap.Add(new SlimeDiet.EatMapEntry()
                {
                    becomesId = ModIds.KOOKADOBA_SLIME,
                    eats = Identifiable.Id.KOOKADOBA_FRUIT,
                    minDrive = 1
                });
                __instance.EatMap.RemoveAll((x) => x.eats == Identifiable.Id.GINGER_VEGGIE);
                __instance.EatMap.Add(new SlimeDiet.EatMapEntry()
                {
                    becomesId = ModIds.GINGER_SLIME,
                    eats = Identifiable.Id.GINGER_VEGGIE,
                    minDrive = 1
                });
            }
        }
    }
}