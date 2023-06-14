using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using PlayerRoles;
using System;
using MEC;


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
            if (arguments.Count != 3)
            {
                response = "Incorrect arguments. Usage: spawndummy [name] [role] [playerID] OR [playerNickname]";
                return false;
            }
            string name = arguments.At(0);
            string roleString = arguments.At(1);
            Player playerID = Player.Get(arguments.At(2));
            if (!Enum.TryParse(roleString, out RoleTypeId role))
            {
                response = $"Invalid role: {roleString}";
                return false;
            }
            if (playerID == null)
            {
                response = $"Invalid player with the specified ID OR Nickname: {arguments.At(2)}";
                return false;
            }
            MECExtensionMethods1.RunCoroutine(Plugin.Instance.spawning.SpawnDum(name, role, playerID));
            response = $"Spawned dummy with name '{name}', role '{role}', for player '{playerID.Nickname}'";
            return true;
        }

    }
}
