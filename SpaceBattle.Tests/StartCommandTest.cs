using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Tests
{
    public class StartRotateCommandTests
    {
        private readonly Mock<IRotateCommandStartable> rotateCommandStartableMock;
        private readonly Mock<IUObject> uObjectMock;
        private readonly StartRotateCommand startRotateCommand;

        public StartRotateCommandTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            rotateCommandStartableMock = new Mock<IRotateCommandStartable>();
            uObjectMock = new Mock<IUObject>();
            rotateCommandStartableMock.Setup(r => r.UObject).Returns(uObjectMock.Object);
            rotateCommandStartableMock.Setup(r => r.initValues).Returns(new Dictionary<string, object>());
            startRotateCommand = new StartRotateCommand(rotateCommandStartableMock.Object);
        }

        [Fact]
        public void LongOperationTest()
        {
            var commandMock = new Mock<ICommand>();
            var queueMock = new Mock<IQueue>();

            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Rotating.Command", (object[] args) => commandMock.Object).Execute();
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Queue", (object[] args) => queueMock.Object).Execute();

            startRotateCommand.Execute();
            rotateCommandStartableMock.Verify(r => r.initValues, Times.Once());
            queueMock.Verify(q => q.Add(It.IsAny<ICommand>()), Times.Once());
        }
    }
}