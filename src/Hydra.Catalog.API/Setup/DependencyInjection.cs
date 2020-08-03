using System;
using Hydra.Catalog.Core.Bus;
using Hydra.Catalog.Data;
using Hydra.Catalog.Data.Repository;
using Hydra.Catalog.Domain.Interfaces;
using Hydra.Catalog.Domain.Interfaces.Services;
using Hydra.Catalog.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Catalog.API.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();

        }
    }
}
