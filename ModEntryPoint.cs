using MonomiPark.SlimeRancher.Regions;
using SRML;
using SRML.SR;
using SRML.SR.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace DeliciousMod
{
    public class Main : ModEntryPoint
    {
        public static Texture2D LoadImage(string filename) // thanks aidan or whoever created this at first- lol
        {
            var a = Assembly.GetExecutingAssembly();
            var spriteData = a.GetManifestResourceStream(a.GetName().Name + "." + filename);
            var rawData = new byte[spriteData.Length];
            spriteData.Read(rawData, 0, rawData.Length);
            var tex = new Texture2D(1, 1);
            tex.LoadImage(rawData);
            tex.filterMode = FilterMode.Bilinear;
            return tex;
        }
        public static Sprite CreateSprite(Texture2D texture) => Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1);

        // Called before GameContext.Awake
        // You want to register new things and enum values here, as well as do all your harmony patching
        public override void PreLoad()
        {
            HarmonyInstance.PatchAll();

            // START SLIMEPEDIA ENTRY: DELICIOUS SLIME
            PediaRegistry.RegisterIdentifiableMapping((PediaDirector.Id)1007, ModIds.DELICIOUS_SLIME);
            PediaRegistry.RegisterIdentifiableMapping(ModIds.DELICIOUS_ENTRY, ModIds.DELICIOUS_SLIME);
            PediaRegistry.SetPediaCategory(ModIds.DELICIOUS_ENTRY, (PediaRegistry.PediaCategory)1);
            new SlimePediaEntryTranslation(ModIds.DELICIOUS_ENTRY)
                .SetTitleTranslation("Delicious Slime")
                .SetIntroTranslation("Slimes love it! Koobaginger-tastic.")
                .SetDietTranslation("(none)")
                .SetFavoriteTranslation("(none)")
                .SetSlimeologyTranslation("When fed to any slime, they produce gilded ginger or kookadoba! That's about it really, you figure out what you wanna do with this new power.")
                .SetRisksTranslation("(none)")
                .SetPlortonomicsTranslation("(none)");
            // END SLIMEPEDIA ENTRY: DELICIOUS SLIME

            // START SLIMEPEDIA ENTRY: GINGER SLIME
            PediaRegistry.RegisterIdentifiableMapping((PediaDirector.Id)1007, ModIds.GINGER_SLIME);
            PediaRegistry.RegisterIdentifiableMapping(ModIds.GINGER_ENTRY, ModIds.GINGER_SLIME);
            PediaRegistry.SetPediaCategory(ModIds.GINGER_ENTRY, (PediaRegistry.PediaCategory)1);
            new SlimePediaEntryTranslation(ModIds.GINGER_ENTRY)
                .SetTitleTranslation("Ginger Slime")
                .SetIntroTranslation("Slimes love it! Ginger-tastic.")
                .SetDietTranslation("(none)")
                .SetFavoriteTranslation("(none)")
                .SetSlimeologyTranslation("When fed to any slime, they produce gilded ginger! That's about it really, you figure out what you wanna do with this new power.")
                .SetRisksTranslation("(none)")
                .SetPlortonomicsTranslation("(none)");
            // END SLIMEPEDIA ENTRY: GINGER SLIME

            // START SLIMEPEDIA ENTRY: KOOKADOBA SLIME
            PediaRegistry.RegisterIdentifiableMapping((PediaDirector.Id)1007, ModIds.KOOKADOBA_SLIME);
            PediaRegistry.RegisterIdentifiableMapping(ModIds.KOOKADOBA_ENTRY, ModIds.KOOKADOBA_SLIME);
            PediaRegistry.SetPediaCategory(ModIds.KOOKADOBA_ENTRY, (PediaRegistry.PediaCategory)1);
            new SlimePediaEntryTranslation(ModIds.KOOKADOBA_ENTRY)
                .SetTitleTranslation("Kookadoba Slime")
                .SetIntroTranslation("Slimes love it! Kookadoba-tastic.")
                .SetDietTranslation("(none)")
                .SetFavoriteTranslation("(none)")
                .SetSlimeologyTranslation("When fed to any slime, they produce kookadoba! That's about it really, you figure out what you wanna do with this new power.")
                .SetRisksTranslation("(none)")
                .SetPlortonomicsTranslation("(none)");
            // END SLIMEPEDIA ENTRY: KOOKADOBA SLIME

            // START DELICIOUS SLIME SPAWNER
            SRCallbacks.PreSaveGameLoad += (s =>
            {
                foreach (DirectedSlimeSpawner spawner in UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>()
                    .Where(ss =>
                    {
                        ZoneDirector.Zone zone = ss.GetComponentInParent<Region>(true).GetZoneId();
                        return zone == ZoneDirector.Zone.NONE || zone == ZoneDirector.Zone.RANCH || zone == ZoneDirector.Zone.REEF || zone == ZoneDirector.Zone.QUARRY || zone == ZoneDirector.Zone.MOSS || zone == ZoneDirector.Zone.DESERT || zone == ZoneDirector.Zone.SEA || zone == ZoneDirector.Zone.RUINS || zone == ZoneDirector.Zone.RUINS_TRANSITION || zone == ZoneDirector.Zone.WILDS || zone == ZoneDirector.Zone.SLIMULATIONS;
                    }))
                {
                    foreach (DirectedActorSpawner.SpawnConstraint constraint in spawner.constraints)
                    {
                        List<SlimeSet.Member> members = new List<SlimeSet.Member>(constraint.slimeset.members)
                        {
                            new SlimeSet.Member
                            {
                                prefab = GameContext.Instance.LookupDirector.GetPrefab(ModIds.DELICIOUS_SLIME),
                                weight = 0.004f // The higher the value is the more often your slime will spawn
                            }
                        };
                        constraint.slimeset.members = members.ToArray();
                    }
                }
            });
            // END DELICIOUS SLIME SPAWNER

            // START GINGER SLIME SPAWNER
            SRCallbacks.PreSaveGameLoad += (s =>
            {
                foreach (DirectedSlimeSpawner spawner in UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>()
                    .Where(ss =>
                    {
                        ZoneDirector.Zone zone = ss.GetComponentInParent<Region>(true).GetZoneId();
                        return zone == ZoneDirector.Zone.NONE || zone == ZoneDirector.Zone.DESERT;
                    }))
                {
                    foreach (DirectedActorSpawner.SpawnConstraint constraint in spawner.constraints)
                    {
                        List<SlimeSet.Member> members = new List<SlimeSet.Member>(constraint.slimeset.members)
                        {
                            new SlimeSet.Member
                            {
                                prefab = GameContext.Instance.LookupDirector.GetPrefab(ModIds.GINGER_SLIME),
                                weight = 0.008f // The higher the value is the more often your slime will spawn
                            }
                        };
                        constraint.slimeset.members = members.ToArray();
                    }
                }
            });
            // END GINGER SLIME SPAWNER

            // START KOOKADOBA SLIME SPAWNER
            SRCallbacks.PreSaveGameLoad += (s =>
            {
                foreach (DirectedSlimeSpawner spawner in UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>()
                    .Where(ss =>
                    {
                        ZoneDirector.Zone zone = ss.GetComponentInParent<Region>(true).GetZoneId();
                        return zone == ZoneDirector.Zone.NONE || zone == ZoneDirector.Zone.WILDS;
                    }))
                {
                    foreach (DirectedActorSpawner.SpawnConstraint constraint in spawner.constraints)
                    {
                        List<SlimeSet.Member> members = new List<SlimeSet.Member>(constraint.slimeset.members)
                        {
                            new SlimeSet.Member
                            {
                                prefab = GameContext.Instance.LookupDirector.GetPrefab(ModIds.KOOKADOBA_SLIME),
                                weight = 0.008f // The higher the value is the more often your slime will spawn
                            }
                        };
                        constraint.slimeset.members = members.ToArray();
                    }
                }
            });
            // END KOOKADOBA SLIME SPAWNER
        }

        // Called before GameContext.Start
        // Used for registering things that require a loaded gamecontext
        public override void Load()
        {
            // START LOAD DELICIOUS SLIME
            (SlimeDefinition, GameObject) DeliciousTuple = DeliciousSlime.CreateSlime(ModIds.DELICIOUS_SLIME, "Delicious Slime"); //Insert your own Id in the first argumeter

            //Getting the SlimeDefinition and GameObject separated
            SlimeDefinition Delicious_Definition = DeliciousTuple.Item1;
            GameObject Delicious_Object = DeliciousTuple.Item2;

            Delicious_Object.GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;
            AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, Delicious_Object);
            LookupRegistry.RegisterVacEntry(ModIds.DELICIOUS_SLIME, new Color32(255, 215, 0, 255), CreateSprite(LoadImage("delicious_slime.png")));
            TranslationPatcher.AddPediaTranslation("t." + ModIds.DELICIOUS_SLIME.ToString().ToLower(), "Delicious Slime");
            LookupRegistry.RegisterVacEntry(VacItemDefinition.CreateVacItemDefinition(ModIds.DELICIOUS_SLIME, new Color32(255, 255, 204, 255), CreateSprite(LoadImage("delicious_slime.png"))));

            // silo stuff
            AmmoRegistry.RegisterSiloAmmo(x => x == SiloStorage.StorageType.NON_SLIMES || x == SiloStorage.StorageType.FOOD, ModIds.DELICIOUS_SLIME);

            //And well, registering it!
            LookupRegistry.RegisterIdentifiablePrefab(Delicious_Object);
            SlimeRegistry.RegisterSlimeDefinition(Delicious_Definition);
            // END LOAD DELICIOUS SLIME

            // START LOAD GINGER SLIME
            (SlimeDefinition, GameObject) GingerTuple = GingerSlime.CreateSlime(ModIds.GINGER_SLIME, "Ginger Slime"); //Insert your own Id in the first argumeter

            //Getting the SlimeDefinition and GameObject separated
            SlimeDefinition Ginger_Definition = GingerTuple.Item1;
            GameObject Ginger_Object = GingerTuple.Item2;

            Ginger_Object.GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;
            AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, Ginger_Object);
            LookupRegistry.RegisterVacEntry(ModIds.GINGER_SLIME, new Color32(255, 215, 0, 255), CreateSprite(LoadImage("ginger_slime.png")));
            TranslationPatcher.AddPediaTranslation("t." + ModIds.GINGER_SLIME.ToString().ToLower(), "Ginger Slime");
            LookupRegistry.RegisterVacEntry(VacItemDefinition.CreateVacItemDefinition(ModIds.GINGER_SLIME, new Color32(255, 215, 0, 255), CreateSprite(LoadImage("ginger_slime.png"))));

            // silo stuff
            AmmoRegistry.RegisterSiloAmmo(x => x == SiloStorage.StorageType.NON_SLIMES || x == SiloStorage.StorageType.FOOD, ModIds.GINGER_SLIME);

            //And well, registering it!
            LookupRegistry.RegisterIdentifiablePrefab(Ginger_Object);
            SlimeRegistry.RegisterSlimeDefinition(Ginger_Definition);
            // END LOAD GINGER SLIME

            // START LOAD KOOKADOBA SLIME
            (SlimeDefinition, GameObject) KookadobaTuple = KookadobaSlime.CreateSlime(ModIds.KOOKADOBA_SLIME, "Kookadoba Slime"); //Insert your own Id in the first argumeter

            //Getting the SlimeDefinition and GameObject separated
            SlimeDefinition Kookadoba_Definition = KookadobaTuple.Item1;
            GameObject Kookadoba_Object = KookadobaTuple.Item2;

            Kookadoba_Object.GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;
            AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, Kookadoba_Object);
            LookupRegistry.RegisterVacEntry(ModIds.KOOKADOBA_SLIME, new Color32(255, 215, 0, 255), CreateSprite(LoadImage("kookadoba_slime.png")));
            TranslationPatcher.AddPediaTranslation("t." + ModIds.KOOKADOBA_SLIME.ToString().ToLower(), "Kookadoba Slime");
            LookupRegistry.RegisterVacEntry(VacItemDefinition.CreateVacItemDefinition(ModIds.KOOKADOBA_SLIME, new Color32(123, 47, 156, 255), CreateSprite(LoadImage("kookadoba_slime.png"))));

            // silo stuff
            AmmoRegistry.RegisterSiloAmmo(x => x == SiloStorage.StorageType.NON_SLIMES || x == SiloStorage.StorageType.FOOD, ModIds.KOOKADOBA_SLIME);

            //And well, registering it!
            LookupRegistry.RegisterIdentifiablePrefab(Kookadoba_Object);
            SlimeRegistry.RegisterSlimeDefinition(Kookadoba_Definition);
            // END LOAD KOOKADOBA SLIME
        }

        // Called after all mods Load's have been called
        // Used for editing existing assets in the game, not a registry step
        public override void PostLoad()
        {

        }

    }
}