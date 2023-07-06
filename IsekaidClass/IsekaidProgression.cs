using Kingmaker.Blueprints.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Isekaid.ClassFeatures;
using Isekaid.ClassFeatures.Magus;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Blueprints;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Properties;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Isekaid.Components;
using Isekaid.ClassFeatures.Barbarian;
using Isekaid.ClassFeatures.Arcanist;
using Utils;
using Isekaid.ClassFeatures.Wizard;
using System;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Enums;
using Kingmaker.Settings;

namespace Isekaid
{
    class IsekaidProgression
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(IsekaidProgression));

        private static readonly string ProgressionName = "IsekaidProgression";

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.IsekaidClass))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                logger.Error("IsekaidProgression.Configure", e);
            }
        }

        public static void ConfigureDisabled()
        {
            configureBardClassLevels(enabled: false);
            configureDruidClassLevels(enabled: false);
            configureMonkClassLevels(enabled: false);
            configureOtherworldlyInitiative(enabled: false);
            configureWeaponMastery(enabled: false);

            IsekaidProficiencies.ConfigureDisabled();
            Cantrips.ConfigureDisabled();

            AlchemistFeatures.ConfigureDisabled();
            ArcanistFeatures.ConfigureDisabled();
            BarbarianFeatures.ConfigureDisabled();
            CavalierFeatures.ConfigureDisabled();
            ClericFeatures.ConfigureDisabled();
            MagusFeatures.ConfigureDisabled();
            PaladinFeatures.ConfigureDisabled();
            RangerFeatures.ConfigureDisabled();
            RogueFeatures.ConfigureDisabled();
            WizardFeatures.ConfigureDisabled();

            ProgressionConfigurator.New(ProgressionName, Guids.IsekaidClassProgression).Configure();
        }

        public static BlueprintProgression ConfigureEnabled()
        {
            logger.Info("Configuring isekai'd progression");

            configureBardClassLevels(enabled: true);
            configureDruidClassLevels(enabled: true);
            configureMonkClassLevels(enabled: true);
            configureOtherworldlyInitiative(enabled: true);
            configureWeaponMastery(enabled: true);

            IsekaidProficiencies.ConfigureEnabled();
            Cantrips.ConfigureEnabled();

            AlchemistFeatures.Configure();
            ArcanistFeatures.Configure();
            BarbarianFeatures.Configure();
            CavalierFeatures.Configure();
            ClericFeatures.Configure();
            MagusFeatures.Configure();
            PaladinFeatures.Configure();
            RangerFeatures.Configure();
            RogueFeatures.Configure();
            WizardFeatures.Configure();

            //applyPatches();

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    Guids.IsekaidClassProficiencies,
                    FeatureRefs.FullCasterFeature.Reference.Get(),
                    FeatureRefs.TouchCalculateFeature.Reference.Get(),
                    FeatureRefs.RayCalculateFeature.Reference.Get(),
                    FeatureRefs.DetectMagic.Reference.Get(),
                    Guids.IsekaidClassCantrips,
                    Guids.BardClassLevels,
                    FeatureRefs.BardicKnowledge.Reference.Get(),
                    FeatureRefs.BloodragerFastMovement.Reference.Get(),
                    FeatureSelectionRefs.AnimalCompanionSelectionDruid.Reference.Get(),
                    FeatureRefs.ScaledFistACBonus.Reference.Get(),
                    FeatureRefs.FlurryOfBlows.Reference.Get(),
                    //FeatureRefs.SoheiDevotedGuardianFeature.Reference.Get(),
                    Guids.IsekaidOtherworldlyInitiative,
                    Guids.AlchemistFeatures,
                    Guids.ArcanistFeatures,
                    Guids.BarbarianFeatures,
                    Guids.CavalierFeatures,
                    Guids.ClericFeatures,
                    Guids.MagusFeatures,
                    Guids.PaladinFeatures,
                    Guids.RangerFeatures,
                    Guids.RogueFeatures,
                    Guids.WizardFeatures
                )
                .AddEntry(
                    2,
                    FeatureRefs.InvulnerableRagerDamageReduction.Reference.Get(),
                    FeatureRefs.ArchaeologistCleverExplorer.Reference.Get(),
                    FeatureRefs.DivineGrace.Reference.Get(),
                    FeatureRefs.Evasion.Reference.Get()
                )
                .AddEntry(
                    3,
                    FeatureRefs.DivineHealth.Reference.Get()
                )
                .AddEntry(
                    4,
                    FeatureRefs.InvulnerableRagerDamageReduction.Reference.Get()
                )
                .AddEntry(
                    5,
                    FeatureRefs.BardLoreMaster.Reference.Get(),
                    FeatureSelectionRefs.WeaponTrainingSelection.Reference.Get()
                )
                .AddEntry(
                    6,
                    FeatureRefs.InvulnerableRagerDamageReduction.Reference.Get(),
                    FeatureRefs.ArchaeologistConfidentExplorerFeature.Reference.Get()
                )
                .AddEntry(
                    8,
                    FeatureRefs.InvulnerableRagerDamageReduction.Reference.Get()
                )
                .AddEntry(
                    9,
                    FeatureRefs.VenomImmunity.Reference.Get(),
                    FeatureSelectionRefs.WeaponTrainingRankUpSelection.Reference.Get(),
                    FeatureRefs.ImprovedEvasion.Reference.Get()
                )
                .AddEntry(
                    10,
                    FeatureRefs.InvulnerableRagerDamageReduction.Reference.Get(),
                    FeatureRefs.BardJackOfAllTrades.Reference.Get(),
                    FeatureRefs.FlurryOfBlowsLevel11.Reference.Get()
                )
                .AddEntry(
                    12,
                    FeatureRefs.InvulnerableRagerDamageReduction.Reference.Get()
                )
                .AddEntry(
                    13,
                    FeatureSelectionRefs.WeaponTrainingRankUpSelection.Reference.Get()
                )
                .AddEntry(
                    14,
                    FeatureRefs.InvulnerableRagerDamageReduction.Reference.Get()
                )
                .AddEntry(
                    16,
                    FeatureRefs.InvulnerableRagerDamageReduction.Reference.Get()
                )
                .AddEntry(
                    17,
                    FeatureSelectionRefs.WeaponTrainingRankUpSelection.Reference.Get()
                )
                .AddEntry(
                    18,
                    FeatureRefs.InvulnerableRagerDamageReduction.Reference.Get()
                )
                .AddEntry(
                    20,
                    FeatureRefs.InvulnerableRagerDamageReduction.Reference.Get(),
                    Guids.IsekaidClassWeaponMastery,
                    FeatureRefs.EnlightenedPhilosopherFinalRevelation.Reference.Get()
                );

            var uiGroups = UIGroupBuilder.New()
                .AddGroup(
                    FeatureRefs.BardicKnowledge.Reference.Get(),
                    FeatureRefs.BardLoreMaster.Reference.Get(),
                    FeatureRefs.ArchaeologistCleverExplorer.Reference.Get(),
                    FeatureRefs.ArchaeologistConfidentExplorerFeature.Reference.Get(),
                    FeatureRefs.BardJackOfAllTrades.Reference.Get()
                )
                .AddGroup(
                    FeatureSelectionRefs.WeaponTrainingSelection.Reference.Get(),
                    FeatureSelectionRefs.WeaponTrainingRankUpSelection.Reference.Get(),
                    Guids.IsekaidClassWeaponMastery
                );

            return ProgressionConfigurator.New(ProgressionName, Guids.IsekaidClassProgression)
                .SetDisplayName("")
                .SetDescription("")
                .SetDescriptionShort("")
                .SetAllowNonContextActions(false)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(false)
                .SetForAllOtherClasses(false)
                .SetLevelEntries(levelEntries)
                .SetUIGroups(uiGroups)
                .Configure();
        }

        private static void configureBardClassLevels(bool enabled)
        {
            logger.Info("   Configuring bard class levels for prerequisites");

            var name = "BardClassLevels";
            var displayName = "Isekaid.BardClassLevels.Name";
            var description = "Isekaid.BardClassLevels.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.BardClassLevels).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.BardClassLevels)
                .SetDisplayName(displayName)
                .SetDescription(description)
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.InspireCourageFeature.Reference.Get().Icon)
                .AddClassLevelsForPrerequisites(
                    actualClass: Guids.IsekaidClass,
                    fakeClass: CharacterClassRefs.BardClass.Reference.Get(),
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

        private static void configureDruidClassLevels(bool enabled)
        {
            logger.Info("   Configuring druid class levels for prerequisites");

            var name = "DruidClassLevels";
            var displayName = "Isekaid.DruidClassLevels.Name";
            var description = "Isekaid.DruidClassLevels.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.DruidClassLevels).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.DruidClassLevels)
                .SetDisplayName(displayName)
                .SetDescription(description)
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.WildShapeIWolfFeature.Reference.Get().Icon)
                .AddClassLevelsForPrerequisites(
                    actualClass: Guids.IsekaidClass,
                    fakeClass: CharacterClassRefs.DruidClass.Reference.Get(),
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

        private static void configureMonkClassLevels(bool enabled)
        {
            logger.Info("   Configuring monk class levels for prerequisites");

            var name = "MonkClassLevels";
            var displayName = "Isekaid.MonkClassLevels.Name";
            var description = "Isekaid.MonkClassLevels.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.MonkClassLevels).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.MonkClassLevels)
                .SetDisplayName(displayName)
                .SetDescription(description)
                .SetDescriptionShort("")
                .SetIcon(FeatureSelectionRefs.ScaledFistBonusFeatSelectionLevel1.Reference.Get().Icon)
                .AddClassLevelsForPrerequisites(
                    actualClass: Guids.IsekaidClass,
                    fakeClass: CharacterClassRefs.MonkClass.Reference.Get(),
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

        private static void applyPatches()
        {
            logger.Info("   Applying all isekai'd progression patches");

            patchBardicKnowledge();
            patchDruidAnimalCompanion();
            patchScaledFistACProperty();
            //patchDevotedGuardian();
        }

        private static void patchBardicKnowledge()
        {
            logger.Info("       Patching bardic knowledge");

            FeatureConfigurator.For(FeatureRefs.BardicKnowledge.Reference.Get())
                .EditComponents<ContextRankConfig>(
                    edit: c => c.m_Class = CommonTool.Append(
                        c.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    ),
                    predicate: c => c.name.Equals("$ContextRankConfig$7b7d844c-d201-45e5-811e-47014d138d65") && c.m_Class.Length > 0
                )
                .Configure();
        }

        private static void patchDruidAnimalCompanion()
        {
            logger.Info("       Patching druid animal companion");

            ProgressionConfigurator.For(ProgressionRefs.DruidAnimalCompanionProgression.Reference.Get())
                .AddToClasses(BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass))
                .Configure();
        }

        private static void patchScaledFistACProperty()
        {
            logger.Info("       Patching scaled fist ac property");

            UnitPropertyConfigurator.For(UnitPropertyRefs.ScaledFistACBonusProperty2.Reference.Get())
                .EditComponent<SummClassLevelGetter>(
                        edit: c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                        )
                    )
                .Configure();
        }

        private static void patchDevotedGuardian()
        {
            logger.Info("       Patching devoted guardian");

            FeatureConfigurator.For(FeatureRefs.SoheiDevotedGuardianFeature.Reference.Get())
                .EditComponents<ContextRankConfig>(
                    edit: c => c.m_Class = CommonTool.Append(
                        c.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    ),
                    predicate: c => c.name.Equals("$ContextRankConfig$975ee89c-54ed-4b3d-a6de-74d601598813") && c.m_Class.Length > 0
                )
                .Configure();
        }

        private static void patchCleverExplorer()
        {
            logger.Info("       Patching clever explorer");

            FeatureConfigurator.For(FeatureRefs.ArchaeologistCleverExplorer.Reference.Get())
                .EditComponents<ContextRankConfig>(
                    edit: c => c.m_Class = CommonTool.Append(
                        c.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    ),
                    predicate: c => c.name.Equals("$ContextRankConfig$81a3b80d-47d0-43c8-8d64-78f6a419a960") && c.m_Class.Length > 0
                )
                .Configure();
        }

        private static void configureOtherworldlyInitiative(bool enabled)
        {
            logger.Info("       Configuring otherworldly initiative");

            string name = "OtherworldlyInitiative";
            string displayName = "Isekaid.OtherworldlyInitiative.Name";
            string description = "Isekaid.OtherworldlyInitiative.Description";

            if (!enabled)
            {
                configureOtherworldlyInitiative20(enabled: false);
                configureOtherworldlyInitiativeReapplyLevel(enabled: false);
                FeatureConfigurator.New(name, Guids.IsekaidOtherworldlyInitiative).Configure();
                return;
            }

            configureOtherworldlyInitiative20(enabled: true);
            configureOtherworldlyInitiativeReapplyLevel(enabled: true);

            FeatureConfigurator.New(name, Guids.IsekaidOtherworldlyInitiative)
                .CopyFrom(FeatureRefs.SoheiDevotedGuardianFeature.Reference.Get())
                .SetDisplayName(displayName)
                .SetDescription(description)
                .AddFeatureOnClassLevel(
                    clazz: BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass),
                    level: 19,
                    feature: Guids.IsekaidOtherworldlyInitiative20,
                    beforeThisLevel: false
                )
                .AddFacts(
                    facts: new() { Guids.IsekaidOtherworldlyInitiativeReapplyLevel },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .Configure();
        }

        private static void configureOtherworldlyInitiative20(bool enabled)
        {
            logger.Info("           Configuring otherworldly initiative level 20");

            string name = "OtherworldlyInitiative20";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.IsekaidOtherworldlyInitiative20).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.IsekaidOtherworldlyInitiative20)
                .CopyFrom(
                    blueprint: FeatureRefs.SoheiDevotedGuardianFeature20.Reference.Get(),
                    componentTypes: new[] { typeof(AddContextStatBonus) }
                )
                .Configure();
        }

        private static void configureOtherworldlyInitiativeReapplyLevel(bool enabled)
        {
            logger.Info("           Configuring otherworldly initiative reapply level");

            string name = "OtherworldlyInitiativeReapplyLevel";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.IsekaidOtherworldlyInitiativeReapplyLevel).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.IsekaidOtherworldlyInitiativeReapplyLevel)
                .CopyFrom(FeatureRefs.SoheiDevotedGuardianReapplyLevelFeature.Reference.Get())
                .AddContextRankConfig(
                    component: ContextRankConfigs.ClassLevel(
                        classes: new[] { Guids.IsekaidClass },
                        type: AbilityRankType.Default,
                        min: 0,
                        max: 20
                    ).WithDiv2Progression()
                )
                .Configure();
        }

        private static void configureWeaponMastery(bool enabled)
        {
            logger.Info("   Building weapon mastery");

            string name = "Isekaid.WeaponMastery.Name";
            string description = "Isekaid.WeaponMastery.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.IsekaidClassWeaponMastery).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.IsekaidClassWeaponMastery)
                .CopyFrom(FeatureRefs.WeaponMastery.Reference.Get())
                .SetDescription(description)
                .AddComponent<WeaponMasteryComponent>()
                .Configure();
        }
    }
}
