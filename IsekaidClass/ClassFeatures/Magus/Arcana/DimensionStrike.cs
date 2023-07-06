using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Class;
using Utils;

namespace Isekaid.ClassFeatures.Magus.Arcana
{
    internal class DimensionStrike
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(DimensionStrike));

        private static readonly string FeatureName = "MagusFeatures.ArcanaSelection.DimensionStrike.Name";

        public static void Configure()
        {
            logger.Info("Configuring dimension strike");

            FeatureConfigurator.New(FeatureName, Guids.DimensionStrike)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.ArcanaSelection.DimensionStrike.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.DimensionStrikeFeature.Reference.Get().Icon)
                .AddFacts(
                    facts: new () { AbilityRefs.DimensionStrikeAbility.Reference.Get() },
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
