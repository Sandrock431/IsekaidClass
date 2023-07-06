using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using BlueprintCore.Utils;
using Class;
using Kingmaker.Blueprints.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Blueprints;
using BlueprintCore.Blueprints.CustomConfigurators;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Enums;
using Kingmaker.Settings;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using System.Collections.Generic;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.UnitLogic.FactLogic;
using System.Linq;
using Kingmaker.Utility;
using Utils;

namespace Isekaid.ClassFeatures.Wizard.SchoolSpecializations
{
    internal class AbjurationSpecialization
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(AbjurationSpecialization));

        private static readonly string ProgressionName = "AbjurationSpecialization";
        private static readonly string Description = "WizardFeatures.AbjurationSpecialization.Description";

        public static void ConfigureDisabled()
        {
            ProgressionConfigurator.New(ProgressionName, Guids.AbjurationSpecialization).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring abjuration specialization progression");

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    Guids.AbjurationSpecializationFeature
                )
                .AddEntry(
                    6,
                    //Guids.EnergyAbsorption
                    FeatureRefs.EnergyAbsorptionFeature.Reference.Get()
                );

            ProgressionConfigurator.New(ProgressionName, Guids.AbjurationSpecialization)
                .CopyFrom(ProgressionRefs.SpecialisationSchoolAbjurationProgression.Reference.Get())
                .SetDescription(Description)
                .AddToClasses(Guids.IsekaidClass)
                .SetLevelEntries(levelEntries)
                .Configure();
        }

        private static void configureSpecialisation(bool enabled)
        {
            logger.Info("   Configuring base abjuration specialization");

            string featureName = "AbjurationSpecializationFeature";
            string description = "WizardFeatures.AbjurationSpecialization.Description";

            if (!enabled)
            {
                configureProtectiveWardFeature(enabled: false);
                //configureAbjurationResistance(enabled: false);

                FeatureConfigurator.New(featureName, Guids.AbjurationSpecializationFeature).Configure();
                return;
            }

            configureProtectiveWardFeature(enabled: true);
            //configureAbjurationResistance(enabled: true);

            FeatureConfigurator.New(featureName, Guids.AbjurationSpecializationFeature)
                .CopyFrom(ProgressionRefs.SpecialisationSchoolAbjurationProgression.Reference.Get())
                .SetDescription(description)
                .AddFacts(
                    facts: new ()
                    {
                        Guids.ProtectiveWard,
                        //Guids.AbjurationResistance
                        FeatureRefs.AbjurationResistanceFeature.Reference.Get()
                    },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .Configure();
        }

        private static void configureProtectiveWardFeature(bool enabled)
        {
            logger.Info("       Configuring protective ward feature");

            string resourceName = "ProtectiveWardResource";
            string abilityName = "WizardFeatures.AbjurationSpecialization.ProtectiveWardAbility.Name";
            string abilityDescription = "WizardFeatures.AbjurationSpecialization.ProtectiveWard.Description";
            string featureName = "ProtectiveWard";
            string featureDescription = "WizardFeatures.AbjurationSpecialization.ProtectiveWard.Description";

            if (!enabled)
            {
                AbilityResourceConfigurator.New(resourceName, Guids.ProtectiveWardResource).Configure();
                AbilityConfigurator.New(abilityName, Guids.ProtectiveWardAbility).Configure();
                FeatureConfigurator.New(featureName, Guids.ProtectiveWard).Configure();
                return;
            }

            /*
            logger.Info("           Patching protective ward effect buff");

            BuffConfigurator.For(BuffRefs.ProtectiveWardEffectBuff.Reference.Get())
                .EditComponent<ContextRankConfig>(c => c.m_Class = CommonTool.Append(
                        c.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    ))
                .Configure();
            */

            logger.Info("           Configuring protective ward resource");

            AbilityResourceConfigurator.New(resourceName, Guids.ProtectiveWardResource)
                .CopyFrom(AbilityResourceRefs.ProtectiveWardResource.Reference.Get())
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

            logger.Info("           Configuring protective ward ability");

            var rankConfig = new ContextRankConfig();
            rankConfig.m_Type = AbilityRankType.ProjectilesCount;
            rankConfig.m_BaseValueType = ContextRankBaseValueType.StatBonus;
            rankConfig.m_Stat = StatType.Charisma;
            rankConfig.m_SpecificModifier = ModifierDescriptor.None;
            rankConfig.m_Progression = ContextRankProgression.AsIs;
            rankConfig.m_StartLevel = 0;
            rankConfig.m_StartLevel = 0;
            rankConfig.m_UseMin = false;
            rankConfig.m_Min = 1;
            rankConfig.m_UseMax = false;
            rankConfig.m_Max = 20;
            rankConfig.m_ExceptClasses = false;
            rankConfig.m_Class = new[] { BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass) };

            AbilityConfigurator.New(abilityName, Guids.ProtectiveWardAbility)
                .CopyFrom(
                    blueprint: AbilityRefs.ProtectiveWardAbility.Reference.Get(),
                    componentTypes: typeof(AbilityEffectRunAction)
                )
                .AddAbilityResourceLogic(
                    requiredResource: Guids.ProtectiveWardResource,
                    isSpendResource: true,
                    costIsCustom: false,
                    amount: 1
                )
                .AddContextRankConfig(
                    component: rankConfig
                )
                .SetDescription(abilityDescription)
                .Configure();

            logger.Info("           Configuring protective ward feature");

            FeatureConfigurator.New(featureName, Guids.ProtectiveWard)
                .CopyFrom(FeatureRefs.ProtectiveWardFeature.Reference.Get())
                .AddFacts(
                    facts: new () { Guids.ProtectiveWardAbility },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .AddAbilityResources(
                    resource: Guids.ProtectiveWardResource,
                    useThisAsResource: false,
                    amount: 0,
                    restoreAmount: true,
                    restoreOnLevelUp: false
                )
                .SetDescription(featureDescription)
                .Configure();
        }

        /*
        private static void configureAbjurationResistance(bool enabled)
        {
            logger.Info("   Configuring abjuration resistance");

            string resourceName = "AbjurationResistanceResource";
            string featureName = "AbjurationResistance";
            string featureDescription = "WizardFeatures.AbjurationSpecialization.ProtectiveWard.Description";

            if (!enabled)
            {
                AbilityResourceConfigurator.New(resourceName, Guids.AbjurationResistanceResource).Configure();
                return;
            }

            logger.Info("       Configuring abjuration resistance resource");

            AbilityResourceConfigurator.New(resourceName, Guids.AbjurationResistanceResource)
                .CopyFrom(AbilityResourceRefs.AbjurationResistanceResource.Reference.Get())
                .SetMaxAmount(
                    new BlueprintAbilityResource.Amount
                    {
                        BaseValue = 1,
                        IncreasedByLevel = false,
                        LevelIncrease = 0,
                        IncreasedByLevelStartPlusDivStep = false,
                        StartingLevel = 0,
                        StartingIncrease = 0,
                        LevelStep = 0,
                        PerStepIncrease = 0,
                        MinClassLevelIncrease = 0,
                        OtherClassesModifier = 0,
                        IncreasedByStat = false,
                        ResourceBonusStat = StatType.Charisma
                    }
                )
                .Configure();

            logger.Info("       Configuring abjuration resistance feature");

            List<BlueprintBuff> buffs = new List<(BlueprintBuff, BlueprintFeature, string)>() {
                    (
                        BuffRefs.AbjurationResistanceAcidBuff.Reference.Get(),
                        FeatureRefs.AcidImmunity.Reference.Get(),
                        Guids.AbjurationResistanceAcidBuff
                    ),
                    (
                        BuffRefs.AbjurationResistanceColdBuff.Reference.Get(),
                        FeatureRefs.ColdImmunity.Reference.Get(),
                        Guids.AbjurationResistanceAcidBuff
                    ),
                    (
                        BuffRefs.AbjurationResistanceElectricityBuff.Reference.Get(),
                        FeatureRefs.ElectricityImmunity.Reference.Get(),
                        Guids.AbjurationResistanceElectricityBuff
                    ),
                    (
                        BuffRefs.AbjurationResistanceFireBuff.Reference.Get(),
                        FeatureRefs.FireImmunity.Reference.Get(),
                        Guids.AbjurationResistanceFireBuff
                    ),
                    (
                        BuffRefs.AbjurationResistanceSonicBuff.Reference.Get(),
                        FeatureRefs.SonicImmunity.Reference.Get(),
                        Guids.AbjurationResistanceSonicBuff
                    )
                }.Select(config => configureResistanceElementBuff(
                    baseBuff: config.Item1,
                    immunity: config.Item2,
                    guid: config.Item3
                )).ToList();

            List<BlueprintAbility> abilities = new List<(DamageEnergyType, BlueprintAbility, string)>() {
                    (
                        DamageEnergyType.Acid,
                        AbilityRefs.AbjurationResistanceAcidAbility.Reference.Get(),
                        Guids.AbjurationResistanceAcidAbility
                    ),
                    (
                        DamageEnergyType.Cold,
                        AbilityRefs.AbjurationResistanceColdAbility.Reference.Get(),
                        Guids.AbjurationResistanceColdAbility
                    ),
                    (
                        DamageEnergyType.Electricity,
                        AbilityRefs.AbjurationResistanceElectricityAbility.Reference.Get(),
                        Guids.AbjurationResistanceElectricityAbility
                    ),
                    (
                        DamageEnergyType.Fire,
                        AbilityRefs.AbjurationResistanceFireAbility.Reference.Get(),
                        Guids.AbjurationResistanceFireAbility
                    ),
                    (
                        DamageEnergyType.Sonic,
                        AbilityRefs.AbjurationResistanceSonicAbility.Reference.Get(),
                        Guids.AbjurationResistanceSonicAbility
                    )
                }.Select(config => configureResistanceElementAbility(
                    energyType: config.Item1,
                    baseAbility: config.Item2,
                    buffs: buffs,
                    guid: config.Item3,
                    enabled: enabled
                )).ToList();


            FeatureConfigurator.New(featureName, Guids.AbjurationResistance)
                .CopyFrom(FeatureRefs.AbjurationResistanceFeature.Reference.Get())
                .AddAbilityResources(
                    resource: Guids.AbjurationResistanceResource,
                    useThisAsResource: false,
                    amount: 0,
                    restoreAmount: true,
                    restoreOnLevelUp: false
                )
                .AddFacts(
                    facts: abilities.ConvertAll(ability => (Blueprint<BlueprintUnitFactReference>)ability),
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .Configure();
        }

        private static void configureResistanceElementAbility(
            DamageEnergyType energyType,
            BlueprintAbility baseAbility,
            List<BlueprintBuff> buffs,
            string guid,
            bool enabled
        )
        {
            logger.Info($"          Configuring resistance element ability - {energyType}");

            var actions = ActionsBuilder.New()
                .ApplyBuffPermanent(
                    buff: buffs.Find(buff => buff.GetComponent<AddDamageResistanceEnergy>().Type == energyType),
                    isFromSpell: false,
                    isNotDispelable: false,
                    toCaster: false,
                    asChild: true,
                    sameDuration: false
                );
            
            // Append all remove buffs
            buffs.Where(buff => buff.GetComponent<AddDamageResistanceEnergy>().Type != energyType)
                .ForEach(buff => actions.RemoveBuff(
                    buff: buff,
                    removeRank: false,
                    toCaster: false
                ));

            AbilityConfigurator.New($"ResistanceElement{energyType}Ability", guid)
                .CopyFrom(
                    blueprint: baseAbility,
                    componentTypes: typeof(AbilitySpawnFx)
                )
                .AddAbilityResourceLogic(
                    requiredResource: Guids.AbjurationResistanceResource,
                    isSpendResource: true,
                    costIsCustom: false,
                    amount: 1
                )
                .AddAbilityEffectRunAction(
                    actions: actions
                )
                .Configure();
        }

        private static void configureResistanceElementBuff(BlueprintBuff baseBuff, BlueprintFeature immunity, string guid)
        {
            var energyType = baseBuff.GetComponent<WizardAbjurationResistance>().Type;

            logger.Info($"          Configuring resistance element buff - {energyType}");

            BuffConfigurator.New($"WizardFeatures.AbjurationSpecialization.EnergyResistance.ResistanceElement{energyType}Buff.Name", guid)
                .CopyFrom(
                   blueprint: baseBuff,
                   componentTypes: typeof(ContextRankConfig)
                )
                .AddDamageResistanceEnergy(
                    type: energyType,
                    value: ContextValues.Rank()
                )
                .AddFeatureOnClassLevel(
                    clazz: Guids.IsekaidClass,
                    level: 20,
                    feature: immunity
                )
                .Configure();
        }
        */

        private static void configureEnergyAbsorption(bool enabled)
        {
            logger.Info("   Configuring energy absorption");

            logger.Info("       Configuring energy absorption resource");

            var resourceGuid = "f4d04faad08d4ca69633960b4e8748ca";
            var resource = AbilityResourceConfigurator.New("WizardFeatures.AbjurationSpecialization.EnergyAbsorptionResource.Name", resourceGuid)
                .CopyFrom(AbilityResourceRefs.EnergyAbsorptionResource.Reference.Get())
                .SetMaxAmount(ResourceAmountBuilder.New(0)
                    .IncreaseByLevel(
                        classes: new[] { Guids.IsekaidClass },
                        bonusPerLevel: 3
                    ).Build()
                )
                .Configure();

            logger.Info("       Configuring energy absorption feature");

            FeatureConfigurator.New("WizardFeatures.AbjurationSpecialization.EnergyAbsorption.Name", Guids.EnergyAbsorption)
                .CopyFrom(
                    blueprint: FeatureRefs.EnergyAbsorptionFeature.Reference.Get(),
                    componentTypes: new[] {
                        typeof(WizardEnergyAbsorption)
                    }
                )
                .SetDescription("WizardFeatures.AbjurationSpecialization.EnergyResistance.Description")
                .AddAbilityResources(
                    resource: resource,
                    useThisAsResource: false,
                    amount: 0,
                    restoreAmount: true,
                    restoreOnLevelUp: false
                )
                .EditComponents<WizardEnergyAbsorption>(
                    edit: c => c.m_Resource = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(resourceGuid),
                    predicate: c => true
                )
                .Configure();
        }
    }
}
