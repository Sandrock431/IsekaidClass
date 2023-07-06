using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Class;
using Utils;

namespace Isekaid.ClassFeatures.Magus.Arcana
{
    internal class DevotedBlade
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(DevotedBlade));

        private static readonly string FeatureName = "MagusFeatures.ArcanaSelection.DevotedBlade.Name";

        public static void Configure()
        {
            logger.Info("Configuring devoted blade");

            FeatureConfigurator.New(FeatureName, Guids.DevotedBlade)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.ArcanaSelection.DevotedBlade.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.DevotedBladeFeature.Reference.Get().Icon)
                .AddPrerequisiteClassLevel(
                    characterClass: Guids.IsekaidClass,
                    level: 12,
                    group: Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.All,
                    checkInProgression: false,
                    hideInUI: false
                )
                .AddFeatureOnAlignment(
                    facts: new() { ActivatableAbilityRefs.ArcaneWeaponUnholyChoice.Reference.Get() },
                    alignment: Kingmaker.Enums.AlignmentComponent.Evil
                )
                .AddFeatureOnAlignment(
                    facts: new() { ActivatableAbilityRefs.ArcaneWeaponHolyChoice.Reference.Get() },
                    alignment: Kingmaker.Enums.AlignmentComponent.Good
                )
                .AddFeatureOnAlignment(
                    facts: new() { ActivatableAbilityRefs.ArcaneWeaponAxiomaticChoice.Reference.Get() },
                    alignment: Kingmaker.Enums.AlignmentComponent.Lawful
                )
                .AddFeatureOnAlignment(
                    facts: new() { ActivatableAbilityRefs.ArcaneWeaponAnarchicChoice.Reference.Get() },
                    alignment: Kingmaker.Enums.AlignmentComponent.Chaotic
                )
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .AddToGroups(Kingmaker.Blueprints.Classes.FeatureGroup.MagusArcana)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(true)
                .Configure();
        }
    }
}
