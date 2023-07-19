using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using System;
using IsekaidClass.Utils;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Blueprints;
using BlueprintCore.Blueprints.CustomConfigurators;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Components;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace IsekaidClass.Isekaid.ClassFeatures
{
    internal class SorcererFeatures
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(SorcererFeatures));

        private static readonly string ProgressionName = "SorcererFeatures";
        internal const string DisplayName = "SorcererFeatures.Name";
        private static readonly string Description = "SorcererFeatures.Description";

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.SorcererFeatures))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                logger.Error("SorcererFeatures.Configure", e);
            }
        }

        public static void ConfigureDisabled()
        {
            ProgressionConfigurator.New(ProgressionName, Guids.SorcererFeatures).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring Sorcerer Features progression");

            patchBloodlines();

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    FeatureSelectionRefs.SorcererBloodlineSelection.Reference.Get(),
                    FeatureSelectionRefs.CrossbloodedSecondaryBloodlineSelection.Reference.Get()
                );

            ProgressionConfigurator.New(ProgressionName, Guids.SorcererFeatures)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(ProgressionRefs.BloodlineArcaneProgression.Reference.Get().Icon)
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
                .Configure();
        }

        private static void patchBloodlines()
        {
            logger.Info("   Patching bloodlines");

            // Patch bloodlines to work with class
            var bloodlines = new [] {
                ProgressionRefs.BloodlineAbyssalProgression.Reference.Get(),
                ProgressionRefs.BloodlineArcaneProgression.Reference.Get(),
                ProgressionRefs.BloodlineCelestialProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicBlackProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicBlueProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicBrassProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicBronzeProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicCopperProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicGoldProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicGreenProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicRedProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicSilverProgression.Reference.Get(),
                ProgressionRefs.BloodlineDraconicWhiteProgression.Reference.Get(),
                ProgressionRefs.BloodlineElementalAirProgression.Reference.Get(),
                ProgressionRefs.BloodlineElementalEarthProgression.Reference.Get(),
                ProgressionRefs.BloodlineElementalFireProgression.Reference.Get(),
                ProgressionRefs.BloodlineElementalWaterProgression.Reference.Get(),
                ProgressionRefs.BloodlineFeyProgression.Reference.Get(),
                ProgressionRefs.BloodlineInfernalProgression.Reference.Get(),
                ProgressionRefs.BloodlineSerpentineProgression.Reference.Get(),
                ProgressionRefs.BloodlineUndeadProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineAbyssalProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineArcaneProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineCelestialProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineDraconicBlackProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineDraconicBlueProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineDraconicBrassProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineDraconicBronzeProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineDraconicCopperProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineDraconicGoldProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineDraconicGreenProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineDraconicRedProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineDraconicSilverProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineDraconicWhiteProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineElementalAirProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineElementalEarthProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineElementalFireProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineElementalWaterProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineFeyProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineInfernalProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineSerpentineProgression.Reference.Get(),
                ProgressionRefs.CrossbloodedSecondaryBloodlineUndeadProgression.Reference.Get()
            };

            foreach (BlueprintFeature bloodline in bloodlines)
            {
                ProgressionConfigurator.For(bloodline)
                    .AddToClasses(Guids.IsekaidClass)
                    .Configure();
            }

            // Patch bloodline features
            var bloodlineFeatures = new[] {
                FeatureRefs.BloodlineAbyssalClawsFeatureAddLevel1,
                FeatureRefs.BloodlineAbyssalResistancesAbilityAdd,
                FeatureRefs.BloodlineAbyssalClawsFeatureAddLevel2,
                FeatureRefs.BloodlineAbyssalClawsFeatureLevel3,
                FeatureRefs.BloodlineAbyssalStrengthAbilityAddLevel1,
                FeatureRefs.BloodlineAbyssalStrengthAbilityAddLevel2,
                FeatureRefs.BloodlineArcaneCombatCastingAdeptFeatureAddLevel1,
                FeatureRefs.BloodlineArcaneCombatCastingAdeptFeatureAddLevel2,
                FeatureRefs.BloodlineCelestialResistancesAbilityAdd,
                FeatureRefs.BloodlineCelestialConvictionAbility,
                FeatureRefs.BloodlineDraconicBlackClawsFeatureAddLevel1,
                FeatureRefs.BloodlineDraconicBlackResistancesAbilityAddLevel1,
                FeatureRefs.BloodlineDraconicBlackClawsFeatureAddLevel2,
                FeatureRefs.BloodlineDraconicBlackClawsFeatureAddLevel3,
                FeatureRefs.BloodlineDraconicBlackResistancesAbilityAddLevel2,
                FeatureRefs.BloodlineDraconicBlueClawsFeatureAddLevel1,
                FeatureRefs.BloodlineDraconicBlueResistancesAbilityAddLevel1,
                FeatureRefs.BloodlineDraconicBlueClawsFeatureAddLevel2,
                FeatureRefs.BloodlineDraconicBlueClawsFeatureAddLevel3,
                FeatureRefs.BloodlineDraconicBlueResistancesAbilityAddLevel2,
                FeatureRefs.BloodlineDraconicBrassClawsFeatureAddLevel1,
                FeatureRefs.BloodlineDraconicBrassResistancesAbilityAddLevel1,
                FeatureRefs.BloodlineDraconicBrassClawsFeatureAddLevel2,
                FeatureRefs.BloodlineDraconicBrassClawsFeatureAddLevel3,
                FeatureRefs.BloodlineDraconicBrassResistancesAbilityAddLevel2,
                FeatureRefs.BloodlineDraconicBronzeClawsFeatureAddLevel1,
                FeatureRefs.BloodlineDraconicBronzeResistancesAbilityAddLevel1,
                FeatureRefs.BloodlineDraconicBronzeClawsFeatureAddLevel2,
                FeatureRefs.BloodlineDraconicBronzeClawsFeatureAddLevel3,
                FeatureRefs.BloodlineDraconicBronzeResistancesAbilityAddLevel2,
                FeatureRefs.BloodlineDraconicCopperClawsFeatureAddLevel1,
                FeatureRefs.BloodlineDraconicCopperResistancesAbilityAddLevel1,
                FeatureRefs.BloodlineDraconicCopperClawsFeatureAddLevel2,
                FeatureRefs.BloodlineDraconicCopperClawsFeatureAddLevel3,
                FeatureRefs.BloodlineDraconicCopperResistancesAbilityAddLevel2,
                FeatureRefs.BloodlineDraconicGoldClawsFeatureAddLevel1,
                FeatureRefs.BloodlineDraconicGoldResistancesAbilityAddLevel1,
                FeatureRefs.BloodlineDraconicGoldClawsFeatureAddLevel2,
                FeatureRefs.BloodlineDraconicGoldClawsFeatureAddLevel3,
                FeatureRefs.BloodlineDraconicGoldResistancesAbilityAddLevel2,
                FeatureRefs.BloodlineDraconicGreenClawsFeatureAddLevel1,
                FeatureRefs.BloodlineDraconicGreenResistancesAbilityAddLevel1,
                FeatureRefs.BloodlineDraconicGreenClawsFeatureAddLevel2,
                FeatureRefs.BloodlineDraconicGreenClawsFeatureAddLevel3,
                FeatureRefs.BloodlineDraconicGreenResistancesAbilityAddLevel2,
                FeatureRefs.BloodlineDraconicRedClawsFeatureAddLevel1,
                FeatureRefs.BloodlineDraconicRedResistancesAbilityAddLevel1,
                FeatureRefs.BloodlineDraconicRedClawsFeatureAddLevel2,
                FeatureRefs.BloodlineDraconicRedClawsFeatureAddLevel3,
                FeatureRefs.BloodlineDraconicRedResistancesAbilityAddLevel2,
                FeatureRefs.BloodlineDraconicSilverClawsFeatureAddLevel1,
                FeatureRefs.BloodlineDraconicSilverResistancesAbilityAddLevel1,
                FeatureRefs.BloodlineDraconicSilverClawsFeatureAddLevel2,
                FeatureRefs.BloodlineDraconicSilverClawsFeatureAddLevel3,
                FeatureRefs.BloodlineDraconicSilverResistancesAbilityAddLevel2,
                FeatureRefs.BloodlineDraconicWhiteClawsFeatureAddLevel1,
                FeatureRefs.BloodlineDraconicWhiteResistancesAbilityAddLevel1,
                FeatureRefs.BloodlineDraconicWhiteClawsFeatureAddLevel2,
                FeatureRefs.BloodlineDraconicWhiteClawsFeatureAddLevel3,
                FeatureRefs.BloodlineDraconicWhiteResistancesAbilityAddLevel2,
                FeatureRefs.BloodlineElementalAirResistanceAbilityAdd,
                FeatureRefs.BloodlineElementalEarthResistanceAbilityAdd,
                FeatureRefs.BloodlineElementalFireResistanceAbilityAdd,
                FeatureRefs.BloodlineElementalWaterResistanceAbilityAdd,
                FeatureRefs.BloodlineInfernalResistancesAbilityAdd,
                FeatureRefs.BloodlineSerpentineSerpentsFangBiteFeatureAddLevel1,
                FeatureRefs.BloodlineSerpentineSerpentsFangBiteFeatureAddLevel2,
                FeatureRefs.BloodlineSerpentineSerpentsFangBiteFeatureAddLevel3,
                FeatureRefs.BloodlineSerpentineSnakeskinFeatureAddLevel1,
                FeatureRefs.BloodlineSerpentineSerpentsFangBiteFeatureAddLevel4,
                FeatureRefs.BloodlineSerpentineSnakeskinFeatureAddLevel2,
            };

            foreach (Blueprint<BlueprintReference<BlueprintFeature>> bloodlineFeature in bloodlineFeatures)
            {
                FeatureConfigurator.For(bloodlineFeature.Reference.Get())
                    .EditComponents<AddFeatureOnClassLevel>(
                        edit: c => c.m_AdditionalClasses= CommonTool.Append(
                            c.m_AdditionalClasses,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                        ),
                        predicate: c => true
                    )
                    .Configure();
            }

            var bloodlineFeaturesToReplaceCasterLevel = new[] {
                FeatureRefs.BloodlineDraconicBlackBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicBlueBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicBrassBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicBronzeBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicCopperBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicGoldBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicGreenBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicRedBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicSilverBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicWhiteBreathWeaponFeature,
                FeatureRefs.BloodlineElementalAirElementalBlastFeature,
                FeatureRefs.BloodlineElementalEarthElementalBlastFeature,
                FeatureRefs.BloodlineElementalFireElementalBlastFeature,
                FeatureRefs.BloodlineElementalWaterElementalBlastFeature,
                FeatureRefs.BloodlineFeySoulOfTheFeyFeature,
                FeatureRefs.BloodlineInfernalHellfireFeature,
                FeatureRefs.BloodlineUndeadGraveTouchFeature,
                FeatureRefs.BloodlineUndeadGraspOfTheDeadFeature,
            };

            foreach (Blueprint<BlueprintReference<BlueprintFeature>> bloodlineFeature in bloodlineFeaturesToReplaceCasterLevel)
            {
                FeatureConfigurator.For(bloodlineFeature.Reference.Get())
                    .EditComponent<ReplaceCasterLevelOfAbility>(
                        c => c.m_AdditionalClasses = CommonTool.Append(
                            c.m_AdditionalClasses,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                        )
                    )
                    .Configure();
            }

            var bloodlineFeaturesToBindToClass = new[]
            {
                FeatureRefs.BloodlineDraconicBlackBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicBlueBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicBrassBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicBronzeBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicCopperBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicGoldBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicGreenBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicRedBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicSilverBreathWeaponFeature,
                FeatureRefs.BloodlineDraconicWhiteBreathWeaponFeature,
                FeatureRefs.BloodlineElementalAirElementalBlastFeature,
                FeatureRefs.BloodlineElementalEarthElementalBlastFeature,
                FeatureRefs.BloodlineElementalFireElementalBlastFeature,
                FeatureRefs.BloodlineElementalWaterElementalBlastFeature,
                FeatureRefs.BloodlineInfernalHellfireFeature,
                FeatureRefs.BloodlineSerpentineDenOfSpidersFeature,
                FeatureRefs.BloodlineUndeadGraspOfTheDeadFeature,
            };

            foreach (Blueprint<BlueprintReference<BlueprintFeature>> bloodlineFeature in bloodlineFeatures)
            {
                FeatureConfigurator.For(bloodlineFeature.Reference.Get())
                    .EditComponent<BindAbilitiesToClass>(
                        c => c.m_AdditionalClasses = CommonTool.Append(
                            c.m_AdditionalClasses,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                        )
                    )
                    .Configure();
            }

            // Patch bloodline resources
            var bloodlineResources = new[] {
                AbilityResourceRefs.BloodlineCelestialHeavenlyFireResource,
                AbilityResourceRefs.BloodlineCelestialAuraOfHeavenResource,
                AbilityResourceRefs.BloodlineElementalElementalBlastResource,
                AbilityResourceRefs.BloodlineFeyFleetingGlanceResource,
                AbilityResourceRefs.BloodlineInfernalHellfireResource,
            };

            foreach (Blueprint<BlueprintReference<BlueprintAbilityResource>> bloodlineResource in bloodlineResources)
            {
                AbilityResourceConfigurator.For(bloodlineResource.Reference.Get())
                    .ModifyMaxAmount(
                        a => a.m_Class = CommonTool.Append(
                            a.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                        )
                    )
                    .Configure();
            }

            // Patch bloodline abilities
            var bloodlineAbilities = new[] {
                AbilityRefs.BloodlineCelestialAuraOfHeavenAbility,
                AbilityRefs.BloodlineDraconicBlackBreathWeaponAbility,
                AbilityRefs.BloodlineDraconicBlueBreathWeaponAbility,
                AbilityRefs.BloodlineDraconicBrassBreathWeaponAbility,
                AbilityRefs.BloodlineDraconicBronzeBreathWeaponAbility,
                AbilityRefs.BloodlineDraconicCopperBreathWeaponAbility,
                AbilityRefs.BloodlineDraconicGoldBreathWeaponAbility,
                AbilityRefs.BloodlineDraconicGreenBreathWeaponAbility,
                AbilityRefs.BloodlineDraconicRedBreathWeaponAbility,
                AbilityRefs.BloodlineDraconicSilverBreathWeaponAbility,
                AbilityRefs.BloodlineDraconicWhiteBreathWeaponAbility,
                AbilityRefs.BloodlineElementalAirElementalRayAbility,
                AbilityRefs.BloodlineElementalAirElementalBlastAbility,
                AbilityRefs.BloodlineElementalEarthElementalRayAbility,
                AbilityRefs.BloodlineElementalEarthElementalBlastAbility,
                AbilityRefs.BloodlineElementalFireElementalRayAbility,
                AbilityRefs.BloodlineElementalFireElementalBlastAbility,
                AbilityRefs.BloodlineElementalWaterElementalRayAbility,
                AbilityRefs.BloodlineElementalWaterElementalBlastAbility,
                AbilityRefs.BloodlineFeySoulOfTheFeyAbility,
                AbilityRefs.BloodlineInfernalCorruptingTouchAbility,
                AbilityRefs.BloodlineInfernalHellfireAbility,
                AbilityRefs.BloodlineUndeadGraveTouchAbility,
                AbilityRefs.BloodlineUndeadGraspOfTheDeadAbility,
                AbilityRefs.BloodlineUndeadIncorporealFormAbility,
            };

            foreach (Blueprint<BlueprintReference<BlueprintAbility>> bloodlineAbility in bloodlineAbilities)
            {
                AbilityConfigurator.For(bloodlineAbility.Reference.Get())
                    .EditComponents<ContextRankConfig>(
                        edit: c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                        ),
                        predicate: c => c.m_Class.Length > 0
                    )
                    .Configure();
            }

            // Patch bloodline buffs
            var bloodlineBuffs = new[] {
                BuffRefs.BloodlineAbyssalArcanaBuff,
                BuffRefs.BloodlineCelestialArcanaBuff,
            };

            foreach (Blueprint<BlueprintReference<BlueprintBuff>> bloodlineBuff in bloodlineBuffs)
            {
                BuffConfigurator.For(bloodlineBuff.Reference.Get())
                    .EditComponents<ContextRankConfig>(
                        edit: c => c.m_Class = CommonTool.Append(
                            c.m_Class,
                            BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                        ),
                        predicate: c => c.m_Class.Length > 0
                    )
                    .Configure();
            }
        }
    }
}
