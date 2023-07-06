using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using System;
using Utils;

namespace Isekaid.ClassFeatures.Magus.Arcana
{
    internal class ArcaneAccuracy
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(ArcaneAccuracy));

        private static readonly string FeatureName = "MagusFeatures.ArcanaSelection.ArcaneAccuracy.Name";
        internal const string DisplayName = "MagusFeatures.Name";
        private static readonly string Description = "MagusFeatures.Description";

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring arcane accuracy");

            ArcaneAccuracyAbility.Configure();

            FeatureConfigurator.New(FeatureName, Guids.ArcaneAccuracy)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.ArcanaSelection.ArcaneAccuracy.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.ArcaneAccuracyFeature.Reference.Get().Icon)
                .AddFacts(
                    facts: new () { Guids.ArcaneAccuracyAbility },
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
                .AddToGroups(Kingmaker.Blueprints.Classes.FeatureGroup.MagusArcana)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(true)
                .Configure();
        }
    }
}
