namespace SpaceBattle;

public interface IRotateCommandStartable
{
    IUObject UObject { get; }
    IDictionary<string, object> initValues { get; }
}
