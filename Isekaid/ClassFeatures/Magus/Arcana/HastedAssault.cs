using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus.Arcana
{
    internal class HastedAssault
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(HastedAssault));

        private static readonly string FeatureName = "MagusFeatures.ArcanaSelection.HastedAssault.Name";

        public static void Configure()
        {
            logger.Info("Configuring hasted assault");

            HastedAssaultAbility.Configure();

            FeatureConfigurator.New(FeatureName, Guids.HastedAssault)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.ArcanaSelection.HastedAssault.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.HastedAssaultFeature.Reference.Get().Icon)
                .AddFacts(
                    facts: new () { Guids.HastedAssaultAbility },
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
