using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using IsekaidClass.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.RuleSystem;
using BlueprintCore.Utils.Types;
using Kingmaker.EntitySystem.Stats;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using Kingmaker.Utility;
using Kingmaker.Enums;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Blueprints.Classes.Prerequisites;

namespace IsekaidClass.Isekaid.ClassFeatures.Alchemist
{
    internal class Bombs
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(Bombs));

        private static readonly string FeatureName = "AlchemistFeatures.Bombs.Name";
        private static readonly string DisplayName = "AlchemistFeatures.Bombs.DisplayName";
        private static readonly string Description = "AlchemistFeatures.Bombs.Description";

        public static void ConfigureDisabled()
        {
            configureBombsResource(enabled: false);
            configureBombsFact(enabled: false);
            configureBombsStandard(enabled: false);
            configureDirectedBlastFeature(enabled: false);
            configureDirectedBlastAbility(enabled: false);
            configureStaggeringBlast(enabled: false);

            FeatureConfigurator.New(FeatureName, Guids.Bombs).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring bombs");

            configureBombsResource(enabled: true);
            configureBombsFact(enabled: true);
            configureBombsStandard(enabled: true);
            configureDirectedBlastFeature(enabled: true);
            configureDirectedBlastAbility(enabled: true);
            configureStaggeringBlast(enabled: true);

            FeatureConfigurator.New(FeatureName, Guids.Bombs)
                .CopyFrom(FeatureRefs.AlchemistBombsFeature.Reference.Get())
                .SetDescription(Description)
                .SetDisplayName(DisplayName)
                .AddCalculatedWeapon(
                    weapon: new CalculatedWeapon()
                    {
                        m_Weapon = BlueprintTool.GetRef<BlueprintItemWeaponReference>(ItemWeaponRefs.BombItem.ToString()),
                        //m_Weapon = ItemWeaponRefs.BombItem.Reference.Get(),
                        DamageFormula = new DiceFormula(
                            rollsCount: 1,
                            diceType: DiceType.D6
                        ),
                        ReplacedDamageStat = true,
                        DamageBonusStat = StatType.Charisma,
                        ReplaceDamageWithText = false
                    },
                    scaleDamageByRank: true
                )
                .AddBindAbilitiesToClass(
                    abilites: new()
                    {
                        Guids.DirectedBlast,
                        Guids.TanglefootBomb,
                        Guids.HolyBomb,
                        Guids.ForceBomb,
                        Guids.ExplosiveBomb,
                        Guids.DispellingBomb,
                        Guids.CurseDeteriorationBomb,
                        Guids.CurseFeebleBodyBomb,
                        Guids.CurseIdiocyBomb,
                        Guids.CurseWeaknessBomb,
                        Guids.ChokingBomb,
                        Guids.BreathWeaponBomb,
                        Guids.BlindingBomb,
                        Guids.AcidBomb,
                        Guids.BombsStandard,
                        Guids.FrostBomb,
                    },
                    cantrip: false,
                    characterClass: BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass),
                    stat: StatType.Charisma,
                    levelStep: 2,
                    odd: true,
                    fullCasterChecks: true
                )
                .AddFactSinglify(
                    newFacts: new()
                    {
                        Guids.BombsFact,
                        Guids.BombsStandard
                    }
                )
                .SetIsPrerequisiteFor(
                    FeatureRefs.BombAbilityFocus.Reference.Get(),
                    FeatureRefs.ExtraBombs.Reference.Get(),
                    Guids.AcidBombsFeature,
                    Guids.BlindingBombsFeature,
                    Guids.BreathWeaponBombFeature,
                    Guids.ChokingBombFeature,
                    Guids.CursedBombsFeature,
                    Guids.DispellingBombsFeature,
                    Guids.ExplosiveBombsFeature,
                    Guids.ForceBombsFeature,
                    Guids.FrostBombsFeature,
                    Guids.HolyBombsFeature,
                    Guids.ShockBombsFeature,
                    Guids.TanglefootBombsFeature
                )
                .Configure();
        }

        private static void configureBombsFact(bool enabled)
        {
            logger.Info("   Configuring bombs fact");

            string name = "AlchemistFeatures.Bombs.BombsFact.Name";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.BombsFact).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.BombsFact)
                .CopyFrom(FeatureRefs.AlchemistBombsFact.Reference.Get())
                .AddAbilityResources(
                    useThisAsResource: false,
                    resource: Guids.BombsResource,
                    amount: 0,
                    restoreAmount: true,
                    restoreOnLevelUp: false
                )
                .Configure();
        }

        private static void configureBombsResource(bool enabled)
        {
            logger.Info("   Configuring bombs resource");

            string name = "AlchemistFeatures.Bombs.BombsResource.Name";

            if (!enabled)
            {
                AbilityResourceConfigurator.New(name, Guids.BombsResource).Configure();
                return;
            }

            AbilityResourceConfigurator.New(name, Guids.BombsResource)
                .CopyFrom(AbilityResourceRefs.AlchemistBombsResource.Reference.Get())
                .SetMaxAmount(
                    new BlueprintAbilityResource.Amount
                    {
                        BaseValue = 0,
                        IncreasedByLevel = true,
                        m_Class = new[] { BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass) },
                        LevelIncrease = 1,
                        IncreasedByLevelStartPlusDivStep = false,
                        StartingLevel = 0,
                        StartingIncrease = 0,
                        LevelStep = 0,
                        PerStepIncrease = 0,
                        MinClassLevelIncrease = 0,
                        OtherClassesModifier = 0,
                        IncreasedByStat = true,
                        ResourceBonusStat = StatType.Charisma
                    }
                )
                .Configure();
        }

        private static void configureBombsStandard(bool enabled)
        {
            logger.Info("   Configuring bombs standard ability");

            string name = "AlchemistFeatures.Bombs.BombsStandard.Name";
            string displayName = "AlchemistFeatures.Bombs.DisplayName";
            string description = "AlchemistFeatures.Bombs.BombsStandard.Description";

            if (!enabled)
            {
                AbilityConfigurator.New(name, Guids.BombsStandard).Configure();
                return;
            }

            AbilityConfigurator.New(name, Guids.BombsStandard)
                .CopyFrom(
                    blueprint: AbilityRefs.BombStandart.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(AbilityDeliverProjectile),
                        typeof(AbilityEffectRunAction),
                        typeof(AbilityEffectMiss),
                        typeof(SpellDescriptorComponent),
                        typeof(AbilityCasterNotPolymorphed),
                        typeof(AbilityIsBomb)
                    }
                )
                .SetDisplayName(displayName)
                .SetDescription(description)
                .AddAbilityResourceLogic(
                    requiredResource: Guids.BombsResource,
                    isSpendResource: true,
                    costIsCustom: false,
                    amount: 1
                )
                .AddAbilityTargetsAround(
                    radius: new Feet() { m_Value = 8 },
                    targetType: TargetType.Any,
                    includeDead: false,
                    condition: ConditionsBuilder.New()
                        .UseOr()
                        .CasterHasFact(
                            fact: FeatureRefs.PreciseBomb.Reference.Get(),
                            negate: true
                        )
                        .IsEnemy(),
                    spreadSpeed: new Feet() { m_Value = 0 }
                )
                .AddContextRankConfig(
                    ContextRankConfigs.FeatureRank(
                        feature: Guids.Bombs,
                        useMaster: false,
                        type: AbilityRankType.DamageDice
                    )
                )
                .AddContextRankConfig(
                    ContextRankConfigs.StatBonus(
                        type: AbilityRankType.DamageBonus,
                        stat: StatType.Charisma,
                        min: 0,
                        max: 20
                    )
                )
                .AddContextRankConfig(
                    ContextRankConfigs.StatBonus(
                        type: AbilityRankType.DamageDiceAlternative,
                        stat: StatType.Charisma,
                        min: 0,
                        max: 20
                    )
                    .WithMultiplyByModifierProgression(2)
                )
                .AddActionPanelLogic(
                    priority: 2,
                    autoCastConditions: ConditionsBuilder.New()
                        .CharacterClass(
                            checkCaster: false,
                            clazz: BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass),
                            minLevel: 0
                        )
                )
                .Configure();
        }

        private static void configureDirectedBlastFeature(bool enabled)
        {
            logger.Info("   Configuring directed blast feature");

            string name = "AlchemistFeatures.Bombs.DirectedBlastFeature.Name";
            string description = "AlchemistFeatures.Bombs.DirectedBlast.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.DirectedBlastFeature).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.DirectedBlastFeature)
                .CopyFrom(FeatureRefs.GrenadierDirectedBlastFeature.Reference.Get())
                .AddFacts(
                    facts: new () { Guids.DirectedBlast },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: Kingmaker.Settings.GameDifficultyOption.Story
                )
                .SetDescription(description)
                .Configure();
        }

        private static void configureDirectedBlastAbility(bool enabled)
        {
            logger.Info("   Configuring directed blast ability");

            string name = "AlchemistFeatures.Bombs.DirectedBlast.Name";
            string description = "AlchemistFeatures.Bombs.DirectedBlast.Description";

            if (!enabled)
            {
                AbilityConfigurator.New(name, Guids.DirectedBlast).Configure();
                return;
            }

            AbilityConfigurator.New(name, Guids.DirectedBlast)
                .CopyFrom(
                    blueprint: AbilityRefs.GrenadierDirectedBlastAbility.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(AbilityDeliverProjectile),
                        typeof(AbilityEffectRunAction),
                        typeof(SpellDescriptorComponent),
                        typeof(AbilityCasterNotPolymorphed),
                    }
                )
                .SetDescription(description)
                .AddAbilityResourceLogic(
                    requiredResource: Guids.BombsResource,
                    isSpendResource: true,
                    costIsCustom: false,
                    amount: 1
                )
                .AddContextRankConfig(
                    ContextRankConfigs.StatBonus(
                        type: AbilityRankType.DamageBonus,
                        stat: StatType.Charisma,
                        min: 0,
                        max: 20
                    )
                )
                .Configure();
        }

        private static void configureStaggeringBlast(bool enabled)
        {
            logger.Info("   Configuring staggering blast feature");

            string name = "AlchemistFeatures.Bombs.StaggeringBlast.Name";
            string description = "AlchemistFeatures.Bombs.StaggeringBlast.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.StaggeringBlast).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.StaggeringBlast)
                .CopyFrom(
                    blueprint: FeatureRefs.GrenadierStaggeringBlast.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(AddInitiatorAttackRollTrigger)
                    }
                )
                .AddContextCalculateAbilityParamsBasedOnClass(
                    useKineticistMainStat: false,
                    statType: StatType.Charisma,
                    characterClass: BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                )
                .AddRecalculateOnStatChange(
                    useKineticistMainStat: false,
                    stat: StatType.Charisma
                )
                .SetDescription(description)
                .Configure();
        }

        private static void patchBombAbilityFocus()
        {
            logger.Info("   Patching bomb ability focus");

            FeatureConfigurator.For(FeatureRefs.BombAbilityFocus.Reference.Get())
                .AddPrerequisiteFeature(
                    feature: Guids.Bombs,
                    checkInProgression: false,
                    group: Prerequisite.GroupType.Any,
                    hideInUI: false
                )
                .AddRecommendationHasFeature(
                    feature: Guids.Bombs,
                    mandatory: false
                )
                .Configure();
        }

        private static void patchExtraBombs()
        {
            logger.Info("   Patching extra bombs");

            FeatureConfigurator.For(FeatureRefs.ExtraBombs.Reference.Get())
                .AddIncreaseResourceAmount(
                    resource: Guids.BombsResource,
                    value: 4
                )
                .EditComponent<PrerequisiteFeaturesFromList>(
                    c => c.m_Features = CommonTool.Append(
                        c.m_Features,
                        BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Bombs)
                    )
                )
                .Configure();
        }
    }
}
