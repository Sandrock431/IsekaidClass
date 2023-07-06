using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using System;
using Utils;

namespace Isekaid.ClassFeatures
{
    internal class CavalierFeatures
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(CavalierFeatures));

        private static readonly string ProgressionName = "CavalierFeatures";
        internal const string DisplayName = "CavalierFeatures.Name";
        private static readonly string Description = "CavalierFeatures.Description";

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.CavalierFeatures))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                logger.Error("CavalierFeatures.Configure", e);
            }
        }

        public static void ConfigureDisabled()
        {
            ProgressionConfigurator.New(ProgressionName, Guids.CavalierFeatures).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring Cavalier Features progression");

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    FeatureSelectionRefs.GendarmeFeatSelection.Reference.Get(),
                    FeatureRefs.DiscipleOfThePikeBiggerTheyAre.Reference.Get()
                )
                .AddEntry(
                    3,
                    FeatureRefs.CavalierCharge.Reference.Get(),
                    FeatureSelectionRefs.GendarmeFeatSelection.Reference.Get()
                )
                .AddEntry(
                    6,
                    FeatureSelectionRefs.GendarmeFeatSelection.Reference.Get(),
                    FeatureRefs.DiscipleOfThePikeBiggerTheyAre6.Reference.Get()
                )
                .AddEntry(
                    9,
                    FeatureSelectionRefs.GendarmeFeatSelection.Reference.Get()
                )
                .AddEntry(
                    11,
                    FeatureRefs.CavalierMightyCharge.Reference.Get()
                )
                .AddEntry(
                    12,
                    FeatureSelectionRefs.GendarmeFeatSelection.Reference.Get(),
                    FeatureRefs.DiscipleOfThePikeBiggerTheyAre12.Reference.Get()
                )
                .AddEntry(
                    15,
                    FeatureSelectionRefs.GendarmeFeatSelection.Reference.Get()
                )
                .AddEntry(
                    18,
                    FeatureSelectionRefs.GendarmeFeatSelection.Reference.Get()
                )
                .AddEntry(
                    20,
                    FeatureRefs.GendarmeTransfixingCharge.Reference.Get(),
                    FeatureSelectionRefs.GendarmeFeatSelection.Reference.Get()
                );

            var uiGroups = UIGroupBuilder.New()
                .AddGroup(
                    FeatureRefs.DiscipleOfThePikeBiggerTheyAre.Reference.Get(),
                    FeatureRefs.DiscipleOfThePikeBiggerTheyAre6.Reference.Get(),
                    FeatureRefs.DiscipleOfThePikeBiggerTheyAre12.Reference.Get()
                )
                .AddGroup(
                    FeatureRefs.CavalierCharge.Reference.Get(),
                    FeatureRefs.CavalierMightyCharge.Reference.Get(),
                    FeatureRefs.GendarmeTransfixingCharge.Reference.Get()
                );

            ProgressionConfigurator.New(ProgressionName, Guids.CavalierFeatures)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(FeatureRefs.CavalierMightyCharge.Reference.Get().Icon)
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
    }
}
