using FluentValidation.AspNetCore;
using Innovision.Core.Filters;
using Innovision.Core.Persistence;
using Microsoft.OpenApi.Models;
using Innovision.Core.Application;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Services;
using Innovision.Core.Infrastructure;
using Core.API.Attributes;
using System.Text.Json;
using Innovision.Core.Common.Models;
using Innovision.Core.Workers;

namespace Innovision.Core.API;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddAppBuilder(this WebApplicationBuilder builder)
    {
        string coreConnString = builder.Configuration.GetConnectionString("CoreDb");

        builder.Services.AddConfigurations(builder.Configuration);
        builder.Services.AddPersistenceLayer(coreConnString);
        builder.Services.AddApplicationLayer();
        builder.Services.AddControllers();
        builder.Services.AddInfrastructureLayer();
        builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();

        //var json = File.ReadAllText("./Assets/notifications.json");
        //var notificationMessages = JsonSerializer.Deserialize<List<NotificationMessage>>(json, new JsonSerializerOptions
        //{
        //    PropertyNameCaseInsensitive = true
        //});

        //builder.Services.AddTransient<INotificationMessageVm>(o => new NotificationMessageVm(notificationMessages));


        // barangay list
        var brgys = File.ReadAllText("./Assets/barangays.json");
        var brgList = JsonSerializer.Deserialize<List<PsgBarangay>>(brgys, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        builder.Services.AddTransient<IPsgBarangayVm>(o => new PsgBarangayVm(brgList));

        // builder.Services.AddApiVersioning(setup  =>
        // {
        //     setup.DefaultApiVersion = new ApiVersion(1, 0);
        //     setup.AssumeDefaultVersionWhenUnspecified = true;
        //     setup.ReportApiVersions = true;
        // });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "allOrigin",
            policy =>
            {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

        // builder.Services.AddVersionedApiExplorer(setup =>
        // {
        //     setup.GroupNameFormat = "'v'VVV";
        //     setup.SubstituteApiVersionInUrl = true;
        // });

        builder.Services.AddSwaggerGen(opts =>
        {
            opts.SwaggerDoc("v1", new OpenApiInfo { Title = "Innovision.Core API", Version = "version 1.0" });
            //opts.SwaggerDoc("v2", new OpenApiInfo { Title = "Innovision.Core API", Version = "version 2.0" });

            opts.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                   {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                       },
                       new[] { "Innovision.Core", "Innovision.Core" }
                   }
            });

            // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            // opts.IncludeXmlComments(xmlPath);
            opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            opts.OperationFilter<FileUploadOperation>();
            opts.OperationFilter<OptionalRouteParameterOperationFilter>();
            opts.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication();
        builder.Services.AddHostedService<NotificationWorker>();

        builder.Services.AddControllers(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>())
            .AddFluentValidation();

        builder.Services.AddMemoryCache();

        return builder;
    }
}