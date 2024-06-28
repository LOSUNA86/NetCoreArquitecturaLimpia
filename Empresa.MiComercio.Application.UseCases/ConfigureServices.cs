using Empresa.MiComercio.Application.Interface.UseCases;
using Empresa.MiComercio.Application.UseCases.Categories;
using Empresa.MiComercio.Application.UseCases.Common.Behaviours;
using Empresa.MiComercio.Application.UseCases.Customers;
using Empresa.MiComercio.Application.UseCases.Discounts;
using Empresa.MiComercio.Application.UseCases.Users;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Empresa.MiComercio.Application.UseCases
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<ICategoriesApplication, CategoriesApplication>();
            services.AddScoped<IDiscountsApplication, DiscountsApplication>();

            //services.AddTransient<UsersDtoValidator>();
            //services.AddTransient<CustomersDtoValidator>();
            //services.AddTransient<DiscountDtoValidator>();

            return services;
        }
    }
}
