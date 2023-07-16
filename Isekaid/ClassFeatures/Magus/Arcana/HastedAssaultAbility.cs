using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus.Arcana
{
    internal class HastedAssaultAbility
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(HastedAssaultAbility));

        private static readonly string AbilityName = "MagusFeatures.ArcanaSelection.HastedAssaultAbility.Name";

        public static void Configure()
        {
            logger.Info("Configuring hasted assault ability");

            AbilityConfigurator.New(AbilityName, Guids.HastedAssaultAbility)
                .SetDisplayName(AbilityName)
                .SetDescription("MagusFeatures.ArcanaSelection.HastedAssaultAbility.Description")
                .SetDescriptionShort("")
                .SetIcon(AbilityRefs.HastedAssaultAbility.Reference.Get().Icon)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuff(
                            buff: BuffRefs.HasteBuff.Reference.Get(),
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
                    anchor: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxAnchor.Caster,
                    delay: 0,
                    destroyOnCast: false,
                    orientationAnchor: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxAnchor.None,
                    orientationMode: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxOrientation.Copy,
                    positionAnchor: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxAnchor.None,
                    prefabLink: "3cf0849b9b4e4344a8b528ff62b28d4b",
                    time: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxTime.OnApplyEffect,
                    weaponTarget: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxWeaponTarget.None
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
                        stat: Kingmaker.EntitySystem.Stats.StatType.Charisma,
                        type: Kingmaker.Enums.AbilityRankType.Default,
                        min: 0,
                        max: 20
                    )
                )
                .SetAllowNonContextActions(false)
                .SetType(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.Extraordinary)
                .SetRange(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityRange.Personal)
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
                .SetEffectOnAlly(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityEffectOnUnit.None)
                .SetEffectOnEnemy(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityEffectOnUnit.None)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Self)
                .SetHasFastAnimation(false)
                .SetTargetMapObjects(false)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
                .SetAvailableMetamagic(
                    Metamagic.Heighten
                )
                .SetIsFullRoundAction(false)
                .SetLocalizedDuration("MagusFeatures.ArcanaSelection.HastedAssaultAbility.Duration")
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
    }
}
