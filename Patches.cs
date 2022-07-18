using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using HarmonyLib;
using UnityEngine;

namespace Patches
{

    [HarmonyPatch(typeof(SlimeEat), "EatAndProduce")]
    internal class Patch_SlimeEatProduce
    {
        // Token: 0x06000036 RID: 54 RVA: 0x00006058 File Offset: 0x00004258
        private static bool Prefix(SlimeEat __instance, SlimeDiet.EatMapEntry em)
        {
            List<Identifiable.Id> list2 = new List<Identifiable.Id>();

            list2.Add(Identifiable.Id.KOOKADOBA_FRUIT);
            list2.Add(Identifiable.Id.GINGER_VEGGIE);

            if (em.eats == ModIds.DELICIOUS_SLIME)
                em.producesId = list2.RandomObject();
            
            return true;
        }
    }
}