using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Settings;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Blueprints.Classes.Spells;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.ClassFeatures.Wizard.SchoolSpecializations
{
    internal class EvocationSpecialization
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(EvocationSpecialization));

        private static readonly string ProgressionName = "EvocationSpecialization";
        private static readonly string Description = "WizardFeatures.EvocationSpecialization.Description";

        public static void ConfigureDisabled()
        {
            configureSpecialization(enabled: false);
            ProgressionConfigurator.New(ProgressionName, Guids.EvocationSpecialization).Configure();
        }

        public static BlueprintProgression ConfigureEnabled()
        {
            logger.Info("Configuring evocation specialization progression");

            //patchElementalWall();

            configureSpecialization(enabled: true);

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    Guids.EvocationSpecializationFeature
                )
                .AddEntry(
                    8,
                    FeatureRefs.ElementalWallFeature.Reference.Get()
                )
                .AddEntry(
                    20,
                    FeatureRefs.IntenseSpellsPenetration.Reference.Get()
                );

            return ProgressionConfigurator.New(ProgressionName, Guids.EvocationSpecialization)
                .CopyFrom(ProgressionRefs.SpecialisationSchoolEvocationProgression.Reference.Get())
                .SetDescription(Description)
                .AddToClasses(Guids.IsekaidClass)
                .SetLevelEntries(levelEntries)
                .Configure();
        }

        private static void configureSpecialization(bool enabled)
        {
            logger.Info("   Configuring base evocation specialization");

            string name = "EvocationSpecializationFeature";
            string description = "WizardFeatures.EvocationSpecialization.EvocationSpecializationFeature.Description";

            if (!enabled)
            {
                configureForceMissile(enabled: false);

                FeatureConfigurator.New(name, Guids.EvocationSpecializationFeature).Configure();
                return;
            }

            configureForceMissile(enabled: true);

            FeatureConfigurator.New(name, Guids.EvocationSpecializationFeature)
                .CopyFrom(FeatureRefs.SpecializationSchoolEvocation.Reference.Get())
                .SetDescription(description)
                .AddFacts(
                    facts: new () { Guids.ForceMissile },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                /*
                .AddComponent<IntenseSpells>(
                    c => c.m_Wizard = BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                )
                */
                .Configure();
        }

        private static void configureForceMissile(bool enabled)
        {
            logger.Info("       Configuring force missile");

            string name = "ForceMissile";
            string description = "WizardFeatures.EvocationSpecialization.ForceMissile.Description";

            if (!enabled)
            {
                configureForceMissile(enabled: false);

                FeatureConfigurator.New(name, Guids.ForceMissile).Configure();
                return;
            }

            configureForceMissileResource(enabled: true);
            configureForceMissileAbility(enabled: true);

            FeatureConfigurator.New(name, Guids.ForceMissile)
                .CopyFrom(FeatureRefs.ForceMissileFeature.Reference.Get())
                .SetDescription(description)
                .AddFacts(
                    facts: new() {
                        Guids.ForceMissileResource,
                        Guids.ForceMissileAbility
                    },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .AddReplaceAbilitiesStat(
                    ability: new() { Guids.ForceMissileAbility },
                    stat: StatType.Charisma
                )
                .Configure();
        }

        private static void configureForceMissileResource(bool enabled)
        {
            logger.Info("           Configuring force missile resource");

            string name = "ForceMissileResource";

            if (!enabled)
            {
                AbilityResourceConfigurator.New(name, Guids.ForceMissile).Configure();
                return;
            }

            AbilityResourceConfigurator.New(name, Guids.ForceMissileResource)
                .CopyFrom(AbilityResourceRefs.ForceMissileResource.Reference.Get())
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

        private static void configureForceMissileAbility(bool enabled)
        {
            logger.Info("           Configuring force missile ability");

            string name = "ForceMissileAbility";
            string description = "WizardFeatures.EvocationSpecialization.ForceMissile.Description";

            if (!enabled)
            {
                AbilityConfigurator.New(name, Guids.ForceMissile).Configure();
                return;
            }

            AbilityConfigurator.New(name, Guids.ForceMissileAbility)
                .CopyFrom(
                    blueprint: AbilityRefs.ForceMissileAbility.Reference.Get(),
                    componentTypes: new[]
                    {
                        typeof(AbilityDeliverProjectile),
                        typeof(AbilityEffectRunAction),
                        typeof(SpellComponent)
                    } 
                )
                .SetDescription(description)
                .AddAbilityResourceLogic(
                    requiredResource: Guids.ForceMissileResource,
                    isSpendResource: true,
                    costIsCustom: false,
                    amount: 1
                )
                .Configure();
        }

        private static void patchElementalWall()
        {
            logger.Info("   Patching elemental wall resource");
            AbilityResourceConfigurator.For(AbilityResourceRefs.ElementalWallResource.Reference.Get())
                .ModifyMaxAmount(
                        c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();
        }
    }
}
