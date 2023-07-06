using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Isekaid.ClassFeatures.Magus.SwordSaint;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Settings;
using System;
using Utils;
using static Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic;

namespace Isekaid.ClassFeatures.Magus
{
    internal class MagusFeatures
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(MagusFeatures));

        private static readonly string ProgressionName = "MagusFeatures";
        internal const string DisplayName = "MagusFeatures.Name";
        private static readonly string Description = "MagusFeatures.Description";

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.MagusFeatures))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                logger.Error("MagusFeatures.Configure", e);
            }
        }

        public static void ConfigureDisabled()
        {
            configureMagusClassLevels(enabled: false);
            configureSwordSaintAbilities(enabled: false);
            ArcanePool.ConfigureDisabled();
            ProgressionConfigurator.New(ProgressionName, Guids.MagusFeatures).Configure();
        }

        public static BlueprintProgression ConfigureEnabled()
        {
            logger.Info("Configuring Magus Features progression");

            configureMagusClassLevels(enabled: true);

            ArcanePool.Configure();

            configureSwordSaintAbilities(enabled: true);

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    Guids.ArcanePool,
                    FeatureRefs.SpellCombatFeature.Reference.Get(),
                    FeatureSelectionRefs.SwordSaintChosenWeaponSelection.Reference.Get(),
                    Guids.CannyDefense
                )
                .AddEntry(
                    2,
                    FeatureRefs.SpellStrikeFeature.Reference.Get()
                )
                .AddEntry(
                    3,
                    Guids.ArcanaSelection
                )
                .AddEntry(
                    4,
                    Guids.PerfectStrike
                )
                .AddEntry(
                    5,
                    FeatureRefs.ArcaneWeaponPlus2.Reference.Get()
                )
                .AddEntry(
                    6,
                    Guids.ArcanaSelection
                )
                .AddEntry(
                    7,
                    Guids.LightningDraw
                )
                .AddEntry(
                    8,
                    FeatureRefs.ImprovedSpellCombat.Reference.Get()
                )
                .AddEntry(
                    9,
                    FeatureRefs.ArcaneWeaponPlus3.Reference.Get(),
                    Guids.ArcanaSelection,
                    Guids.CriticalPerfection
                )
                .AddEntry(
                    11,
                    Guids.SuperiorReflexes
                )
                .AddEntry(
                    12,
                    Guids.ArcanaSelection
                )
                .AddEntry(
                    13,
                    FeatureRefs.ArcaneWeaponPlus4.Reference.Get(),
                    Guids.LethalFocus
                )
                .AddEntry(
                    14,
                    FeatureRefs.GreaterSpellCombat.Reference.Get()
                )
                .AddEntry(
                    15,
                    Guids.ArcanaSelection
                )
                .AddEntry(
                    16,
                    FeatureRefs.Counterstrike.Reference.Get()
                )
                .AddEntry(
                    17,
                    FeatureRefs.ArcaneWeaponPlus5.Reference.Get()
                )
                .AddEntry(
                    18,
                    Guids.ArcanaSelection  
                );

            var uiGroups = UIGroupBuilder.New()
                .AddGroup(
                    Guids.ArcanePool,
                    FeatureRefs.ArcaneWeaponPlus2.Reference.Get(),
                    FeatureRefs.ArcaneWeaponPlus3.Reference.Get(),
                    FeatureRefs.ArcaneWeaponPlus4.Reference.Get(),
                    FeatureRefs.ArcaneWeaponPlus5.Reference.Get()
                )
                .AddGroup(
                    FeatureRefs.SpellCombatFeature.Reference.Get(),
                    FeatureRefs.ImprovedSpellCombat.Reference.Get(),
                    FeatureRefs.GreaterSpellCombat.Reference.Get()
                )
                .AddGroup(
                    FeatureRefs.SpellStrikeFeature.Reference.Get(),
                    Guids.DimensionStrike,
                    Guids.PerfectStrike,
                    FeatureRefs.Counterstrike.Reference.Get(),
                    Guids.CriticalPerfection,
                    Guids.LethalFocus
                )
                .AddGroup(
                    Guids.CannyDefense,
                    Guids.SuperiorReflexes
                )
                .AddGroup(
                    Guids.ArcanaSelection,
                    Guids.LightningDraw
                );

            return ProgressionConfigurator.New(ProgressionName, Guids.MagusFeatures)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(FeatureRefs.SpellCombatFeature.Reference.Get().Icon)
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

        private static void configureMagusClassLevels(bool enabled)
        {
            logger.Info("   Configuring magus class levels for prerequisites");

            string name = "MagusClassLevels";
            string displayName = "MagusFeatures.MagusClassLevels.Name";
            string description = "MagusFeatures.MagusClassLevels.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.MagusClassLevels).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.MagusClassLevels)
                .SetDisplayName(displayName)
                .SetDescription(description)
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.SpellCombatFeature.Reference.Get().Icon)
                .AddClassLevelsForPrerequisites(
                    actualClass: Guids.IsekaidClass,
                    fakeClass: CharacterClassRefs.MagusClass.Reference.Get(),
                    modifier: 1.0,
                    summand: 0
                )
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(true)
                .Configure();
        }

        private static void configureSwordSaintAbilities(bool enabled)
        {
            logger.Info("   Configuring sword saint abilities");

            if (!enabled)
            {
                configureCannyDefense(enabled: false);
                configurePerfectStrike(enabled: false);
                configureLightningDraw(enabled: false);
                configureCriticalPerfection(enabled: false);
                configureSuperiorReflexes(enabled: false);
                configureLethalFocus(enabled: false);
                return;
            }

            configureCannyDefense(enabled: true);
            configurePerfectStrike(enabled: true);
            configureLightningDraw(enabled: true);
            configureCriticalPerfection(enabled: true);
            configureSuperiorReflexes(enabled: true);
            configureLethalFocus(enabled: true);

            //CannyDefense.Configure();
            //PerfectStrike.Configure();
            //LightningDraw.Configure();
            //CriticalPerfection.Configure();
            //SuperiorReflexes.Configure();
            //LethalFocus.Configure();
        }

        private static void configureCannyDefense(bool enabled)
        {
            logger.Info("       Configuring canny defense");

            string featureName = "MagusFeatures.SwordSaint.CannyDefense.Name";
            string description = "MagusFeatures.SwordSaint.CannyDefense.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(featureName, Guids.CannyDefense).Configure();
                return;
            }

            FeatureConfigurator.New(featureName, Guids.CannyDefense)
                .CopyFrom(FeatureRefs.CannyDefense.Reference.Get())
                .SetDescription(description)
                .AddComponent<CannyDefenseComponent>()
                .Configure();
        }

        private static void configurePerfectStrike(bool enabled)
        {
            logger.Info("       Configuring perfect strike");

            string featureName = "MagusFeatures.SwordSaint.PerfectStrike.Name";
            string description = "MagusFeatures.SwordSaint.PerfectStrike.Description";

            if (!enabled)
            {
                configurePerfectStrikeAbility(enabled: false);
                configurePerfectStrikeCritAbility(enabled: false);
                FeatureConfigurator.New(featureName, Guids.PerfectStrike).Configure();
                return;
            }

            configurePerfectStrikeAbility(enabled: true);
            configurePerfectStrikeCritAbility(enabled: true);

            FeatureConfigurator.New(featureName, Guids.PerfectStrike)
                .CopyFrom(FeatureRefs.PerfectStrikeFeature.Reference.Get())
                .SetDescription(description)
                .AddFacts(
                    facts: new()
                    {
                        Guids.PerfectStrikeAbility,
                        Guids.PerfectStrikeCritAbility,
                    },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .Configure();
        }

        private static void configurePerfectStrikeAbility(bool enabled)
        {
            logger.Info("       Configuring perfect strike ability");

            string featureName = "MagusFeatures.SwordSaint.PerfectStrikeAbility.Name";
            string description = "MagusFeatures.SwordSaint.PerfectStrikeAbility.Description";

            if (!enabled)
            {
                ActivatableAbilityConfigurator.New(featureName, Guids.PerfectStrikeAbility).Configure();
                return;
            }

            ActivatableAbilityConfigurator.New(featureName, Guids.PerfectStrikeAbility)
                .CopyFrom(ActivatableAbilityRefs.SwordSaintPerfectStrikeAbility.Reference.Get())
                .SetDescription(description)
                .AddActivatableAbilityResourceLogic(
                    requiredResource: Guids.ArcanePoolResource,
                    spendType: ResourceSpendType.AttackHit
                )
                .Configure();
        }

        private static void configurePerfectStrikeCritAbility(bool enabled)
        {
            logger.Info("       Configuring perfect strike crit ability");

            string featureName = "MagusFeatures.SwordSaint.PerfectStrikeCritAbility.Name";
            string description = "MagusFeatures.SwordSaint.PerfectStrikeCritAbility.Description";

            if (!enabled)
            {
                ActivatableAbilityConfigurator.New(featureName, Guids.PerfectStrikeCritAbility).Configure();
                return;
            }

            ActivatableAbilityConfigurator.New(featureName, Guids.PerfectStrikeCritAbility)
                .CopyFrom(ActivatableAbilityRefs.SwordSaintPerfectStrikeCritAbility.Reference.Get())
                .SetDescription(description)
                .AddActivatableAbilityResourceLogic(
                    requiredResource: Guids.ArcanePoolResource,
                    spendType: ResourceSpendType.AttackHit
                )
                .Configure();
        }

        private static void configureLightningDraw(bool enabled)
        {
            logger.Info("       Configuring lightning draw");

            string featureName = "MagusFeatures.SwordSaint.LightningDraw.Name";
            string description = "MagusFeatures.SwordSaint.LightningDraw.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(featureName, Guids.LightningDraw).Configure();
                return;
            }

            FeatureConfigurator.New(featureName, Guids.LightningDraw)
                .CopyFrom(FeatureRefs.SwordSaintLightningDraw.Reference.Get())
                .SetDescription(description)
                .AddDerivativeStatBonus(
                    baseStat: StatType.Charisma,
                    derivativeStat: StatType.Initiative,
                    descriptor: Kingmaker.Enums.ModifierDescriptor.None
                )
                .AddRecalculateOnStatChange(
                    stat: StatType.Charisma,
                    useKineticistMainStat: false
                )
                .Configure();
        }

        private static void configureCriticalPerfection(bool enabled)
        {
            logger.Info("       Configuring critical perfection");

            string featureName = "MagusFeatures.SwordSaint.CriticalPerfection.Name";
            string description = "MagusFeatures.SwordSaint.CriticalPerfection.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(featureName, Guids.CriticalPerfection).Configure();
                return;
            }

            FeatureConfigurator.New(featureName, Guids.CriticalPerfection)
                .CopyFrom(FeatureRefs.SwordSaintCriticalPerfection.Reference.Get())
                .SetDescription(description)
                .AddComponent<CriticalPerfectionComponent>()
                .Configure();
        }

        private static void configureSuperiorReflexes(bool enabled)
        {
            logger.Info("       Configuring superior reflexes");

            string featureName = "MagusFeatures.SwordSaint.SuperiorReflexes.Name";
            string description = "MagusFeatures.SwordSaint.SuperiorReflexes.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(featureName, Guids.SuperiorReflexes).Configure();
                return;
            }

            FeatureConfigurator.New(featureName, Guids.SuperiorReflexes)
                .CopyFrom(FeatureRefs.SwordSaintSuperiorReflexes.Reference.Get())
                .SetDescription(description)
                .AddDerivativeStatBonus(
                    baseStat: StatType.Charisma,
                    derivativeStat: StatType.AttackOfOpportunityCount,
                    descriptor: Kingmaker.Enums.ModifierDescriptor.None
                )
                .AddRecalculateOnStatChange(
                    stat: StatType.Charisma,
                    useKineticistMainStat: false
                )
                .Configure();
        }

        private static void configureLethalFocus(bool enabled)
        {
            logger.Info("       Configuring lethal focus");

            string featureName = "MagusFeatures.SwordSaint.LethalFocus.Name";
            string description = "MagusFeatures.SwordSaint.LethalFocus.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(featureName, Guids.LethalFocus).Configure();
                return;
            }

            FeatureConfigurator.New(featureName, Guids.LethalFocus)
                .CopyFrom(FeatureRefs.SwordSaintInstantFocus.Reference.Get())
                .SetDescription(description)
                .AddComponent<LethalFocusComponent>()
                .Configure();
        }
    }
}
