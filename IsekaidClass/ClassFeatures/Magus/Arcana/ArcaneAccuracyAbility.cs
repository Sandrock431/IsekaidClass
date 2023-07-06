using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Mechanics;
using System;
using Utils;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;
using static Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell;

namespace Isekaid.ClassFeatures.Magus.Arcana
{
    internal class ArcaneAccuracyAbility
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(ArcaneAccuracyAbility));

        private static readonly string AbilityName = "MagusFeatures.ArcanaSelection.ArcaneAccuracyAbility.Name";
        internal const string DisplayName = "MagusFeatures.Name";
        private static readonly string Description = "MagusFeatures.Description";

        public static void Configure()
        {
            logger.Info("Configuring arcane accuracy ability");

            AbilityConfigurator.New(AbilityName, Guids.ArcaneAccuracyAbility)
                .SetDisplayName(AbilityName)
                .SetDescription(Description)
                .SetDescriptionShort("")
                .SetIcon(AbilityRefs.ArcaneAccuracyAbility.Reference.Get().Icon)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuff(
                            buff: BuffRefs.ArcaneAccuracyBuff.Reference.Get(),
                            durationValue: ContextDuration.Variable(
                                value: ContextValues.Rank(),
                                rate: DurationRate.Rounds,
                                isExtendable: false
                            ),
                            isFromSpell: false,
                            isNotDispelable: false,
                            toCaster: false,
                            asChild: true,
                            sameDuration: false
                        )
                )
                .AddAbilitySpawnFx(
                    anchor: AbilitySpawnFxAnchor.Caster,
                    delay: 0,
                    destroyOnCast: false,
                    orientationAnchor: AbilitySpawnFxAnchor.None,
                    orientationMode: AbilitySpawnFxOrientation.Copy,
                    positionAnchor: AbilitySpawnFxAnchor.None,
                    prefabLink: "c388856d0e8855f429a83ccba67944ba",
                    time: AbilitySpawnFxTime.OnApplyEffect,
                    weaponTarget: AbilitySpawnFxWeaponTarget.None
                )
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
                )
                .AddContextCalculateSharedValue(
                    valueType: AbilitySharedValue.StatBonus,
                    value: getDiceValue(),
                    modifier: 1
                )
                .SetAllowNonContextActions(false)
                .SetType(AbilityType.Extraordinary)
                .SetRange(AbilityRange.Personal)
                .SetCustomRange(0)
                .SetShowNameForVariant(false)
                .SetOnlyForAllyCaster(false)
                .SetCanTargetPoint(false)
                .SetCanTargetEnemies(false)
                .SetCanTargetFriends(false)
                .SetCanTargetSelf(true)
                .SetSpellResistance(false)
                .SetActionBarAutoFillIgnored(false)
                .SetHidden(false)
                .SetNeedEquipWeapons(false)
                .SetNotOffensive(false)
                .SetEffectOnAlly(AbilityEffectOnUnit.None)
                .SetEffectOnEnemy(AbilityEffectOnUnit.None)
                .SetAnimation(CastAnimationStyle.Self)
                .SetHasFastAnimation(false)
                .SetTargetMapObjects(false)
                .SetActionType(CommandType.Swift)
                .SetAvailableMetamagic(
                    Metamagic.Heighten
                )
                .SetIsFullRoundAction(false)
                .SetLocalizedDuration("MagusFeatures.ArcanaSelection.ArcaneAccuracyAbility.Duration")
                .SetAutoUseIsForbidden(false)
                .SetIgnoreMinimalRangeLimit(false)
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
                .SetResourceAssetIds(
                    "3cf0849b9b4e4344a8b528ff62b28d4b",
                    "91ef30ab58fa0d3449d4d2ccc20cb0f8"
                )
                .Configure();
        }

        private static ContextDiceValue getDiceValue()
        {
            var diceValue = new ContextDiceValue();
            diceValue.DiceType = DiceType.Zero;
            diceValue.BonusValue = 0;
            diceValue.DiceCountValue = 0;

            return diceValue;
        }
    }
}
