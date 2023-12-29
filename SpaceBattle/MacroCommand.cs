namespace SpaceBattle;

public class MacroCommand
{
    private readonly List<ICommand> commands;
    public MacroCommand(List<ICommand> Commands)
    {
        commands = Commands;
    }

    public void Execute()
    {
        commands.ToList().ForEach(c => c.Execute());
    }
}
