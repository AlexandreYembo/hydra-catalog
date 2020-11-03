using Hydra.Catalog.API.Services;
using Hydra.Core.Extensions;
using Hydra.Core.MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Catalog.API.Setup
{
    public static class MessageBusConfig
    {
        public static void AddMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                    .AddHostedService<CatalogIntegrationHandler>();
        }
    }
} 