using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Utils;

namespace Isekaid.ClassFeatures.Magus.SwordSaint
{
    internal class LethalFocus
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(LethalFocus));

        private static readonly string FeatureName = "MagusFeatures.SwordSaint.LethalFocus.Name";

        public static void Configure()
        {
            logger.Info("Configuring lethal focus");

            FeatureConfigurator.New(FeatureName, Guids.LethalFocus)
                .SetDisplayName(FeatureName)
                .SetDescription("MagusFeatures.SwordSaint.LethalFocus.Description")
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.SwordSaintInstantFocus.Reference.Get().Icon)
                .AddComponent<LethalFocusComponent>()
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
