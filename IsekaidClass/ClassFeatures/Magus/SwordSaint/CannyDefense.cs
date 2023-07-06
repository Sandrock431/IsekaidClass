using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Utils;

namespace Isekaid.ClassFeatures.Magus.SwordSaint
{
    internal class CannyDefense
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(CannyDefense));

        private static readonly string FeatureName = "MagusFeatures.SwordSaint.CannyDefense.Name";

        public static void Configure()
        {
            logger.Info("Configuring canny defense");

            FeatureConfigurator.New(FeatureName, Guids.CannyDefense)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.SwordSaint.CannyDefense.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.SwordSaintCannyDefense.Reference.Get().Icon)
                .AddComponent<CannyDefenseComponent>()
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(true)
                .SetIsClassFeature(true)
                .Configure();
        }
    }
}
