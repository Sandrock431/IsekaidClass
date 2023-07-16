using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Settings;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.Spells
{
    internal class Cantrips
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(Cantrips));

        private static readonly string FeatureName = "Isekaid.Cantrips.Name";
        private static readonly string Description = "Isekaid.Cantrips.Description";

        public static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatureName, Guids.IsekaidClassCantrips).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring cantrips");

            FeatureConfigurator.New(FeatureName, Guids.IsekaidClassCantrips)
                .SetDisplayName(FeatureName)
                .SetDescription(Description)
                .SetDescriptionShort("")
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(true)
                .AddFacts(
                    facts: new()
                    {
                        AbilityRefs.AcidSplash.Reference.Get(),
                        AbilityRefs.RayOfFrost.Reference.Get(),
                        AbilityRefs.Jolt.Reference.Get(),
                        AbilityRefs.Daze.Reference.Get(),
                        AbilityRefs.Resistance.Reference.Get(),
                        AbilityRefs.TouchOfFatigueCast.Reference.Get(),
                        AbilityRefs.MageLight.Reference.Get(),
                        AbilityRefs.DisruptUndead.Reference.Get(),
                        AbilityRefs.Flare.Reference.Get(),
                        AbilityRefs.Virtue.Reference.Get(),
                        AbilityRefs.Guidance.Reference.Get(),
                        AbilityRefs.DivineZap.Reference.Get(),
                        AbilityRefs.DismissAreaEffect.Reference.Get()
                    },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .AddLearnSpells(
                    characterClass: Guids.IsekaidClass,
                    spells: new()
                    {
                        AbilityRefs.AcidSplash.Reference.Get(),
                        AbilityRefs.RayOfFrost.Reference.Get(),
                        AbilityRefs.Jolt.Reference.Get(),
                        AbilityRefs.Daze.Reference.Get(),
                        AbilityRefs.Resistance.Reference.Get(),
                        AbilityRefs.TouchOfFatigueCast.Reference.Get(),
                        AbilityRefs.MageLight.Reference.Get(),
                        AbilityRefs.DisruptUndead.Reference.Get(),
                        AbilityRefs.Flare.Reference.Get(),
                        AbilityRefs.Virtue.Reference.Get(),
                        AbilityRefs.Guidance.Reference.Get(),
                        AbilityRefs.DivineZap.Reference.Get(),
                        AbilityRefs.DismissAreaEffect.Reference.Get()
                    }
                )
                .AddBindAbilitiesToClass(
                    abilites: new()
                    {
                        AbilityRefs.AcidSplash.Reference.Get(),
                        AbilityRefs.RayOfFrost.Reference.Get(),
                        AbilityRefs.Jolt.Reference.Get(),
                        AbilityRefs.Daze.Reference.Get(),
                        AbilityRefs.Resistance.Reference.Get(),
                        AbilityRefs.TouchOfFatigueCast.Reference.Get(),
                        AbilityRefs.MageLight.Reference.Get(),
                        AbilityRefs.DisruptUndead.Reference.Get(),
                        AbilityRefs.Flare.Reference.Get(),
                        AbilityRefs.Virtue.Reference.Get(),
                        AbilityRefs.Guidance.Reference.Get(),
                        AbilityRefs.DivineZap.Reference.Get()
                    },
                    cantrip: true,
                    characterClass: Guids.IsekaidClass,
                    stat: StatType.Charisma,
                    levelStep: 1,
                    odd: false,
                    fullCasterChecks: true
                )
                .Configure();
        }
    }
}
