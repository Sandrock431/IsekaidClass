using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus.SwordSaint
{
    internal class PerfectStrikeAbility
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(PerfectStrikeAbility));

        private static readonly string AbilityName = "MagusFeatures.SwordSaint.PerfectStrikeAbility.Name";

        public static void Configure()
        {
            logger.Info("Configuring perfect strike ability");

            ActivatableAbilityConfigurator.New(AbilityName, Guids.PerfectStrikeAbility)
                .SetDisplayName(AbilityName)
                .SetDescription("MagusFeatures.SwordSaint.PerfectStrikeAbility.Description")
                .SetDescriptionShort("")
                .SetIcon(AbilityRefs.PerfectStrikeAbility.Reference.Get().Icon)
                .AddActivatableAbilityResourceLogic(
                    requiredResource: Guids.ArcanePoolResource,
                    spendType: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic.ResourceSpendType.AttackHit
                )
                .SetBuff(BuffRefs.SwordSaintPerfectStrikeBuff.Reference.Get())
                .SetAllowNonContextActions(false)
                .SetGroup(Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityGroup.None)
                .SetWeightInGroup(1)
                .SetIsOnByDefault(true)
                .SetDeactivateIfCombatEnded(false)
                .SetDeactivateAfterFirstRound(false)
                .SetDeactivateImmediately(true)
                .SetIsTargeted(false)
                .SetDeactivateIfOwnerDisabled(false)
                .SetDeactivateIfOwnerUnconscious(false)
                .SetOnlyInCombat(false)
                .SetDoNotTurnOffOnRest(false)
                .SetActivationType(Kingmaker.UnitLogic.ActivatableAbilities.AbilityActivationType.Immediately)
                .SetActivateOnUnitAction(Kingmaker.UnitLogic.ActivatableAbilities.AbilityActivateOnUnitActionType.Attack)
                .Configure();
        }
    }
}
