using EcommerceDev.Application.Common;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceDev.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddSingleton<IMediator, Mediator>();

        services.Scan(s =>
            s.FromAssemblies(typeof(ApplicationModule).Assembly)
                .AddClasses(c => c.AssignableTo(typeof(IHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
        );

        return services;
    }
}
