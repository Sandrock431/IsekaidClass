using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Class;
using Utils;

namespace Isekaid.ClassFeatures.Magus.Arcana
{
    internal class ReachArcana
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(ReachArcana));

        private static readonly string FeatureName = "MagusFeatures.ArcanaSelection.ReachArcana.Name";

        public static void Configure()
        {
            logger.Info("Configuring reach arcana");

            FeatureConfigurator.New(FeatureName, Guids.ReachArcana)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.ArcanaSelection.ReachArcana.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.ReachArcanaFeature.Reference.Get().Icon)
                .AddFacts(
                    facts: new () { AbilityRefs.ReachArcanaAbility.Reference.Get() },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: Kingmaker.Settings.GameDifficultyOption.Story
                )
                .AddAbilityResources(
                    resource: AbilityResourceRefs.ReachArcanaResource.Reference.Get(),
                    useThisAsResource: false,
                    amount: 0,
                    restoreAmount: true,
                    restoreOnLevelUp: false
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
