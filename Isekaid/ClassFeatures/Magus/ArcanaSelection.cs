using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Settings;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Mechanics.Components;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus
{
    internal class ArcanaSelection
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(ArcanaSelection));

        private static readonly string FeatureName = "ArcanaSelection";
        internal const string DisplayName = "MagusFeatures.ArcanaSelection.Name";
        private static readonly string Description = "MagusFeatures.ArcanaSelection.Description";

        public static void ConfigureDisabled()
        {
            configureArcaneAccuracy(enabled: false);
            configureHastedAssault(enabled: false);
            configurePrescientAttack(enabled: false);
            FeatureConfigurator.New(FeatureName, Guids.ArcanaSelection).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring arcana selection");

            configureArcaneAccuracy(enabled: true);
            configureHastedAssault(enabled: true);
            configurePrescientAttack(enabled: true);

            FeatureSelectionConfigurator.New(FeatureName, Guids.ArcanaSelection)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetDescriptionShort("")
                .SetIcon(FeatureSelectionRefs.MagusArcanaSelection.Reference.Get().Icon)
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(true)
                .SetIgnorePrerequisites(false)
                .SetObligatory(false)
                .SetMode(Kingmaker.Blueprints.Classes.Selection.SelectionMode.Default)
                .SetGroup(FeatureGroup.MagusArcana)
                .SetGroup2(FeatureGroup.None)
                .SetAllFeatures(new Blueprint<Kingmaker.Blueprints.BlueprintFeatureReference>[]
                {
                    Guids.ArcaneAccuracy,
                    FeatureRefs.BaneBladeFeature.Reference.Get(),
                    FeatureRefs.DevotedBladeFeature.Reference.Get(),
                    FeatureRefs.DimensionStrikeFeature.Reference.Get(),
                    FeatureRefs.EmpoweredArcanaFeature.Reference.Get(),
                    FeatureRefs.EnduringBladeFeature.Reference.Get(),
                    FeatureRefs.ExtendedArcanaFeature.Reference.Get(),
                    FeatureRefs.GhostBladeFeature.Reference.Get(),
                    Guids.HastedAssault,
                    FeatureRefs.MaximizedArcanaFeature.Reference.Get(),
                    Guids.PrescientAttack,
                    FeatureRefs.QuickenedArcanaFeature.Reference.Get(),
                    FeatureRefs.ReachArcanaFeature.Reference.Get()
                })
                .Configure();
        }

        private static void configureArcaneAccuracy(bool enabled)
        {
            logger.Info("   Configuring arcane accuracy");

            string name = "ArcaneAccuracy";
            string description = "MagusFeatures.ArcanaSelection.ArcaneAccuracy.Description";

            if (!enabled)
            {
                configureArcaneAccuracyAbility(enabled: false);

                FeatureConfigurator.New(name, Guids.ArcaneAccuracy).Configure();
                return;
            }

            configureArcaneAccuracyAbility(enabled: true);

            FeatureConfigurator.New(name, Guids.ArcaneAccuracy)
                .CopyFrom(FeatureRefs.ArcaneAccuracyFeature.Reference.Get())
                .SetDescription(description)
                .AddFacts(
                    facts: new() { Guids.ArcaneAccuracyAbility },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .Configure();
        }

        private static void configureArcaneAccuracyAbility(bool enabled)
        {
            logger.Info("       Configuring arcane accuracy ability");

            string name = "ArcaneAccuracyAbility";
            string description = "MagusFeatures.ArcanaSelection.ArcaneAccuracyAbility.Description";

            if (!enabled)
            {
                AbilityConfigurator.New(name, Guids.ArcaneAccuracyAbility).Configure();
                return;
            }

            AbilityConfigurator.New(name, Guids.ArcaneAccuracyAbility)
                .CopyFrom(
                    blueprint: AbilityRefs.ArcaneAccuracyAbility.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(AbilityEffectRunAction),
                        typeof(AbilitySpawnFx),
                        typeof(ContextCalculateSharedValue)
                    }
                )
                .SetDescription(description)
                .AddAbilityResourceLogic(
                    requiredResource: Guids.ArcanePoolResource,
                    amount: 1,
                    costIsCustom: false,
                    isSpendResource: true,
                    resourceCostIncreasingFacts: new()
                )
                .AddContextRankConfig(
                    component: ContextRankConfigs.StatBonus(
                        stat: StatType.Charisma,
                        type: AbilityRankType.Default,
                        min: 0,
                        max: 20
                    )
                ).Configure();
        }

        private static void configureHastedAssault(bool enabled)
        {
            logger.Info("   Configuring hasted assault");

            string name = "HastedAssault";
            string description = "MagusFeatures.ArcanaSelection.HastedAssault.Description";

            if (!enabled)
            {
                configureHastedAssaultAbility(enabled: false);

                FeatureConfigurator.New(name, Guids.HastedAssault).Configure();
                return;
            }

            configureHastedAssaultAbility(enabled: true);

            FeatureConfigurator.New(name, Guids.HastedAssault)
                .CopyFrom(
                    blueprint: FeatureRefs.HastedAssaultFeature.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(PrerequisiteClassLevel)
                    }
                )
                .SetDescription(description)
                .AddFacts(
                    facts: new() { Guids.HastedAssaultAbility },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .Configure();
        }

        private static void configureHastedAssaultAbility(bool enabled)
        {
            logger.Info("       Configuring hasted assault ability");

            string name = "HastedAssaultAbility";
            string description = "MagusFeatures.ArcanaSelection.HastedAssaultAbility.Description";

            if (!enabled)
            {
                AbilityConfigurator.New(name, Guids.HastedAssaultAbility).Configure();
                return;
            }

            AbilityConfigurator.New(name, Guids.HastedAssaultAbility)
                .CopyFrom(
                    blueprint: AbilityRefs.HastedAssaultAbility.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(AbilityEffectRunAction),
                        typeof(AbilitySpawnFx),
                        typeof(ContextCalculateSharedValue)
                    }
                )
                .SetDescription(description)
                .SetLocalizedDuration("MagusFeatures.ArcanaSelection.HastedAssaultAbility.Duration")
                .AddAbilityResourceLogic(
                    requiredResource: Guids.ArcanePoolResource,
                    amount: 1,
                    costIsCustom: false,
                    isSpendResource: true,
                    resourceCostIncreasingFacts: new()
                )
                .AddContextRankConfig(
                    component: ContextRankConfigs.StatBonus(
                        stat: StatType.Charisma,
                        type: AbilityRankType.Default,
                        min: 0,
                        max: 20
                    )
                ).Configure();
        }

        private static void configurePrescientAttack(bool enabled)
        {
            logger.Info("   Configuring prescient attack");

            string name = "PrescientAttack";
            string description = "MagusFeatures.ArcanaSelection.PrescientAttack.Description";

            if (!enabled)
            {
                configurePrescientAttackAbility(enabled: false);

                FeatureConfigurator.New(name, Guids.PrescientAttack).Configure();
                return;
            }

            configurePrescientAttackAbility(enabled: true);

            FeatureConfigurator.New(name, Guids.PrescientAttack)
                .CopyFrom(
                    blueprint: FeatureRefs.PrescientAttackFeature.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(PrerequisiteClassLevel)
                    }
                )
                .SetDescription(description)
                .AddFacts(
                    facts: new() { Guids.PrescientAttackAbility },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .Configure();
        }

        private static void configurePrescientAttackAbility(bool enabled)
        {
            logger.Info("       Configuring prescient attack ability");

            string name = "PrescientAttackAbility";
            string description = "MagusFeatures.ArcanaSelection.PrescientAttackAbility.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.PrescientAttackAbility).Configure();
                return;
            }

            AbilityConfigurator.New(name, Guids.PrescientAttackAbility)
                .CopyFrom(
                    blueprint: AbilityRefs.PrescientAttackAbility.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(AbilityEffectRunAction),
                        typeof(AbilitySpawnFx),
                        typeof(ContextCalculateSharedValue)
                    }
                )
                .SetDescription(description)
                .AddAbilityResourceLogic(
                    requiredResource: Guids.ArcanePoolResource,
                    amount: 1,
                    costIsCustom: false,
                    isSpendResource: true,
                    resourceCostIncreasingFacts: new()
                )
                .Configure();
        }
    }
}
