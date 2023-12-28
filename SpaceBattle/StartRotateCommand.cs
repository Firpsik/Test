using Hwdtech;

namespace SpaceBattle;

public class StartRotateCommand
{
    private readonly IRotateCommandStartable obj;
    public StartRotateCommand(IRotateCommandStartable Obj)
    {
        obj = Obj;
    }

    public void Execute()
    {
        obj.initValues.ToList().ForEach(operation => obj.UObject.SetProperty(operation.Key, operation.Value));
        var rotate_command = IoC.Resolve<ICommand>("Rotating.Command", obj.UObject);
        obj.UObject.SetProperty("Rotate", rotate_command);
        IoC.Resolve<IQueue>("Queue").Add(rotate_command);
    }
}
