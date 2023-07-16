using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using IsekaidClass.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Enums;
using Kingmaker.Settings;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;

namespace IsekaidClass.Isekaid.ClassFeatures.Alchemist
{
    internal class DiscoverySelection
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(DiscoverySelection));

        private static readonly string FeatureName = "DiscoverySelection";
        private static readonly string Description = "AlchemistFeatures.DiscoverySelection.Description";
        
        private static readonly Blueprint<BlueprintFeatureReference>[] discoveries = new Blueprint<BlueprintFeatureReference>[]
        {
            Guids.AcidBombsFeature,
            Guids.BlindingBombsFeature,
            Guids.BreathWeaponBombFeature,
            Guids.ChokingBombFeature,
            FeatureRefs.CognatogenFeature.Reference.Get(),
            Guids.CursedBombsFeature,
            Guids.DispellingBombsFeature,
            FeatureRefs.EnhancePotion.Reference.Get(),
            Guids.ExplosiveBombsFeature,
            FeatureRefs.ExtendPotion.Reference.Get(),
            FeatureRefs.FastBombsFeature.Reference.Get(),
            FeatureRefs.FeralMutagenFeature.Reference.Get(),
            FeatureRefs.FeralWings.Reference.Get(),
            Guids.ForceBombsFeature,
            Guids.FrostBombsFeature,
            FeatureRefs.GrandCognatogenFeature.Reference.Get(),
            FeatureRefs.GrandMutagenFeature.Reference.Get(),
            FeatureRefs.GreaterCognatogenFeature.Reference.Get(),
            FeatureRefs.GreaterMutagenFeature.Reference.Get(),
            Guids.HolyBombsFeature,
            FeatureRefs.Infusion.Reference.Get(),
            FeatureRefs.Mummification.Reference.Get(),
            FeatureRefs.NauseatingFlesh.Reference.Get(),
            FeatureRefs.PreciseBomb.Reference.Get(),
            FeatureRefs.PreserveOrgans.Reference.Get(),   
            Guids.ShockBombsFeature,
            FeatureRefs.SpontaneousHealingFeature.Reference.Get(),
            Guids.TanglefootBombsFeature
        };

        private static readonly (string, BlueprintFeature, string, BlueprintAbility, int)[] bombDiscoveries = new (string, BlueprintFeature, string, BlueprintAbility, int)[]
        {
            (
                Guids.AcidBombsFeature,
                FeatureRefs.AcidBombsFeature.Reference.Get(),
                Guids.AcidBomb,
                AbilityRefs.AcidBomb.Reference.Get(),
                0
            ),
            (
                Guids.BlindingBombsFeature,
                FeatureRefs.BlindingBombsFeature.Reference.Get(),
                Guids.BlindingBomb,
                AbilityRefs.BlindingBomb.Reference.Get(),
                8
            ),
            (
                Guids.BreathWeaponBombFeature,
                FeatureRefs.BreathWeaponBombFeature.Reference.Get(),
                Guids.BreathWeaponBomb,
                AbilityRefs.BreathWeaponBomb.Reference.Get(),
                6
            ),
            (
                Guids.ChokingBombFeature,
                FeatureRefs.ChokingBombFeature.Reference.Get(),
                Guids.ChokingBomb,
                AbilityRefs.ChokingBomb.Reference.Get(),
                0
            ),
            (
                // Handled manually
                // Ability handled manually
                Guids.CursedBombsFeature,
                FeatureRefs.CursedBombsFeature.Reference.Get(),
                "",
                null,
                12
            ),
            (
                Guids.DispellingBombsFeature,
                FeatureRefs.DispellingBombsFeature.Reference.Get(),
                Guids.DispellingBomb,
                AbilityRefs.DispellingBomb.Reference.Get(),
                6
            ),
            (
                Guids.ExplosiveBombsFeature,
                FeatureRefs.ExplosiveBombsFeature.Reference.Get(),
                Guids.ExplosiveBomb,
                AbilityRefs.ExplosiveBomb.Reference.Get(),
                0
            ),
            (
                Guids.ForceBombsFeature,
                FeatureRefs.ForceBombsFeature.Reference.Get(),
                Guids.ForceBomb,
                AbilityRefs.ForceBomb.Reference.Get(),
                8
            ),
            (
                Guids.FrostBombsFeature,
                FeatureRefs.FrostBombsFeature.Reference.Get(),
                Guids.FrostBomb,
                AbilityRefs.FrostBomb.Reference.Get(),
                0
            ),
            (
                Guids.HolyBombsFeature,
                FeatureRefs.HolyBombsFeature.Reference.Get(),
                Guids.HolyBomb,
                AbilityRefs.HolyBomb.Reference.Get(),
                8
            ),
            (
                Guids.ShockBombsFeature,
                FeatureRefs.ShockBombsFeature.Reference.Get(),
                Guids.ShockBomb,
                AbilityRefs.ShockBomb.Reference.Get(),
                0
            ),
            (
                Guids.TanglefootBombsFeature,
                FeatureRefs.TanglefootBombsFeature.Reference.Get(),
                Guids.TanglefootBomb,
                AbilityRefs.TanglefootBomb.Reference.Get(),
                0
            ),
        };

        public static void ConfigureDisabled()
        {
            configureBombDiscoveries(enabled: false);
            FeatureSelectionConfigurator.New(FeatureName, Guids.DiscoverySelection).Configure();
        }

        public static void ConfigureEnabled()
        {
            logger.Info("Configuring discovery selection");

            configureBombDiscoveries(enabled: true);

            patchEnhancePotion();
            patchNauseatingFlesh();
            patchPreciseBomb();
            patchSpontaneousHealingResource();

            FeatureSelectionConfigurator.New(FeatureName, Guids.DiscoverySelection)
                .CopyFrom(FeatureSelectionRefs.DiscoverySelection.Reference.Get())
                .SetDescription(Description)
                .SetAllFeatures(discoveries)
                .Configure();
        }

        private static void configureBombDiscoveries(bool enabled)
        {
            logger.Info("   Configuring bomb discoveries");

            if (!enabled)
            {
                foreach (var (feature, baseFeature, ability, baseAbility, level) in bombDiscoveries)
                {
                    FeatureConfigurator.New(StringUtils.getName(baseFeature.name), feature).Configure();

                    if (feature == Guids.CursedBombsFeature)
                    {
                        AbilityConfigurator.New(StringUtils.getName(AbilityRefs.CurseWeaknessBomb.Reference.Get().name), Guids.CurseWeaknessBomb).Configure();
                        AbilityConfigurator.New(StringUtils.getName(AbilityRefs.CurseFeebleBodyBomb.Reference.Get().name), Guids.CurseFeebleBodyBomb).Configure();
                        AbilityConfigurator.New(StringUtils.getName(AbilityRefs.CurseIdiocyBomb.Reference.Get().name), Guids.CurseIdiocyBomb).Configure();
                        AbilityConfigurator.New(StringUtils.getName(AbilityRefs.CurseDeteriorationBomb.Reference.Get().name), Guids.CurseDeteriorationBomb).Configure();
                    }
                    else
                    {
                        AbilityConfigurator.New(StringUtils.getName(baseAbility.name), ability).Configure();
                    }
                }
                return;
            }

            foreach (var (feature, baseFeature, ability, baseAbility, level) in bombDiscoveries)
            {
                configureBombFeature(
                    feature: feature,
                    baseFeature: baseFeature,
                    ability: ability,
                    level: level
                );

                if (feature == Guids.CursedBombsFeature)
                {
                    configureBombAbility(
                        ability: Guids.CurseWeaknessBomb,
                        baseAbility: AbilityRefs.CurseWeaknessBomb.Reference.Get()
                    );
                    configureBombAbility(
                        ability: Guids.CurseFeebleBodyBomb,
                        baseAbility: AbilityRefs.CurseFeebleBodyBomb.Reference.Get()
                    );
                    configureBombAbility(
                        ability: Guids.CurseIdiocyBomb,
                        baseAbility: AbilityRefs.CurseIdiocyBomb.Reference.Get()
                    );
                    configureBombAbility(
                        ability: Guids.CurseDeteriorationBomb,
                        baseAbility: AbilityRefs.CurseDeteriorationBomb.Reference.Get()
                    );
                }
                else
                {
                    configureBombAbility(
                        ability: ability,
                        baseAbility: baseAbility
                    );
                }
            }
        }

        private static void configureBombFeature(string feature, BlueprintFeature baseFeature, string ability, int level)
        {
            var name = StringUtils.getName(baseFeature.name);

            logger.Info($"      Configuring {name}");

            var configurator = FeatureConfigurator.New(name, feature)
                .CopyFrom(baseFeature)
                .AddPrerequisiteFeature(
                    group: Prerequisite.GroupType.All,
                    checkInProgression: false,
                    hideInUI: false,
                    feature: Guids.Bombs
                );

            if (feature == Guids.CursedBombsFeature)
            {
                configurator.AddFacts(
                   facts: new() {
                        Guids.CurseWeaknessBomb,
                        Guids.CurseFeebleBodyBomb,
                        Guids.CurseIdiocyBomb,
                        Guids.CurseDeteriorationBomb
                   },
                   casterLevel: 0,
                   doNotRestoreMissingFacts: false,
                   hasDifficultyRequirements: false,
                   invertDifficultyRequirements: false,
                   minDifficulty: GameDifficultyOption.Story
               );
            }
            else
            {
                configurator.AddFacts(
                   facts: new() { ability },
                   casterLevel: 0,
                   doNotRestoreMissingFacts: false,
                   hasDifficultyRequirements: false,
                   invertDifficultyRequirements: false,
                   minDifficulty: GameDifficultyOption.Story
               );
            }

            if (level > 0)
            {
                configurator.AddPrerequisiteClassLevel(
                    characterClass: Guids.IsekaidClass,
                    level: level,
                    group: Prerequisite.GroupType.All,
                    checkInProgression: false,
                    hideInUI: false
                );
            }

            configurator.Configure();
        }

        private static void configureBombAbility(string ability, BlueprintAbility baseAbility)
        {
            var name = StringUtils.getName(baseAbility.name);

            logger.Info($"      Configuring {name}");

            AbilityConfigurator.New(name, ability)
                .CopyFrom(
                    blueprint: baseAbility,
                    componentTypes: new[]
                    {
                        typeof(AbilityDeliverProjectile),
                        typeof(AbilityEffectRunAction),
                        typeof(AbilityEffectMiss),
                        typeof(AbilityTargetsAround),
                        typeof(SpellDescriptorComponent),
                        typeof(AbilitySpawnFx),
                        typeof(AbilityCasterNotPolymorphed),
                        typeof(AbilityIsBomb)
                    }
                )
                .AddAbilityResourceLogic(
                    requiredResource: Guids.BombsResource,
                    isSpendResource: true,
                    costIsCustom: false,
                    amount: 1
                )
                .AddContextRankConfig(
                    ContextRankConfigs.StatBonus(
                        type: AbilityRankType.DamageBonus,
                        stat: StatType.Charisma,
                        min: 0,
                        max: 20
                    )
                )
                .AddContextRankConfig(
                    ContextRankConfigs.StatBonus(
                        type: AbilityRankType.DamageDiceAlternative,
                        stat: StatType.Charisma,
                        min: 0,
                        max: 20
                    )
                )
                .Configure();
        }

        private static void patchFastBombsBuff()
        {
            logger.Info("   Patching fast bombs buff");

            BuffConfigurator.For(BuffRefs.FastBombsBuff.Reference.Get())
                .EditComponent<FastBombs>(
                    c => c.m_Abilities = CommonTool.Append(
                        c.m_Abilities,
                        new BlueprintAbilityReference[] {
                            BlueprintTool.GetRef<BlueprintAbilityReference>(Guids.BombsStandard),
                            BlueprintTool.GetRef<BlueprintAbilityReference>(Guids.AcidBomb),
                            BlueprintTool.GetRef<BlueprintAbilityReference>(Guids.BlindingBomb),
                            BlueprintTool.GetRef<BlueprintAbilityReference>(Guids.ChokingBomb),
                            BlueprintTool.GetRef<BlueprintAbilityReference>(Guids.CurseDeteriorationBomb),
                            BlueprintTool.GetRef<BlueprintAbilityReference>(Guids.CurseFeebleBodyBomb),
                            BlueprintTool.GetRef<BlueprintAbilityReference>(Guids.CurseIdiocyBomb),
                            BlueprintTool.GetRef<BlueprintAbilityReference>(Guids.CurseWeaknessBomb),
                            BlueprintTool.GetRef<BlueprintAbilityReference>(Guids.DispellingBomb),
                            BlueprintTool.GetRef<BlueprintAbilityReference>(Guids.ExplosiveBomb),
                            BlueprintTool.GetRef<BlueprintAbilityReference>(Guids.ForceBomb),
                            BlueprintTool.GetRef<BlueprintAbilityReference>(Guids.FrostBomb),
                            BlueprintTool.GetRef<BlueprintAbilityReference>(Guids.HolyBomb),
                            BlueprintTool.GetRef<BlueprintAbilityReference>(Guids.ShockBomb),
                            BlueprintTool.GetRef<BlueprintAbilityReference>(Guids.TanglefootBomb)
                        }
                ))
                .Configure();
        }

        private static void patchEnhancePotion()
        {
            logger.Info("   Patching enhance potion");

            FeatureConfigurator.For(FeatureRefs.EnhancePotion.Reference.Get())
                .EditComponent<EnhancePotion>(
                    c => c.m_Classes = CommonTool.Append(
                        c.m_Classes,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                ))
                .Configure();
        }

        private static void patchNauseatingFlesh()
        {
            logger.Info("   Patching nauseating flesh");

            FeatureConfigurator.For(FeatureRefs.EnhancePotion.Reference.Get())
                .EditComponent<ContextRankConfig>(
                    c => c.m_Class = CommonTool.Append(
                        c.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                ))
                .Configure();
        }

        private static void patchPreciseBomb()
        {
            logger.Info("   Patching precise bombs");

            FeatureConfigurator.For(FeatureRefs.PreciseBomb.Reference.Get())
                .EditComponent<PrerequisiteFeature>(
                    c => c.Group = Prerequisite.GroupType.Any
                )
                .AddPrerequisiteFeature(
                    group: Prerequisite.GroupType.Any,
                    checkInProgression: false,
                    hideInUI: false,
                    feature: Guids.Bombs
                )
                .Configure();
        }

        private static void patchSpontaneousHealingResource()
        {
            logger.Info("   Patching spontaneous healing resource");

            AbilityResourceConfigurator.For(AbilityResourceRefs.SpontaneousHealingResource.Reference.Get())
                .ModifyMaxAmount(
                    a => a.m_ClassDiv = CommonTool.Append(
                        a.m_ClassDiv,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();
        }
    }
}
