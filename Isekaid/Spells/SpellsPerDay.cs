using BlueprintCore.Blueprints.Configurators.Classes.Spells;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.Spells
{
    internal class SpellsPerDay
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(SpellsPerDay));

        private static readonly string SpellsTableName = "Isekaid.SpellsPerDay.Name";

        public static void ConfigureDisabled()
        {
            SpellsTableConfigurator.New(SpellsTableName, Guids.IsekaidClassSpellsPerDay).Configure();
        }
        public static void ConfigureEnabled()
        {
            logger.Info("Configuring spells per day");

            //var maxLevelSpells = CreateSpellLevelEntry(0, 7, 7, 7, 7, 7, 7, 7, 7, 7);
            var maxLevelSpells = CreateSpellLevelEntry(0, 99, 99, 99, 99, 99, 99, 99, 99, 99);

            SpellsTableConfigurator.New(SpellsTableName, Guids.IsekaidClassSpellsPerDay)
                .SetLevels(
                    new SpellsLevelEntry(),
                    // Entries for bubble buff workaround
                    // 1st level
                    CreateSpellLevelEntry(0, 99),
                    CreateSpellLevelEntry(0, 99),
                    // 2nd level
                    CreateSpellLevelEntry(0, 99, 99),
                    CreateSpellLevelEntry(0, 99, 99),
                    // 3rd level
                    CreateSpellLevelEntry(0, 99, 99, 99),
                    CreateSpellLevelEntry(0, 99, 99, 99),
                    // 4th level
                    CreateSpellLevelEntry(0, 99, 99, 99, 99),
                    CreateSpellLevelEntry(0, 99, 99, 99, 99),
                    // 5th level
                    CreateSpellLevelEntry(0, 99, 99, 99, 99, 99),
                    CreateSpellLevelEntry(0, 99, 99, 99, 99, 99),
                    // 6th level
                    CreateSpellLevelEntry(0, 99, 99, 99, 99, 99, 99),
                    CreateSpellLevelEntry(0, 99, 99, 99, 99, 99, 99),
                    // 7th level
                    CreateSpellLevelEntry(0, 99, 99, 99, 99, 99, 99, 99),
                    CreateSpellLevelEntry(0, 99, 99, 99, 99, 99, 99, 99),
                    // 8th level
                    CreateSpellLevelEntry(0, 99, 99, 99, 99, 99, 99, 99, 99),
                    CreateSpellLevelEntry(0, 99, 99, 99, 99, 99, 99, 99, 99),
                    // 9th level
                    CreateSpellLevelEntry(0, 99, 99, 99, 99, 99, 99, 99, 99, 99),
                    maxLevelSpells,
                    maxLevelSpells,
                    /* Entries for normal, memorized spells
                    // 1st level
                    CreateSpellLevelEntry(0, 4),
                    CreateSpellLevelEntry(0, 5),
                    // 2nd level
                    CreateSpellLevelEntry(0, 5, 4),
                    CreateSpellLevelEntry(0, 6, 5),
                    // 3rd level
                    CreateSpellLevelEntry(0, 6, 5, 4),
                    CreateSpellLevelEntry(0, 6, 6, 5),
                    // 4th level
                    CreateSpellLevelEntry(0, 7, 6, 5, 4),
                    CreateSpellLevelEntry(0, 7, 6, 6, 5),
                    // 5th level
                    CreateSpellLevelEntry(0, 7, 7, 6, 5, 4),
                    CreateSpellLevelEntry(0, 7, 7, 6, 6, 5),
                    // 6th level
                    CreateSpellLevelEntry(0, 7, 7, 7, 6, 5, 4),
                    CreateSpellLevelEntry(0, 7, 7, 7, 6, 6, 5),
                    // 7th level
                    CreateSpellLevelEntry(0, 7, 7, 7, 7, 6, 5, 4),
                    CreateSpellLevelEntry(0, 7, 7, 7, 7, 6, 6, 5),
                    // 8th level
                    CreateSpellLevelEntry(0, 7, 7, 7, 7, 7, 6, 5, 4),
                    CreateSpellLevelEntry(0, 7, 7, 7, 7, 7, 6, 6, 5),
                    // 9th level
                    CreateSpellLevelEntry(0, 7, 7, 7, 7, 7, 7, 6, 5, 4),
                    CreateSpellLevelEntry(0, 7, 7, 7, 7, 7, 7, 6, 6, 5),
                    CreateSpellLevelEntry(0, 7, 7, 7, 7, 7, 7, 7, 6, 6),
                    */
                    maxLevelSpells,
                    // Levels 21-40
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells,
                    maxLevelSpells
                )
                .Configure();
        }

        private static SpellsLevelEntry CreateSpellLevelEntry(params int[] count)
        {
            var entry = new SpellsLevelEntry
            {
                Count = count
            };
            return entry;
        }
    }
}
