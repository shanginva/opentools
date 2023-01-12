using OpenTools.Mediator.Abstrations;

namespace OpenTools.Mediator.App;

public class TestCommand : ICommand { }

public class TestCommandHandler : ICommandHandler<TestCommand>
{
    public Task Handle(TestCommand command, CancellationToken cancellationToken)
    {
        Console.WriteLine("Test command handler");
        return Task.CompletedTask;
    }
}
