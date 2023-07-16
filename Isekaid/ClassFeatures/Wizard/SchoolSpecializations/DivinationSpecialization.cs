using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Blueprints.Classes.Spells;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.ClassFeatures.Wizard.SchoolSpecializations
{
    internal class DivinationSpecialization
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(DivinationSpecialization));

        private static readonly string ProgressionName = "DivinationSpecialization";
        private static readonly string Description = "WizardFeatures.DivinationSpecialization.Description";

        public static void ConfigureDisabled()
        {
            configureSpecialization(enabled: false);
            ProgressionConfigurator.New(ProgressionName, Guids.DivinationSpecialization).Configure();
        }

        public static BlueprintProgression ConfigureEnabled()
        {
            logger.Info("Configuring divination specialization progression");

            configureSpecialization(enabled: true);

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    Guids.DivinationSpecializationFeature
                )
                .AddEntry(
                    8,
                    //buildGreaterFeature()
                    FeatureRefs.DivinationSchoolGreaterFeature.Reference.Get()
                )
                .AddEntry(
                    20,
                    FeatureRefs.DivinationSchoolForewarnedFeature.Reference.Get()
                );

            return ProgressionConfigurator.New(ProgressionName, Guids.DivinationSpecialization)
                .CopyFrom(ProgressionRefs.SpecialisationSchoolDivinationProgression.Reference.Get())
                .SetDescription(Description)
                .AddToClasses(Guids.IsekaidClass)
                .SetLevelEntries(levelEntries)
                .Configure();
        }

        private static void configureSpecialization(bool enabled)
        {
            logger.Info("   Configuring base Divination specialization");

            string name = "DivinationSpecializationFeature";
            string description = "WizardFeatures.DivinationSpecialization.DivinationSpecializationFeature.Description";

            if (!enabled)
            {
                configureBaseResource(enabled: false);
                configureBaseAbility(enabled: false);

                FeatureConfigurator.New(name, Guids.DivinationSpecializationFeature).Configure();
                return;
            }

            /*
            logger.Info("       Patching initiative feature");
            FeatureConfigurator.For(FeatureRefs.DivinationSchoolBaseInitiativeFeature.Reference.Get())
                .EditComponent<ContextRankConfig>(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();
            */

            configureBaseResource(enabled: true);
            configureBaseAbility(enabled: true);

            FeatureConfigurator.New(name, Guids.DivinationSpecializationFeature)
                .CopyFrom(FeatureRefs.DivinationSchoolBaseFeature.Reference.Get())
                .SetDescription(description)
                .AddAbilityResources(
                    resource: Guids.BaseDivinationResource
                )
                .AddReplaceAbilitiesStat(
                    ability: new () { Guids.BaseDivinationAbility },
                    stat: StatType.Charisma
                )
                .AddFacts(
                    facts: new ()
                    {
                        FeatureRefs.DivinationSchoolBaseInitiativeFeature.Reference.Get(),
                        Guids.BaseDivinationAbility
                    }
                )
                .Configure();
        }

        private static void configureBaseResource(bool enabled)
        {
            logger.Info("       Configuring base resource");

            string name = "BaseDivinationResource";

            if (!enabled)
            {
                AbilityResourceConfigurator.New(name, Guids.BaseDivinationResource).Configure();
                return;
            }

            AbilityResourceConfigurator.New("WizardFeatures.DivinationSpecialization.BaseDivinationResource.Name", Guids.BaseDivinationResource)
                .CopyFrom(AbilityResourceRefs.DivinationSchoolBaseResource.Reference.Get())
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

            string name = "BaseDivinationAbility";
            string description = "WizardFeatures.DivinationSpecialization.BaseDivinationAbility.Description";

            if (!enabled)
            {
                configureBaseResource(enabled: false);
                //configureAbjurationResistance(enabled: false);

                AbilityConfigurator.New(name, Guids.BaseDivinationAbility).Configure();
                return;
            }

            /*
            logger.Info("           Patching base ability");
            AbilityConfigurator.For(AbilityRefs.DivinationSchoolBaseAbility.Reference.Get())
                .EditComponent<ContextRankConfig>(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();

            logger.Info("           Patching base buff");
            BuffConfigurator.For(BuffRefs.DivinationSchoolBaseBuff.Reference.Get())
                .EditComponent<ContextRankConfig>(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();
            */

            AbilityConfigurator.New("WizardFeatures.DivinationSpecialization.BaseDivinationAbility.Name", Guids.BaseDivinationAbility)
                .CopyFrom(
                    blueprint: AbilityRefs.DivinationSchoolBaseAbilityCast.Reference.Get(),
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
            AbilityResourceConfigurator.For(AbilityResourceRefs.DivinationSchoolGreaterResource.Reference.Get())
                .ModifyMaxAmount(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();

            FeatureConfigurator.New("WizardFeatures.DivinationSpecialization.GreaterDivinationAbility.Name", Guids.GreaterDivinationAbility)
                .CopyFrom(FeatureRefs.DivinationSchoolGreaterFeature.Reference.Get())
                .SetDescription("WizardFeatures.DivinationSpecialization.GreaterDivinationAbility.Description")
                .Configure();
        }
    }
}
