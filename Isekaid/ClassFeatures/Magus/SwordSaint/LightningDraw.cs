using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus.SwordSaint
{
    internal class LightningDraw
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(LightningDraw));

        private static readonly string FeatureName = "MagusFeatures.SwordSaint.LightningDraw.Name";

        public static void Configure()
        {
            logger.Info("Configuring lightning draw");

            FeatureConfigurator.New(FeatureName, Guids.LightningDraw)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.SwordSaint.LightningDraw.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.SwordSaintLightningDraw.Reference.Get().Icon)
                .AddDerivativeStatBonus(
                    baseStat: Kingmaker.EntitySystem.Stats.StatType.Charisma,
                    derivativeStat: Kingmaker.EntitySystem.Stats.StatType.Initiative,
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
