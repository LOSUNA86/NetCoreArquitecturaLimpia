using Empresa.MiComercio.Application.Interface.Infrastructure;
using Empresa.MiComercio.Infrastructure.EventBus;
using Empresa.MiComercio.Infrastructure.EventBus.Options;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.Extensions.Options;
using Empresa.MiComercio.Infrastructure.Notification.Options;
using SendGrid.Extensions.DependencyInjection;
using Empresa.MiComercio.Infrastructure.Notification;

namespace Empresa.MiComercio.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.ConfigureOptions<RabbitMqOptionsSetup>();
            services.AddScoped<IEventBus, EventBusRabbitMQ>();
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    RabbitMqOptions? opt = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<RabbitMqOptions>>()
                        .Value;

                    cfg.Host(opt.HostName, opt.VirtualHost, h =>
                    {
                        h.Username(opt.UserName);
                        h.Password(opt.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            /*Servicio de SendGrid*/
            services.AddScoped<INotification, NotificationSendGrid>();
            services.ConfigureOptions<SendgridOptionsSetup>();
            SendgridOptions? sendgridOptions = services.BuildServiceProvider()
                .GetRequiredService<IOptions<SendgridOptions>>()
                .Value;

            services.AddSendGrid((options =>
            {
                options.ApiKey = sendgridOptions.ApiKey;
            }));

            return services;
        }
    }
}
