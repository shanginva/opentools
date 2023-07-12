using OpenTools.Mediator.Abstractions;
using SimpleInjector;
using System.Reflection;

namespace OpenTools.Mediator.App;

internal class DependencyResolver : IDependencyResolver
{
    private readonly Container container;

    public DependencyResolver(Container container)
    {
        this.container = container;
    }

    public void Register<TQuery, TResult, TQueryHandler>()
        where TQuery : IQuery<TResult>
        where TQueryHandler : class, IQueryHandler<TQuery, TResult>
    {
        container.Register<IQueryHandler<TQuery, TResult>, TQueryHandler>();
    }

    public void Register<TCommand, TCommandHandler>()
        where TCommand : ICommand
        where TCommandHandler : class, ICommandHandler<TCommand>
    {
        container.Register<ICommandHandler<TCommand>, TCommandHandler>();
    }

    public void RegisterFrom(Assembly assembly)
    {
        var queryInterface = typeof(IQuery<>);
        var queryTypes = assembly.GetTypes().Where(t => t.IsAssignableFrom(queryInterface));
        var queryHandlerAbstractions = queryTypes
            .Select(queryType => typeof(IQueryHandler<,>)
                .MakeGenericType(queryType, queryType.GetGenericArguments().First()));
        foreach (var queryHandlerInterface in queryHandlerAbstractions)
        {
            var queryHandlers = assembly.GetTypes().Where(t => t.IsAssignableFrom(queryHandlerInterface));

        }
        assembly.GetTypes();
    }

    public IQueryHandler<TQuery, TResult> Resolve<TQuery, TResult>()
        where TQuery : IQuery<TResult>
    {
        IQueryHandler<TQuery, TResult> result = container.GetInstance<IQueryHandler<TQuery, TResult>>();
        return result;
    }

    public ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand
    {
        return container.GetInstance<ICommandHandler<TCommand>>();
    }
}
