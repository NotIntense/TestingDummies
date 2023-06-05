using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using PlayerRoles;
using System;


namespace TestingDummies.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SpawnDummy : ICommand
    {
        public string Command => "SpawnDevDummy";

        public string[] Aliases =>  new string[] { "spawndummy", "spawndevdummy", "spawndev" };

        public string Description => "Spawns a dummy for development or testing purposes, it has 0 logic";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {           
            if (arguments.Count < 3)
            {
                response = "Insufficient arguments. Usage: spawndummy [name] [role] [playerID]";
                return false;
            }
            string name = arguments.At(0);
            string roleString = arguments.At(1);
            string playerID = arguments.At(2);
            if (!Enum.TryParse(roleString, out RoleTypeId role))
            {
                response = $"Invalid role: {roleString}";
                return false;
            }
            if (Player.Get(playerID) == null)
            {
                response = $"Invalid player: {playerID}";
                return false;
            }
            MEC.MECExtensionMethods1.RunCoroutine(Plugin.Instance.spawning.SpawnDum(name, role, Player.Get(playerID)));
            response = $"Spawned dummy with name '{name}', role '{role}', for player '{Player.Get(playerID).Nickname}'";
            return true;
        }

    }
}
