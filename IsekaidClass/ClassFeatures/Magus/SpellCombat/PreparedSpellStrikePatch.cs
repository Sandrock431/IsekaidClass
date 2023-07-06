using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Parts;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Isekaid.ClassFeatures.Magus
{
    [HarmonyPatch]
    static class PreparedSpellStrikePatch
    {
        private static readonly LogWrapper logger = LogWrapper.Get(nameof(PreparedSpellStrikePatch));

        [HarmonyPatch(typeof(UnitEntityData), nameof(UnitEntityData.PreparedSpellStrike))]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            logger.Info("Patching UnitEntityData.PreparedSpellStrike");

            try
            {
                var code = new List<CodeInstruction>(instructions);

                var isMagusSpellCheckIndex = 0;

                for (int index = code.Count - 1; index >= 0; index--)
                {
                    if (
                        code[index].opcode == OpCodes.Callvirt
                        && code[index].Calls(AccessTools.Method(typeof(UnitPartMagus), nameof(UnitPartMagus.IsSpellFromMagusSpellList)))
                    )
                    {
                        // the place to insert our true value
                        isMagusSpellCheckIndex = index + 1;
                        break;
                    }
                }

                if (isMagusSpellCheckIndex == 0)
                {
                    throw new InvalidOperationException("Unable to find the PreparedSpellStrike index.");
                }

                var pop = new CodeInstruction(OpCodes.Pop);
                var pushTrue = new CodeInstruction(OpCodes.Ldc_I4_1);

                code.Insert(isMagusSpellCheckIndex, pushTrue);
                code.Insert(isMagusSpellCheckIndex, pop);

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
