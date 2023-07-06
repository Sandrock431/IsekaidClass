using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using System;
using Utils;

namespace Isekaid.ClassFeatures
{
    internal class RogueFeatures
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(RogueFeatures));

        private static readonly string ProgressionName = "RogueFeatures";
        internal const string DisplayName = "RogueFeatures.Name";
        private static readonly string Description = "RogueFeatures.Description";

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.RogueFeatures))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                logger.Error("RogueFeatures.Configure", e);
            }
        }

        public static void ConfigureDisabled()
        {
            ProgressionConfigurator.New(ProgressionName, Guids.RogueFeatures).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring Rogue Features progression");

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    FeatureRefs.SneakAttack.Reference.Get(),
                    FeatureRefs.WeaponFinesse.Reference.Get()
                )
                .AddEntry(
                    2,
                    FeatureSelectionRefs.RogueTalentSelection.Reference.Get()
                )
                .AddEntry(
                    3,
                    FeatureRefs.SneakAttack.Reference.Get(),
                    FeatureSelectionRefs.FinesseTrainingSelection.Reference.Get()
                )
                .AddEntry(
                    4,
                    FeatureSelectionRefs.RogueTalentSelection.Reference.Get(),
                    FeatureRefs.DebilitatingInjury.Reference.Get()
                )
                .AddEntry(
                    5,
                    FeatureRefs.SneakAttack.Reference.Get()
                )
                .AddEntry(
                    6,
                    FeatureSelectionRefs.RogueTalentSelection.Reference.Get()
                )
                .AddEntry(
                    7,
                    FeatureRefs.SneakAttack.Reference.Get()
                )
                .AddEntry(
                    8,
                    FeatureSelectionRefs.RogueTalentSelection.Reference.Get()
                )
                .AddEntry(
                    9,
                    FeatureRefs.SneakAttack.Reference.Get()
                )
                .AddEntry(
                    10,
                    FeatureSelectionRefs.RogueTalentSelection.Reference.Get(),
                    FeatureRefs.AdvanceTalents.Reference.Get()
                )
                .AddEntry(
                    11,
                    FeatureRefs.SneakAttack.Reference.Get(),
                    FeatureSelectionRefs.FinesseTrainingSelection.Reference.Get()
                )
                .AddEntry(
                    12,
                    FeatureSelectionRefs.RogueTalentSelection.Reference.Get()
                )
                .AddEntry(
                    13,
                    FeatureRefs.SneakAttack.Reference.Get()
                )
                .AddEntry(
                    14,
                    FeatureSelectionRefs.RogueTalentSelection.Reference.Get()
                )
                .AddEntry(
                    15,
                    FeatureRefs.SneakAttack.Reference.Get()
                )
                .AddEntry(
                    16,
                    FeatureSelectionRefs.RogueTalentSelection.Reference.Get()
                )
                .AddEntry(
                    17,
                    FeatureRefs.SneakAttack.Reference.Get()
                )
                .AddEntry(
                    18,
                    FeatureSelectionRefs.RogueTalentSelection.Reference.Get()
                )
                .AddEntry(
                    19,
                    FeatureRefs.SneakAttack.Reference.Get(),
                    FeatureSelectionRefs.FinesseTrainingSelection.Reference.Get()
                )
                .AddEntry(
                    20,
                    FeatureSelectionRefs.RogueTalentSelection.Reference.Get()
                );
            var uiGroups = UIGroupBuilder.New()
                .AddGroup(
                    FeatureRefs.DebilitatingInjury.Reference.Get(),
                    FeatureRefs.DoubleDebilitation.Reference.Get(),
                    FeatureRefs.AdvanceTalents.Reference.Get()
                );

            ProgressionConfigurator.New(ProgressionName, Guids.RogueFeatures)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(FeatureRefs.SneakAttack.Reference.Get().Icon)
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
    }
}
