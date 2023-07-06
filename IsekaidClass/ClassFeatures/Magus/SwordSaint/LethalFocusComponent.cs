using BlueprintCore.Utils;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using System;

namespace Isekaid.ClassFeatures.Magus.SwordSaint
{
    internal class LethalFocusComponent : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>, ISubscriber, IInitiatorRulebookSubscriber
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(LethalFocusComponent));

        public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
        {
            evt.WeaponStats.AddDamageModifier(Math.Max(0, Owner.Stats.Charisma.Bonus), Fact, ModifierDescriptor.UntypedStackable);
        }

        public void OnEventDidTrigger(RuleAttackWithWeapon evt)
        {
        }
    }
}
