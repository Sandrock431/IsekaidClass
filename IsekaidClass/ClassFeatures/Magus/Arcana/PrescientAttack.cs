using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Class;
using Utils;

namespace Isekaid.ClassFeatures.Magus.Arcana
{
    internal class PrescientAttack
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(PrescientAttack));

        private static readonly string FeatureName = "MagusFeatures.ArcanaSelection.PrescientAttack.Name";

        public static void Configure()
        {
            logger.Info("Configuring prescient attack");

            FeatureConfigurator.New(FeatureName, Guids.PrescientAttack)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.ArcanaSelection.PrescientAttack.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.PrescientAttackFeature.Reference.Get().Icon)
                .AddFacts(
                    facts: new () { AbilityRefs.PrescientAttackAbility.Reference.Get() },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: Kingmaker.Settings.GameDifficultyOption.Story
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
