using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using Utils;

namespace Isekaid.ClassFeatures
{
    internal class AlchemistFeatures
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(AlchemistFeatures));

        private static readonly string ProgressionName = "AlchemistFeatures";
        internal const string DisplayName = "AlchemistFeatures.Name";
        private static readonly string Description = "AlchemistFeatures.Description";

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.AlchemistFeatures))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                logger.Error("AlchemistFeatures.Configure", e);
            }
        }

        public static void ConfigureDisabled()
        {
            configureAlchemistClassLevels(enabled: false);

            ProgressionConfigurator.New(ProgressionName, Guids.AlchemistFeatures).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring Alchemist Features progression");

            configureAlchemistClassLevels(enabled: true);

            //patchBaseMutagens();
            //patchGreaterMutagens();
            //patchGrandMutagens();
            //patchTrueMutagen();

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    Guids.AlchemistClassLevels,
                    FeatureRefs.AlchemistBombsFeature.Reference.Get(),
                    FeatureRefs.MutagenFeature.Reference.Get(),
                    FeatureRefs.BrewPotions.Reference.Get(),
                    FeatureRefs.IncenseFogFeature.Reference.Get()
                )
                .AddEntry(
                    2,
                    FeatureSelectionRefs.DiscoverySelection.Reference.Get(),
                    FeatureRefs.PreciseBomb.Reference.Get()
                )
                .AddEntry(
                    3,
                    FeatureRefs.AlchemistBombsFeature.Reference.Get(),
                    FeatureSelectionRefs.IncenseSynthesizerIncenseFogSelection.Reference.Get()
                )
                .AddEntry(
                    4,
                    FeatureSelectionRefs.DiscoverySelection.Reference.Get()
                )
                .AddEntry(
                    5,
                    FeatureRefs.AlchemistBombsFeature.Reference.Get()
                )
                .AddEntry(
                    6,
                    FeatureSelectionRefs.DiscoverySelection.Reference.Get(),
                    FeatureRefs.GrenadierDirectedBlastFeature.Reference.Get(),
                    FeatureSelectionRefs.IncenseSynthesizerIncenseFogSelection.Reference.Get()
                )
                .AddEntry(
                    7,
                    FeatureRefs.AlchemistBombsFeature.Reference.Get()
                )
                .AddEntry(
                    8,
                    FeatureSelectionRefs.DiscoverySelection.Reference.Get()
                )
                .AddEntry(
                    9,
                    FeatureRefs.AlchemistBombsFeature.Reference.Get(),
                    FeatureSelectionRefs.IncenseSynthesizerIncenseFogSelection.Reference.Get()
                )
                .AddEntry(
                    10,
                    FeatureSelectionRefs.DiscoverySelection.Reference.Get(),
                    FeatureRefs.GrenadierStaggeringBlast.Reference.Get()
                )
                .AddEntry(
                    11,
                    FeatureRefs.AlchemistBombsFeature.Reference.Get()
                )
                .AddEntry(
                    12,
                    // Greater Mutagen available
                    FeatureSelectionRefs.DiscoverySelection.Reference.Get(),
                    FeatureSelectionRefs.IncenseSynthesizerIncenseFogSelection.Reference.Get()
                )
                .AddEntry(
                    13,
                    FeatureRefs.AlchemistBombsFeature.Reference.Get()
                )
                .AddEntry(
                    14,
                    FeatureSelectionRefs.DiscoverySelection.Reference.Get(),
                    FeatureRefs.PersistantMutagen.Reference.Get()
                )
                .AddEntry(
                    15,
                    FeatureRefs.AlchemistBombsFeature.Reference.Get(),
                    FeatureSelectionRefs.IncenseSynthesizerIncenseFogSelection.Reference.Get()
                )
                .AddEntry(
                    16,
                    // Grand Mutagen available
                    FeatureSelectionRefs.DiscoverySelection.Reference.Get()
                )
                .AddEntry(
                    17,
                    FeatureRefs.AlchemistBombsFeature.Reference.Get()
                )
                .AddEntry(
                    18,
                    FeatureSelectionRefs.DiscoverySelection.Reference.Get(),
                    FeatureSelectionRefs.IncenseSynthesizerIncenseFogSelection.Reference.Get()
                )
                .AddEntry(
                    19,
                    FeatureRefs.AlchemistBombsFeature.Reference.Get()
                )
                .AddEntry(
                    20,
                    FeatureSelectionRefs.GrandDiscoverySelection.Reference.Get()
                );

            var uiGroups = UIGroupBuilder.New()
                .AddGroup(
                    FeatureRefs.MutagenFeature.Reference.Get(),
                    FeatureRefs.GreaterMutagenFeature.Reference.Get(),
                    FeatureRefs.PersistantMutagen.Reference.Get(),
                    FeatureRefs.GrandMutagenFeature.Reference.Get()
                )
                .AddGroup(
                    FeatureSelectionRefs.MutationWarriorDiscoverySelection.Reference.Get(),
                    FeatureSelectionRefs.GrandDiscoverySelection.Reference.Get()
                );

            ProgressionConfigurator.New(ProgressionName, Guids.AlchemistFeatures)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(true)
                .SetClasses(Guids.IsekaidClass)
                .SetForAllOtherClasses(false)
                .SetLevelEntries(levelEntries)
                .SetUIGroups(uiGroups)
                .Configure();
        }

        private static void configureAlchemistClassLevels(bool enabled)
        {
            logger.Info("   Configuring alchemist class levels for prerequisites");

            string name = "AlchemistClassLevels";
            string displayName = "AlchemistFeatures.AlchemistClassLevels.Name";
            string description = "AlchemistFeatures.AlchemistClassLevels.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.AlchemistClassLevels).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.AlchemistClassLevels)
                .SetDisplayName(displayName)
                .SetDescription(description)
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.BrewPotions.Reference.Get().Icon)
                .AddClassLevelsForPrerequisites(
                    actualClass: Guids.IsekaidClass,
                    fakeClass: CharacterClassRefs.AlchemistClass.Reference.Get(),
                    modifier: 1.0,
                    summand: 0
                )
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(true)
                .Configure();
        }

        private static void patchBaseMutagens()
        {
            logger.Info("   Patching base mutagens");

            // Patch base mutagens to work with class
            var mutagens = new List<Blueprint<BlueprintReference<BlueprintAbility>>>() {
                AbilityRefs.MutagenStrength.Reference.Get(),
                AbilityRefs.MutagenDexterity.Reference.Get(),
                AbilityRefs.MutagenConstitution.Reference.Get()
            };

            foreach (Blueprint<BlueprintReference<BlueprintAbility>> mutagen in mutagens)
            {
                AbilityConfigurator.For(mutagen)
                    .EditComponent<ContextRankConfig>(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    ))
                    .Configure();
            }
        }

        private static void patchGreaterMutagens()
        {
            logger.Info("   Patching greater mutagens");

            // Patch greater mutagens to work with class
            var mutagens = new List<Blueprint<BlueprintReference<BlueprintAbility>>>() {
                AbilityRefs.GreaterMutagenStrengthDexterity.Reference.Get(),
                AbilityRefs.GreaterMutagenStrengthConstitution.Reference.Get(),
                AbilityRefs.GreaterMutagenDexterityStrength.Reference.Get(),
                AbilityRefs.GreaterMutagenDexterityConstitution.Reference.Get(),
                AbilityRefs.GreaterMutagenConstitutionStrength.Reference.Get(),
                AbilityRefs.GreaterMutagenConstitutionDexterity.Reference.Get()
            };

            foreach (Blueprint<BlueprintReference<BlueprintAbility>> mutagen in mutagens)
            {
                AbilityConfigurator.For(mutagen)
                    .EditComponent<ContextRankConfig>(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    ))
                    .Configure();
            }
        }

        private static void patchGrandMutagens()
        {
            logger.Info("   Patching grand mutagens");

            // Patch greater mutagens to work with class
            var mutagens = new List<Blueprint<BlueprintReference<BlueprintAbility>>>() {
                AbilityRefs.GrandMutagenStrengthDexterity.Reference.Get(),
                AbilityRefs.GrandMutagenStrengthConstitution.Reference.Get(),
                AbilityRefs.GrandMutagenDexterityStrength.Reference.Get(),
                AbilityRefs.GrandMutagenDexterityConstitution.Reference.Get(),
                AbilityRefs.GrandMutagenConstitutionStrength.Reference.Get(),
                AbilityRefs.GrandMutagenConstitutionDexterity.Reference.Get()
            };

            foreach (Blueprint<BlueprintReference<BlueprintAbility>> mutagen in mutagens)
            {
                AbilityConfigurator.For(mutagen)
                    .EditComponent<ContextRankConfig>(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    ))
                    .Configure();
            }
        }

        private static void patchTrueMutagen()
        {
            logger.Info("   Patching true mutagens");

            // Patch true mutagen to work with class
            AbilityConfigurator.For(AbilityRefs.TrueMutagenAbility.Reference.Get())
                .EditComponent<ContextRankConfig>(
                    c => c.m_Class = CommonTool.Append(
                        c.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                ))
                .Configure();
        }
    }
}
