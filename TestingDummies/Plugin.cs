using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using TestingDummies.SpawningHandler;
using HarmonyLib;
using Exiled.Events.EventArgs.Player;

namespace TestingDummies
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;


        public List<ReferenceHub> DumRef = new();
        public Spawn spawning;

        private Harmony _harmony;

        public override string Name => "Dev Dummies";
        public override string Prefix => "Dev Dum";
        public override string Author => "NotIntense";
        public override PluginPriority Priority => PluginPriority.Medium;
        public override Version Version => new(2, 0, 4);
        public override Version RequiredExiledVersion => new(7, 0, 0);

        public override void OnEnabled()
        {           

            Instance = this;
            spawning = new Spawn();
            _harmony = new("DevDummies-Rotation-Patch");
            _harmony.PatchAll();

            Exiled.Events.Handlers.Player.Left += Test;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            _harmony.UnpatchAll();
            _harmony = null;
            Instance = null;
            spawning = null;

            base.OnDisabled();
        }

        public static bool IsAI(ReferenceHub hub)
        {
            bool isDummy = Instance.DumRef.Contains(hub);
            return isDummy;
        }        

        public void Test(LeftEventArgs ev)
        {
            Log.Info($"{ev.Player.Nickname} left the server");
        }
    }
}
