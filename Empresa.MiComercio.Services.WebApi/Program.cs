using Empresa.MiComercio.Services.WebApi.Modules.Feature;
using Empresa.MiComercio.Services.WebApi.Modules.HealthCheck;
using Empresa.MiComercio.Services.WebApi.Modules.Injection;
using Empresa.MiComercio.Services.WebApi.Modules.Swagger;
using Empresa.MiComercio.Services.WebApi.Modules.Versioning;
using Empresa.MiComercio.Services.WebApi.Modules.Authentication;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Empresa.MiComercio.Services.WebApi.Modules.Redis;
using Empresa.MiComercio.Services.WebApi.Modules.RateLimiter;
using Empresa.MiComercio.Application.UseCases;
using Empresa.MiComercio.Persistence;
using Empresa.MiComercio.Infrastructure;
using Asp.Versioning.ApiExplorer;
using Empresa.MiComercio.Services.WebApi.Modules.Middleware;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddFeature(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddInjection(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddVersioning();
builder.Services.AddSwagger();
//builder.Services.AddHealthCheck(builder.Configuration);
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddRatelimiting(builder.Configuration);

var app = builder.Build();

// configure the Http request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerUI(c =>
    {
        // build a swagger endpoint for each discovered API version        
        foreach (var description in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });

    app.UseReDoc(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.DocumentTitle = "MiEmpresa Technology Services API Market";
            options.SpecUrl = $"/swagger/{description.GroupName}/swagger.json";
        }
    });
}

app.UseHttpsRedirection();
app.UseCors("policyApiEcommerce");
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers();
//app.MapHealthChecksUI();
//app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
//{
//    Predicate = _ => true,
//    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//});

app.AddMiddleware();

app.Run();