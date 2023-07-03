using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using TestingDummies.SpawningHandler;
using HarmonyLib;

namespace TestingDummies
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;

        public Spawn spawning;
        private Harmony _harmony;

        public override string Name => "Dev Dummies";
        public override string Prefix => Name;
        public override string Author => "NotIntense";
        public override PluginPriority Priority => PluginPriority.Medium;
        public override Version Version => new(2, 1, 7);
        public override Version RequiredExiledVersion => new(7, 0, 0);

        public override void OnEnabled()
        {           
            Instance = this;
            spawning = new Spawn();
            _harmony = new("DevDummies-Rotation-Patch");
            _harmony.PatchAll();

            base.OnEnabled();

            Log.Warn($"{Name.ToUpper()} DOES AND WILL VIOLATE NORTHWOOD VSR WHEN DUMMIES ARE SPAWNED. USE ON PRIVATE SERVERS ONLY AND AT YOUR OWN RISK.");
        }

        public override void OnDisabled()
        {
            _harmony.UnpatchAll();
            _harmony = null;
            Instance = null;
            spawning = null;

            base.OnDisabled();
        }           
    }
}
