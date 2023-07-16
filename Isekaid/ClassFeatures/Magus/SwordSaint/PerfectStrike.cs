using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus.SwordSaint
{
    internal class PerfectStrike
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(PerfectStrike));

        private static readonly string FeatureName = "MagusFeatures.SwordSaint.PerfectStrike.Name";

        public static void Configure()
        {
            logger.Info("Configuring perfect strike");

            PerfectStrikeAbility.Configure();
            PerfectStrikeCritAbility.Configure();

            FeatureConfigurator.New(FeatureName, Guids.PerfectStrike)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.SwordSaint.PerfectStrike.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.SwordSaintPerfectStrikeFeature.Reference.Get().Icon)
                .AddFacts(
                    facts: new()
                    {
                        Guids.PerfectStrikeAbility,
                        Guids.PerfectStrikeCritAbility,
                    },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: Kingmaker.Settings.GameDifficultyOption.Story
                )
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(true)
                .Configure();
        }
    }
}
