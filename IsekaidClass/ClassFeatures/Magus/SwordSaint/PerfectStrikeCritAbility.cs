using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Utils;

namespace Isekaid.ClassFeatures.Magus.SwordSaint
{
    internal class PerfectStrikeCritAbility
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(PerfectStrikeCritAbility));

        private static readonly string AbilityName = "MagusFeatures.SwordSaint.PerfectStrikeCritAbility.Name";

        public static void Configure()
        {
            logger.Info("Configuring perfect strike crit ability");

            ActivatableAbilityConfigurator.New(AbilityName, Guids.PerfectStrikeCritAbility)
                .SetDisplayName(AbilityName)
                .SetDescription("MagusFeatures.SwordSaint.PerfectStrikeCritAbility.Description")
                .SetDescriptionShort("")
                .SetIcon(AbilityRefs.PerfectStrikeAbility.Reference.Get().Icon)
                .AddActivatableAbilityResourceLogic(
                    requiredResource: Guids.ArcanePoolResource,
                    spendType: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic.ResourceSpendType.AttackCrit
                )
                .SetBuff(BuffRefs.SwordSaintPerfectStrikeCritBuff.Reference.Get())
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
                .SetActivateWithUnitCommand(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .SetActivateOnUnitAction(Kingmaker.UnitLogic.ActivatableAbilities.AbilityActivateOnUnitActionType.Attack)
                .Configure();
        }
    }
}
