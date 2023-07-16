using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus.Arcana
{
    internal class GhostBlade
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(GhostBlade));

        private static readonly string FeatureName = "MagusFeatures.ArcanaSelection.GhostBlade.Name";

        public static void Configure()
        {
            logger.Info("Configuring ghost blade");

            FeatureConfigurator.New(FeatureName, Guids.GhostBlade)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.ArcanaSelection.GhostBlade.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.EnduringBladeFeature.Reference.Get().Icon)
                .AddFacts(
                    facts: new() {
                        ActivatableAbilityRefs.ArcaneWeaponGhostTouchChoice.Reference.Get(),
                        ActivatableAbilityRefs.ArcaneWeaponBrilliantEnergyChoice.Reference.Get()
                    },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: Kingmaker.Settings.GameDifficultyOption.Story
                )
                .AddPrerequisiteClassLevel(
                    characterClass: Guids.IsekaidClass,
                    level: 9,
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
