using BlueprintCore.Blueprints.Configurators.Root;
using BlueprintCore.Utils;
using Class;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using System;
using UnityModManagerNet;
using Utils;

namespace IsekaiClass
{
    public static class Main
    {
        public static bool Enabled;
        private static readonly LogWrapper logger = LogWrapper.Get("IsekaiClass");

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                modEntry.OnToggle = OnToggle;
                var harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll();
                logger.Info("Finished patching.");
            }
            catch (Exception e)
            {
                logger.Error("Failed to patch", e);
            }
            return true;
        }

        public static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Enabled = value;
            return true;
        }

        [HarmonyPatch(typeof(BlueprintsCache))]
        static class BlueprintsCaches_Patch
        {
            private static bool Initialized = false;

            [HarmonyPriority(Priority.First)]
            [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
            static void Init()
            {
                try
                {
                    if (Initialized)
                    {
                        logger.Info("Already configured blueprints.");
                        return;
                    }
                    Initialized = true;

                    logger.Info("Configuring localization packs");

                    LocalizationTool.LoadEmbeddedLocalizationPacks(
                        "IsekaidClass.Localization.Settings.json",
                        "IsekaidClass.Localization.IsekaidClass.json",
                        "IsekaidClass.Localization.AlchemistFeatures.json",
                        "IsekaidClass.Localization.ArcanistFeatures.json",
                        "IsekaidClass.Localization.BarbarianFeatures.json",
                        "IsekaidClass.Localization.CavalierFeatures.json",
                        "IsekaidClass.Localization.ClericFeatures.json",
                        "IsekaidClass.Localization.MagusFeatures.json",
                        "IsekaidClass.Localization.PaladinFeatures.json",
                        "IsekaidClass.Localization.RangerFeatures.json",
                        "IsekaidClass.Localization.RogueFeatures.json",
                        "IsekaidClass.Localization.SorcererFeatures.json",
                        "IsekaidClass.Localization.WizardFeatures.json"
                    );

                    logger.Info("Configuring blueprints.");

                    Settings.Init();

                    IsekaidClass.Configure();
                    Patches.Miscellaneous.Configure();
                }
                catch (Exception e)
                {
                    logger.Error("Failed to configure blueprints.", e);
                }
            }
        }

        [HarmonyPatch(typeof(StartGameLoader))]
        static class StartGameLoader_Patch
        {
            private static bool Initialized = false;

            [HarmonyPatch(nameof(StartGameLoader.LoadPackTOC)), HarmonyPostfix]
            static void LoadPackTOC()
            {
                try
                {
                    if (Initialized)
                    {
                        logger.Info("Already configured delayed blueprints.");
                        return;
                    }
                    Initialized = true;

                    RootConfigurator.ConfigureDelayedBlueprints();
                }
                catch (Exception e)
                {
                    logger.Error("Failed to configure delayed blueprints.", e);
                }
            }
        }
    }
}
