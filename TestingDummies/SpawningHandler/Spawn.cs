using Exiled.API.Features;
using PlayerRoles;
using UnityEngine;

namespace TestingDummies.SpawningHandler
{
    public class Spawn : MonoBehaviour
    {
        public void SpawnDummy(string Name, RoleTypeId Role, Player Target)
        {
            Npc GeneratedNPC = Npc.Spawn(Name, Role, position: Target.Position);
            if (Plugin.Instance.Config.NPCAFKImmunity) GeneratedNPC.RemoteAdminPermissions = PlayerPermissions.AFKImmunity;
            if (Plugin.Instance.Config.NPCBadgeEnabled)
            {
                GeneratedNPC.RankName = Plugin.Instance.Config.NPCBadgeName;
                GeneratedNPC.RankColor = Plugin.Instance.Config.NPCBadgeColor;
            }        
        }
    }
}
