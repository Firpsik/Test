namespace SpaceBattle;

public interface IRotable
{
    public Angle Direction { get; set; }
    public Angle AngularVelocity { get; }
}

public class RotateCommand : ICommand
{
    IRotable _rotable;

    public RotateCommand(IRotable rotable)
    {
        _rotable = rotable;
    }

    public void Execute()
    {
        _rotable.Direction += _rotable.AngularVelocity;
    }
}
