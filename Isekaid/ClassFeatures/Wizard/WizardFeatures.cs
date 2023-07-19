using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using BlueprintCore.Utils;
using IsekaidClass.Utils;
using System;
using IsekaidClass.Isekaid.ClassFeatures.Wizard.SchoolSpecializations;

namespace IsekaidClass.Isekaid.ClassFeatures.Wizard
{
    internal class WizardFeatures
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(WizardFeatures));

        private static readonly string ProgressionName = "WizardFeatures";
        internal const string DisplayName = "WizardFeatures.Name";
        private static readonly string Description = "WizardFeatures.Description";

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.WizardFeatures))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                logger.Error("WizardFeatures.Configure", e);
            }
        }

        public static void ConfigureDisabled()
        {
            ProgressionConfigurator.New(ProgressionName, Guids.WizardFeatures).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring Wizard Features progression");

            SpecialistSchoolSelection.ConfigureEnabled();

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    FeatureRefs.WizardArcaneBond.Reference.Get(),
                    Guids.SpecialistSchoolSelection,
                    FeatureRefs.ScribingScrolls.Reference.Get(),
                    FeatureSelectionRefs.WizardFeatSelection.Reference.Get()
                )
                .AddEntry(
                    5,
                    FeatureSelectionRefs.WizardFeatSelection.Reference.Get()
                )
                .AddEntry(
                    10,
                    FeatureSelectionRefs.WizardFeatSelection.Reference.Get()
                )
                .AddEntry(
                    15,
                    FeatureSelectionRefs.WizardFeatSelection.Reference.Get()
                )
                .AddEntry(
                    20,
                    FeatureSelectionRefs.WizardFeatSelection.Reference.Get()
                );

            ProgressionConfigurator.New(ProgressionName, Guids.WizardFeatures)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetDescriptionShort("")
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
    }
}
