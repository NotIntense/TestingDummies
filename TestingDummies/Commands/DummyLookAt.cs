using CommandSystem;
using Exiled.API.Features;
using PlayerRoles.FirstPersonControl;
using System;
using UnityEngine;

namespace TestingDummies.Commands;

public class DummyLookAt : ICommand
{
    public string Command => "LookAt";

    public string[] Aliases => new string[] { "devdummylook", "dummylookat", "dummylook", "lookat" };

    public string Description => "Makes the specified dummy look at the target player";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count != 2)
        {
            response = "Incorrect arguments. Usage: DummyLookAt [dummyID] [TargetID]";
            return false;
        }
        Player dummy = Player.Get(arguments.At(0));
        Player target = Player.Get(arguments.At(1));
        if(!Extensions.HasDummyPermissions(target))
        {
            response = "You do not have the needed permissions to run this command! Needed perms : devdummies";
            return false;
        }
        if (dummy == null || target == null)
        {
            response = dummy == null ? $"The player with ID '{arguments.At(0)}' doesn't exist!" :
                      target == null ? $"The player with ID '{arguments.At(1)}' doesn't exist!" :
                      "";
            return false; 
        }

        if (dummy.IsNPC)
        {
            Vector3 direction = target.Position - dummy.Position;
            Quaternion quat = Quaternion.LookRotation(direction, Vector3.up);
            FpcMouseLook mouseLook = ((IFpcRole)dummy.ReferenceHub.roleManager.CurrentRole).FpcModule.MouseLook;
            (ushort horizontal, ushort vertical) = quat.ToClientUShorts();
            mouseLook.ApplySyncValues(horizontal, vertical);
            response = $"Rotated {dummy.Nickname} to the target {target.Nickname}";
            return true;
        }
        else
        {

            response = $"ID : '{dummy.Id}', Nickname : '{dummy.Nickname}' is not a dummy or you entered a incorrect ID!";
            return false;
        }
    }

}

