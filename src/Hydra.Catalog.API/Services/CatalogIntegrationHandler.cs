using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hydra.Catalog.Domain.Interfaces;
using Hydra.Catalog.Entities.Models;
using Hydra.Core.DomainObjects;
using Hydra.Core.Integration.Messages.OrderMessages;
using Hydra.Core.MessageBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hydra.Catalog.API.Services
{
    public class CatalogIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _messageBus;
        private readonly IServiceProvider _serviceProvider;

        public CatalogIntegrationHandler(IMessageBus messageBus, IServiceProvider serviceProvider)
        {
            _messageBus = messageBus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscriber();
            return Task.CompletedTask;
        }

        private void SetSubscriber() => _messageBus.SubscribeAsync<OrderAuthorizedIntegrationEvent>("AuthorizedOrder", async request =>
                                        await RemoveFromStock(request));

        private async Task RemoveFromStock(OrderAuthorizedIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();

            var productsAvailable = new List<Product>();
            var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
            var productIds = string.Join(",", message.Items.Select(i => i.Key));
            var products = await productRepository.GetProductsById(productIds);

            if(products.Count != message.Items.Count)
            {
                CancelOrderWithoutStock(message);
                return;
            }

            foreach (var product in products)
            {
                var qty = message.Items.FirstOrDefault(p => p.Key == product.Id).Value;

                if(product.IsAvailable(qty))
                {
                    product.RemoveStock(qty);
                    productsAvailable.Add(product);
                }
            }

            if(productsAvailable.Count != message.Items.Count)
            {
                CancelOrderWithoutStock(message);
                return;
            }

            foreach (var product in productsAvailable)
            {
                productRepository.Update(product);
            }

            if(!await productRepository.UnitOfWork.Commit())
            {
                //this exception will not be market as completed and it will return to the queue to be managed later.
                //This needs to be checked, depends on the situation is not better to throw an exception, but manage this in a log process, etc.
                throw new DomainException($"Error to update the stock of the order {message.OrderId}");
            }

            var orderItemRemovedStock = new OrderItemRemovedFromStockIntegrationEvent(message.CustomerId, message.OrderId);
            await _messageBus.PublishAsync(orderItemRemovedStock); //Payment API will subscribe this event
        }

        /// <summary>
        /// There will have 2 consumers (Payment and Order)
        /// </summary>
        /// <param name="message"></param>
        private async void CancelOrderWithoutStock(OrderAuthorizedIntegrationEvent message)
        {
            var canceledOrder = new OrderCanceledIntegrationEvent(message.CustomerId, message.OrderId);
            await _messageBus.PublishAsync(canceledOrder);
        }
    }
}