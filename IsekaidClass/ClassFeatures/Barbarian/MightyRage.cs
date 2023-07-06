using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Utils;

namespace Isekaid.ClassFeatures.Barbarian
{
    internal class MightyRage
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(MightyRage));

        private static readonly string FeatureName = "MightyRage.Name";

        public static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatureName, Guids.MightyRage).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring mighty rage");

            patchRageBuff();

            FeatureConfigurator.New(FeatureName, Guids.MightyRage)
                .CopyFrom(FeatureRefs.MightyRage.Reference.Get())
                .Configure();
        }

        private static void patchRageBuff()
        {
            logger.Info("   Patching rage buff");

            var reference = BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.MightyRage);

            BuffConfigurator.For(BuffRefs.StandartRageBuff.Reference.Get())
                .EditComponent<ContextRankConfig>(
                    edit: c => c.m_FeatureList = CommonTool.Append(
                        c.m_FeatureList,
                        reference
                    )
                )
                .Configure();
        }
    }
}
