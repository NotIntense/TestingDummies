using CommandSystem;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingDummies.Commands;

public class DummyStats : ICommand
{
    public string Command => "Stats";

    public string[] Aliases => new string[] { "devdummystats", "dummystats", "stats" };

    public string Description => "Gives you stats on the specified dummy";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!Extensions.HasDummyPermissions(Player.Get(sender)))
        {
            response = "You do not have the needed permissions to run this command! Needed perms : devdummies";
            return false;
        }

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

        if (Dummy.IsNPC)
        {
            List<string> effectNames = Dummy.ActiveEffects.Select(effect => effect.name).ToList();
            string activeEffectsString = effectNames.Count == 0 ? "None" : string.Join(", ", effectNames);

            response = $"Stats: Player ID: {Dummy.Id}, Name: {Dummy.Nickname}, Health: {Dummy.Health}, Role: {Dummy.Role.Name}, Active Effects: {activeEffectsString}";
            return true;
        }
        else
        {

            response = $"ID : '{Dummy.Id}', Nickname : '{Dummy.Nickname}' is not a dummy or you entered a incorrect ID!";
            return false;
        }
    }
}

