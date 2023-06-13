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
            if (arguments.Count > 1 || arguments.Count < 1)
            {
                response = "Insufficient arguments. Usage: dummystats [dummyID]";
                return false;
            }
            string dummyID = arguments.At(0);

            if (Player.Get(dummyID) == null)
            {
                response = $"The player, '{dummyID}', dosent exist!";
                return false;
            }
            if (Plugin.Instance.DumRef.Contains(Player.Get(dummyID).ReferenceHub))
            {
                Player dummy = Player.Get(dummyID);

                response = $"Stats: Player ID : {dummy.Id}, Name : {dummy.Nickname}, Health : {dummy.Health}, Role : {dummy.Role.Name}";
                return true;
            }
            else
            {
                Player InvalidDummy = Player.Get(dummyID);

                response = $"ID : '{InvalidDummy.Id}', Nickname : '{InvalidDummy.Nickname}' is not a dummy or you entered a incorrect ID!";
                return false;
            }
        }

    }
}

