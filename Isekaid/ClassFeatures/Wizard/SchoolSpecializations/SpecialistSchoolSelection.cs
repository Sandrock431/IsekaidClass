using BlueprintCore.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using IsekaidClass.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;

namespace IsekaidClass.Isekaid.ClassFeatures.Wizard.SchoolSpecializations
{
    internal class SpecialistSchoolSelection
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(SpecialistSchoolSelection));

        private static readonly string FeatureName = "WizardFeatures.SpecialistSchoolSelection.Name";
        internal const string DisplayName = "WizardFeatures.SpecialistSchoolSelection.Name";
        private static readonly string Description = "WizardFeatures.SpecialistSchoolSelection.Description";

        public static void ConfigureDisabled()
        {
            AbjurationSpecialization.ConfigureDisabled();
            ConjurationSpecialization.ConfigureDisabled();
            DivinationSpecialization.ConfigureDisabled();
            EnchantmentSpecialization.ConfigureDisabled();
            EvocationSpecialization.ConfigureDisabled();
            IllusionSpecialization.ConfigureDisabled();
            NecromancySpecialization.ConfigureDisabled();
            TransmutationSpecialization.ConfigureDisabled();

            FeatureConfigurator.New(FeatureName, Guids.SpecialistSchoolSelection).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring specialist school selection");

            AbjurationSpecialization.ConfigureEnabled();
            ConjurationSpecialization.ConfigureEnabled();
            DivinationSpecialization.ConfigureEnabled();
            EnchantmentSpecialization.ConfigureEnabled();
            EvocationSpecialization.ConfigureEnabled();
            IllusionSpecialization.ConfigureEnabled();
            NecromancySpecialization.ConfigureEnabled();
            TransmutationSpecialization.ConfigureEnabled();

            FeatureSelectionConfigurator.New(FeatureName, Guids.SpecialistSchoolSelection)
                .CopyFrom(FeatureSelectionRefs.SpecialistSchoolSelection.Reference.Get())
                .SetDescription(Description)
                .SetAllFeatures(
                    Guids.AbjurationSpecialization,
                    Guids.ConjurationSpecialization,
                    Guids.DivinationSpecialization,
                    Guids.EnchantmentSpecialization,
                    Guids.EvocationSpecialization,
                    Guids.IllusionSpecialization,
                    Guids.NecromancySpecialization,
                    Guids.TransmutationSpecialization
                )
                .Configure();
        }
    }
}
