using BlueprintCore.Utils;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using System;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus.SwordSaint
{
    internal class CriticalPerfectionComponent : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackRoll>, IRulebookHandler<RuleAttackRoll>, ISubscriber, IInitiatorRulebookSubscriber
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(CriticalPerfectionComponent));

        public void OnEventAboutToTrigger(RuleAttackRoll evt)
        {
            evt.CriticalConfirmationBonus += Math.Max(0, Owner.Stats.Charisma.Bonus);
        }

        public void OnEventDidTrigger(RuleAttackRoll evt)
        {
        }
    }
}
