using System.Reflection;

namespace OpenTools.Mediator.Abstrations;

public interface IDependencyResolver
{
    IQueryHandler<TQuery, TResult> Resolve<TQuery, TResult>() where TQuery : IQuery<TResult>;

    ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand;

    void Register<TQuery, TResult, TQueryHandler>()
        where TQuery : IQuery<TResult>
        where TQueryHandler : class, IQueryHandler<TQuery, TResult>;

    void Register<TCommand, TCommandHandler>()
        where TCommand : ICommand
        where TCommandHandler : class, ICommandHandler<TCommand>;

    void RegisterFrom(Assembly assembly);
}
