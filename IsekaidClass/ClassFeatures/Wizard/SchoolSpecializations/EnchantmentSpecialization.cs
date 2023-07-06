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
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Designers.Mechanics.Facts;
using Utils;

namespace Isekaid.ClassFeatures.Wizard.SchoolSpecializations
{
    internal class EnchantmentSpecialization
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(EnchantmentSpecialization));

        private static readonly string ProgressionName = "EnchantmentSpecialization";
        private static readonly string Description = "WizardFeatures.EnchantmentSpecialization.Description";

        public static void ConfigureDisabled()
        {
            configureSpecialization(enabled: false);
            ProgressionConfigurator.New(ProgressionName, Guids.EnchantmentSpecialization).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring enchantment specialization progression");

            configureSpecialization(enabled: true);

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    Guids.EnchantmentSpecializationFeature
                )
                .AddEntry(
                    8,
                    FeatureRefs.EnchantmentSchoolGreaterFeature.Reference.Get()
                );

            ProgressionConfigurator.New(ProgressionName, Guids.EnchantmentSpecialization)
                .CopyFrom(ProgressionRefs.SpecialisationSchoolEnchantmentProgression.Reference.Get())
                .SetDescription(Description)
                .AddToClasses(Guids.IsekaidClass)
                .SetLevelEntries(levelEntries)
                .Configure();
        }

        private static void configureSpecialization(bool enabled)
        {
            logger.Info("   Configuring base enchantment specialization");

            string name = "EnchantmentSpecializationFeature";
            string description = "WizardFeatures.EnchantmentSpecialization.EnchantmentSpecializationFeature.Description";

            if (!enabled)
            {
                configureBaseResource(enabled: false);
                configureBaseAbility(enabled: false);

                FeatureConfigurator.New(name, Guids.EnchantmentSpecializationFeature).Configure();
                return;
            }

            configureBaseResource(enabled: true);
            configureBaseAbility(enabled: true);

            FeatureConfigurator.New(name, Guids.EnchantmentSpecializationFeature)
                .CopyFrom(
                    blueprint: FeatureRefs.EnchantmentSchoolBaseFeature.Reference.Get(),
                    componentTypes: new[] {
                        typeof(AddContextStatBonus),
                        typeof(ContextRankConfig),
                        typeof(SavingThrowBonusAgainstSchoolAbilityValue)
                    }
                )
                .SetDescription(description)
                .AddFacts(
                    facts: new () { Guids.BaseEnchantmentAbility },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .AddAbilityResources(
                    resource: Guids.BaseEnchantmentResource
                )
                .AddReplaceAbilitiesStat(
                    ability: new () { Guids.BaseEnchantmentAbility },
                    stat: StatType.Charisma
                )
                /*
                .EditComponent<ContextRankConfig>(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                */
                .Configure();
        }

        private static void configureBaseResource(bool enabled)
        {
            logger.Info("       Configuring base resource");

            string name = "BaseEnchantmentResource";

            if (!enabled)
            {
                AbilityResourceConfigurator.New(name, Guids.EnchantmentSpecializationFeature).Configure();
                return;
            }

            AbilityResourceConfigurator.New(name, Guids.BaseEnchantmentResource)
                .CopyFrom(AbilityResourceRefs.EnchantmentSchoolBaseResource.Reference.Get())
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

            string name = "BaseEnchantmentAbility";
            string description = "WizardFeatures.EnchantmentSpecialization.BaseEnchantmentAbility.Description";

            if (!enabled)
            {
                AbilityConfigurator.New(name, Guids.BaseEnchantmentAbility).Configure();
                return;
            }

            /*
            logger.Info("           Patching base ability");
            AbilityConfigurator.For(AbilityRefs.EnchantmentSchoolBaseAbility.Reference.Get())
                .EditComponent<ContextRankConfig>(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();
            */

            AbilityConfigurator.New(name, Guids.BaseEnchantmentAbility)
                .CopyFrom(
                    blueprint: AbilityRefs.EnchantmentSchoolBaseAbilityCast.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(SpellComponent),
                        typeof(AbilityEffectStickyTouch)
                    } 
                )
                .SetDescription(description)
                .AddAbilityResourceLogic(
                    requiredResource: Guids.BaseDivinationResource,
                    isSpendResource: true,
                    costIsCustom: false,
                    amount: 1
                )
                .Configure();
        }

        private static void configureGreaterFeature(bool enabled)
        {
            logger.Info("   Configuring greater feature");

            logger.Info("           Patching greater resource");
            AbilityResourceConfigurator.For(AbilityResourceRefs.EnchantmentSchoolGreaterResource.Reference.Get())
                .ModifyMaxAmount(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();

            FeatureConfigurator.New("WizardFeatures.EnchantmentSpecialization.GreaterEnchantmentAbility.Name", Guids.GreaterEnchantmentAbility)
                .CopyFrom(FeatureRefs.EnchantmentSchoolGreaterFeature.Reference.Get())
                .SetDescription("WizardFeatures.EnchantmentSpecialization.GreaterEnchantmentAbility.Description")
                .Configure();
        }
    }
}
