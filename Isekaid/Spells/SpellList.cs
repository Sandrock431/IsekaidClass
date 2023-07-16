using BlueprintCore.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Spells;
using Kingmaker.Blueprints.Classes.Spells;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using IsekaidClass.Utils;

namespace IsekaidClass.Isekaid.Spells
{
    internal class SpellList
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(SpellList));

        private static readonly string SpellListName = "Isekaid.SpellList.Name";

        public static void ConfigureDisabled()
        {
            SpellListConfigurator.New(SpellListName, Guids.IsekaidClassSpellList).Configure();
        } 
        public static void ConfigureEnabled()
        {
            logger.Info("Configuring spell list");

            SpellListConfigurator.New(SpellListName, Guids.IsekaidClassSpellList)
                .AddToSpellsByLevel(
                    get0thLevelSpells(),
                    get1stLevelSpells(),
                    get2ndLevelSpells(),
                    get3rdLevelSpells(),
                    get4thLevelSpells(),
                    get5thLevelSpells(),
                    get6thLevelSpells(),
                    get7thLevelSpells(),
                    get8thLevelSpells(),
                    get9thLevelSpells()
                )
                .SetIsMythic(false)
                .SetFilterByMaxLevel(0)
                .SetFilterByDescriptor(false)
                .SetDescriptor(SpellDescriptor.None)
                .SetFilterBySchool(true)
                .SetExcludeFilterSchool(false)
                .SetFilterSchool(SpellSchool.None)
                .SetFilterSchool2(SpellSchool.None)
                .Configure();
        }

        private static SpellLevelList get0thLevelSpells()
        {
            var spellList = new SpellLevelList(0);

            spellList.m_Spells = new()
            {
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MageLight.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Jolt.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DisruptUndead.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AcidSplash.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DismissAreaEffect.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Daze.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.TouchOfFatigueCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Flare.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RayOfFrost.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Resistance.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DivineZap.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Guidance.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Virtue.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Stabilize.ToString())
            };

            return spellList;
        }


        private static SpellLevelList get1stLevelSpells()
        {
            var spellList = new SpellLevelList(1);

            spellList.m_Spells = new()
            {
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Snowball.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Vanish.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ColorSpray.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ShockingGraspCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MagicWeapon.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.EarPiercingScream.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.StunningBarrier.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MageShield.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CorrosiveTouchCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ExpeditiousRetreat.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.TouchOfGracelessnessCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MagicMissile.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ProtectionFromAlignment.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BurningHands.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CauseFear.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.TrueStrike.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FlareBurst.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HurricaneBow.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MageArmor.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Grease.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Sleep.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ReducePerson.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RayOfSickening.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.StoneFist.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RayOfEnfeeblement.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonMonsterISingle.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.EnlargePerson.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Hypnotism.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Bane.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Bless.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CureLightWoundsCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DivineFavor.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Doom.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Firebelly.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.InflictLightWoundsCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RemoveFear.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RemoveSickness.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ShieldOfFaith.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.UnbreakableHeart.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HazeOfDreams.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Command.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.StrandOfTheTangledKnot.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonNaturesAllyI.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AcidMaw.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MagicFang.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AspectOfTheFalcon.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FeatherStep.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Entangle.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FaerieFire.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Longstrider.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RemoveSickness.ToString())
            };

            return spellList;
        }

        private static SpellLevelList get2ndLevelSpells()
        {
            var spellList = new SpellLevelList(2);

            spellList.m_Spells = new()
            {
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.OwlsWisdom.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BurningArc.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FalseLife.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AnimalAspectBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonElementalSmallBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Boneshaker.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.StoneCall.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HideousLaughter.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MoltenOrb.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ProtectionFromAlignmentCommunal.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AcidArrow.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SeeInvisibility.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ScorchingRay.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Scare.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BullsStrength.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Glitterdust.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CreatePit.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BearsEndurance.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SenseVitals.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.EaglesSplendor.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ResistEnergy.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CommandUndead.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Blur.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ProtectionFromArrows.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PerniciousPoison.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MirrorImage.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Web.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Invisibility.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Blindness.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FrigidTouchCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CatsGrace.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonMonsterIIBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FoxsCunning.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MortalTerror.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BoneFists.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ArrowOfLaw.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Aid.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BlessingOfCourageAndLife.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BlessingOfLuckAndResolveCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CureModerateWoundsCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DelayPoison.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.EffortlessArmor.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FindTraps.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Grace.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.InflictModerateWoundsCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RemoveParalysis.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RestorationLesser.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SoundBurst.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HoldPerson.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AlignWeapon.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PoxPustules.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.NaturalRythm.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonNaturesAllyII.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Barkskin.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HoldAnimal.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AspectOfTheBear.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SickeningEntanglement.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.WinterGrasp.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.LeadBlades.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BlessWeaponCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ChallengeEvil.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ProtectionFromChaosEvil.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.VeilOfHeaven.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.VeilOfPositiveEnergy.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MagicWeapon.ToString()),
            };

            return spellList;
        }

        private static SpellLevelList get3rdLevelSpells()
        {
            var spellList = new SpellLevelList(3);

            spellList.m_Spells = new()
            {
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.VampiricTouchCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MagicWeaponGreater.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ProtectionFromArrowsCommunal.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ProtectionFromEnergy.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SeeInvisibilityCommunal.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Rage.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Haste.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.LightningBolt.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Displacement.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonMonsterIIIBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BatteringBlast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ForcePunchCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Slow.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SpikedPit.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.StinkingCloud.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Blink.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Heroism.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DeepSlumber.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Fireball.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BeastShapeI.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ResistEnergyCommunal.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DispelMagic.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RayOfExhaustion.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ArchonsAura.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BestowCurse.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Contagion.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CureSeriousWoundsCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DelayPoisonCommunal.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.InflictSeriousWoundsCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MagicalVestment.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Prayer.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RemoveBlindness.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RemoveCurse.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RemoveDisease.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SearingLight.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AnimateDead.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FeatherStepMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CallLightning.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PoisonCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SpitVenom.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MagicFangGreater.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.LongstriderGreater.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.NeutralizePoison.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DominateAnimal.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonNaturesAllyIII.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SoothingMud.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SpikeGrowth.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AnimalAspectGreaterBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BurningEntangle.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.NaturesExile.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AuraOfGreaterCourage.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BestowGraceCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.LitanyOfEloquence.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.LitanyOfEntanglement.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ProtectionFromChaosEvilCommunal.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ResistEnergy.ToString()),
            };

            return spellList;
        }

        private static SpellLevelList get4thLevelSpells()
        {
            var spellList = new SpellLevelList(4);

            spellList.m_Spells = new()
            {
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.OverwhelmingGrief.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CrushingDespair.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ObsidianFlow.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RainbowPattern.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Boneshatter.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonMonsterIVBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.VolcanicStorm.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ControlledFireball.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ProtectionFromEnergyCommunal.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ConfusionSpell.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FalseLifeGreater.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Fear.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DragonsBreath.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BeastShapeII.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Shout.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.TouchOfSlimeCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SymbolOfRevelation.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.IceStorm.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DimensionDoorBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Enervation.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Stoneskin.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PhantasmalKiller.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonElementalMediumBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ShadowConjuration.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AcidPit.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ReducePersonMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ElementalBodyIBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.EnlargePersonMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.InvisibilityGreater.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ChaosHammer.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CrusadersEdgeCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CureCriticalWoundsCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DeathWardCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DivinePower.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FreedomOfMovementCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HolySmite.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.InflictCriticalWoundsCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.OrdersWrath.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Restoration.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ShieldOfDawn.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.UnholyBlight.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Dismissal.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.LifeBlast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Echolocation.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SlowMud.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ThornBody.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CapeOfWasps.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SpikeStones.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonNaturesAllyIV.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FlameStrike.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.LifeBubble.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ExplosionOfRot.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ThirstingEntangle.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ChameleonStride.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SpikeGrowth.ToString()),
            };

            return spellList;
        }

        private static SpellLevelList get5thLevelSpells()
        {
            var spellList = new SpellLevelList(5);

            spellList.m_Spells = new()
            {
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AngelicAspect.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AcidicSpray.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HoldMonster.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ConstrictingCoils.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ElementalBodyIIBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.StoneskinCommunal.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonElementalLargeBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ConeOfCold.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.IcyPrison.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PolymorphBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DominatePerson.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MindFog.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.WavesOfFatigue.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Feeblemind.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FireSnake.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.VampiricShadowShield.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BreakEnchantment.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HungryPit.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BalefulPolymorph.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AnimalGrowth.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Geniekind.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ShadowEvocation.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonMonsterVBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BeastShapeIII.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PhantasmalWeb.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.WrackingRay.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Thoughtsense.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Cloudkill.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Serenity.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BreathOfLifeCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BurstOfGlory.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Cleanse.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CureLightWoundsMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DisruptingWeaponCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.InflictLightWoundsMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PillarOfLife.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ProfaneNimbus.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RaiseDead.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RighteousMight.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SacredNimbus.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SlayLivingCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SpellResistance.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.TrueSeeingCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Vinetrap.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CommandGreater.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CallLightningStorm.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AspectOfTheWolf.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonNaturesAllyV.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CaveFangs.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BlessingOfTheSalamander.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.TidalSurge.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ChameleonStrideGreater.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.InstantEnemy.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HolyWhisper.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MagicWeaponGreater.ToString()),
            };

            return spellList;
        }

        private static SpellLevelList get6thLevelSpells()
        {
            var spellList = new SpellLevelList(6);

            spellList.m_Spells = new()
            {
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ChainsOfLight.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ElementalAssessor.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.StoneToFlesh.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonMonsterVIBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DispelMagicGreater.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FormOfTheDragonI.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.UndeathToDeath.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CatsGraceMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BullsStrengthMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CircleOfDeath.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HeroismGreater.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Disintegrate.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ChainLightning.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.OwlsWisdomMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AcidFog.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BearsEnduranceMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HellfireRay.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CloakofDreams.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FoxsCunningMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.EaglesSplendorMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BeastShapeIVBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Transformation.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Sirocco.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ElementalBodyIIIBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.TarPool.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ColdIceStrike.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BansheeBlast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonElementalHugeBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PhantasmalPutrefaction.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Eyebite.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CreateUndeadBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BlessingOfLuckAndResolveMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Eaglesoul.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.TrueSeeingCommunal.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BladeBarrier.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CureModerateWoundsMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HarmCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HealCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.InflictModerateWoundsMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.InspiringRecovery.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PlagueStorm.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Banishment.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.JoyfulRapture.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PrimalRegression.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonNaturesAllyVI.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PoisonBreath.ToString()),
            };

            return spellList;
        }

        private static SpellLevelList get7thLevelSpells()
        {
            var spellList = new SpellLevelList(7);

            spellList.m_Spells = new()
            {
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.UmbralStrike.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Insanity.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PolymorphGreaterBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PowerWordBlind.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ElementalBodyIVBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FingerOfDeath.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.WalkThroughSpace.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonMonsterVIIBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FormOfTheDragonII.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CircleOfClarity.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.KiShout.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ResonatingWord.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PrismaticSpray.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.InvisibilityMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.IceBody.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.WavesOfEctasy.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HoldPersonMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonElementalGreaterBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CausticEruption.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.WavesOfExhaustion.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.LegendaryProportions.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ShadowConjurationGreater.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Firebrand.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BestowGraceOfTheChampionCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Arbitrament.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Blasphemy.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Dictum.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HolyWord.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.WordOfChaos.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CureSeriousWoundsMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Destruction.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.InflictSeriousWoundsMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.JoltingPortent.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PoisonBreath.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RestorationGreater.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Resurrection.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.BestowCurseGreater.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonNaturesAllyVII.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CreepingDoom.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Sunbeam.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FireStorm.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Changestaff.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AngelicAspectGreater.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ForcedRepentance.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HolySword.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.OathOfPeace.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ResoundingBlow.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SacredNimbus.ToString()),
            };

            return spellList;
        }

        private static SpellLevelList get8thLevelSpells()
        {
            var spellList = new SpellLevelList(8);

            spellList.m_Spells = new()
            {
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ShoutGreater.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ProtectionFromSpells.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonMonsterVIIIBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ScintillatingPattern.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonElementalElderBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HorridWilting.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Sunburst.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DeathClutch.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PolarRay.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FrightfulAspect.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Seamantle.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FormOfTheDragonIII.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.IronBody.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.RiftOfRuin.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PredictionOfFailure.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PowerWordStun.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ShadowEvocationGreater.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Stormbolts.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MindBlank.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.EuphoricTranquilityCast.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Soulreaver.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CloakOfChaos.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.CureCriticalWoundsMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HolyAura.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.InflictCriticalWoundsMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ShieldOfLaw.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.UnholyAura.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.AnimalShapes.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonNaturesAllyVIII.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.EuphoricTranquilityCast.ToString()),
            };

            return spellList;
        }

        private static SpellLevelList get9thLevelSpells()
        {
            var spellList = new SpellLevelList(9);

            spellList.m_Spells = new()
            {
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.OverwhelmingPresence.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.IcyPrisonMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Shades.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Tsunami.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PowerWordKill.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.WailOfBanshee.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ClashingRocks.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Shapechange.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HeroicInvocation.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.EnergyDrain.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Weird.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonMonsterIXBase.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.FieryBody.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.DominateMonster.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HoldMonsterMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.Foresight.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.MindBlankCommunal.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.WindsOfVengeance.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.HealMass.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.PolarMidnight.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonNaturesAllyIX.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.ElementalSwarm.ToString()),
                BlueprintTool.GetRef<BlueprintAbilityReference>(AbilityRefs.SummonElderWorm.ToString()),
            };

            return spellList;
        }
    }
}
