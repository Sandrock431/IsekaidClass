using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Isekaid.ClassFeatures.Arcanist
{
    [HarmonyPatch]
    static class CalculatePowerfulChangeBonusPatch
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(CalculatePowerfulChangeBonusPatch));

        [HarmonyPatch(typeof(AddStatBonus), nameof(AddStatBonus.CalculatePowerfulChangeBonus))]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            logger.Info("Patching AddStatBonus.CalculatePowerfulChangeBonus");

            try
            {
                var code = new List<CodeInstruction>(instructions);

                var targetIndex = 0;
                for (int index = code.Count - 1; index >= 0; index--)
                {
                    if (
                        code[index].opcode == OpCodes.Ldfld
                        && code[index].LoadsField(AccessTools.Field(typeof(BlueprintSpellbook), nameof(BlueprintSpellbook.IsArcanist)))
                    )
                    {
                        // is false check is right after
                        targetIndex = index + 1;
                        break;
                    }
                }

                if (targetIndex == 0)
                {
                    throw new InvalidOperationException("Unable to find the target index.");
                }

                code[targetIndex].opcode = OpCodes.Pop;
                code[targetIndex].operand = null;

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
