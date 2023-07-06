﻿using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Class;
using Utils;

namespace Isekaid.ClassFeatures.Magus.Arcana
{
    internal class BaneBlade
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(BaneBlade));

        private static readonly string FeatureName = "MagusFeatures.ArcanaSelection.BaneBlade.Name";

        public static void Configure()
        {
            logger.Info("Configuring bane blade");

            FeatureConfigurator.New(FeatureName, Guids.BaneBlade)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.ArcanaSelection.BaneBlade.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.EnduringBladeFeature.Reference.Get().Icon)
                .AddFacts(
                    facts: new() { ActivatableAbilityRefs.ArcaneWeaponBaneChoice.Reference.Get() },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: Kingmaker.Settings.GameDifficultyOption.Story
                )
                .AddPrerequisiteClassLevel(
                    characterClass: Guids.IsekaidClass,
                    level: 15,
                    group: Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.All,
                    checkInProgression: false,
                    hideInUI: false
                )
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .AddToGroups(Kingmaker.Blueprints.Classes.FeatureGroup.MagusArcana)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(true)
                .Configure();
        }
    }
}