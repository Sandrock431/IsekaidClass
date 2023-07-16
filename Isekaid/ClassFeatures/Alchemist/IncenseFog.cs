using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using IsekaidClass.Utils;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.Settings;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;

namespace IsekaidClass.Isekaid.ClassFeatures.Alchemist
{
    internal class IncenseFog
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(IncenseFog));

        private static readonly string FeatureName = "AlchemistFeatures.IncenseFog.Name";

        public static void ConfigureDisabled()
        {
            configureIncenseFogFeature(enabled: false);
            configureIncenseFogToggleAbility(enabled: false);
            configureIncenseFogArea(enabled: true);

            configureIncenseFog30Feature(enabled: false);
            configureIncenseFog30ToggleAbility(enabled: false);
            configureIncenseFog30Area(enabled: true);

            configureIncenseFogResource(enabled: false);
            configureIncenseFogResourceFact(enabled: false);
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring incense fog");

            configureIncenseFogFeature(enabled: true);
            configureIncenseFogToggleAbility(enabled: true);
            configureIncenseFogArea(enabled: true);

            configureIncenseFog30Feature(enabled: true);
            configureIncenseFog30ToggleAbility(enabled: true);
            configureIncenseFog30Area(enabled: true);

            configureIncenseFogResource(enabled: true);
            configureIncenseFogResourceFact(enabled: true);

            patchIncenseFogIncreasedRange();
        }

        private static void configureIncenseFogFeature(bool enabled)
        {
            logger.Info("   Configuring incense fog feature");

            string name = "AlchemistFeatures.IncenseFog.Name";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.IncenseFogFeature).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.IncenseFogFeature)
                .CopyFrom(FeatureRefs.IncenseFogFeature.Reference.Get())
                .AddFacts(
                    facts: new() { Guids.IncenseFogToggleAbility },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .Configure();
        }

        private static void configureIncenseFogToggleAbility(bool enabled)
        {
            logger.Info("   Configuring incense fog toggle ability");

            string name = "AlchemistFeatures.IncenseFog.ToggleAbility.Name";

            if (!enabled)
            {
                ActivatableAbilityConfigurator.New(name, Guids.IncenseFogToggleAbility).Configure();
                return;
            }

            ActivatableAbilityConfigurator.New(name, Guids.IncenseFogToggleAbility)
                .CopyFrom(ActivatableAbilityRefs.IncenseFogToggleAbility.Reference.Get())
                .AddActivatableAbilityResourceLogic(
                    spendType: ActivatableAbilityResourceLogic.ResourceSpendType.NewRound,
                    requiredResource: Guids.IncenseFogResource
                )
                .SetDeactivateIfCombatEnded(false)
                .Configure();
        }

        private static void configureIncenseFog30Feature(bool enabled)
        {
            logger.Info("   Configuring incense fog 30 feature");

            string name = "AlchemistFeatures.IncenseFog30.Name";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.IncenseFog30Feature).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.IncenseFog30Feature)
                .CopyFrom(FeatureRefs.IncenseFog30Feature.Reference.Get())
                .AddFacts(
                    facts: new() { Guids.IncenseFog30ToggleAbility },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .Configure();
        }

        private static void configureIncenseFog30ToggleAbility(bool enabled)
        {
            logger.Info("   Configuring incense fog 30 toggle ability");

            string name = "AlchemistFeatures.IncenseFog30.ToggleAbility.Name";

            if (!enabled)
            {
                ActivatableAbilityConfigurator.New(name, Guids.IncenseFog30ToggleAbility).Configure();
                return;
            }

            ActivatableAbilityConfigurator.New(name, Guids.IncenseFog30ToggleAbility)
                .CopyFrom(ActivatableAbilityRefs.IncenseFog30ToggleAbility.Reference.Get())
                .AddActivatableAbilityResourceLogic(
                    spendType: ActivatableAbilityResourceLogic.ResourceSpendType.NewRound,
                    requiredResource: Guids.IncenseFogResource
                )
                .SetDeactivateIfCombatEnded(false)
                .Configure();
        }

        private static void configureIncenseFogResource(bool enabled)
        {
            logger.Info("   Configuring incense fog resource");

            string name = "AlchemistFeatures.IncenseFog.Resource.Name";

            if (!enabled)
            {
                AbilityResourceConfigurator.New(name, Guids.IncenseFogResource).Configure();
                return;
            }

            AbilityResourceConfigurator.New(name, Guids.IncenseFogResource)
                .CopyFrom(AbilityResourceRefs.IncenseFogResource.Reference.Get())
                .SetMaxAmount(
                    new BlueprintAbilityResource.Amount
                    {
                        BaseValue = 3,
                        IncreasedByLevel = false,
                        m_Class = new[] { BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass) },
                        LevelIncrease = 1,
                        IncreasedByLevelStartPlusDivStep = true,
                        StartingLevel = 0,
                        StartingIncrease = 0,
                        LevelStep = 2,
                        PerStepIncrease = 1,
                        MinClassLevelIncrease = 0,
                        m_ClassDiv = new[] { BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass) },
                        OtherClassesModifier = 0,
                        IncreasedByStat = true,
                        ResourceBonusStat = StatType.Charisma
                    }
                )
                .Configure();
        }

        private static void configureIncenseFogResourceFact(bool enabled)
        {
            logger.Info("   Configuring incense fog resource fact");

            string name = "AlchemistFeatures.IncenseFog.ResourceFact.Name";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.IncenseFogResourceFact).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.IncenseFogResourceFact)
                .CopyFrom(FeatureRefs.IncenseFogResourceFact.Reference.Get())
                .AddAbilityResources(
                    useThisAsResource: false,
                    resource: Guids.IncenseFogResource,
                    amount: 0,
                    restoreAmount: false,
                    restoreOnLevelUp: false
                )
                .Configure();
        }

        private static void configureIncenseFogArea(bool enabled)
        {
            logger.Info("   Configuring incense fog area");

            string name = "AlchemistFeatures.IncenseFog.Area.Name";

            if (!enabled)
            {
                AbilityAreaEffectConfigurator.New(name, Guids.IncenseFogArea).Configure();
                return;
            }

            AbilityAreaEffectConfigurator.New(name, Guids.IncenseFogArea)
                .CopyFrom(
                    blueprint: AbilityAreaEffectRefs.IncenseFogArea.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(AbilityAreaEffectRunAction)
                    }
                )
                .AddContextCalculateAbilityParamsBasedOnClass(
                    useKineticistMainStat: false,
                    statType: StatType.Charisma,
                    characterClass: BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                )
                .Configure();
        }

        private static void configureIncenseFog30Area(bool enabled)
        {
            logger.Info("   Configuring incense fog 30 area");

            string name = "AlchemistFeatures.IncenseFog30.Area.Name";

            if (!enabled)
            {
                AbilityAreaEffectConfigurator.New(name, Guids.IncenseFog30Area).Configure();
                return;
            }

            AbilityAreaEffectConfigurator.New(name, Guids.IncenseFog30Area)
                .CopyFrom(
                    blueprint: AbilityAreaEffectRefs.IncenseFog30Area.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(AbilityAreaEffectRunAction)
                    }
                )
                .AddContextCalculateAbilityParamsBasedOnClass(
                    useKineticistMainStat: false,
                    statType: StatType.Charisma,
                    characterClass: BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                )
                .Configure();
        }

        private static void patchIncenseFogIncreasedRange()
        {
            logger.Info("   Patching incense fog increased range");

            FeatureConfigurator.For(FeatureRefs.IncenseFogIncreasedRangeFeature.Reference.Get())
                .AddRemoveFeatureOnApply(Guids.IncenseFogFeature)
                .AddFeatureOnApply(Guids.IncenseFog30Feature)
                .Configure();
        }
    }
}
