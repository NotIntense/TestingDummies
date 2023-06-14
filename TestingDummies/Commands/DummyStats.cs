using CommandSystem;
using Exiled.API.Features;
using Mirror;
using System;

namespace TestingDummies.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class DummyStats : ICommand
    {
        public string Command => "DummyStats";

        public string[] Aliases => new string[] { "devdummystats", "dummystats" };

        public string Description => "Gives you stats on the specified dummy";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = "Incorrect arguments. Usage: dummystats [dummyID]";
                return false;
            }
            Player Dummy = Player.Get(arguments.At(0));

            if (Dummy == null)
            {
                response = $"The player with the specified ID, '{arguments.At(0)}', dosent exist!";
                return false;
            }
            if (Plugin.Instance.DumRef.Contains(Dummy.ReferenceHub))
            {
                response = $"Stats: Player ID : {Dummy.Id}, Name : {Dummy.Nickname}, Health : {Dummy.Health}, Role : {Dummy.Role.Name}";
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

