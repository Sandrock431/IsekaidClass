using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Blueprints;
using BlueprintCore.Blueprints.CustomConfigurators;
using Kingmaker.Blueprints.Classes.Prerequisites;
using System.Linq;
using IsekaidClass.Utils;
using System;
using Kingmaker.Enums;

namespace IsekaidClass.Isekaid.ClassFeatures
{
    internal class PaladinFeatures
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(PaladinFeatures));

        private static readonly string ProgressionName = "PaladinFeatures";
        internal const string DisplayName = "PaladinFeatures.Name";
        private static readonly string Description = "PaladinFeatures.Description";

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.PaladinFeatures))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                logger.Error("PaladinFeatures.Configure", e);
            }
        }

        public static void ConfigureDisabled()
        {
            ProgressionConfigurator.New(ProgressionName, Guids.PaladinFeatures).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring Paladin Features progression");

            patchSmiteEvil();
            patchLayOnHands();
            patchMercies();
            patchAuraOfJusticeAbility();

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    FeatureRefs.SmiteEvilFeature.Reference.Get()
                )
                .AddEntry(
                    2,
                    FeatureRefs.LayOnHandsFeature.Reference.Get()
                )
                .AddEntry(
                    3,
                    FeatureRefs.AuraOfCourageFeature.Reference.Get(),
                    FeatureSelectionRefs.SelectionMercy.Reference.Get()
                )
                .AddEntry(
                    4,
                    FeatureRefs.SmiteEvilAdditionalUse.Reference.Get()
                )
                .AddEntry(
                    6,
                    FeatureSelectionRefs.SelectionMercy.Reference.Get()
                )
                .AddEntry(
                    7,
                    FeatureRefs.SmiteEvilAdditionalUse.Reference.Get()
                )
                .AddEntry(
                    8,
                    FeatureRefs.AuraOfResolveFeature.Reference.Get()
                )
                .AddEntry(
                    9,
                    FeatureSelectionRefs.SelectionMercy.Reference.Get()
                )
                .AddEntry(
                    10,
                    FeatureRefs.SmiteEvilAdditionalUse.Reference.Get()
                )
                .AddEntry(
                    11,
                    FeatureRefs.AuraOfJusticeFeature.Reference.Get()
                )
                .AddEntry(
                    12,
                    FeatureSelectionRefs.SelectionMercy.Reference.Get()
                )
                .AddEntry(
                    13,
                    FeatureRefs.SmiteEvilAdditionalUse.Reference.Get()
                )
                .AddEntry(
                    14,
                    FeatureRefs.AuraOfFaithFeature.Reference.Get()
                )
                .AddEntry(
                    15,
                    FeatureSelectionRefs.SelectionMercy.Reference.Get()
                )
                .AddEntry(
                    16,
                    FeatureRefs.SmiteEvilAdditionalUse.Reference.Get()
                )
                .AddEntry(
                    17,
                    FeatureRefs.AuraOfRighteousnessFeature.Reference.Get()
                )
                .AddEntry(
                    18,
                    FeatureSelectionRefs.SelectionMercy.Reference.Get()
                )
                .AddEntry(
                    19,
                    FeatureRefs.SmiteEvilAdditionalUse.Reference.Get()
                );

            var uiGroups = UIGroupBuilder.New()
                .AddGroup(
                    FeatureRefs.SmiteEvilFeature.Reference.Get(),
                    FeatureRefs.SmiteEvilAdditionalUse.Reference.Get()
                )
                .AddGroup(
                    FeatureRefs.AuraOfCourageFeature.Reference.Get(),
                    FeatureRefs.AuraOfFaithFeature.Reference.Get(),
                    FeatureRefs.AuraOfResolveFeature.Reference.Get(),
                    FeatureRefs.AuraOfRighteousnessFeature.Reference.Get(),
                    FeatureRefs.AuraOfJusticeFeature.Reference.Get(),
                    FeatureRefs.LayOnHandsFeature.Reference.Get()
                );

            ProgressionConfigurator.New(ProgressionName, Guids.PaladinFeatures)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.SmiteEvilFeature.Reference.Get().Icon)
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
                .SetUIGroups(uiGroups)
                .Configure();
        }

        private static void patchSmiteEvil()
        {
            logger.Info("   Patching smite evil");

            AbilityConfigurator.For(AbilityRefs.SmiteEvilAbility.Reference.Get())
                .EditComponents<ContextRankConfig>(
                    edit: c => c.m_Class = CommonTool.Append(
                        c.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    ),
                    predicate: c => c.name.Equals("$ContextRankConfig$808349e5-6a40-4c4d-96be-01e0a5db02ab") && c.m_Class.Length > 0
                )
                .Configure();
        }

        private static void patchLayOnHands()
        {
            logger.Info("   Patching lay on hands");

            // Lay on Hands resource
            AbilityResourceConfigurator.For(AbilityResourceRefs.LayOnHandsResource.Reference.Get())
                .ModifyMaxAmount(
                    c => c.m_ClassDiv = CommonTool.Append(
                        c.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();

            // Lay on Hands Self
            AbilityConfigurator.For(AbilityRefs.LayOnHandsSelf.Reference.Get())
                .EditComponent<ContextRankConfig>(
                    edit: c => c.m_Class = CommonTool.Append(
                        c.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();

            // Lay on Hands Others
            AbilityConfigurator.For(AbilityRefs.LayOnHandsOthers.Reference.Get())
                .EditComponent<ContextRankConfig>(
                    c => c.m_Class = CommonTool.Append(
                        c.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();

            // Lay on Hands feature
            FeatureConfigurator.For(FeatureRefs.LayOnHandsFeature.Reference.Get())
                .AddReplaceCasterLevelOfAbility(
                    spell: AbilityRefs.LayOnHandsSelf.Reference.Get(),
                    clazz: BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                )
                .AddReplaceCasterLevelOfAbility(
                    spell: AbilityRefs.LayOnHandsOthers.Reference.Get(),
                    clazz: BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                )
                .Configure();
        }

        private static void patchMercies()
        {
            logger.Info("   Patching mercies");

            // Only mercies with level requirements will be patched
            var mercies = new[]
            {
                FeatureRefs.MercyBlinded.Reference.Get(),
                FeatureRefs.MercyConfused.Reference.Get(),
                FeatureRefs.MercyCursed.Reference.Get(),
                FeatureRefs.MercyDazed.Reference.Get(),
                FeatureRefs.MercyDiseased.Reference.Get(),
                FeatureRefs.MercyExhausted.Reference.Get(),
                FeatureRefs.MercyFrightened.Reference.Get(),
                FeatureRefs.MercyNauseated.Reference.Get(),
                FeatureRefs.MercyParalyzed.Reference.Get(),
                FeatureRefs.MercyPoisoned.Reference.Get(),
                FeatureRefs.MercyStaggered.Reference.Get(),
                FeatureRefs.MercyStunned.Reference.Get(),
            };

            foreach (BlueprintFeature mercy in mercies)
            {
                FeatureConfigurator.For(mercy)
                    .EditComponents<PrerequisiteClassLevel>(
                        edit: c => c.Group = Prerequisite.GroupType.Any,
                        predicate: c => c.CharacterClass == CharacterClassRefs.PaladinClass.Reference.Get()
                            && c.Group == Prerequisite.GroupType.All
                    )
                    .AddPrerequisiteClassLevel(
                        characterClass: Guids.IsekaidClass,
                        group: Prerequisite.GroupType.Any,
                        checkInProgression: false,
                        hideInUI: false,
                        level: mercy.GetComponents<PrerequisiteClassLevel>()
                            .Select(c => c.Level)
                            .FirstOrDefault()
                    )
                    .Configure();
            }
        }

        private static void patchAuraOfJusticeAbility()
        {
            logger.Info("   Patching aura of justice ability");

            AbilityConfigurator.For(AbilityRefs.AuraOfJusticeAbility.Reference.Get())
                .EditComponents<ContextRankConfig>(
                    edit: c => c.m_Class = CommonTool.Append(
                        c.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    ),
                    predicate: c => c.m_Type == AbilityRankType.DamageBonus
                        && c.m_BaseValueType == ContextRankBaseValueType.ClassLevel
                        && c.m_Class.Length > 0
                )
                .Configure();
        }
    }
}
