using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Isekaid.ClassFeatures.Magus.SpellCombat
{
    [HarmonyPatch]
    static class IsSpellCombatThisRoundAllowedPatch
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(IsSpellCombatThisRoundAllowedPatch));

        [HarmonyPatch(typeof(UnitPartMagus), nameof(UnitPartMagus.IsSpellCombatThisRoundAllowed))]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            logger.Info("Patching UnitPartMagus.IsSpellCombatThisRoundAllowed");

            try
            {
                var code = new List<CodeInstruction>(instructions);

                var eldritchArcherCheckResultIndex = 0;
                var oneHandedAndFreeCheckIndex = 0;

                for (int index = code.Count - 1; index >= 0; index--)
                {
                    // find index of EldrichArcher check's result
                    /*
                    if (
                        code[index].opcode == OpCodes.Call
                        && code[index].Calls(AccessTools.Method(typeof(CountableFlag), "op_Implicit"))
                    )
                    {
                        eldritchArcherCheckResultIndex = index + 1;
                    }
                    */
                    // find index of HasOneHandedMeleeWeaponAndFreehand check
                    if (
                        code[index].opcode == OpCodes.Call
                        && code[index].Calls(AccessTools.Method(typeof(UnitPartMagus), nameof(UnitPartMagus.HasOneHandedMeleeWeaponAndFreehand)))
                    )
                    {
                        // the place to insert our true value
                        oneHandedAndFreeCheckIndex = index + 1;
                        break;
                    }

                    //if (eldritchArcherCheckResultIndex == 0 && oneHandedAndFreeCheckIndex == 0) { break; }
                }

                /*
                if (eldritchArcherCheckResultIndex == 0)
                {
                    throw new InvalidOperationException("Unable to find the EldrichArcher index.");
                }
                */

                if (oneHandedAndFreeCheckIndex == 0)
                {
                    throw new InvalidOperationException("Unable to find the HasOneHandedMeleeWeaponAndFreeehand index.");
                }

                // Patch Is_EldrichArcher check result
                //code[eldritchArcherCheckResultIndex].opcode = OpCodes.Pop;
                //code[eldritchArcherCheckResultIndex].operand = null;

                // Patch HasOneHandedMeleeWeaponAndFreehand check result
                var pop = new CodeInstruction(OpCodes.Pop);
                var pushTrue = new CodeInstruction(OpCodes.Ldc_I4_1);

                code.Insert(oneHandedAndFreeCheckIndex, pushTrue);
                code.Insert(oneHandedAndFreeCheckIndex, pop);

                return code;
            }
            catch (Exception e)
            {
                logger.Error("Transpiler failed.", e);
                return instructions;
            }
        }
    }
}
