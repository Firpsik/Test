namespace SpaceBattle;

public class Angle
{
    public readonly int value;
    static int degrees = 360;

    public Angle(int Value)
    {
        value = Value;
    }

    public static Angle operator + (Angle direction, Angle velocity)
    {
        return new Angle((direction.value + velocity.value) % degrees);
    }
}
