using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using IsekaidClass.Isekaid.ClassFeatures.Magus.SwordSaint;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Settings;
using System;
using IsekaidClass.Utils;
using static Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic;
using HarmonyLib;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.EntitySystem.Entities;
using System.Reflection;

namespace IsekaidClass.Isekaid.ClassFeatures.Magus
{
    internal class MagusFeatures
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(MagusFeatures));

        private static readonly string ProgressionName = "MagusFeatures";
        internal const string DisplayName = "MagusFeatures.Name";
        private static readonly string Description = "MagusFeatures.Description";

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.MagusFeatures))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                logger.Error("MagusFeatures.Configure", e);
            }
        }

        public static void ConfigureDisabled()
        {
            ProgressionConfigurator.New(ProgressionName, Guids.MagusFeatures).Configure();
            configureMagusClassLevels(enabled: false);
            ArcanePool.ConfigureDisabled();
            configureSpellStrike(enabled: false);
            //configureSwordSaintAbilities(enabled: false);
        }

        public static BlueprintProgression ConfigureEnabled()
        {
            logger.Info("Configuring Magus Features progression");

            configureMagusClassLevels(enabled: true);
            ArcanePool.ConfigureEnabled();
            configureSpellStrike(enabled: true);
            //configureSwordSaintAbilities(enabled: true);

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    Guids.ArcanePool,
                    FeatureRefs.SpellCombatFeature.Reference.Get(),
                    FeatureSelectionRefs.SwordSaintChosenWeaponSelection.Reference.Get(),
                    Guids.CannyDefense
                )
                .AddEntry(
                    2,
                    //FeatureRefs.SpellStrikeFeature.Reference.Get()
                    Guids.IsekaidSpellStrike
                )
                .AddEntry(
                    3,
                    Guids.ArcanaSelection
                )
                .AddEntry(
                    4,
                    Guids.PerfectStrike
                )
                .AddEntry(
                    5,
                    FeatureRefs.ArcaneWeaponPlus2.Reference.Get()
                )
                .AddEntry(
                    6,
                    Guids.ArcanaSelection
                )
                .AddEntry(
                    7,
                    Guids.LightningDraw
                )
                .AddEntry(
                    8,
                    FeatureRefs.ImprovedSpellCombat.Reference.Get()
                )
                .AddEntry(
                    9,
                    FeatureRefs.ArcaneWeaponPlus3.Reference.Get(),
                    Guids.ArcanaSelection,
                    Guids.CriticalPerfection
                )
                .AddEntry(
                    11,
                    Guids.SuperiorReflexes
                )
                .AddEntry(
                    12,
                    Guids.ArcanaSelection
                )
                .AddEntry(
                    13,
                    FeatureRefs.ArcaneWeaponPlus4.Reference.Get(),
                    Guids.LethalFocus
                )
                .AddEntry(
                    14,
                    FeatureRefs.GreaterSpellCombat.Reference.Get()
                )
                .AddEntry(
                    15,
                    Guids.ArcanaSelection
                )
                .AddEntry(
                    16,
                    FeatureRefs.Counterstrike.Reference.Get()
                )
                .AddEntry(
                    17,
                    FeatureRefs.ArcaneWeaponPlus5.Reference.Get()
                )
                .AddEntry(
                    18,
                    Guids.ArcanaSelection  
                );

            var uiGroups = UIGroupBuilder.New()
                .AddGroup(
                    Guids.ArcanePool,
                    FeatureRefs.ArcaneWeaponPlus2.Reference.Get(),
                    FeatureRefs.ArcaneWeaponPlus3.Reference.Get(),
                    FeatureRefs.ArcaneWeaponPlus4.Reference.Get(),
                    FeatureRefs.ArcaneWeaponPlus5.Reference.Get()
                )
                .AddGroup(
                    FeatureRefs.SpellCombatFeature.Reference.Get(),
                    FeatureRefs.ImprovedSpellCombat.Reference.Get(),
                    FeatureRefs.GreaterSpellCombat.Reference.Get()
                )
                .AddGroup(
                    FeatureRefs.SpellStrikeFeature.Reference.Get(),
                    Guids.DimensionStrike,
                    Guids.PerfectStrike,
                    FeatureRefs.Counterstrike.Reference.Get(),
                    Guids.CriticalPerfection,
                    Guids.LethalFocus
                )
                .AddGroup(
                    Guids.CannyDefense,
                    Guids.SuperiorReflexes
                )
                .AddGroup(
                    Guids.ArcanaSelection,
                    Guids.LightningDraw
                );

            return ProgressionConfigurator.New(ProgressionName, Guids.MagusFeatures)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(FeatureRefs.SpellCombatFeature.Reference.Get().Icon)
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

        private static void configureMagusClassLevels(bool enabled)
        {
            logger.Info("   Configuring magus class levels for prerequisites");

            string name = "MagusClassLevels";
            string displayName = "MagusFeatures.MagusClassLevels.Name";
            string description = "MagusFeatures.MagusClassLevels.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.MagusClassLevels).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.MagusClassLevels)
                .SetDisplayName(displayName)
                .SetDescription(description)
                .SetDescriptionShort("")
                .SetIcon(FeatureRefs.SpellCombatFeature.Reference.Get().Icon)
                .AddClassLevelsForPrerequisites(
                    actualClass: Guids.IsekaidClass,
                    fakeClass: CharacterClassRefs.MagusClass.Reference.Get(),
                    modifier: 1.0,
                    summand: 0
                )
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(false)
                .SetIsClassFeature(true)
                .Configure();
        }

        private static void configureSpellStrike(bool enabled)
        {
            logger.Info("   Configuring spell strike");

            string name = "MagusFeatures.IsekaidSpellstrike.Name";
            string abilityName = "MagusFeatures.IsekaidSpellstrikeAbility.Name";
            string displayName = "MagusFeatures.IsekaidSpellstrike.DisplayName";
            string description = "MagusFeatures.IsekaidSpellstrike.Description";
            var icon = FeatureRefs.SpellStrikeFeature.Reference.Get().Icon;

            if (!enabled)
            {
                ActivatableAbilityConfigurator.New(abilityName, Guids.IsekaidSpellStrikeAbility).Configure();
                FeatureConfigurator.New(name, Guids.IsekaidSpellStrike).Configure();
                return;
            }

            var spellStrikeAbility = ActivatableAbilityConfigurator.New(abilityName, Guids.IsekaidSpellStrikeAbility)
                .SetDisplayName(displayName)
                .SetDescription(description)
                .SetIcon(icon)
                .SetIsOnByDefault()
                .SetDeactivateImmediately()
                .SetBuff(BuffRefs.SpellStrikeBuff.ToString())
                .Configure();

            FeatureConfigurator.New(name, Guids.IsekaidSpellStrike)
                .SetDisplayName(displayName)
                .SetDescription(description)
                .SetDescriptionShort("")
                .SetIcon(icon)
                .SetIsClassFeature(true)
                //.AddMagusMechanicPart(AddMagusMechanicPart.Feature.EldritchArcher)
                .AddMagusMechanicPart(AddMagusMechanicPart.Feature.Spellstrike)
                .AddFacts(new() { spellStrikeAbility })
                .Configure();
        }

        private static void configureSwordSaintAbilities(bool enabled)
        {
            logger.Info("   Configuring sword saint abilities");

            if (!enabled)
            {
                configureCannyDefense(enabled: false);
                configurePerfectStrike(enabled: false);
                configureLightningDraw(enabled: false);
                configureCriticalPerfection(enabled: false);
                configureSuperiorReflexes(enabled: false);
                configureLethalFocus(enabled: false);
                return;
            }

            configureCannyDefense(enabled: true);
            configurePerfectStrike(enabled: true);
            configureLightningDraw(enabled: true);
            configureCriticalPerfection(enabled: true);
            configureSuperiorReflexes(enabled: true);
            configureLethalFocus(enabled: true);

            //CannyDefense.Configure();
            //PerfectStrike.Configure();
            //LightningDraw.Configure();
            //CriticalPerfection.Configure();
            //SuperiorReflexes.Configure();
            //LethalFocus.Configure();
        }

        private static void configureCannyDefense(bool enabled)
        {
            logger.Info("       Configuring canny defense");

            string name = "MagusFeatures.SwordSaint.CannyDefense.Name";
            string description = "MagusFeatures.SwordSaint.CannyDefense.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.CannyDefense).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.CannyDefense)
                .CopyFrom(FeatureRefs.CannyDefense.Reference.Get())
                .SetDescription(description)
                .AddComponent<CannyDefenseComponent>()
                .Configure();
        }

        private static void configurePerfectStrike(bool enabled)
        {
            logger.Info("       Configuring perfect strike");

            string name = "MagusFeatures.SwordSaint.PerfectStrike.Name";
            string description = "MagusFeatures.SwordSaint.PerfectStrike.Description";

            if (!enabled)
            {
                configurePerfectStrikeAbility(enabled: false);
                configurePerfectStrikeCritAbility(enabled: false);
                FeatureConfigurator.New(name, Guids.PerfectStrike).Configure();
                return;
            }

            configurePerfectStrikeAbility(enabled: true);
            configurePerfectStrikeCritAbility(enabled: true);

            FeatureConfigurator.New(name, Guids.PerfectStrike)
                .CopyFrom(FeatureRefs.PerfectStrikeFeature.Reference.Get())
                .SetDescription(description)
                .AddFacts(
                    facts: new()
                    {
                        Guids.PerfectStrikeAbility,
                        Guids.PerfectStrikeCritAbility,
                    },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
                )
                .Configure();
        }

        private static void configurePerfectStrikeAbility(bool enabled)
        {
            logger.Info("       Configuring perfect strike ability");

            string name = "MagusFeatures.SwordSaint.PerfectStrikeAbility.Name";
            string description = "MagusFeatures.SwordSaint.PerfectStrikeAbility.Description";

            if (!enabled)
            {
                ActivatableAbilityConfigurator.New(name, Guids.PerfectStrikeAbility).Configure();
                return;
            }

            ActivatableAbilityConfigurator.New(name, Guids.PerfectStrikeAbility)
                .CopyFrom(ActivatableAbilityRefs.SwordSaintPerfectStrikeAbility.Reference.Get())
                .SetDescription(description)
                .AddActivatableAbilityResourceLogic(
                    requiredResource: Guids.ArcanePoolResource,
                    spendType: ResourceSpendType.AttackHit
                )
                .Configure();
        }

        private static void configurePerfectStrikeCritAbility(bool enabled)
        {
            logger.Info("       Configuring perfect strike crit ability");

            string name = "MagusFeatures.SwordSaint.PerfectStrikeCritAbility.Name";
            string description = "MagusFeatures.SwordSaint.PerfectStrikeCritAbility.Description";

            if (!enabled)
            {
                ActivatableAbilityConfigurator.New(name, Guids.PerfectStrikeCritAbility).Configure();
                return;
            }

            ActivatableAbilityConfigurator.New(name, Guids.PerfectStrikeCritAbility)
                .CopyFrom(ActivatableAbilityRefs.SwordSaintPerfectStrikeCritAbility.Reference.Get())
                .SetDescription(description)
                .AddActivatableAbilityResourceLogic(
                    requiredResource: Guids.ArcanePoolResource,
                    spendType: ResourceSpendType.AttackHit
                )
                .Configure();
        }

        private static void configureLightningDraw(bool enabled)
        {
            logger.Info("       Configuring lightning draw");

            string name = "MagusFeatures.SwordSaint.LightningDraw.Name";
            string description = "MagusFeatures.SwordSaint.LightningDraw.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.LightningDraw).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.LightningDraw)
                .CopyFrom(FeatureRefs.SwordSaintLightningDraw.Reference.Get())
                .SetDescription(description)
                .AddDerivativeStatBonus(
                    baseStat: StatType.Charisma,
                    derivativeStat: StatType.Initiative,
                    descriptor: Kingmaker.Enums.ModifierDescriptor.None
                )
                .AddRecalculateOnStatChange(
                    stat: StatType.Charisma,
                    useKineticistMainStat: false
                )
                .Configure();
        }

        private static void configureCriticalPerfection(bool enabled)
        {
            logger.Info("       Configuring critical perfection");

            string name = "MagusFeatures.SwordSaint.CriticalPerfection.Name";
            string description = "MagusFeatures.SwordSaint.CriticalPerfection.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.CriticalPerfection).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.CriticalPerfection)
                .CopyFrom(FeatureRefs.SwordSaintCriticalPerfection.Reference.Get())
                .SetDescription(description)
                .AddComponent<CriticalPerfectionComponent>()
                .Configure();
        }

        private static void configureSuperiorReflexes(bool enabled)
        {
            logger.Info("       Configuring superior reflexes");

            string name = "MagusFeatures.SwordSaint.SuperiorReflexes.Name";
            string description = "MagusFeatures.SwordSaint.SuperiorReflexes.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.SuperiorReflexes).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.SuperiorReflexes)
                .CopyFrom(FeatureRefs.SwordSaintSuperiorReflexes.Reference.Get())
                .SetDescription(description)
                .AddDerivativeStatBonus(
                    baseStat: StatType.Charisma,
                    derivativeStat: StatType.AttackOfOpportunityCount,
                    descriptor: Kingmaker.Enums.ModifierDescriptor.None
                )
                .AddRecalculateOnStatChange(
                    stat: StatType.Charisma,
                    useKineticistMainStat: false
                )
                .Configure();
        }

        private static void configureLethalFocus(bool enabled)
        {
            logger.Info("       Configuring lethal focus");

            string name = "MagusFeatures.SwordSaint.LethalFocus.Name";
            string description = "MagusFeatures.SwordSaint.LethalFocus.Description";

            if (!enabled)
            {
                FeatureConfigurator.New(name, Guids.LethalFocus).Configure();
                return;
            }

            FeatureConfigurator.New(name, Guids.LethalFocus)
                .CopyFrom(FeatureRefs.SwordSaintInstantFocus.Reference.Get())
                .SetDescription(description)
                .AddComponent<LethalFocusComponent>()
                .Configure();
        }

        /// <summary>
        /// Flags spells from the Isekai'd spellbook as valid "magus" spells so the magus features work normally.
        /// </summary>
        [HarmonyPatch(typeof(UnitPartMagus))]
        static class UnitPartMagus_Patch
        {
            static BlueprintSpellbook _spellbook;
            static BlueprintSpellbook IsekaidSpellbook
            {
                get
                {
                    _spellbook ??= BlueprintTool.Get<BlueprintSpellbook>(Guids.IsekaidClassSpellBook);
                    return _spellbook;
                }
            }

            [HarmonyPatch(nameof(UnitPartMagus.IsSpellFromMagusSpellList)), HarmonyPostfix]
            static void IsSpellFromMagusSpellList(AbilityData spell, ref bool __result)
            {
                if (spell.SpellbookBlueprint == IsekaidSpellbook)
                {
                    __result = true;
                }
            }

            [HarmonyPatch(nameof(UnitPartMagus.HasOneHandedMeleeWeaponAndFreehand)), HarmonyPostfix]
            static void HasOneHandedMeleeWeaponAndFreehand(UnitDescriptor unit, ref bool __result)
            {
                __result = true;
            }
        }

        [HarmonyPatch(typeof(UnitEntityData))]
        static class UnitEntityData_Patch
        {
            [HarmonyPatch(nameof(UnitEntityData.PreparedSpellCombat)), HarmonyPostfix]
            static void PreparedSpellCombat(UnitEntityData __instance, ref bool __result)
            {
                logger.Info("###############################");
                logger.Info($"PreparedSpellCombat: {__result}");

                UnitPartMagus unitPartMagus = __instance.Ensure<UnitPartMagus>();

                if (unitPartMagus == null)
                {
                    logger.Info("Null unit part magus");
                }
                else
                {
                    logger.Info($"IsCastMagusSpellInThisRound = {unitPartMagus.IsCastMagusSpellInThisRound}");
                    logger.Info($"LastCastedMagusSpellTime = {unitPartMagus.LastCastedMagusSpellTime}");
                    logger.Info($"LastAttackTime = {unitPartMagus.LastAttackTime}");
                    logger.Info($"CanUseSpellCombatInThisRound = {unitPartMagus.CanUseSpellCombatInThisRound}");
                }
                logger.Info("###############################");

            }

            [HarmonyPatch(nameof(UnitEntityData.PreparedSpellStrike)), HarmonyPostfix]
            static void PreparedSpellStrike(UnitEntityData __instance, ref bool __result, bool __runOriginal)
            {
                logger.Info("###############################");

                logger.Info($"PreparedSpellStrike: {__result}");
                logger.Info($"runOriginal: {__runOriginal}");

                UnitPartMagus unitPartMagus = __instance.Get<UnitPartMagus>();

                if (unitPartMagus == null)
                {
                    logger.Info("Null unit part magus");
                }
                else
                {
                    logger.Info($"IsCastMagusSpellInThisRound = {unitPartMagus.IsCastMagusSpellInThisRound}");
                    logger.Info($"LastCastedMagusSpellTime = {unitPartMagus.LastCastedMagusSpellTime}");
                    logger.Info($"LastAttackTime = {unitPartMagus.LastAttackTime}");
                    logger.Info($"Spellstrike.Active = {unitPartMagus.Spellstrike.Active}");

                    if (unitPartMagus.EldritchArcherSpell != null)
                    {
                        logger.Info($"EldritchArcherSpell = {unitPartMagus.EldritchArcherSpell}");
                    }
                    else
                    {
                        logger.Info($"EldritchArcherSpell = null");
                    }

                    UnitPartTouch unitPartTouch = __instance.Get<UnitPartTouch>();

                    if (unitPartTouch == null)
                    {
                        logger.Info("Null unit part touch");
                    }
                    else
                    {
                        logger.Info("Getting ability data");

                        Ability ability = unitPartTouch.Ability;

                        if (ability != null)
                        {
                            AbilityData abilityData = ability.Data;

                            if (abilityData != null)
                            {
                                logger.Info($"abilityData = {abilityData}");
                                logger.Info($"IsSpellFromMagusSpellList = {unitPartMagus.IsSpellFromMagusSpellList(abilityData)}");
                            }
                            else
                            {
                                logger.Info("Null ability data");

                            }
                        }
                        else
                        {
                            logger.Info("Ability = null");
                        }
                    }
                }
                logger.Info("###############################");

            }
        }
    }
}
