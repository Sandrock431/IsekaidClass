using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.UnitLogic.Abilities;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace IsekaidClass.Isekaid.ClassFeatures.Alchemist
{
    [HarmonyPatch]
    static class AlchemistInfusionPatch
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(AlchemistInfusionPatch));

        [HarmonyPatch(typeof(AbilityData), nameof(AbilityData.AlchemistInfusion), MethodType.Getter)]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            logger.Info("Patching AbilityData.AlchemistInfusion");

            try
            {
                var code = new List<CodeInstruction>(instructions);

                var targetIndexes = new List<int>();

                for (int index = code.Count - 1; index >= 0; index--)
                {
                    if (
                        code[index].opcode == OpCodes.Call
                        && code[index].Calls(AccessTools.PropertyGetter(typeof(AbilityData), nameof(AbilityData.IsAlchemistSpell)))
                    )
                    {
                        // is false check is right after
                        targetIndexes.Add(index + 1);
                    }
                }

                if (targetIndexes.Count != 2)
                {
                    throw new InvalidOperationException("Unable to find the target indexes.");
                }

                foreach (int targetIndex in targetIndexes)
                {
                    code[targetIndex].opcode = OpCodes.Pop;
                    code[targetIndex].operand = null;
                }
                
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
