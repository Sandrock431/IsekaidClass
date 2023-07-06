using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Settings;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using BlueprintCore.Blueprints.CustomConfigurators;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Designers.Mechanics.Facts;
using Utils;

namespace Isekaid.ClassFeatures.Wizard.SchoolSpecializations
{
    internal class IllusionSpecialization
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(IllusionSpecialization));

        private static readonly string ProgressionName = "IllusionSpecialization";
        private static readonly string Description = "WizardFeatures.IllusionSpecialization.Description";

        public static void ConfigureDisabled()
        {
            configureSpecialization(enabled: false);
            ProgressionConfigurator.New(ProgressionName, Guids.IllusionSpecialization).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring illusion specialization progression");

            configureSpecialization(enabled: true);

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    Guids.IllusionSpecializationFeature
                )
                .AddEntry(
                    8,
                    FeatureRefs.IllusionSchoolGreaterFeature.Reference.Get()
                );

            ProgressionConfigurator.New(ProgressionName, Guids.IllusionSpecialization)
                .CopyFrom(ProgressionRefs.SpecialisationSchoolIllusionProgression.Reference.Get())
                .SetDescription(Description)
                .AddToClasses(Guids.IsekaidClass)
                .SetLevelEntries(levelEntries)
                .Configure();
        }

        private static void configureSpecialization(bool enabled)
        {
            logger.Info("   Configuring base illusion specialization");

            string name = "IllusionSpecializationFeature";
            string description = "WizardFeatures.IllusionSpecialization.IllusionSpecializationFeature.Description";

            if (!enabled)
            {
                configureBaseResource(enabled: false);
                configureBaseAbility(enabled: false);

                FeatureConfigurator.New(name, Guids.IllusionSpecializationFeature).Configure();
                return;
            }

            configureBaseResource(enabled: true);
            configureBaseAbility(enabled: true);

            FeatureConfigurator.New(name, Guids.IllusionSpecializationFeature)
                .CopyFrom(FeatureRefs.IllusionSchoolBaseFeature.Reference.Get())
                .SetDescription(description)
                .AddFacts(
                    facts: new() { Guids.BaseIllusionAbility },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .AddAbilityResources(
                    resource: Guids.BaseIllusionResource
                )
                .AddReplaceAbilitiesStat(
                    ability: new() { Guids.BaseIllusionResource },
                    stat: StatType.Charisma
                )
                .Configure();
        }


        private static void configureBaseResource(bool enabled)
        {
            logger.Info("       Configuring base resource");

            string name = "BaseIllusionResource";

            if (!enabled)
            {
                AbilityResourceConfigurator.New(name, Guids.IllusionSpecializationFeature).Configure();
                return;
            }

            AbilityResourceConfigurator.New(name, Guids.BaseIllusionResource)
                .CopyFrom(AbilityResourceRefs.IllusionSchoolBaseResource.Reference.Get())
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

        private static void configureBaseAbility(bool enabled)
        {
            logger.Info("       Configuring base ability cast");

            string name = "BaseIllusionAbility";
            string description = "WizardFeatures.IllusionSpecialization.IllusionSpecializationFeature.Description";

            if (!enabled)
            {
                AbilityConfigurator.New(name, Guids.BaseIllusionAbility).Configure();
                return;
            }

            /*
            logger.Info("           Patching base ability");
            AbilityConfigurator.For(AbilityRefs.IllusionSchoolBaseAbility.Reference.Get())
                .EditComponent<ContextRankConfig>(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();
            */

            AbilityConfigurator.New(name, Guids.BaseIllusionAbility)
                .CopyFrom(
                    blueprint: AbilityRefs.IllusionSchoolBaseAbility.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(AbilityEffectRunAction),
                        typeof(SpellComponent),
                        typeof(ContextCalculateSharedValue),
                        typeof(AbilityDeliverProjectile)
                    }
                )
                .SetDescription(description)
                .AddAbilityResourceLogic(
                    requiredResource: Guids.BaseIllusionResource,
                    isSpendResource: true,
                    costIsCustom: false,
                    amount: 1
                )
                .Configure();
        }

        private static void configureGreaterFeature(bool enabled)
        {
            logger.Info("   Configuring greater feature");

            logger.Info("       Patching greater resource");
            AbilityResourceConfigurator.For(AbilityResourceRefs.IllusionSchoolGreaterResource.Reference.Get())
                .ModifyMaxAmount(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();

            FeatureConfigurator.New("WizardFeatures.IllusionSpecialization.GreaterIllusionAbility.Name", Guids.GreaterIllusionAbility)
                .CopyFrom(
                    blueprint: FeatureRefs.IllusionSchoolGreaterFeature.Reference.Get(),
                    componentTypes: new[] {
                        typeof(AddFacts),
                        typeof(AddAbilityResources)
                    }
                )
                .SetDescription("WizardFeatures.IllusionSpecialization.GreaterIllusionAbility.Description")
                .Configure();
        }
    }
}
