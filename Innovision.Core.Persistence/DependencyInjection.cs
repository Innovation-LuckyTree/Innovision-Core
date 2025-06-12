using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Common.Services;
using Innovision.Core.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Innovision.Core.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextFactory<CoreDbContext>(opts => opts.UseNpgsql(connectionString));
        services.AddScoped<ICoreDbContext>(provider => provider.GetService<CoreDbContext>());
        services.AddScoped<IAccountServices>(provider => provider.GetService<AccountServices>());
        services.AddScoped<IBranchServices>(provider => provider.GetService<BranchServices>());

        return services;
    }
}
