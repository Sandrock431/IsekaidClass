using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Settings;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Mechanics.Components;
using IsekaidClass.Utils;
using IsekaidClass.Isekaid.ClassFeatures.Magus.Arcana;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus
{
    internal class ArcanePool
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(ArcanePool));

        private static readonly string FeatureName = "ArcanePool";
        internal const string DisplayName = "MagusFeatures.ArcanePool.Name";
        private static readonly string Description = "MagusFeatures.ArcanePool.Description";

        public static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatureName, Guids.ArcanePool).Configure();
            configureArcanePoolResource(enabled: false);
            configureArcaneWeaponSwitchAbility(enabled: false);
            ArcanaSelection.ConfigureDisabled();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring arcane pool");

            //ArcanePoolResource.Configure();
            //ArcaneWeaponSwitchAbility.Configure();
            configureArcanePoolResource(enabled: true);
            configureArcaneWeaponSwitchAbility(enabled: true);
            ArcanaSelection.ConfigureEnabled();

            FeatureConfigurator.New(FeatureName, Guids.ArcanePool)
                .CopyFrom(FeatureRefs.ArcanePoolFeature.Reference.Get())
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(
                    facts: new() {
                        Guids.ArcaneWeaponSwitchAbility
                    },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .AddAbilityResources(
                    resource: Guids.ArcanePoolResource,
                    useThisAsResource: false,
                    amount: 0,
                    restoreAmount: true,
                    restoreOnLevelUp: false
                )
                .Configure();
        }

        private static void configureArcanePoolResource(bool enabled)
        {
            logger.Info("   Configuring arcane pool resource");

            string featureName = "ArcanePoolResource";

            if (!enabled)
            {
                AbilityResourceConfigurator.New(featureName, Guids.ArcanePoolResource).Configure();
                return;
            }

            AbilityResourceConfigurator.New(featureName, Guids.ArcanePoolResource)
                .CopyFrom(AbilityResourceRefs.ArcanePoolResourse.Reference.Get())
                .SetMaxAmount(
                    new BlueprintAbilityResource.Amount
                    {
                        BaseValue = 0,
                        IncreasedByLevel = false,
                        LevelIncrease = 0,
                        IncreasedByLevelStartPlusDivStep = true,
                        StartingLevel = 0,
                        StartingIncrease = 0,
                        LevelStep = 2,
                        PerStepIncrease = 1,
                        MinClassLevelIncrease = 1,
                        m_ClassDiv = new[] { BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass) }, 
                        OtherClassesModifier = 0,
                        IncreasedByStat = true,
                        ResourceBonusStat = StatType.Charisma
                    }
                )
                .Configure();
        }

        private static void configureArcaneWeaponSwitchAbility(bool enabled)
        {
            logger.Info("   Configuring arcane weapon switch ability");

            string abilityName = "ArcaneWeaponSwitchAbility";
            string description = "MagusFeatures.ArcaneWeaponSwitchAbility.Description";

            if (!enabled)
            {
                AbilityConfigurator.New(abilityName, Guids.ArcaneWeaponSwitchAbility).Configure();
                return;
            }

            AbilityConfigurator.New(abilityName, Guids.ArcaneWeaponSwitchAbility)
                .CopyFrom(
                    blueprint: AbilityRefs.ArcaneWeaponSwitchAbility.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(AbilityEffectRunAction),
                        typeof(ContextRankConfig),
                        typeof(AbilitySpawnFx)
                    } 
                )
                .SetDescription(description)
                .AddAbilityResourceLogic(
                    requiredResource: Guids.ArcanePoolResource,
                    isSpendResource: true,
                    costIsCustom: false,
                    resourceCostIncreasingFacts: new()
                    {
                        BuffRefs.ArcaneWeaponBaneBuff.Reference.Get(),
                        BuffRefs.EnduringBladeBuff.Reference.Get(),
                    }
                )
                .Configure();
        }
    }
}
