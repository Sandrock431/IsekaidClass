using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Isekaid.ClassFeatures.Wizard.SchoolSpecializations;
using Kingmaker.Blueprints;
using System.Collections.Generic;
using Utils;

namespace Isekaid
{
    internal class IsekaidProficiencies
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(IsekaidProficiencies));

        private static readonly string FeatureName = "Isekaid.Proficiencies.Name";
        private static readonly string Description = "Isekaid.Proficiencies.Description";

        private static readonly List<Blueprint<BlueprintUnitFactReference>> ExoticWeaponProficiencies = new()
        {
            FeatureRefs.BastardSwordProficiency.Reference.Get(),
            FeatureRefs.DoubleAxeProficiency.Reference.Get(),
            FeatureRefs.DoubleSwordProficiency.Reference.Get(),
            FeatureRefs.DuelingSwordProficiency.Reference.Get(),
            FeatureRefs.DwarvenWaraxeProficiency.Reference.Get(),
            FeatureRefs.ElvenCurvedBladeProficiency.Reference.Get(),
            FeatureRefs.EstocProficiency.Reference.Get(),
            FeatureRefs.FalcataProficiency.Reference.Get(),
            FeatureRefs.FauchardProficiency.Reference.Get(),
            FeatureRefs.HookedHammerProficiency.Reference.Get(),
            FeatureRefs.SlingStaffProficiency.Reference.Get(),
            FeatureRefs.UrgroshProficiency.Reference.Get()
        };

        public static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatureName, Guids.SpecialistSchoolSelection).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring proficiencies");

            FeatureConfigurator.New(FeatureName, Guids.IsekaidClassProficiencies)
                .SetDisplayName(FeatureName)
                .SetDescription(Description)
                .AddFacts(new() {
                    FeatureRefs.SimpleWeaponProficiency.Reference.Get(),
                    FeatureRefs.MartialWeaponProficiency.Reference.Get(),
                    FeatureRefs.MonkWeaponProficiency.Reference.Get(),
                    FeatureRefs.ShieldsProficiency.Reference.Get(),
                    FeatureRefs.TowerShieldProficiency .Reference.Get(),
                    FeatureRefs.LightArmorProficiency.Reference.Get(),
                    FeatureRefs.MediumArmorProficiency.Reference.Get(),
                    FeatureRefs.HeavyArmorProficiency.Reference.Get(),
                    FeatureRefs.ShieldsProficiency.Reference.Get()
                })
                .AddFacts(ExoticWeaponProficiencies)
                .SetHideInUI(false)
                .SetHideNotAvailibleInUI(false)
                .SetIsClassFeature(true)
                .SetReapplyOnLevelUp(false)
                .SetRanks(1)
                .SetAllowNonContextActions(false)
                .Configure();
        }
    }
}
