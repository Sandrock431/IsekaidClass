using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.UnitLogic.ActivatableAbilities;

namespace Patches
{
    internal class Miscellaneous
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(Miscellaneous));

        public static void Configure()
        {
            logger.Info("Applying miscellaneous patches");

            patchIncenseFog();
            patchBardSongs();
            patchSkaldSongs();
        }

        private static void patchIncenseFog()
        {
            logger.Info("   Patching incense fog");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.IncenseFogToggleAbility.Reference.Get())
                .SetDeactivateIfCombatEnded(false)
                .Configure();

            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.IncenseFog30ToggleAbility.Reference.Get())
                .SetDeactivateIfCombatEnded(false)
                .Configure();
        }

        private static void patchBardSongs()
        {
            logger.Info("   Patching bard songs");

            logger.Info("       Patching inspire courage song");          
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.InspireCourageToggleAbility.Reference.Get())
                .SetGroup(ActivatableAbilityGroup.None)
                .SetDeactivateIfCombatEnded(false)
                .Configure();

            logger.Info("       Patching inspire competence song");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.InspireCompetenceToggleAbility.Reference.Get())
                .SetGroup(ActivatableAbilityGroup.None)
                .SetDeactivateIfCombatEnded(false)
                .Configure();

            logger.Info("       Patching fascinate song");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.FascinateToggleAbility.Reference.Get())
                .SetGroup(ActivatableAbilityGroup.None)
                .SetDeactivateIfCombatEnded(false)
                .Configure();

            logger.Info("       Patching dirge of doom song");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.DirgeOfDoomToggleAbility.Reference.Get())
                .SetGroup(ActivatableAbilityGroup.None)
                .SetDeactivateIfCombatEnded(false)
                .Configure();

            logger.Info("       Patching inspire greatness song");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.InspireGreatnessToggleAbility.Reference.Get())
                .SetGroup(ActivatableAbilityGroup.None)
                .SetDeactivateIfCombatEnded(false)
                .Configure();

            logger.Info("       Patching frightening tune song");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.FrighteningTuneToggleAbility.Reference.Get())
                .SetGroup(ActivatableAbilityGroup.None)
                .SetDeactivateIfCombatEnded(false)
                .Configure();

            logger.Info("       Patching inspire heroics song");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.InspireHeroicsToggleAbility.Reference.Get())
                .SetGroup(ActivatableAbilityGroup.None)
                .SetDeactivateIfCombatEnded(false)
                .Configure();
        }

        private static void patchSkaldSongs()
        {
            logger.Info("   Patching skald songs");

            logger.Info("       Patching inspire rage song");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.InspiredRageAbility.Reference.Get())
                .SetGroup(ActivatableAbilityGroup.None)
                .SetDeactivateIfCombatEnded(false)
                .Configure();

            logger.Info("       Patching song of strength song");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.SongOfStrengthAbility.Reference.Get())
                .SetGroup(ActivatableAbilityGroup.None)
                .SetDeactivateIfCombatEnded(false)
                .Configure();

            logger.Info("       Patching dirge of doom song");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.DirgeOfDoomAbility.Reference.Get())
                .SetGroup(ActivatableAbilityGroup.None)
                .SetDeactivateIfCombatEnded(false)
                .Configure();

            logger.Info("       Patching song of the fallen song");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.SongOfTheFallenAbility.Reference.Get())
                .SetGroup(ActivatableAbilityGroup.None)
                .SetDeactivateIfCombatEnded(false)
                .Configure();

            logger.Info("       Patching insightful contemplation song");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.InsightfulContemplationSongAbility.Reference.Get())
                .SetGroup(ActivatableAbilityGroup.None)
                .SetDeactivateIfCombatEnded(false)
                .Configure();

            logger.Info("       Patching song of inspiration song");
            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.SongOfInspirationAbility.Reference.Get())
                .SetGroup(ActivatableAbilityGroup.None)
                .SetDeactivateIfCombatEnded(false)
                .Configure();
        }
    }
}
