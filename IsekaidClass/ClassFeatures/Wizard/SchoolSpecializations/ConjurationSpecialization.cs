using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using BlueprintCore.Utils;
using Class;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Settings;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using BlueprintCore.Blueprints.CustomConfigurators;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Blueprints.Classes.Spells;
using Utils;

namespace Isekaid.ClassFeatures.Wizard.SchoolSpecializations
{
    internal class ConjurationSpecialization
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(AbjurationSpecialization));

        private static readonly string ProgressionName = "ConjurationSpecialization";
        private static readonly string Description = "WizardFeatures.ConjurationSpecialization.Description";

        public static void ConfigureDisabled()
        {
            ProgressionConfigurator.New(ProgressionName, Guids.ConjurationSpecialization).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring conjuration specialization progression");

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    Guids.ConjurationSpecializationFeature
                )
                .AddEntry(
                    8,
                    //Guids.DimensionalSteps
                    FeatureRefs.ConjurationDimensionalStepsFeature.Reference.Get()
                );

            ProgressionConfigurator.New(ProgressionName, Guids.ConjurationSpecialization)
                .CopyFrom(ProgressionRefs.SpecialisationSchoolConjurationProgression.Reference.Get())
                .SetDescription(Description)
                .AddToClasses(Guids.IsekaidClass)
                .SetLevelEntries(levelEntries)
                .Configure();
        }

        private static void configureSpecialisation(bool enabled)
        {
            logger.Info("   Building base conjuration specialization");

            string name = "ConjurationSpecializationFeature";
            string description = "WizardFeatures.ConjurationSpecialization.ConjurationSpecializationFeature.Description";

            if (!enabled)
            {
                configureAcidDart(enabled: false);
                //configureAbjurationResistance(enabled: false);

                FeatureConfigurator.New(name, Guids.ConjurationSpecializationFeature).Configure();
                return;
            }

            configureAcidDart(enabled: true);

            FeatureConfigurator.New(name, Guids.ConjurationSpecializationFeature)
                .CopyFrom(FeatureRefs.SpecializationSchoolConjuration.Reference.Get())
                .SetDescription(description)
                /*
                .AddClassLevelToSummonDuration(
                    characterClass: BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass),
                    half: true
                )
                */
                .AddFacts(
                    facts: new()
                    {
                        Guids.AcidDart
                    },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .Configure();
        }

        private static void configureAcidDart(bool enabled)
        {
            logger.Info("       Building acid dart");

            string name = "AcidDart";
            string description = "WizardFeatures.ConjurationSpecialization.ConjurationSpecializationFeature.Description";

            if (!enabled)
            {
                configureAcidDartResource(enabled: false);
                configureAcidDartAbility(enabled: false);

                FeatureConfigurator.New(name, Guids.ConjurationSpecializationFeature).Configure();
                return;
            }

            configureAcidDartResource(enabled: true);
            configureAcidDartAbility(enabled: true);

            FeatureConfigurator.New(name, Guids.AcidDart)
                .CopyFrom(FeatureRefs.ConjurationAcidDartFeature.Reference.Get())
                .SetDescription(description)
                .AddFacts(
                    facts: new()
                    {
                        Guids.AcidDartAbility
                    },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .AddReplaceAbilitiesStat(
                    ability: new () { Guids.AcidDartAbility }, 
                    stat: StatType.Charisma
                )
                .AddAbilityResources(
                    resource: Guids.AcidDartResource,
                    amount: 0,
                    restoreAmount: true,
                    restoreOnLevelUp: false
                )
                .Configure();
        }

        private static void configureAcidDartResource(bool enabled)
        {
            logger.Info("           Building acid dart resource");

            string name = "AcidDartResource";

            if (!enabled)
            {
                AbilityResourceConfigurator.New(name, Guids.AcidDartResource).Configure();
                return;
            }

            AbilityResourceConfigurator.New(name, Guids.AcidDartResource)
                .CopyFrom(AbilityResourceRefs.ConjurationAcidDartResource.Reference.Get())
                .SetMaxAmount(
                    new BlueprintAbilityResource.Amount
                    {
                        BaseValue = 3,
                        IncreasedByLevel = false,
                        LevelIncrease = 0,
                        IncreasedByLevelStartPlusDivStep = false,
                        StartingLevel = 0,
                        StartingIncrease = 0,
                        LevelStep = 0,
                        PerStepIncrease = 0,
                        MinClassLevelIncrease = 0,
                        OtherClassesModifier = 0,
                        IncreasedByStat = true,
                        ResourceBonusStat = StatType.Charisma
                    }
                )
                .Configure();
        }

        private static void configureAcidDartAbility(bool enabled)
        {
            logger.Info("           Building acid dart ability");

            string name = "AcidDartAbility";
            string description = "WizardFeatures.ConjurationSpecialization.AcidDart.Description";

            if (!enabled)
            {
                configureAcidDartResource(enabled: false);
                configureAcidDartAbility(enabled: false);

                AbilityConfigurator.New(name, Guids.AcidDartAbility).Configure();
                return;
            }

            AbilityConfigurator.New(description, Guids.AcidDartAbility)
                .CopyFrom(
                    blueprint: AbilityRefs.ConjurationAcidDartAbility.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(AbilityDeliverProjectile),
                        typeof(AbilityEffectRunAction),
                        typeof(SpellComponent)
                    } 
                )
                .SetDescription(description)
                .AddAbilityResourceLogic(
                    requiredResource: Guids.AcidDartResource,
                    isSpendResource: true,
                    costIsCustom: false,
                    amount: 1
                )
                .AddContextRankConfig(
                    ContextRankConfigs.ClassLevel(
                        classes: new [] { Guids.IsekaidClass },
                        min: 1,
                        max: 20
                    ).WithDiv2Progression()
                )
                .Configure();
        }

        private static void configureDimensionalSteps(bool enabled)
        {
            logger.Info("   Building dimensional steps");

            string name = "DimensionalSteps";
            string description = "WizardFeatures.ConjurationSpecialization.DimensionalSteps.Description";

            if (!enabled)
            {
                configureDimensionalStepsResource(enabled: false);
                configureDimensionalStepsAbility(enabled: false);

                FeatureConfigurator.New(name, Guids.DimensionalSteps).Configure();
                return;
            }

            configureDimensionalStepsResource(enabled: true);
            configureDimensionalStepsAbility(enabled: true);

            FeatureConfigurator.New(name, Guids.DimensionalSteps)
                .CopyFrom(FeatureRefs.ConjurationDimensionalStepsFeature.Reference.Get())
                .SetDescription(description)
                .AddFacts(
                    facts: new()
                    {
                        Guids.DimensionalStepsAbility
                    },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .AddReplaceAbilitiesStat(
                    ability: new() { Guids.DimensionalStepsAbility },
                    stat: StatType.Charisma
                )
                .AddAbilityResources(
                    resource: Guids.DimensionalStepsResource,
                    amount: 0,
                    restoreAmount: true,
                    restoreOnLevelUp: false
                )
                .Configure();
        }

        private static void configureDimensionalStepsResource(bool enabled)
        {
            logger.Info("       Building dimensional steps resource");

            string name = "DimensionalStepsResource";

            if (!enabled)
            {
                AbilityResourceConfigurator.New(name, Guids.DimensionalStepsResource).Configure();
                return;
            }

            AbilityResourceConfigurator.New(name, Guids.DimensionalStepsResource)
                .CopyFrom(AbilityResourceRefs.ConjurationDimensionalStepsResource.Reference.Get())
                .SetMaxAmount(
                    new BlueprintAbilityResource.Amount
                    {
                        BaseValue = 0,
                        IncreasedByLevel = true,
                        m_Class = new[] { BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass) },
                        LevelIncrease = 1,
                        IncreasedByLevelStartPlusDivStep = false,
                        StartingLevel = 0,
                        StartingIncrease = 0,
                        LevelStep = 0,
                        PerStepIncrease = 0,
                        MinClassLevelIncrease = 0,
                        OtherClassesModifier = 0,
                        IncreasedByStat = true,
                        ResourceBonusStat = StatType.Charisma
                    }
                )
                .Configure();
        }

        private static void configureDimensionalStepsAbility(bool enabled)
        {
            logger.Info("           Building dimensional steps ability");

            string name = "DimensionalStepsResource";
            string description = "WizardFeatures.ConjurationSpecialization.DimensionalSteps.Description";

            if (!enabled)
            {
                AbilityConfigurator.New(name, Guids.DimensionalStepsAbility).Configure();
                return;
            }

            AbilityConfigurator.New(name, Guids.DimensionalStepsAbility)
                .CopyFrom(
                    blueprint: AbilityRefs.ConjurationDimensionalStepsAbility.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(SpellComponent),
                        typeof(AbilityCustomDimensionDoor)
                    }
                )
                .SetDescription(description)
                .AddAbilityResourceLogic(
                    requiredResource: Guids.DimensionalStepsResource,
                    isSpendResource: true,
                    costIsCustom: false,
                    amount: 1
                )
                .Configure();
        }

    }
}
