using Innovision.Core.Infrastructure.AccountServices;
using Innovision.Core.Infrastructure.Config;
using Innovision.Core.Infrastructure.CoreIdentity;
using Innovision.Core.Infrastructure.Games;
using Innovision.Core.Infrastructure.GameSchedule;
using Innovision.Core.Infrastructure.PaymentServices;
using Innovision.Core.Infrastructure.WebsocketServices;
using Innovision.Core.Infrastructure.Helpers;
using Innovision.Core.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Innovision.Core.Infrastructure.MessageBrokerClient;

namespace Innovision.Core.Infrastructure;

public static class DepedencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddTransient<IdentityBearerTokenHandler>();

        services.AddHttpClient<ICoreIdentityApi, CoreIdentityApi>()
            .ConfigurePrimaryHttpMessageHandler(PrimaryHttpClientHandlerFactory.CreateHttpClientHandler);

        services.AddHttpClient<IGamesApi, GamesApi>()
            .ConfigurePrimaryHttpMessageHandler(PrimaryHttpClientHandlerFactory.CreateHttpClientHandler)
            .AddHttpMessageHandler<IdentityBearerTokenHandler>();

        services.AddHttpClient<IAccountServiceApi, AccountServiceApi>()
            .ConfigurePrimaryHttpMessageHandler(PrimaryHttpClientHandlerFactory.CreateHttpClientHandler)
            .AddHttpMessageHandler<IdentityBearerTokenHandler>();

        services.AddHttpClient<IGameScheduleApi, GameScheduleApi>()
            .ConfigurePrimaryHttpMessageHandler(PrimaryHttpClientHandlerFactory.CreateHttpClientHandler);

        services.AddHttpClient<ISupportApi, SupportApi>()
            .ConfigurePrimaryHttpMessageHandler(PrimaryHttpClientHandlerFactory.CreateHttpClientHandler);

        services.AddHttpClient<IPaymentServicesApi, PaymentServicesApi>()
            .ConfigurePrimaryHttpMessageHandler(PrimaryHttpClientHandlerFactory.CreateHttpClientHandler)
            .AddHttpMessageHandler<IdentityBearerTokenHandler>();

        services.AddHttpClient<IWebsocketServicesApi, WebsocketServicesApi>()
            .ConfigurePrimaryHttpMessageHandler(PrimaryHttpClientHandlerFactory.CreateHttpClientHandler);

        services.AddHttpClient<IMessageBrokerClientApi, MessageBrokerClientApi>()
            .ConfigurePrimaryHttpMessageHandler(PrimaryHttpClientHandlerFactory.CreateHttpClientHandler);

        return services;
    }
}
