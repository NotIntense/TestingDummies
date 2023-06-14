using Exiled.API.Features;
using Mirror;
using PlayerRoles;
using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Exiled.API.Features.Pickups;
using System.Linq;

namespace TestingDummies.SpawningHandler
{
    public class Spawn : MonoBehaviour
    {
        readonly int IDs = 1000;
        public Dictionary<Player, GameObject> PlayerPrefabs = new();
        public Dictionary<Player, FakeConnection> PlayerConnIDs = new();
        public IEnumerator<float> SpawnDum(string Name, RoleTypeId Role, Player target)
        {
            GameObject newPlayer = Instantiate(NetworkManager.singleton.playerPrefab);
            Player NewPlayer = new(newPlayer);
            PlayerPrefabs.Add(NewPlayer, newPlayer);
            var fakeConnection = new FakeConnection(IDs + Plugin.Instance.DumRef.Count);
            ReferenceHub hubPlayer = newPlayer.GetComponent<ReferenceHub>();
            Plugin.Instance.DumRef.Add(hubPlayer);
            NetworkServer.AddPlayerForConnection(fakeConnection, newPlayer);
            PlayerConnIDs.Add(NewPlayer, fakeConnection);
            try
            {
                hubPlayer.characterClassManager.UserId = $"DevDummy{Plugin.Instance.DumRef.Count}@server";
            }
            catch (Exception e)
            {
                Log.Debug(e);
            }
            hubPlayer.enabled = true;
            hubPlayer.nicknameSync.Network_myNickSync = $"{Name}-{Plugin.Instance.DumRef.Count}";
            Player.Dictionary.Add(newPlayer, NewPlayer);
            if (Plugin.Instance.Config.NPCBadgeEnabled)
            {
                NewPlayer.RankName = Plugin.Instance.Config.NPCBadgeName;
                NewPlayer.RankColor = Plugin.Instance.Config.NPCBadgeColor;
            }          
            hubPlayer.characterClassManager.GodMode = false;
            if (Plugin.Instance.Config.NPCAFKImmunity) NewPlayer.RemoteAdminPermissions = PlayerPermissions.AFKImmunity;
            yield return Timing.WaitForSeconds(0.3f);
            NewPlayer.Role.Set(Role, Exiled.API.Enums.SpawnReason.ForceClass);
            NewPlayer.Position = target.Position;
            NewPlayer.SessionVariables.Add("npc", true);
            yield break;
        }
    }
}
