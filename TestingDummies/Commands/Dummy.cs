using CommandSystem;
using System;

namespace TestingDummies.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class Dummy : ParentCommand
{
    public Dummy() => LoadGeneratedCommands();

    public override string Command { get; } = "devdummy";

    public override string[] Aliases { get; } = new[] { "DevDummy", "DEVDUMMY", "Devdummy" };

    public override string Description { get; } = "Parent command for handling Dev-Dummies.";

    public override void LoadGeneratedCommands()
    {
        RegisterCommand(new DummyLookAt());
        RegisterCommand(new DummyStats());
        RegisterCommand(new RemoveDummy());
        RegisterCommand(new SpawnDummy());
    }

    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = "Invalid subcommand! Valid subcommands : LookAt, Stats, Remove, Spawn";
        return false;
    }
}
