using Hwdtech;

namespace SpaceBattle;

public class StartRotateCommand : ICommand
{
    private readonly IRotateCommandStartable obj;
    public StartRotateCommand(IRotateCommandStartable Obj)
    {
        obj = Obj;
    }

    public void Execute()
    {
        obj.initValues.ToList().ForEach(operation => IoC.Resolve<ICommand>("UObject.Register", obj.UObject, operation.Key, operation.Value).Execute());

        var rotate_command = IoC.Resolve<ICommand>("Rotating.Command", obj.UObject);
        var injectCommand = IoC.Resolve<ICommand>("InjectCommand", rotate_command);

        IoC.Resolve<ICommand>("UObject.Register", obj.UObject, "Rotating.Command", rotate_command).Execute();
        IoC.Resolve<IQueue>("Queue", injectCommand).Add(rotate_command);
    }
}
