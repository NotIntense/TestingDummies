using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.FirstPersonControl;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace TestingDummies.Patches
{
    //Thank you to the SCP-575 Plugin that I st- borrowed this code from :3
    [HarmonyPatch(typeof(FpcMouseLook), nameof(FpcMouseLook.UpdateRotation))]
    public class AIRotation
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            Label skip = generator.DefineLabel();

            newInstructions[newInstructions.Count - 1].labels.Add(skip);

            newInstructions.InsertRange(0, new List<CodeInstruction>()
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldfld, AccessTools.Field(typeof(FpcMouseLook), nameof(FpcMouseLook._hub))),
                new(OpCodes.Call, AccessTools.Method(typeof(Plugin), nameof(Plugin.IsAI))),
                new(OpCodes.Brtrue_S, skip)
            });

            foreach (CodeInstruction instruction in newInstructions)
                yield return instruction;

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}
