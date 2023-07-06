using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using System;
using Utils;

namespace Isekaid.ClassFeatures
{
    internal class SorcererFeatures
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(SorcererFeatures));

        private static readonly string ProgressionName = "SorcererFeatures";
        internal const string DisplayName = "SorcererFeatures.Name";
        private static readonly string Description = "SorcererFeatures.Description";

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.SorcererFeatures))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                logger.Error("SorcererFeatures.Configure", e);
            }
        }

        public static void ConfigureDisabled()
        {
            ProgressionConfigurator.New(ProgressionName, Guids.SorcererFeatures).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring Sorcerer Features progression");

            //patchBloodlines();

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    Guids.SorcererClassLevels,
                    FeatureSelectionRefs.SorcererBloodlineSelection.Reference.Get(),
                    FeatureSelectionRefs.CrossbloodedSecondaryBloodlineSelection.Reference.Get()
                );

            ProgressionConfigurator.New(ProgressionName, Guids.SorcererFeatures)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(ProgressionRefs.BloodlineArcaneProgression.Reference.Get().Icon)
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

        private static void configureSorcererClassLevels(bool enabled)
        {
            logger.Info("   Configuring sorcerer class levels for prerequisites");

            string name = "SorcererClassLevels";
            string displayName = "SorcererFeatures.SorcererClassLevels.Name";
            string description = "SorcererFeatures.SorcererClassLevels.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.SorcererClassLevels).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.SorcererClassLevels)
                .SetDisplayName(displayName)
                .SetDescription(description)
                .SetDescriptionShort("")
                .SetIcon(ProgressionRefs.BloodlineArcaneProgression.Reference.Get().Icon)
                .AddClassLevelsForPrerequisites(
                    actualClass: Guids.IsekaidClass,
                    fakeClass: CharacterClassRefs.SorcererClass.Reference.Get(),
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

        private static void patchBloodlines()
        {
            logger.Info("   Patching bloodlines");

            // Patch bloodlines to work with class
            var bloodlines = new [] {
                ProgressionRefs.BloodlineAbyssalProgression.Reference.Get(),
                ProgressionRefs.BloodlineArcaneProgression.Reference.Get(),
                ProgressionRefs.BloodlineCelestialProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicBlackProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicBlueProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicBrassProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicBronzeProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicCopperProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicGoldProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicGreenProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicRedProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicSilverProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicWhiteProgression.Reference.Get(),
                ProgressionRefs.BloodlineElementalAirProgression.Reference.Get(),
                ProgressionRefs.BloodlineElementalEarthProgression.Reference.Get(),
                ProgressionRefs.BloodlineElementalFireProgression.Reference.Get(),
                ProgressionRefs.BloodlineElementalWaterProgression.Reference.Get(),
                ProgressionRefs.BloodlineFeyProgression.Reference.Get(),
                ProgressionRefs.BloodlineInfernalProgression.Reference.Get(),
                ProgressionRefs.BloodlineSerpentineProgression.Reference.Get(),
                ProgressionRefs.BloodlineUndeadProgression.Reference.Get()
            };

            foreach (BlueprintFeature bloodline in bloodlines)
            {
                ProgressionConfigurator.For(bloodline)
                    .AddToClasses(Guids.IsekaidClass)
                    .Configure();
            }
        }
    }
}
