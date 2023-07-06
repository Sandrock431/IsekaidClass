using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Class;
using Utils;

namespace Isekaid.ClassFeatures.Magus.Arcana
{
    internal class QuickenedArcana
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(QuickenedArcana));

        private static readonly string FeatureName = "MagusFeatures.ArcanaSelection.QuickenedArcana.Name";

        public static void Configure()
        {
            logger.Info("Configuring quickened arcana");

            FeatureConfigurator.New(FeatureName, Guids.QuickenedArcana)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.ArcanaSelection.QuickenedArcana.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.QuickenedArcanaFeature.Reference.Get().Icon)
                .AddFacts(
                    facts: new () { AbilityRefs.QuickenedArcanaAbility.Reference.Get() },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: Kingmaker.Settings.GameDifficultyOption.Story
                )
                .AddAbilityResources(
                    resource: AbilityResourceRefs.QuickenedArcanaResource.Reference.Get(),
                    useThisAsResource: false,
                    amount: 0,
                    restoreAmount: true,
                    restoreOnLevelUp: false
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
