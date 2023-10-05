﻿using CommandSystem;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using System;

namespace TestingDummies.Commands;

public class RemoveDummy : ICommand
{
    public string Command => "Remove";

    public string[] Aliases => new string[] { "removedummy", "removedevdummy", "removedev", "remove" };

    public string Description => "Removes a spawned dev dummy";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!Extensions.HasDummyPermissions(Player.Get(sender)))
        {
            response = "You do not have the needed permissions to run this command! Needed perms : devdummies";
            return false;
        }
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
        if(Dummy.IsNPC)           
        {
            Dummy.ReferenceHub.OnDestroy();

            LeftEventArgs newLeft = new(Dummy);
            Exiled.Events.Handlers.Player.OnLeft(newLeft);

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
