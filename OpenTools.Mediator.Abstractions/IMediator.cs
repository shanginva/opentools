namespace OpenTools.Mediator.Abstractions;

public interface IMediator
{
    Task<TResult> Send<TResult>(IQuery<TResult> query, CancellationToken cancellationToken);

    Task Send<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : ICommand;
}
