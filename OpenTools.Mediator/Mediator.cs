using OpenTools.Mediator.Abstrations;
using System.Reflection;

namespace OpenTools.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IDependencyResolver dependencyResolver;

        public Mediator(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        public Task Send<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : ICommand
        {
            var commandHandler = dependencyResolver.Resolve<TCommand>();
            return commandHandler.Handle(command, cancellationToken);
        }

        public Task<TResult> Send<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        {
            MethodInfo? methodInfo = typeof(Mediator).GetMethod(nameof(Send), BindingFlags.Instance | BindingFlags.NonPublic);
            MethodInfo? generic = methodInfo?.MakeGenericMethod(query.GetType(), typeof(TResult));
            var result = generic?.Invoke(this, new object?[] { query, cancellationToken });
            return result as Task<TResult> ?? throw new QueryHandlerNotRegisteredException();
        }

        private Task<TResult> Send<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
            where TQuery : IQuery<TResult>
        {
            var queryHandler = dependencyResolver.Resolve<TQuery, TResult>();
            return queryHandler.Handle(query, cancellationToken);
        }
    }
}