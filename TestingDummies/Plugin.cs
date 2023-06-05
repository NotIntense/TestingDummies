using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using TestingDummies.SpawningHandler;

namespace TestingDummies
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;
        public List<ReferenceHub> DumRef = new List<ReferenceHub>();
        public Spawn spawning;
        public override string Name => "Dev Dummies";
        public override string Prefix => "Dev Dum";
        public override string Author => "NotIntense";
        public override PluginPriority Priority => PluginPriority.Medium;
        public override Version Version => new(2, 0, 2);
        public override Version RequiredExiledVersion => new(7, 0, 0);

        public override void OnEnabled()
        {
            Instance = this;
            spawning = new Spawn();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            spawning = null;
            base.OnDisabled();
        }

        public static bool IsAI(ReferenceHub hub)
        {
            bool isDummy = Instance.DumRef.Contains(hub);
            return isDummy;
        }
    }
}
