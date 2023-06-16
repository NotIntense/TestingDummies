using CommandSystem;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
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
            if (arguments.Count != 1)
            {
                response = "Insufficient arguments. Usage: removedummy [dummyID]";
                return false;
            }
            Player Dummy = Player.Get(arguments.At(0));

            if(Dummy == null)
            {
                response = $"The player with the specified ID, '{arguments.At(0)}', dosent exist!";
                return false;
            }
            if(Plugin.Instance.DumRef.Contains(Dummy.ReferenceHub))           
            {
                Dummy.ReferenceHub.OnDestroy();

                LeftEventArgs newLeft = new(Dummy);
                Exiled.Events.Handlers.Player.OnLeft(newLeft);

                Plugin.Instance.DumRef.Remove(Dummy.ReferenceHub);

                response = $"Removed {Dummy.Nickname}!";
                return true;
            }
            else
            {
                response = $"ID : '{Dummy.Id}', Nickname : '{Dummy.Nickname}' is not a dummy or you entered a incorrect ID!";
                return false;
            }
        }

    }
}
