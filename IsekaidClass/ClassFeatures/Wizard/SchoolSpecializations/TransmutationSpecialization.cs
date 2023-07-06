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
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using Kingmaker.Designers.Mechanics.Buffs;
using Utils;
using Mono.Cecil;

namespace Isekaid.ClassFeatures.Wizard.SchoolSpecializations
{
    internal class TransmutationSpecialization
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(TransmutationSpecialization));

        private static readonly string ProgressionName = "WizardFeatures.TransmutationSpecialization.Name";
        private static readonly string Description = "WizardFeatures.TransmutationSpecialization.Description";

        public static void ConfigureDisabled()
        {
            configureSpecialization(enabled: false);
            ProgressionConfigurator.New(ProgressionName, Guids.TransmutationSpecialization).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring transmutation specialization progression");

            configureSpecialization(enabled: true);
            //patchChangeShape();

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    Guids.TransmutationSpecializationFeature
                )
                .AddEntry(
                    8,
                    FeatureRefs.TransmutationSchoolChangeShapeFeature.Reference.Get()
                )
                .AddEntry(
                    20,
                    FeatureRefs.TransmutationPhysicalEnhancementCapstone.Reference.Get()
                );

            ProgressionConfigurator.New(ProgressionName, Guids.TransmutationSpecialization)
                .CopyFrom(ProgressionRefs.SpecialisationSchoolTransmutationProgression.Reference.Get())
                .SetDescription(Description)
                .AddToClasses(Guids.IsekaidClass)
                .SetLevelEntries(levelEntries)
                .Configure();
        }

        private static void configureSpecialization(bool enabled)
        {
            logger.Info("   Configuring base transmutation specialization");

            string name = "TransmutationSpecializationFeature";
            string description = "WizardFeatures.TransmutationSpecialization.TransmutationSpecializationFeature.Description";

            if (!enabled)
            {
                configureTelekineticFist(enabled: false);

                FeatureConfigurator.New(name, Guids.NecromancySpecializationFeature).Configure();
                return;
            }

            configureTelekineticFist(enabled: true);

            //patchPhysicalEnhancement();

            FeatureConfigurator.New(name, Guids.TransmutationSpecializationFeature)
                .CopyFrom(FeatureRefs.SpecializationSchoolTransmutation.Reference.Get())
                .SetDescription(description)
                .AddFacts(
                    facts: new() { 
                        Guids.TelekineticFist,
                        FeatureRefs.TransmutationPhysicalEnhancementFeature.Reference.Get()
                    },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .Configure();
        }

        private static void configureTelekineticFist(bool enabled)
        {
            logger.Info("       Configuring telekinetic fist");

            string name = "TelekineticFist";
            string description = "WizardFeatures.TransmutationSpecialization.TelekineticFist.Description";

            if (!enabled)
            {
                configureTelekineticFistResourceFact(enabled: false);
                configureTelekineticFistResource(enabled: false);
                configureTelekineticFistAbility(enabled: false);

                FeatureConfigurator.New(name, Guids.TelekineticFist).Configure();
                return;
            }

            configureTelekineticFistResourceFact(enabled: true);
            configureTelekineticFistResource(enabled: true);
            configureTelekineticFistAbility(enabled: true);

            FeatureConfigurator.New(name, Guids.TelekineticFist)
                .CopyFrom(FeatureRefs.TelekineticFistFeature.Reference.Get())
                .SetDescription(description)
                .AddFacts(
                    facts: new() {
                        Guids.TelekineticFistResourceFact,
                        Guids.TelekineticFistAbility
                    },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .AddAbilityResources(
                    resource: Guids.TelekineticFistResource
                )
                .AddReplaceAbilitiesStat(
                    ability: new() { Guids.TelekineticFistAbility },
                    stat: StatType.Charisma
                )
                .Configure();
        }

        private static void configureTelekineticFistResourceFact(bool enabled)
        {
            logger.Info("               Configuring telekinetic fist resource fact");

            string name = "TelekineticFistResourceFact";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.TelekineticFistResourceFact).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.TelekineticFistResourceFact)
                .CopyFrom(FeatureRefs.TelekineticFistResourceFact.Reference.Get())
                .AddAbilityResources(
                    resource: Guids.TelekineticFistResource,
                    amount: 0,
                    restoreAmount: true,
                    restoreOnLevelUp: false
                )
                .Configure();
        }

        private static void configureTelekineticFistResource(bool enabled)
        {
            logger.Info("               Configuring telekinetic fist resource");

            string name = "TelekineticFistResource";

            if (!enabled)
            {
                AbilityResourceConfigurator.New(name, Guids.TelekineticFistResource).Configure();
                return;
            }

            AbilityResourceConfigurator.New(name, Guids.TelekineticFistResource)
                .CopyFrom(AbilityResourceRefs.TelekineticFistResource.Reference.Get())
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

        private static void configureTelekineticFistAbility(bool enabled)
        {
            logger.Info("               Configuring telekinetic fist ability");

            string name = "TelekineticFistAbility";
            string description = "WizardFeatures.TransmutationSpecialization.TelekineticFist.Description";

            if (!enabled)
            {
                AbilityConfigurator.New(name, Guids.TelekineticFistAbility).Configure();
                return;
            }

            AbilityConfigurator.New(name, Guids.TelekineticFistAbility)
                .CopyFrom(
                    blueprint: AbilityRefs.TelekineticFist.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(AbilityDeliverProjectile),
                        typeof(AbilityEffectRunAction),
                        typeof(SpellComponent)
                    }
                )
                .SetDescription(description)
                .AddAbilityResourceLogic(
                    requiredResource: Guids.TelekineticFistResource,
                    isSpendResource: true,
                    costIsCustom: false,
                    amount: 1
                )
                .EditComponent<ContextRankConfig>(
                    c => c.m_Class = CommonTool.Append(
                        c.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();
        }

        private static void patchPhysicalEnhancement()
        {
            logger.Info("       Patching physical enchancement");

            var scaling = new BuffScaling();
            scaling.TypeOfScaling = BuffScaling.ScalingType.ByClassLevel;
            scaling.Modifier = 5;
            scaling.m_ChosenClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass);
            scaling.StartingMod = 1;
            scaling.Minimum = 1;

            logger.Info("           Patching dexterity buff");
            BuffConfigurator.For(BuffRefs.TransmutationSchoolDexterityBuff.Reference.Get())
                .AddComponent<AddStatBonusScaled>(
                    c =>
                    {
                        c.Descriptor = Kingmaker.Enums.ModifierDescriptor.Enhancement;
                        c.Stat = StatType.Dexterity;
                        c.Value = 1;
                        c.Scaling = scaling;
                    }
                )
                .Configure();

            logger.Info("           Patching constitution buff");
            BuffConfigurator.For(BuffRefs.TransmutationSchoolConstitutionBuff.Reference.Get())
                .AddComponent<AddStatBonusScaled>(
                    c =>
                    {
                        c.Descriptor = Kingmaker.Enums.ModifierDescriptor.Enhancement;
                        c.Stat = StatType.Constitution;
                        c.Value = 1;
                        c.Scaling = scaling;
                    }
                )
                .Configure();

            logger.Info("           Patching strength buff");
            BuffConfigurator.For(BuffRefs.TransmutationSchoolStrengthBuff.Reference.Get())
                .AddComponent<AddStatBonusScaled>(
                    c =>
                    {
                        c.Descriptor = Kingmaker.Enums.ModifierDescriptor.Enhancement;
                        c.Stat = StatType.Strength;
                        c.Value = 1;
                        c.Scaling = scaling;
                    }
                )
                .Configure();
        }

        private static void patchChangeShape()
        {
            logger.Info("   Patching change shape");

            logger.Info("       Patching change shape resource");
            AbilityResourceConfigurator.For(AbilityResourceRefs.TransmutationSchoolChangeShapeResource.Reference.Get())
                .ModifyMaxAmount(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();

            logger.Info("       Patching change shape feature add");
            FeatureConfigurator.For(FeatureRefs.TransmutationSchoolChangeShapeFeatureAdd.Reference.Get())
                .AddFeatureOnClassLevel(
                    clazz: BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass),
                    level: 12,
                    feature: FeatureRefs.TransmutationSchoolChangeShapeFeature.Reference.Get(),
                    beforeThisLevel: false
                )
                .Configure();
        }
    }
}
