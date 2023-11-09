using Exiled.API.Interfaces;
using System.ComponentModel;

namespace TestingDummies
{
    public class Config : IConfig
    {
        [Description("Gets or sets if the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Gets or sets if the plugins debug is enabled.")]
        public bool Debug { get; set; } = false;

        [Description("Gives and shows a badge on the AI")]
        public bool NPCBadgeEnabled { get; set; } = true;

        [Description("If NPCBadge is enabled, sets the color")]
        public string NPCBadgeColor { get; set; } = "aqua";

        [Description("If NPCBadge is enabled, sets the name")]
        public string NPCBadgeName { get; set; } = "NPC";

        [Description("Gives spawned NPCs AFK Immunity (HIGHLY RECOMMENED TO KEEP TRUE AS NPCS ARE CONSTANTLY AFK)")]
        public bool NPCAFKImmunity { get; set; } = true;

        [Description("Gets or sets if using DevDummy commands require the 'devdummies' permission.")]
        public bool RequirePermission { get; set; } = false;
    }
}
