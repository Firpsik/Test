using SpaceBattle;

public class AngleTests
{
    [Fact]
    public void SummingAnglesIsPossible()
    {
        var firstAngle = new Angle(45);
        var secondAngle = new Angle(45);
        var exprctedAngle = new Angle(90);

        var result = firstAngle + secondAngle;
        Assert.Equal(exprctedAngle.value, result.value);
    }
}
