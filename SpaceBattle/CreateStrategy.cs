namespace SpaceBattle;

using Hwdtech;

public class CreateStrategy
{
    public static object Invoke(params object[] args)
    {
        var name = (string)args[0];
        var obj = (IUObject)args[1];

        var command = IoC.Resolve<IEnumerable<string>>(name + ".Command");

        var add_command = command.Select(p => IoC.Resolve<ICommand>(p, obj));
        return IoC.Resolve<ICommand>("CreateMacro.Command", add_command);
    }
}
