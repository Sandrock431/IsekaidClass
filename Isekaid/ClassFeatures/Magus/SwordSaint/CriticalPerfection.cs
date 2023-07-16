using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus.SwordSaint
{
    internal class CriticalPerfection
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(CriticalPerfection));

        private static readonly string FeatureName = "MagusFeatures.SwordSaint.CriticalPerfection.Name";

        public static void Configure()
        {
            logger.Info("Configuring critical perfection");

            FeatureConfigurator.New(FeatureName, Guids.CriticalPerfection)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.SwordSaint.CriticalPerfection.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.SwordSaintCriticalPerfection.Reference.Get().Icon)
                .AddComponent<CriticalPerfectionComponent>()
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
