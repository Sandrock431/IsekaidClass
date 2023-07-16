using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Mechanics;
using IsekaidClass.Utils;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;
using static Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus
{
    internal class ArcaneWeaponSwitchAbility
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(ArcaneWeaponSwitchAbility));

        private static readonly string AbilityName = "MagusFeatures.ArcaneWeaponSwitchAbility.Name";

        public static void Configure()
        {
            logger.Info("Configuring arcane weapon switch ability");

            AbilityConfigurator.New(AbilityName, Guids.ArcaneWeaponSwitchAbility)
                .SetDisplayName(AbilityName)
                .SetDescription("MagusFeatures.ArcaneWeaponSwitchAbility.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.ArcanePoolFeature.Reference.Get().Icon)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .Conditional(
                            conditions: ConditionsBuilder.New()
                                .CasterHasFact(BuffRefs.EnduringBladeBuff.Reference.Get()),
                            ifTrue: ActionsBuilder.New()
                                .WeaponEnchantPool(
                                    enchantPool: Kingmaker.UnitLogic.FactLogic.EnchantPoolType.ArcanePool,
                                    group: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityGroup.ArcaneWeaponProperty,
                                    enchantmentPlus1: WeaponEnchantmentRefs.TemporaryEnhancement1.Reference.Get(),
                                    enchantmentPlus2: WeaponEnchantmentRefs.TemporaryEnhancement2.Reference.Get(),
                                    enchantmentPlus3: WeaponEnchantmentRefs.TemporaryEnhancement3.Reference.Get(),
                                    enchantmentPlus4: WeaponEnchantmentRefs.TemporaryEnhancement4.Reference.Get(),
                                    enchantmentPlus5: WeaponEnchantmentRefs.TemporaryEnhancement5.Reference.Get(),
                                    durationValue: ContextDuration.Variable(
                                        value: ContextValues.Rank(),
                                        rate: DurationRate.Minutes,
                                        isExtendable: true
                                    )
                                )
                                .ApplyBuff(
                                    buff: BuffRefs.ArcaneWeaponEnchantDurationBuff.Reference.Get(),
                                    durationValue: ContextDuration.Variable(
                                        value: ContextValues.Rank(),
                                        rate: DurationRate.Minutes,
                                        isExtendable: false
                                    ),
                                    asChild: true,
                                    ignoreParentContext: false,
                                    isFromSpell: false,
                                    isNotDispelable: false,
                                    notLinkToAreaEffect: false,
                                    sameDuration: false,
                                    toCaster: false
                                ),
                            ifFalse: ActionsBuilder.New()
                                .WeaponEnchantPool(
                                    enchantPool: Kingmaker.UnitLogic.FactLogic.EnchantPoolType.ArcanePool,
                                    group: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityGroup.ArcaneWeaponProperty,
                                    enchantmentPlus1: WeaponEnchantmentRefs.TemporaryEnhancement1.Reference.Get(),
                                    enchantmentPlus2: WeaponEnchantmentRefs.TemporaryEnhancement2.Reference.Get(),
                                    enchantmentPlus3: WeaponEnchantmentRefs.TemporaryEnhancement3.Reference.Get(),
                                    enchantmentPlus4: WeaponEnchantmentRefs.TemporaryEnhancement4.Reference.Get(),
                                    enchantmentPlus5: WeaponEnchantmentRefs.TemporaryEnhancement5.Reference.Get(),
                                    durationValue: ContextDuration.VariableDice(
                                        diceType: Kingmaker.RuleSystem.DiceType.Zero,
                                        diceCount: 0,
                                        bonus: 1,
                                        rate: DurationRate.Minutes,
                                        isExtendable: false
                                    )
                                )
                                .ApplyBuff(
                                    buff: BuffRefs.ArcaneWeaponEnchantDurationBuff.Reference.Get(),
                                    durationValue: ContextDuration.VariableDice(
                                        diceType: Kingmaker.RuleSystem.DiceType.Zero,
                                        diceCount: 0,
                                        bonus: 1,
                                        rate: DurationRate.Minutes,
                                        isExtendable: false
                                    ),
                                    asChild: true,
                                    ignoreParentContext: false,
                                    isFromSpell: false,
                                    isNotDispelable: false,
                                    notLinkToAreaEffect: false,
                                    sameDuration: false,
                                    toCaster: false
                                )
                        )
                )
                .AddAbilityResourceLogic(
                    requiredResource: Guids.ArcanePoolResource,
                    amount: 1,
                    costIsCustom: false,
                    isSpendResource: true,
                    resourceCostIncreasingFacts: new ()
                    {
                        BuffRefs.ArcaneWeaponBaneBuff.Reference.Get(),
                        BuffRefs.EnduringBladeBuff.Reference.Get(),
                    }
                )
                .AddContextRankConfig(                    
                    component: ContextRankConfigs.ClassLevel(
                            classes: new string[] { Guids.IsekaidClass },
                            type: Kingmaker.Enums.AbilityRankType.Default,
                            max: 20,
                            min: 0
                        )                  
                )
                .AddAbilitySpawnFx(
                    anchor: AbilitySpawnFxAnchor.Caster,
                    delay: 0,
                    destroyOnCast: false,
                    orientationAnchor: AbilitySpawnFxAnchor.None,
                    orientationMode: AbilitySpawnFxOrientation.Copy,
                    positionAnchor: AbilitySpawnFxAnchor.None,
                    prefabLink: "b90122225a171974b8565820c92ff143",
                    time: AbilitySpawnFxTime.OnPrecastStart,
                    weaponTarget: AbilitySpawnFxWeaponTarget.All
                )
                .SetAllowNonContextActions(false)
                .SetAutoUseIsForbidden(false)
                .SetType(AbilityType.Supernatural)
                .SetRange(AbilityRange.Personal)
                .SetIgnoreMinimalRangeLimit(false)
                .SetCustomRange(0)
                .SetShowNameForVariant(false)
                .SetOnlyForAllyCaster(false)
                .SetCanTargetPoint(false)
                .SetCanTargetEnemies(false)
                .SetCanTargetFriends(false)
                .SetCanTargetSelf(true)
                .SetShouldTurnToTarget(true)
                .SetSpellResistance(false)
                .SetIgnoreSpellResistanceForAlly(false)
                .SetActionBarAutoFillIgnored(false)
                .SetHidden(false)
                .SetNeedEquipWeapons(false)
                .SetNotOffensive(false)
                .SetEffectOnAlly(AbilityEffectOnUnit.Helpful)
                .SetEffectOnEnemy(AbilityEffectOnUnit.None)
                .SetAnimation(CastAnimationStyle.Omni)
                .SetHasFastAnimation(false)
                .SetTargetMapObjects(false)
                .SetActionType(CommandType.Swift)
                .SetAvailableMetamagic(
                    Metamagic.Extend,
                    Metamagic.Heighten
                )
                .SetIsFullRoundAction(false)
                .SetLocalizedDuration("MagusFeatures.ArcaneWeaponSwitchAbility.Duration")
                .SetDisableLog(false)
                .SetResourceAssetIds("b90122225a171974b8565820c92ff143")
                .Configure();
        }
    }
}
