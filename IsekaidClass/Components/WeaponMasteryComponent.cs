using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;

namespace Isekaid.Components
{
    [TypeId("bb2f0c70bd334539a61a540b4e953e12")]
    public class WeaponMasteryComponent : UnitFactComponentDelegate, ITargetRulebookHandler<RuleCombatManeuver>, IRulebookHandler<RuleCombatManeuver>, IInitiatorRulebookHandler<RuleAttackRoll>, IRulebookHandler<RuleAttackRoll>, ISubscriber, IInitiatorRulebookSubscriber, IInitiatorRulebookHandler<RuleCalculateWeaponStats>, IRulebookHandler<RuleCalculateWeaponStats>, ITargetRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleAttackRoll evt)
        {
            evt.AutoCriticalConfirmation = true;
        }

        public void OnEventDidTrigger(RuleAttackRoll evt)
        {
        }

        public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
        {
            evt.AdditionalCriticalMultiplier.Add(new Modifier(1, base.Fact, ModifierDescriptor.UntypedStackable));
        }

        public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
        {
        }

        public void OnEventAboutToTrigger(RuleCombatManeuver evt)
        {
            if (evt.Type != CombatManeuver.Disarm)
            {
                return;
            }

            evt.AutoFailure = true;
        }

        public void OnEventDidTrigger(RuleCombatManeuver evt)
        {
        }
    }
}
