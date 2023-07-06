using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using Utils;

namespace Isekaid.ClassFeatures.Arcanist
{
    internal class ArcanistFeatures
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(ArcanistFeatures));

        private static readonly string ProgressionName = "ArcanistFeatures";
        internal const string DisplayName = "ArcanistFeatures.Name";
        private static readonly string Description = "ArcanistFeatures.Description";

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.ArcanistFeatures))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                logger.Error("ArcanistFeatures.Configure", e);
            }
        }

        public static void ConfigureDisabled()
        {
            ProgressionConfigurator.New(ProgressionName, Guids.ArcanistFeatures).Configure();
        }

        public static BlueprintProgression ConfigureEnabled()
        {
            logger.Info("Configuring Arcanist Features progression");

            patchArcaneReservoir();
            //patchPowerfulChange(); // Only need to patch if not using BubbleBuffs
            //patchShareTransmutation(); // Only need to patch if not using BubbleBuffs

            var levelEntries = LevelEntryBuilder.New()
                .AddEntry(
                    1,
                    FeatureRefs.ArcanistArcaneReservoirFeature.Reference.Get()
                )
                .AddEntry(
                    3,
                    FeatureRefs.PowerfulChange.Reference.Get()
                )
                .AddEntry(
                    9,
                    FeatureRefs.ShareTransmutation.Reference.Get()
                )
                .AddEntry(
                    20,
                    FeatureRefs.TransmutationSupremacy.Reference.Get()
                );

            var uiGroups = UIGroupBuilder.New()
                .AddGroup(
                    FeatureRefs.PowerfulChange.Reference.Get(),
                    FeatureRefs.ShareTransmutation.Reference.Get(),
                    FeatureRefs.TransmutationSupremacy.Reference.Get()
                );

            return ProgressionConfigurator.New(ProgressionName, Guids.ArcanistFeatures)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(FeatureSelectionRefs.ArcanistExploitSelection.Reference.Get().Icon)
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

        private static void patchArcaneReservoir()
        {
            logger.Info("   Patching arcane reservoir");

            AbilityResourceConfigurator.For(AbilityResourceRefs.ArcanistArcaneReservoirResource.Reference.Get())
                .ModifyMaxAmount(
                    a => a.m_Class = CommonTool.Append(
                        a.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();

            logger.Info("   Patching arcane reservoir resource buff");

            BuffConfigurator.For(BuffRefs.ArcanistArcaneReservoirResourceBuff.Reference.Get())
                .EditComponent<ContextRankConfig>(
                    c => c.m_Class = CommonTool.Append(
                        c.m_Class,
                        BlueprintTool.GetRef<BlueprintCharacterClassReference>(Guids.IsekaidClass)
                    )
                )
                .Configure();

            logger.Info("   Patching arcane reservoir caster level buff");

            BuffConfigurator.For(BuffRefs.ArcanistArcaneReservoirCLBuff.Reference.Get())
                .EditComponent<AddCasterLevelForSpellbook>(
                    c => c.m_Spellbooks = CommonTool.Append(
                        c.m_Spellbooks,
                        BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.IsekaidClassSpellBook)
                    )
                )
                .EditComponent<AddAbilityUseTrigger>(
                    t => t.m_Spellbooks = CommonTool.Append(
                        t.m_Spellbooks,
                        BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.IsekaidClassSpellBook)
                    )
                )
                .Configure();

            logger.Info("   Patching arcane reservoir dc buff");

            BuffConfigurator.For(BuffRefs.ArcanistArcaneReservoirDCBuff.Reference.Get())
                .EditComponent<IncreaseSpellSpellbookDC>(
                    c => c.m_Spellbooks = CommonTool.Append(
                        c.m_Spellbooks,
                        BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.IsekaidClassSpellBook)
                    )
                )
                .EditComponent<AddAbilityUseTrigger>(
                    t => t.m_Spellbooks = CommonTool.Append(
                        t.m_Spellbooks,
                        BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.IsekaidClassSpellBook)
                    )
                )
                .Configure();
        }

        private static void patchPowerfulChange()
        {
            logger.Info("   Patching powerful change buff");

            BuffConfigurator.For(BuffRefs.PowerfulChangeBuff.Reference.Get())
                .EditComponent<AddAbilityUseTrigger>(
                    t => t.m_Spellbooks = CommonTool.Append(
                        t.m_Spellbooks,
                        BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.IsekaidClassSpellBook)
                    )
                )
                .Configure();


            logger.Info("   Patching powerful change greater buff");

            BuffConfigurator.For(BuffRefs.PowerfulChangeBuffGreater.Reference.Get())
                .EditComponent<AddAbilityUseTrigger>(
                    t => t.m_Spellbooks = CommonTool.Append(
                        t.m_Spellbooks,
                        BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.IsekaidClassSpellBook)
                    )
                )
                .Configure();
        }

        private static void patchShareTransmutation()
        {
            logger.Info("   Patching share transmutation buff");

            BuffConfigurator.For(BuffRefs.ShareTransmutationBuff.Reference.Get())
                .EditComponent<AddAbilityUseTrigger>(
                    t => t.m_Spellbooks = CommonTool.Append(
                        t.m_Spellbooks,
                        BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.IsekaidClassSpellBook)
                    )
                )
                .Configure();


            logger.Info("   Patching share transmutation greater buff");

            BuffConfigurator.For(BuffRefs.ShareTransmutationBuffGreater.Reference.Get())
                .EditComponent<AddAbilityUseTrigger>(
                    t => t.m_Spellbooks = CommonTool.Append(
                        t.m_Spellbooks,
                        BlueprintTool.GetRef<BlueprintSpellbookReference>(Guids.IsekaidClassSpellBook)
                    )
                )
                .Configure();
        }
    }
}
