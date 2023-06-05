using CommandSystem;
using Exiled.API.Features;
using Mirror;
using System;

namespace TestingDummies.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class RemoveDummy : ICommand
    {
        public string Command => "RemoveDevDummy";

        public string[] Aliases => new string[] { "removedummy", "removedevdummy", "removedev" };

        public string Description => "Removes a spawned dev dummy";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count > 1 || arguments.Count < 1)
            {
                response = "Insufficient arguments. Usage: removedummy [dummyID]";
                return false;
            }
            string dummyID = arguments.At(0);

            if(Player.Get(dummyID) == null)
            {
                response = $"The player ID, '{dummyID}', dosent exist!";
                return false;
            }
            if(Plugin.Instance.DumRef.Contains(Player.Get(dummyID).ReferenceHub))
            {
                Player.Dictionary.Remove(Plugin.Instance.spawning.PlayerPrefabs[Player.Get(dummyID)]);
                Plugin.Instance.DumRef.Remove(Player.Get(dummyID).ReferenceHub);
                NetworkServer.DestroyPlayerForConnection(Plugin.Instance.spawning.PlayerConnIDs[Player.Get(dummyID)]);
                NetworkServer.Destroy(Player.Get(dummyID).GameObject);
                response = $"Removed {Player.Get(dummyID).Nickname}!";
                return true;
            }
            else
            {
                response = $"ID : '{Player.Get(dummyID).Id}', Nickname : '{Player.Get(dummyID).Nickname}' is not a dummy or you entered a incorrect ID!";
                return false;
            }
        }

    }
}
