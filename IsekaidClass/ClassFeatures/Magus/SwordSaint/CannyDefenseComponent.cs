using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using System;
using Utils;

namespace Isekaid.ClassFeatures.Magus.SwordSaint
{
    internal class CannyDefenseComponent : UnitFactComponentDelegate, IGlobalSubscriber, ISubscriber
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(CannyDefenseComponent));

        public override void OnTurnOn()
        {
            base.OnTurnOn();
            ActivateModifier();
        }

        public override void OnTurnOff()
        {
            base.OnTurnOff();
            DeactivateModifier();
        }

        private void ActivateModifier()
        {
            int value = Math.Min(
                Owner.Stats.Charisma.Bonus,
                Owner.Progression.GetClassLevel(BlueprintTool.Get<BlueprintCharacterClass>(Guids.IsekaidClass))
            );
            Owner.Stats.AC.AddModifierUnique(value, Runtime, ModifierDescriptor.Dodge);
        }

        private void DeactivateModifier()
        {
            Owner.Stats.AC.RemoveModifiersFrom(Runtime);
        }
    }
}
