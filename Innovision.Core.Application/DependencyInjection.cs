namespace Innovision.Core.Application;

using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Common.Services;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(opts => opts.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddTransient<ILoadingAccountServices, LoadingAccountServices>();
        services.AddTransient<IAccountServices, AccountServices>();
        services.AddTransient<IBranchServices, BranchServices>();
        services.AddTransient<IFileService, FileService>();
        services.AddTransient<IUserStatusServices, UserStatusServices>();
        services.AddSingleton<IBackgroundCommandQueue, BackgroundCommandQueue>();
        services.AddTransient<INotificationMessageVm, NotificationMessageVm>();

        return services;
    }
}
