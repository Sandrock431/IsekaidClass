using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus.Arcana
{
    internal class MaximizedArcana
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(MaximizedArcana));

        private static readonly string FeatureName = "MagusFeatures.ArcanaSelection.MaximizedArcana.Name";

        public static void Configure()
        {
            logger.Info("Configuring maximized arcana");

            FeatureConfigurator.New(FeatureName, Guids.MaximizedArcana)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.ArcanaSelection.MaximizedArcana.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.MaximizedArcanaFeature.Reference.Get().Icon)
                .AddFacts(
                    facts: new () { AbilityRefs.MaximizedArcanaAbility.Reference.Get() },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: Kingmaker.Settings.GameDifficultyOption.Story
                )
                .AddAbilityResources(
                    resource: AbilityResourceRefs.MaximizedArcanaResource.Reference.Get(),
                    useThisAsResource: false,
                    amount: 0,
                    restoreAmount: true,
                    restoreOnLevelUp: false
                )
                .AddPrerequisiteClassLevel(
                    characterClass: Guids.IsekaidClass,
                    level: 12,
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
