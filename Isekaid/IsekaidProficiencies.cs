using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using IsekaidClass.Utils;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Enums;

namespace IsekaidClass.Isekaid
{
    internal class IsekaidProficiencies
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(IsekaidProficiencies));

        private static readonly string FeatureName = "Isekaid.Proficiencies.Name";
        private static readonly string Description = "Isekaid.Proficiencies.Description";

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
                .AddComponent(FeatureRefs.FighterProficiencies.Reference.Get().GetComponent<AddFacts>())
                .AddProficiencies(
                    weaponProficiencies: new WeaponCategory[] {
                        WeaponCategory.BastardSword,
                        WeaponCategory.DuelingSword,
                        WeaponCategory.DwarvenWaraxe,
                        WeaponCategory.ElvenCurvedBlade,
                        WeaponCategory.Estoc,
                        WeaponCategory.Falcata,
                        WeaponCategory.Fauchard,
                        WeaponCategory.Kama,
                        WeaponCategory.Sai,
                        WeaponCategory.Tongi,
                        WeaponCategory.SlingStaff,
                        WeaponCategory.DoubleAxe,
                        WeaponCategory.DoubleSword,
                        WeaponCategory.Urgrosh,
                        WeaponCategory.HookedHammer,
                        WeaponCategory.Nunchaku
                    }
                )
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
