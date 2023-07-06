using BlueprintCore.Utils;
using BlueprintCore.Blueprints.Configurators.Classes.Spells;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Utils;

namespace Isekaid.Spells
{
    internal class SpellBook
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(SpellBook));

        private static readonly string SpellBookName = "Isekaid.SpellBook.Name";

        public static void ConfigureDisabled()
        {
            SpellsPerDay.ConfigureDisabled();
            SpellList.ConfigureDisabled();

            SpellbookConfigurator.New(SpellBookName, Guids.IsekaidClassSpellBook).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring spellbook");

            SpellsPerDay.ConfigureEnabled();
            SpellList.ConfigureEnabled();

            SpellbookConfigurator.New(SpellBookName, Guids.IsekaidClassSpellBook)
                .SetName(SpellBookName)
                .SetSpellsPerDay(Guids.IsekaidClassSpellsPerDay)
                .SetSpellList(Guids.IsekaidClassSpellList)
                .SetCharacterClass(Guids.IsekaidClass)
                .SetCastingAttribute(StatType.Charisma)
                //.SetSpontaneous(false)
                .SetSpontaneous(true)
                //.SetSpellsPerLevel(0)
                .SetSpellsPerLevel(0)
                .SetAllSpellsKnown(true)
                .SetCantripsType(CantripsType.Cantrips)
                .SetCasterLevelModifier(0)
                .SetCanCopyScrolls(false)
                .SetIsArcane(false)
                .SetIsArcanist(false)
                .SetHasSpecialSpellList(false)
                .Configure();
        }
    }
}
