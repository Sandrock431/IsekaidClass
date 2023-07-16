using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus.Arcana
{
    internal class EmpoweredArcana
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(EmpoweredArcana));

        private static readonly string FeatureName = "MagusFeatures.ArcanaSelection.EmpoweredArcana.Name";

        public static void Configure()
        {
            logger.Info("Configuring empowered arcana");

            FeatureConfigurator.New(FeatureName, Guids.EmpoweredArcana)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.ArcanaSelection.EmpoweredArcana.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.EmpoweredArcanaFeature.Reference.Get().Icon)
                .AddFacts(
                    facts: new () { AbilityRefs.EmpoweredArcanaAbility.Reference.Get() },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: Kingmaker.Settings.GameDifficultyOption.Story
                )
                .AddAbilityResources(
                    resource: AbilityResourceRefs.EmpoweredArcanaResource.Reference.Get(),
                    useThisAsResource: false,
                    amount: 0,
                    restoreAmount: true,
                    restoreOnLevelUp: false
                )
                .AddPrerequisiteClassLevel(
                    characterClass: Guids.IsekaidClass,
                    level: 6,
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
