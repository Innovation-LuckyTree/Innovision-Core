using System.Text;
using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Innovision.Core.Application.Common.Models;
using Innovision.Core.Application.Common.Interfaces;

namespace Innovision.Core.API;

public static class ConfigurationServices
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        var appConfig = configuration.GetSection("AppConfig").Get<AppConfig>();
        services.AddSingleton<IAppConfig>(appConfig);

        var sftpConfig = configuration.GetSection("SftpConfig").Get<SftpConfig>();
        services.AddSingleton<ISftpConfig>(sftpConfig);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = appConfig.JwtConfig.Issuer,
                    ValidAudience = appConfig.JwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfig.JwtConfig.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false
                };
            });
            
        return services;
    }
}
