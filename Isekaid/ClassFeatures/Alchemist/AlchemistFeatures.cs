using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.ClassFeatures.Alchemist
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
            Bombs.ConfigureDisabled();
            DiscoverySelection.ConfigureDisabled();
            IncenseFog.ConfigureDisabled();

            ProgressionConfigurator.New(ProgressionName, Guids.AlchemistFeatures).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring Alchemist Features progression");

            configureAlchemistClassLevels(enabled: true);
            Bombs.ConfigureEnabled();
            DiscoverySelection.ConfigureEnabled();
            IncenseFog.ConfigureEnabled();

            patchBaseMutagens();
            patchGreaterMutagens();
            patchGrandMutagens();
            patchTrueMutagen();

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    Guids.AlchemistClassLevels,
                    Guids.Bombs,
                    FeatureRefs.MutagenFeature.Reference.Get(),
                    FeatureRefs.BrewPotions.Reference.Get(),
                    Guids.IncenseFogFeature,
                    Guids.IncenseFogResourceFact
                )
                .AddEntry(
                    2,
                    Guids.DiscoverySelection,
                    FeatureRefs.PreciseBomb.Reference.Get()
                )
                .AddEntry(
                    3,
                    Guids.Bombs,
                    FeatureSelectionRefs.IncenseSynthesizerIncenseFogSelection.Reference.Get()
                )
                .AddEntry(
                    4,
                    Guids.DiscoverySelection
                )
                .AddEntry(
                    5,
                    Guids.Bombs
                )
                .AddEntry(
                    6,
                    Guids.DiscoverySelection,
                    Guids.DirectedBlastFeature,
                    FeatureSelectionRefs.IncenseSynthesizerIncenseFogSelection.Reference.Get()
                )
                .AddEntry(
                    7,
                    Guids.Bombs
                )
                .AddEntry(
                    8,
                    Guids.DiscoverySelection
                )
                .AddEntry(
                    9,
                    Guids.Bombs,
                    FeatureSelectionRefs.IncenseSynthesizerIncenseFogSelection.Reference.Get()
                )
                .AddEntry(
                    10,
                    Guids.DiscoverySelection,
                    Guids.StaggeringBlast
                )
                .AddEntry(
                    11,
                    Guids.Bombs
                )
                .AddEntry(
                    12,
                    // Greater Mutagen available
                    Guids.DiscoverySelection,
                    FeatureSelectionRefs.IncenseSynthesizerIncenseFogSelection.Reference.Get()
                )
                .AddEntry(
                    13,
                    Guids.Bombs
                )
                .AddEntry(
                    14,
                    Guids.DiscoverySelection,
                    FeatureRefs.PersistantMutagen.Reference.Get()
                )
                .AddEntry(
                    15,
                    Guids.Bombs,
                    FeatureSelectionRefs.IncenseSynthesizerIncenseFogSelection.Reference.Get()
                )
                .AddEntry(
                    16,
                    // Grand Mutagen available
                    Guids.DiscoverySelection
                )
                .AddEntry(
                    17,
                    Guids.Bombs
                )
                .AddEntry(
                    18,
                    Guids.DiscoverySelection,
                    FeatureSelectionRefs.IncenseSynthesizerIncenseFogSelection.Reference.Get()
                )
                .AddEntry(
                    19,
                    Guids.Bombs
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
                    Guids.DiscoverySelection,
                    FeatureSelectionRefs.GrandDiscoverySelection.Reference.Get()
                )
                .AddGroup(
                    FeatureRefs.PreciseBomb.Reference.Get(),
                    Guids.DirectedBlastFeature,
                    Guids.StaggeringBlast
                );

            ProgressionConfigurator.New(ProgressionName, Guids.AlchemistFeatures)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(FeatureRefs.BrewPotions.Reference.Get().Icon)
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

            string name = "AlchemistFeatures.AlchemistClassLevels.Name";
            string displayName = "AlchemistFeatures.AlchemistClassLevels.DisplayName";
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
