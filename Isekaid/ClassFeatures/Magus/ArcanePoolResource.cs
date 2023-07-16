using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus
{
    internal class ArcanePoolResource
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(ArcanePoolResource));

        private static readonly string ResourceName = "MagusFeatures.ArcanePoolResource.Name";

        public static void Configure()
        {
            logger.Info("Configuring arcane pool resource");

            AbilityResourceConfigurator.New(ResourceName, Guids.ArcanePoolResource)
                .SetIcon(FeatureRefs.ArcanePoolFeature.Reference.Get().Icon)
                .SetMaxAmount(
                    new BlueprintAbilityResource.Amount
                    {
                        BaseValue = 0,
                        IncreasedByLevel = false,
                        LevelIncrease = 0,
                        IncreasedByLevelStartPlusDivStep = true,
                        StartingLevel = 0,
                        StartingIncrease = 0,
                        LevelStep = 2,
                        PerStepIncrease = 1,
                        MinClassLevelIncrease = 1,
                        OtherClassesModifier = 0,
                        IncreasedByStat = true,
                        ResourceBonusStat = StatType.Charisma
                    }
                )
                .SetUseMax(false)
                .SetMax(10)
                .SetMin(0)
                .Configure();
        }
    }
}
