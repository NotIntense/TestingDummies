using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using Exiled.Permissions.Extensions;
using UnityEngine;

namespace TestingDummies;

public class Plugin : Plugin<Config>
{
    public static Plugin Instance;

    public override string Name => "TestingDummies";
    public override string Prefix => Name;
    public override string Author => "NotIntense";
    public override PluginPriority Priority => PluginPriority.Medium;
    public override Version Version => new(2, 1, 8);
    public override Version RequiredExiledVersion => new(8, 11, 0);

    public override void OnEnabled()
    {           
        Instance = this;
        base.OnEnabled();
        Log.Warn($"{Name.ToUpper()} DOES AND WILL VIOLATE NORTHWOOD VSR WHEN DUMMIES ARE SPAWNED. USE ON PRIVATE SERVERS ONLY AND AT YOUR OWN RISK.");
    }

    public override void OnDisabled()
    {
        Instance = null;
        base.OnDisabled();
    }                
}

public static class Extensions
{
    public static bool HasDummyPermissions(this Player player)
    {
        if(Plugin.Instance.Config.RequirePermission)
        {
            if(player.CheckPermission("devdummies"))
                return true;
            return false;
        }
        return true;
    }

    public static (ushort horizontal, ushort vertical) ToClientUShorts(this Quaternion rotation)
    {
        const float ToHorizontal = ushort.MaxValue / 360f;
        const float ToVertical = ushort.MaxValue / 176f;

        float fixVertical = -rotation.eulerAngles.x;

        if (fixVertical < -90f)
        {
            fixVertical += 360f;
        }
        else if (fixVertical > 270f)
        {
            fixVertical -= 360f;
        }

        float horizontal = Mathf.Clamp(rotation.eulerAngles.y, 0f, 360f);
        float vertical = Mathf.Clamp(fixVertical, -88f, 88f) + 88f;

        return ((ushort)Math.Round(horizontal * ToHorizontal), (ushort)Math.Round(vertical * ToVertical));
    }
}
