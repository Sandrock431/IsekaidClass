using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
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
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Items;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using Kingmaker.Enums;

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
            ArcanePool.ConfigureDisabled();
            configureSpellCombat(enabled: false);
            configureSpellStrike(enabled: false);
            configureSwordSaintAbilities(enabled: false);
        }

        public static BlueprintProgression ConfigureEnabled()
        {
            logger.Info("Configuring Magus Features progression");

            ArcanePool.ConfigureEnabled();
            configureSpellCombat(enabled: true);
            configureSpellStrike(enabled: true);
            configureSwordSaintAbilities(enabled: true);

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    Guids.ArcanePool,
                    Guids.IsekaidSpellCombat,
                    FeatureSelectionRefs.SwordSaintChosenWeaponSelection.Reference.Get(),
                    Guids.CannyDefense
                )
                .AddEntry(
                    2,
                    Guids.IsekaidSpellstrike
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
                )
                ;

            var uiGroups = UIGroupBuilder.New()
                .AddGroup(
                    Guids.ArcanePool,
                    FeatureRefs.ArcaneWeaponPlus2.Reference.Get(),
                    FeatureRefs.ArcaneWeaponPlus3.Reference.Get(),
                    FeatureRefs.ArcaneWeaponPlus4.Reference.Get(),
                    FeatureRefs.ArcaneWeaponPlus5.Reference.Get()
                )
                .AddGroup(
                    Guids.IsekaidSpellCombat,
                    FeatureRefs.ImprovedSpellCombat.Reference.Get(),
                    FeatureRefs.GreaterSpellCombat.Reference.Get()
                )
                .AddGroup(
                    Guids.IsekaidSpellstrike,
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
                )
                ;

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

        private static void configureSpellCombat(bool enabled)
        {
            logger.Info("   Configuring spell combat");

            string name = "MagusFeatures.IsekaidSpellCombat.Name";
            string abilityName = "MagusFeatures.IsekaidSpellCombatAbility.Name";
            string description = "MagusFeatures.IsekaidSpellCombat.Description";

            if (!enabled)
            {
                ActivatableAbilityConfigurator.New(abilityName, Guids.IsekaidSpellCombatAbility).Configure();
                FeatureConfigurator.New(name, Guids.IsekaidSpellCombat).Configure();
                return;
            }

            var spellCombatAbility = ActivatableAbilityConfigurator.New(abilityName, Guids.IsekaidSpellCombatAbility)
                .CopyFrom(ActivatableAbilityRefs.SpellCombatAbility.Reference.Get())
                .SetDescription(description)
                .SetIsOnByDefault()
                .SetDeactivateImmediately()
                .Configure();

            FeatureConfigurator.New(name, Guids.IsekaidSpellCombat)
                .CopyFrom(FeatureRefs.SpellCombatFeature.Reference.Get())
                .SetDescription(description)
                .AddMagusMechanicPart(AddMagusMechanicPart.Feature.EldritchArcher)
                .AddMagusMechanicPart(AddMagusMechanicPart.Feature.SpellCombat)
                .AddFacts(new() { spellCombatAbility })
                .Configure();
        }

        private static void configureSpellStrike(bool enabled)
        {
            logger.Info("   Configuring spell strike");

            string name = "MagusFeatures.IsekaidSpellstrike.Name";
            string abilityName = "MagusFeatures.IsekaidSpellstrikeAbility.Name";
            string description = "MagusFeatures.IsekaidSpellstrike.Description";

            if (!enabled)
            {
                ActivatableAbilityConfigurator.New(abilityName, Guids.IsekaidSpellstrikeAbility).Configure();
                FeatureConfigurator.New(name, Guids.IsekaidSpellstrike).Configure();
                return;
            }

            var spellStrikeAbility = ActivatableAbilityConfigurator.New(abilityName, Guids.IsekaidSpellstrikeAbility)
                .CopyFrom(ActivatableAbilityRefs.SpellStrikeAbility.Reference.Get())
                .SetDescription(description)
                .SetIsOnByDefault()
                .SetDeactivateImmediately()
                .Configure();

            FeatureConfigurator.New(name, Guids.IsekaidSpellstrike)
                .CopyFrom(FeatureRefs.SpellStrikeFeature.Reference.Get())
                .SetDescription(description)
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

            string abilityName = "MagusFeatures.SwordSaint.PerfectStrikeAbility.Name";
            string abilityDescription = "MagusFeatures.SwordSaint.PerfectStrikeAbility.Description";
            string buffName = "MagusFeatures.SwordSaint.PerfectStrikeBuff.Name";

            string critAbilityName = "MagusFeatures.SwordSaint.PerfectStrikeCritAbility.Name";
            string critAbilityDescription = "MagusFeatures.SwordSaint.PerfectStrikeCritAbility.Description";
            string critBuffName = "MagusFeatures.SwordSaint.PerfectStrikeCritBuffName.Name"; 

            if (!enabled)
            {
                BuffConfigurator.New(buffName, Guids.PerfectStrikeBuff).Configure();
                ActivatableAbilityConfigurator.New(abilityName, Guids.PerfectStrikeAbility).Configure();
                BuffConfigurator.New(critBuffName, Guids.PerfectStrikeCritBuff).Configure();
                ActivatableAbilityConfigurator.New(critAbilityName, Guids.PerfectStrikeCritAbility).Configure();
                FeatureConfigurator.New(name, Guids.PerfectStrike).Configure();
                return;
            }

            // Perfect Strike Ability
            var buff = BuffConfigurator.New(buffName, Guids.PerfectStrikeBuff)
                .CopyFrom(BuffRefs.SwordSaintPerfectStrikeBuff.Reference.Get())
                .SetDescription(abilityDescription)
                .AddComponent<PerfectStrikeComponent>()
                .Configure();
            
            var ability = ActivatableAbilityConfigurator.New(abilityName, Guids.PerfectStrikeAbility)
                .CopyFrom(ActivatableAbilityRefs.SwordSaintPerfectStrikeAbility.Reference.Get())
                .SetDescription(abilityDescription)
                .AddActivatableAbilityResourceLogic(
                    requiredResource: Guids.ArcanePoolResource,
                    spendType: ResourceSpendType.AttackHit
                )
                .SetBuff(buff)
                .Configure();

            // Perfect Strike Crit Ability
            var critBuff = BuffConfigurator.New(critBuffName, Guids.PerfectStrikeCritBuff)
                .CopyFrom(BuffRefs.SwordSaintPerfectStrikeCritBuff.Reference.Get())
                .SetDescription(critAbilityDescription)
                .AddComponent<PerfectStrikeCritComponent>()
                .Configure();

            var critAbility = ActivatableAbilityConfigurator.New(critAbilityName, Guids.PerfectStrikeCritAbility)
                .CopyFrom(ActivatableAbilityRefs.SwordSaintPerfectStrikeCritAbility.Reference.Get())
                .SetDescription(critAbilityDescription)
                .AddActivatableAbilityResourceLogic(
                    requiredResource: Guids.ArcanePoolResource,
                    spendType: ResourceSpendType.AttackHit
                )
                .SetBuff(critBuff)
                .Configure();

            // Perfect Strike feature
            FeatureConfigurator.New(name, Guids.PerfectStrike)
                .CopyFrom(FeatureRefs.SwordSaintPerfectStrikeFeature.Reference.Get())
                .SetDescription(description)
                .AddFacts(
                    facts: new()
                    {
                        ability,
                        critAbility
                    },
                    casterLevel: 0,
                    doNotRestoreMissingFacts: false,
                    hasDifficultyRequirements: false,
                    invertDifficultyRequirements: false,
                    minDifficulty: GameDifficultyOption.Story
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
                    descriptor: ModifierDescriptor.None
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

        public class CannyDefenseComponent : UnitFactComponentDelegate, IGlobalSubscriber, ISubscriber
        {
            private static readonly LogWrapper logger = LogWrapper.Get(nameof(CannyDefenseComponent));

            public override void OnTurnOn()
            {
                base.OnTurnOn();
                ActivateModifier();
            }

            public override void OnTurnOff()
            {
                base.OnTurnOff();
                DeactivateModifier();
            }

            private void ActivateModifier()
            {
                int value = Math.Min(
                    Owner.Stats.Charisma.Bonus,
                    Owner.Progression.GetClassLevel(BlueprintTool.Get<BlueprintCharacterClass>(Guids.IsekaidClass))
                );
                Owner.Stats.AC.AddModifierUnique(value, Runtime, ModifierDescriptor.Dodge);
            }

            private void DeactivateModifier()
            {
                Owner.Stats.AC.RemoveModifiersFrom(Runtime);
            }
        }

        public class CriticalPerfectionComponent : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackRoll>, IRulebookHandler<RuleAttackRoll>, ISubscriber, IInitiatorRulebookSubscriber
        {
            private static readonly LogWrapper logger = LogWrapper.Get(nameof(CriticalPerfectionComponent));

            public void OnEventAboutToTrigger(RuleAttackRoll evt)
            {
                evt.CriticalConfirmationBonus += Math.Max(0, Owner.Stats.Charisma.Bonus);
            }

            public void OnEventDidTrigger(RuleAttackRoll evt)
            {
            }
        }

        public class LethalFocusComponent : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>, ISubscriber, IInitiatorRulebookSubscriber
        {
            private static readonly LogWrapper logger = LogWrapper.Get(nameof(LethalFocusComponent));

            public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
            {
                evt.WeaponStats.AddDamageModifier(Math.Max(0, Owner.Stats.Charisma.Bonus), Fact, ModifierDescriptor.UntypedStackable);
            }

            public void OnEventDidTrigger(RuleAttackWithWeapon evt)
            {
            }
        }

        [TypeId("087af39e46f54aa89e4124fa6c52b58a")]
        [AllowMultipleComponents]
        [AllowedOn(typeof(BlueprintUnitFact), false)]
        public class PerfectStrikeComponent : UnitFactComponentDelegate, ISubscriber, IInitiatorRulebookSubscriber, IInitiatorRulebookHandler<RuleAttackWithWeapon>, IRulebookHandler<RuleAttackWithWeapon>
        {
            public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
            {
                evt.Maximized.Set(value: true, base.Fact);
            }

            public void OnEventDidTrigger(RuleAttackWithWeapon evt)
            {
            }
        }

        [TypeId("bb031a5418e34ddd9de67a39b621be77")]
        [AllowMultipleComponents]
        [AllowedOn(typeof(BlueprintUnitFact), false)]
        public class PerfectStrikeCritComponent : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>, IRulebookHandler<RuleCalculateWeaponStats>, ISubscriber, IInitiatorRulebookSubscriber
        {
            public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
            {
                evt.AdditionalCriticalMultiplier.Add(new Modifier(1, base.Fact, ModifierDescriptor.UntypedStackable));
            }

            public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
            {
            }
        }

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

            /// <summary>
            /// Flags spells from the Isekai'd spellbook as valid "magus" spells so the magus features work normally.
            /// </summary>
            [HarmonyPatch(nameof(UnitPartMagus.IsSpellFromMagusSpellList)), HarmonyPostfix]
            [HarmonyPriority(1000)]
            static void IsSpellFromMagusSpellList(AbilityData spell, UnitPartMagus __instance, ref bool __result)
            {
                if (spell.IsInSpellList(IsekaidSpellbook.SpellList) || spell.Caster.GetSpellbook(IsekaidSpellbook).IsKnown(spell.Blueprint))
                {
                    __result = true;
                }
            }

            /// <summary>
            /// Disables weapon check
            /// </summary>
            [HarmonyPatch(nameof(UnitPartMagus.HasOneHandedMeleeWeaponAndFreehand)), HarmonyPostfix]
            [HarmonyPriority(1000)]
            static void HasOneHandedMeleeWeaponAndFreehand(UnitDescriptor unit, ref bool __result)
            {
                __result = true;
            }
        }

        [HarmonyPatch(typeof(UnitUseAbility))]
        static class UnitUseAbility_Patch
        {
            /// <summary>
            /// Allows both melee or ranged spellstrike
            /// </summary>
            [HarmonyPatch(nameof(UnitUseAbility.CreateCastCommand)), HarmonyPostfix]
            [HarmonyPriority(1000)]
            static void CreateCastCommand(AbilityData spell, TargetWrapper target, UnitUseAbility __instance, ref UnitCommand __result)
            {
                UnitEntityData unit = spell.Caster.Unit;
                UnitPartTouch unitPartTouch = unit.Get<UnitPartTouch>();
                if (unitPartTouch != null)
                {
                    BlueprintAbility blueprint = unitPartTouch.Ability.Blueprint;
                    AbilityEffectStickyTouch abilityEffectStickyTouch = spell.Blueprint.StickyTouch.Or(null);

                    if (blueprint == ((abilityEffectStickyTouch != null) ? abilityEffectStickyTouch.TouchDeliveryAbility : null) && unitPartTouch.Ability.SourceItem == spell.SourceItem)
                    {
                        spell = unitPartTouch.Ability.Data;
                    }
                }

                UnitPartMagus unitPartMagus = unit.Get<UnitPartMagus>();
                if (unitPartMagus != null && target.Unit != null)
                {
                    if (unitPartMagus.EldritchArcher)
                    {
                        bool flag;
                        if (unitPartMagus.EldritchArcherSpell == spell && unitPartMagus.Spellstrike.Active && unit.IsEnemy(target.Unit))
                        {
                            ItemEntityWeapon firstWeapon = unit.GetFirstWeapon();
                            flag = firstWeapon != null && firstWeapon.Blueprint.IsRanged;
                        }
                        else
                        {
                            flag = false;
                        }

                        if (flag)
                        {
                            __result = new UnitAttack(target.Unit, null);
                        }
                    }

                    if (unitPartTouch != null
                        && unitPartTouch.Ability.Data == spell
                            && unitPartMagus.Spellstrike.Active
                            && unitPartMagus.IsSpellFromMagusSpellList(unitPartTouch.Ability.Data)
                            && unit.IsEnemy(target.Unit) && unit.GetThreatHand() != null
                        )
                    {
                        __result = new UnitAttack(target.Unit, null);
                    }
                }
            }
        }
    }
}
