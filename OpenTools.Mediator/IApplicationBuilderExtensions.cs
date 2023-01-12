using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTools.Mediator.Abstrations;

namespace OpenTools.Mediator;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMediator<TDependencyResolver>(
        this IServiceCollection services)
        where TDependencyResolver : class, IDependencyResolver
    {
        services.AddSingleton<IMediator, Mediator>();
        services.AddSingleton(typeof(IDependencyResolver), typeof(TDependencyResolver));
        return services;
    }
}
