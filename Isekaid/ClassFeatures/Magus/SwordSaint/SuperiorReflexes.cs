using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus.SwordSaint
{
    internal class SuperiorReflexes
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(SuperiorReflexes));

        private static readonly string FeatureName = "MagusFeatures.SwordSaint.SuperiorReflexes.Name";

        public static void Configure()
        {
            logger.Info("Configuring superior reflexes");

            FeatureConfigurator.New(FeatureName, Guids.SuperiorReflexes)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.SwordSaint.SuperiorReflexes.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.SwordSaintSuperiorReflexes.Reference.Get().Icon)
                .AddDerivativeStatBonus(
                    baseStat: Kingmaker.EntitySystem.Stats.StatType.Charisma,
                    derivativeStat: Kingmaker.EntitySystem.Stats.StatType.AttackOfOpportunityCount,
                    descriptor: Kingmaker.Enums.ModifierDescriptor.None
                )
                .AddRecalculateOnStatChange(
                    stat: Kingmaker.EntitySystem.Stats.StatType.Charisma,
                    useKineticistMainStat: false
                )
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(true)
                .Configure();
        }
    }
}
