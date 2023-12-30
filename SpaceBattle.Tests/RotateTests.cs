using Moq;
using TechTalk.SpecFlow;

namespace SpaceBattle.Tests
{
    [Binding]
    public class RotationSteps
    {
        private readonly Mock<IRotable> rotateMock;
        private readonly ICommand rc;
        private Action? command;

        public RotationSteps()
        {
            rotateMock = new Mock<IRotable>();
            rc = new RotateCommand(rotateMock.Object);
        }

        [Given(@"космический корабль имеет угол наклона (.*) град к оси OX")]
        public void ДопустимКосмическийКорабльИмеетУголНаклонаГрадКОсиOX(int p0)
        {
            rotateMock.SetupGet(r => r.Direction).Returns(new Angle(p0));
        }

        [Given(@"имеет мгновенную угловую скорость (.*) град")]
        public void ДопустимИмеетМгновеннуюУгловуюСкоростьГрад(int p0)
        {
            rotateMock.SetupGet(r => r.AngularVelocity).Returns(new Angle(p0));
        }

        [Given(@"космический корабль, угол наклона которого невозможно определить")]
        public void ДопустимКосмическийКорабльУголНаклонаКоторогоНевозможноОпределить()
        {
            rotateMock.SetupGet(r => r.Direction).Throws<Exception>();
        }

        [Given(@"мгновенную угловую скорость невозможно определить")]
        public void ДопустимМгновеннуюУгловуюСкоростьНевозможноОпределить()
        {
            rotateMock.SetupGet(r => r.AngularVelocity).Throws<Exception>();
        }

        [Given(@"невозможно изменить угол наклона к оси OX космического корабля")]
        public void ДопустимНевозможноИзменитьУголНаклонаКОсиOXКосмическогоКорабля()
        {
            rotateMock.SetupSet(r => r.Direction = It.IsAny<Angle>()).Throws<Exception>();
        }

        [When(@"происходит вращение вокруг собственной оси")]
        public void КогдаПроисходитВращениеВокругСобственнойОси()
        {
            ICommand rc = new RotateCommand(rotateMock.Object);
            command = () => { rc.Execute(); };
        }

        [Then(@"угол наклона космического корабля к оси OX составляет (.*) град")]
        public void ТоУголНаклонаКосмическогоКорабляКОсиOXСоставляетГрад(int expectedAngle)
        {
            Assert.NotNull(command);
            command.Invoke();
            rotateMock.VerifySet(r => r.Direction = It.Is<Angle>(angle => angle.value == expectedAngle));
        }

        [Then(@"возникает ошибка Exception")]
        public void ТоВозникаетОшибкаException()
        {
            Assert.Throws<Exception>(() => rc.Execute());
        }
    }
}
