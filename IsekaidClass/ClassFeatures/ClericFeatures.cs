using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using Utils;

namespace Isekaid.ClassFeatures
{
    internal class ClericFeatures
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(ClericFeatures));

        private static readonly string ProgressionName = "ClericFeatures";
        internal const string DisplayName = "ClericFeatures.Name";
        private static readonly string Description = "ClericFeatures.Description";

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.ClericFeatures))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                logger.Error("ClericFeatures.Configure", e);
            }
        }

        public static void ConfigureDisabled()
        {
            configureClericClassLevels(enabled: false);

            ProgressionConfigurator.New(ProgressionName, Guids.ClericFeatures).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring Cleric Features progression");

            configureClericClassLevels(enabled: true);

            //patchChannelEnergy();
            //patchDomains();

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    FeatureSelectionRefs.ChannelEnergySelection.Reference.Get(),
                    FeatureSelectionRefs.DomainsSelection.Reference.Get(),
                    FeatureSelectionRefs.SecondDomainsSelection.Reference.Get()
                );

            ProgressionConfigurator.New(ProgressionName, Guids.ClericFeatures)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(FeatureSelectionRefs.DomainsSelection.Reference.Get().Icon)
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
                .Configure();
        }

        private static void configureClericClassLevels(bool enabled)
        {
            logger.Info("   Configuring cleric class levels for prerequisites");

            string name = "ClericClassLevels";
            string displayName = "ClericFeatures.ClericClassLevels.Name";
            string description = "ClericFeatures.ClericClassLevels.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.ClericClassLevels).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.ClericClassLevels)
                .SetDisplayName(displayName)
                .SetDescription(description)
                .SetDescriptionShort("")
                .SetIcon(FeatureSelectionRefs.DomainsSelection.Reference.Get().Icon)
                .AddClassLevelsForPrerequisites(
                    actualClass: Guids.IsekaidClass,
                    fakeClass: CharacterClassRefs.ClericClass.Reference.Get(),
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

        private static void patchChannelEnergy()
        {
            logger.Info("   Patching channel energy");

            // Patch domains to work with class
            var channelAbilities = new List<Blueprint<BlueprintReference<BlueprintAbility>>>() {
                AbilityRefs.ChannelEnergy.Reference.Get(),
                AbilityRefs.ChannelPositiveHarm.Reference.Get(),
                AbilityRefs.ChannelNegativeEnergy.Reference.Get(),
                AbilityRefs.ChannelNegativeHeal.Reference.Get(),
            };

            foreach (Blueprint<BlueprintReference<BlueprintAbility>> channelAbility in channelAbilities)
            {
                AbilityConfigurator.For(channelAbility)
                    .EditComponents<ContextRankConfig>(
                        edit: c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                        ),
                        predicate: c => c.name.Contains("AbilityRankConfig") && !c.m_Class.Empty()
                    )
                    .Configure();
            }
        }

        private static void patchDomains()
        {
            logger.Info("   Patching domains");

            // Patch domains to work with class
            var domains = new [] {
                ProgressionRefs.AirDomainProgression.Reference.Get(),
                ProgressionRefs.AirDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.AnimalDomainProgression.Reference.Get(),
                ProgressionRefs.AnimalDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.ArtificeDomainProgression.Reference.Get(),
                ProgressionRefs.ArtificeDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.ChaosDomainProgression.Reference.Get(),
                ProgressionRefs.ChaosDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.CharmDomainProgression.Reference.Get(),
                ProgressionRefs.CharmDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.CommunityDomainProgression.Reference.Get(),
                ProgressionRefs.CommunityDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.DarknessDomainProgression.Reference.Get(),
                ProgressionRefs.DarknessDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.DeathDomainProgression.Reference.Get(),
                ProgressionRefs.DeathDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.DestructionDomainProgression.Reference.Get(),
                ProgressionRefs.DestructionDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.EarthDomainProgression.Reference.Get(),
                ProgressionRefs.EarthDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.EvilDomainProgression.Reference.Get(),
                ProgressionRefs.EvilDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.FireDomainProgression.Reference.Get(),
                ProgressionRefs.FireDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.GloryDomainProgression.Reference.Get(),
                ProgressionRefs.GloryDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.GoodDomainProgression.Reference.Get(),
                ProgressionRefs.GoodDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.HealingDomainProgression.Reference.Get(),
                ProgressionRefs.HealingDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.KnowledgeDomainProgression.Reference.Get(),
                ProgressionRefs.KnowledgeDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.LawDomainProgression.Reference.Get(),
                ProgressionRefs.LawDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.LiberationDomainProgression.Reference.Get(),
                ProgressionRefs.LiberationDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.LuckDomainProgression.Reference.Get(),
                ProgressionRefs.LuckDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.MadnessDomainProgression.Reference.Get(),
                ProgressionRefs.MadnessDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.MagicDomainProgression.Reference.Get(),
                ProgressionRefs.MagicDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.NobilityDomainProgression.Reference.Get(),
                ProgressionRefs.NobilityDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.PlantDomainProgression.Reference.Get(),
                ProgressionRefs.PlantDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.ProtectionDomainProgression.Reference.Get(),
                ProgressionRefs.ProtectionDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.ReposeDomainProgression.Reference.Get(),
                ProgressionRefs.ReposeDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.RuneDomainProgression.Reference.Get(),
                ProgressionRefs.RuneDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.StrengthDomainProgression.Reference.Get(),
                ProgressionRefs.StrengthDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.SunDomainProgression.Reference.Get(),
                ProgressionRefs.SunDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.TravelDomainProgression.Reference.Get(),
                ProgressionRefs.TravelDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.TrickeryDomainProgression.Reference.Get(),
                ProgressionRefs.TrickeryDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.WarDomainProgression.Reference.Get(),
                ProgressionRefs.WarDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.WaterDomainProgression.Reference.Get(),
                ProgressionRefs.WaterDomainProgressionSecondary.Reference.Get(),
                ProgressionRefs.WeatherDomainProgression.Reference.Get(),
                ProgressionRefs.WeatherDomainProgressionSecondary.Reference.Get()
            };

            foreach (BlueprintFeature domain in domains)
            {
                ProgressionConfigurator.For(domain)
                    .AddToClasses(Guids.IsekaidClass)
                    .Configure();
            }
        }
    }
}
