using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Tests
{
    public class MacroCommandTest
    {
        private readonly Mock<ICommand> mICommand;
        private readonly Mock<IUObject> obj;
        private readonly Mock<IEnumerable<string>> mstring;
        private readonly Mock<IStrategy> strategy;
        private readonly Mock<IStrategy> new_strategy;
        private readonly Mock<IStrategy> ReturnCommand;

        public MacroCommandTest()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

            mICommand = new Mock<SpaceBattle.ICommand>();
            obj = new Mock<SpaceBattle.IUObject>();
            mstring = new Mock<IEnumerable<string>>();
            strategy = new Mock<SpaceBattle.IStrategy>();
            new_strategy = new Mock<SpaceBattle.IStrategy>();
            ReturnCommand = new Mock<SpaceBattle.IStrategy>();

            strategy.Setup(m => m.Invoke(It.IsAny<IEnumerable<SpaceBattle.ICommand>>())).Returns(mICommand.Object).Verifiable();
            strategy.Setup(m => m.Invoke(It.IsAny<SpaceBattle.IUObject>())).Returns(mICommand.Object).Verifiable();
            new_strategy.Setup(m => m.Invoke()).Returns(mstring.Object).Verifiable();

            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Burn_Fuel", (object[] args) => ReturnCommand.Object.Invoke(args)).Execute();
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Burn_Fuel.Command", (object[] args) => new_strategy.Object.Invoke(args)).Execute();
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateMacro.Command", (object[] args) => strategy.Object.Invoke(args)).Execute();
        }

        [Fact]
        public void Execute_CallsForEachCommand()
        {
            var mCommand = new Mock<SpaceBattle.ICommand>();
            mCommand.Setup(m => m.Execute()).Verifiable();

            var commands = new List<SpaceBattle.ICommand> { mCommand.Object };
            var macroCommand = new MacroCommand(commands);

            macroCommand.Execute();
            mCommand.Verify();
        }

        [Fact]
        public void MacroTest()
        {
            _ = new Creation();

            Creation.Invoke("Burn_Fuel", obj.Object);

            new_strategy.Verify();
            ReturnCommand.Verify();
        }
    }
}
