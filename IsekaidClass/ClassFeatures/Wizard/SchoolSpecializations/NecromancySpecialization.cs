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
using Kingmaker.UnitLogic.Mechanics.Components;
using Utils;

namespace Isekaid.ClassFeatures.Wizard.SchoolSpecializations
{
    internal class NecromancySpecialization
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(NecromancySpecialization));

        private static readonly string ProgressionName = "NecromancySpecialization";
        private static readonly string Description = "WizardFeatures.NecromancySpecialization.Description";

        public static void ConfigureDisabled()
        {
            configureSpecialization(enabled: false);
            ProgressionConfigurator.New(ProgressionName, Guids.NecromancySpecialization).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring necromancy specialization progression");

            configureSpecialization(enabled: true);

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    Guids.NecromancySpecializationFeature
                )
                .AddEntry(
                    8,
                    FeatureRefs.NecromancySchoolGreaterFeature.Reference.Get()
                );

            ProgressionConfigurator.New(ProgressionName, Guids.NecromancySpecialization)
                .CopyFrom(ProgressionRefs.SpecialisationSchoolNecromancyProgression.Reference.Get())
                .SetDescription(Description)
                .AddToClasses(Guids.IsekaidClass)
                .SetLevelEntries(levelEntries)
                .Configure();
        }

        private static void configureSpecialization(bool enabled)
        {
            logger.Info("   Configuring base necromancy specialization");

            string name = "NecromancySpecializationFeature";
            string description = "WizardFeatures.NecromancySpecialization.NecromancySpecializationFeature.Description";

            if (!enabled)
            {
                configureBaseResource(enabled: false);
                configureBaseAbility(enabled: false);

                FeatureConfigurator.New(name, Guids.NecromancySpecializationFeature).Configure();
                return;
            }

            configureBaseResource(enabled: true);
            configureBaseAbility(enabled: true);

            FeatureConfigurator.New(name, Guids.NecromancySpecializationFeature)
                .CopyFrom(FeatureRefs.NecromancySchoolBaseFeature.Reference.Get())
                .SetDescription(description)
                .AddFacts(
                    facts: new() { 
                        Guids.BaseNecromancyAbility,
                        AbilityRefs.TurnUndead.Reference.Get()
                    },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .AddAbilityResources(
                    resource: Guids.BaseNecromancyResource
                )
                .AddReplaceAbilitiesStat(
                    ability: new() { Guids.BaseNecromancyAbility },
                    stat: StatType.Charisma
                )
                .Configure();
        }


        private static void configureBaseResource(bool enabled)
        {
            logger.Info("       Configuring base resource");

            string name = "BaseNecromancyResource";

            if (!enabled)
            {
                AbilityResourceConfigurator.New(name, Guids.BaseNecromancyResource).Configure();
                return;
            }

            AbilityResourceConfigurator.New(name, Guids.BaseNecromancyResource)
                .CopyFrom(AbilityResourceRefs.NecromancySchoolBaseResource.Reference.Get())
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
            logger.Info("       Configuring base ability");

            string name = "BaseNecromancyAbility";
            string description = "WizardFeatures.NecromancySpecialization.BaseNecromancyAbility.Description";

            if (!enabled)
            {
                AbilityConfigurator.New(name, Guids.BaseNecromancyAbility).Configure();
                return;
            }

            /*
            logger.Info("           Patching base ability");
            AbilityConfigurator.For(AbilityRefs.NecromancySchoolBaseAbility.Reference.Get())
                .EditComponent<ContextRankConfig>(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();
            */

            AbilityConfigurator.New(name, Guids.BaseNecromancyAbility)
                .CopyFrom(
                    blueprint: AbilityRefs.NecromancySchoolBaseAbility.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(AbilityDeliverTouch),
                        typeof(AbilityEffectRunAction),
                        typeof(SpellComponent),
                        typeof(ContextCalculateSharedValue)
                    }
                )
                .SetDescription(description)
                .AddAbilityResourceLogic(
                    requiredResource: Guids.BaseNecromancyResource,
                    isSpendResource: true,
                    costIsCustom: false,
                    amount: 1
                )
                .EditComponents<ContextRankConfig>(
                    edit: c => c.m_Class = CommonTool.Append(
                        c.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    ),
                    predicate: c => true
                )
                .Configure();
        }

        private static void configureGreaterFeature(bool enabled)
        {
            logger.Info("   Configuring greater feature");

            logger.Info("       Patching greater resource");
            AbilityResourceConfigurator.For(AbilityResourceRefs.NecromancySchoolGreaterResource.Reference.Get())
                .ModifyMaxAmount(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();

            FeatureConfigurator.New("WizardFeatures.NecromancySpecialization.GreaterNecromancyAbility.Name", Guids.GreaterNecromancyAbility)
                .CopyFrom(FeatureRefs.NecromancySchoolGreaterFeature.Reference.Get())
                .SetDescription("WizardFeatures.NecromancySpecialization.GreaterNecromancyAbility.Description")
                .Configure();
        }
    }
}
