using Empresa.MiComercio.Application.Interface.Presentation;
using Empresa.MiComercio.Services.WebApi.Modules.GlobalException;
using Empresa.MiComercio.Services.WebApi.Services;


namespace Empresa.MiComercio.Services.WebApi.Modules.Injection
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            //services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<GlobalExceptionHandler>();
            services.AddScoped<ICurrentUser, CurrentUser>();

            return services;
        }
    }
}
