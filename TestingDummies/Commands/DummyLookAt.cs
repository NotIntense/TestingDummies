using CommandSystem;
using Exiled.API.Features;
using Mirror;
using PlayerRoles.FirstPersonControl;
using System;
using UnityEngine;

namespace TestingDummies.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class DummyLookAt : ICommand
    {
        public string Command => "DummyLookAt";

        public string[] Aliases => new string[] { "devdummylook", "dummylookat", "dummylook" };

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

            if (dummy == null)
            {
                response = $"The player with the specified ID, '{arguments.At(0)}', dosent exist!";
                return false;
            }
            if (target == null)
            {
                response = $"The player with the specified ID, '{arguments.At(1)}', dosent exist!";
                return false;
            }
            if (dummy.IsNPC)
            {
                Quaternion quat = Quaternion.LookRotation(target.Position - dummy.Position, Vector3.up);
                var mouseLook = ((IFpcRole)dummy.ReferenceHub.roleManager.CurrentRole).FpcModule.MouseLook;
                mouseLook.CurrentHorizontal = quat.eulerAngles.y;
                mouseLook.CurrentVertical = 0f;

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
}

