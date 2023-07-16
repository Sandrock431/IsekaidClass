using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.UnitLogic.Abilities;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace IsekaidClass.Isekaid.ClassFeatures.Arcanist
{
    [HarmonyPatch]
    static class ArcanistShareTransmutationPatch
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(ArcanistShareTransmutationPatch));

        [HarmonyPatch(typeof(AbilityData), nameof(AbilityData.ArcanistShareTransmutation), MethodType.Getter)]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            logger.Info("Patching AbilityData.ArcanistShareTransmutation");

            try
            {
                var code = new List<CodeInstruction>(instructions);

                var targetIndex = 0;
                for (int index = code.Count - 1; index >= 0; index--)
                {
                    if (
                        code[index].opcode == OpCodes.Call
                        && code[index].Calls(AccessTools.PropertyGetter(typeof(AbilityData), nameof(AbilityData.IsArcanistSpell)))
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
