using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Properties;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System;
using Utils;

namespace Isekaid.ClassFeatures.Barbarian
{
    internal class BarbarianFeatures
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(BarbarianFeatures));

        private static readonly string ProgressionName = "BarbarianFeatures";
        internal const string DisplayName = "BarbarianFeatures.Name";
        private static readonly string Description = "BarbarianFeatures.Description";

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.BarbarianFeatures))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                logger.Error("BarbarianFeatures.Configure", e);
            }
        }

        public static void ConfigureDisabled()
        {
            //MightyRage.ConfigureDisabled();
            ProgressionConfigurator.New(ProgressionName, Guids.BarbarianFeatures).Configure();
        }

        public static BlueprintProgression ConfigureEnabled()
        {
            logger.Info("Configuring Barbarian Features progression");

           // MightyRage.ConfigureEnabled();

            patchRageBuff();
            patchRageLevelProperty();

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    FeatureRefs.RageFeature.Reference.Get(),
                    FeatureRefs.TirelessRage.Reference.Get()
                )
                .AddEntry(
                    2,
                    FeatureRefs.UncannyDodgeChecker.Reference.Get(),
                    FeatureSelectionRefs.RagePowerSelection.Reference.Get()
                )
                .AddEntry(
                    4,
                    FeatureSelectionRefs.RagePowerSelection.Reference.Get()
                )
                .AddEntry(
                    5,
                    FeatureRefs.ImprovedUncannyDodge.Reference.Get()
                )
                .AddEntry(
                    6,
                    FeatureSelectionRefs.RagePowerSelection.Reference.Get()
                )
                .AddEntry(
                    8,
                    FeatureSelectionRefs.RagePowerSelection.Reference.Get()
                )
                .AddEntry(
                    10,
                    FeatureSelectionRefs.RagePowerSelection.Reference.Get()
                )
                .AddEntry(
                    11,
                    FeatureRefs.GreaterRageFeature.Reference.Get()
                )
                .AddEntry(
                    12,
                    FeatureSelectionRefs.RagePowerSelection.Reference.Get()
                )
                .AddEntry(
                    14,
                    FeatureSelectionRefs.RagePowerSelection.Reference.Get()
                )
                .AddEntry(
                    16,
                    FeatureSelectionRefs.RagePowerSelection.Reference.Get()
                )
                .AddEntry(
                    18,
                    FeatureSelectionRefs.RagePowerSelection.Reference.Get()
                )
                .AddEntry(
                    20,
                    //FeatureRefs.MightyRage.Reference.Get(),
                    //buildMightyRage(),
                    Guids.MightyRage,
                    FeatureSelectionRefs.RagePowerSelection.Reference.Get()
                );

            var uiGroups = UIGroupBuilder.New()
                .AddGroup(
                    FeatureRefs.RageFeature.Reference.Get(),
                    FeatureRefs.GreaterRageFeature.Reference.Get(),
                    //FeatureRefs.MightyRage.Reference.Get()
                    Guids.MightyRage
                )
                .AddGroup(
                    FeatureRefs.UncannyDodgeChecker.Reference.Get(),
                    FeatureRefs.ImprovedUncannyDodge.Reference.Get()
                );

            return ProgressionConfigurator.New(ProgressionName, Guids.BarbarianFeatures)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(FeatureRefs.RageFeature.Reference.Get().Icon)
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(true)
                .SetClasses(Guids.IsekaidClass)
                .SetForAllOtherClasses(false)
                .SetLevelEntries(levelEntries)
                .SetUIGroups(uiGroups)
                .Configure();
        }

        private static void patchRageBuff()
        {
            logger.Info("   Patching rage buff");

            BuffConfigurator.For(BuffRefs.StandartRageBuff.Reference.Get())
                .RemoveComponents(c => c.name.Contains("ForbidSpellCasting"))
                .Configure();
        }

        private static void patchRageLevelProperty()
        {
            logger.Info("   Patching rage level property");

            var rageLevelProperty = BlueprintTool.Get<BlueprintUnitProperty>("6a8e9d4b8ba547f5819354a05dd2a291");

            UnitPropertyConfigurator.For(rageLevelProperty)
                .EditComponent<SummClassLevelGetter>(
                    edit: c => c.m_Class = CommonTool.Append(
                        c.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();
        }
    }
}
