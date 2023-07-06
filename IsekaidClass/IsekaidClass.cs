using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Isekaid;
using Isekaid.Spells;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using System;
using Utils;
using static Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite;

namespace Class
{
    public class IsekaidClass
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(IsekaidClass));

        private static readonly string ClassName = "Isekaid";
        internal const string DisplayName = "Isekaid.Name";
        private static readonly string Description = "Isekaid.Description";

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.IsekaidClass))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                logger.Error("IsekaidClass.Configure", e);
            }
        }

        private static void ConfigureDisabled()
        {
            IsekaidProgression.ConfigureDisabled();
            SpellBook.ConfigureDisabled();
            CharacterClassConfigurator.New(ClassName, Guids.IsekaidClass).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring isekai'd class");

            IsekaidProgression.ConfigureEnabled();
            SpellBook.ConfigureEnabled();

            var isekaidClass = CharacterClassConfigurator.New(ClassName, Guids.IsekaidClass)
                .SetLocalizedName(DisplayName)
                .SetLocalizedDescription(Description)
                .SetLocalizedDescriptionShort("")
                .AddPrerequisiteIsPet(
                    group: GroupType.All,
                    checkInProgression: false,
                    hideInUI: true,
                    not: true
                )
                .SetSkillPoints(11)
                .SetHitDie(DiceType.D12)
                .SetHideIfRestricted(false)
                .SetPrestigeClass(false)
                .SetIsMythic(false)
                .SetBaseAttackBonus(StatProgressionRefs.BABFull.Reference.Get())
                .SetFortitudeSave(StatProgressionRefs.SavesHigh.Reference.Get())
                .SetReflexSave(StatProgressionRefs.SavesHigh.Reference.Get())
                .SetWillSave(StatProgressionRefs.SavesHigh.Reference.Get())
                .SetProgression(Guids.IsekaidClassProgression)
                .SetSpellbook(Guids.IsekaidClassSpellBook)
                .AddToClassSkills(
                    StatType.SkillAthletics,
                    StatType.SkillKnowledgeArcana,
                    StatType.SkillKnowledgeWorld,
                    StatType.SkillLoreNature,
                    StatType.SkillLoreReligion,
                    StatType.SkillMobility,
                    StatType.SkillPerception,
                    StatType.SkillPersuasion,
                    StatType.SkillStealth,
                    StatType.SkillThievery,
                    StatType.SkillUseMagicDevice
                )
                .SetIsDivineCaster(true)
                .SetIsArcaneCaster(true)
                .SetStartingGold(5000)
                .SetStartingItems(
                    ItemWeaponRefs.ColdIronDagger.Reference.Get(),
                    ItemWeaponRefs.CompositeLongbow.Reference.Get(),
                    ItemWeaponRefs.ColdIronShortsword.Reference.Get(),
                    ItemArmorRefs.ScalemailStandard.Reference.Get(),
                    ItemEquipmentUsableRefs.PotionOfCureLightWounds.Reference.Get(),
                    ItemEquipmentUsableRefs.ScrollOfMageArmor.Reference.Get(),
                    ItemEquipmentUsableRefs.ScrollOfMageShield.Reference.Get()
                )
                .SetPrimaryColor(46)
                .SetSecondaryColor(46)
                .AddToMaleEquipmentEntities(
                    "1aa1272c6312fc449b33f81f2f39bf6e",
                    "85e1930d5f4fe35498ec29c8dc689d53",
                    "aa33fb01b1db1444ca27ba2f537e5837"
                )
                .AddToFemaleEquipmentEntities(
                    "4eb7ca3ef38cbb6449c2a7db101d20fa",
                    "16db9eaeb326aa04c85fa7fde940b236",
                    "aa33fb01b1db1444ca27ba2f537e5837"
                )
                .SetDifficulty(1)
                .AddToRecommendedAttributes(
                    StatType.Charisma
                )
                .Configure();

            BlueprintCharacterClassReference classref = isekaidClass.ToReference<BlueprintCharacterClassReference>();
            BlueprintRoot root = BlueprintTool.Get<BlueprintRoot>("2d77316c72b9ed44f888ceefc2a131f6");
            root.Progression.m_CharacterClasses = CommonTool.Append(root.Progression.m_CharacterClasses, classref);
        }
    }
}
