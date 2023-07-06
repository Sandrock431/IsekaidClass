using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using BlueprintCore.Utils;
using Utils;
using System;

namespace Isekaid.ClassFeatures
{
    internal class RangerFeatures
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(RangerFeatures));

        private static readonly string ProgressionName = "RangerFeatures";
        internal const string DisplayName = "RangerFeatures.Name";
        private static readonly string Description = "RangerFeatures.Description";

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.RangerFeatures))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                logger.Error("RangerFeatures.Configure", e);
            }
        }

        public static void ConfigureDisabled()
        {
            ProgressionConfigurator.New(ProgressionName, Guids.RangerFeatures).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring Ranger Features progression");

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    FeatureRefs.DemonslayerFavoriteEnemy.Reference.Get(),
                    FeatureRefs.DemonslayerFavoriteEnemyHiddenFeature.Reference.Get()
                )
                .AddEntry(
                    3,
                    FeatureSelectionRefs.FavoriteTerrainSelection.Reference.Get()
                )
                .AddEntry(
                    5,
                    FeatureRefs.DemonslayerFavoriteEnemy.Reference.Get(),
                    FeatureRefs.FavoriteEnemyDemonOfMagic.Reference.Get(),
                    FeatureRefs.FavoriteEnemyDemonOfSlaughter.Reference.Get(),
                    FeatureRefs.FavoriteEnemyDemonOfStrength.Reference.Get()
                )
                .AddEntry(
                    8,
                    FeatureSelectionRefs.FavoriteTerrainSelection.Reference.Get(),
                    FeatureSelectionRefs.FavoriteTerrainSelectionRankUp.Reference.Get()
                )
                .AddEntry(
                    10,
                    FeatureRefs.DemonslayerFavoriteEnemy.Reference.Get(),
                    FeatureRefs.FavoriteEnemyDemonOfMagic.Reference.Get(),
                    FeatureRefs.FavoriteEnemyDemonOfSlaughter.Reference.Get(),
                    FeatureRefs.FavoriteEnemyDemonOfStrength.Reference.Get()
                )
                .AddEntry(
                    13,
                    FeatureSelectionRefs.FavoriteTerrainSelection.Reference.Get(),
                    FeatureSelectionRefs.FavoriteTerrainSelectionRankUp.Reference.Get()
                )
                .AddEntry(
                    15,
                    FeatureRefs.DemonslayerFavoriteEnemy.Reference.Get(),
                    FeatureRefs.FavoriteEnemyDemonOfMagic.Reference.Get(),
                    FeatureRefs.FavoriteEnemyDemonOfSlaughter.Reference.Get(),
                    FeatureRefs.FavoriteEnemyDemonOfStrength.Reference.Get()
                )
                .AddEntry(
                    18,
                    FeatureSelectionRefs.FavoriteTerrainSelection.Reference.Get(),
                    FeatureSelectionRefs.FavoriteTerrainSelectionRankUp.Reference.Get()
                )
                .AddEntry(
                    20,
                    FeatureRefs.DemonslayerFavoriteEnemy.Reference.Get(),
                    FeatureRefs.FavoriteEnemyDemonOfMagic.Reference.Get(),
                    FeatureRefs.FavoriteEnemyDemonOfSlaughter.Reference.Get(),
                    FeatureRefs.FavoriteEnemyDemonOfStrength.Reference.Get()
                )
                ;

            ProgressionConfigurator.New(ProgressionName, Guids.RangerFeatures)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetDescriptionShort("")
                .SetIcon(AbilityRefs.AspectOfTheFalcon.Reference.Get().Icon)
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
                .Configure();
        }
    }
}
