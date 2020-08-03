using System;
using System.Threading;
using System.Threading.Tasks;
using Hydra.Catalog.Domain.Interfaces;
using MediatR;

namespace Hydra.Catalog.Domain.Events
{
    /// <summary>
    /// Class of Manipulation of event
    /// </summary>
    public class ProductEventHandler : INotificationHandler<ProductLowStockEvent>
    {
        private readonly IProductRepository _productRepository;

        public ProductEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        /// <summary>
        /// message will have aggreate ID.abstract It is important that you use an aggregate ID for the events, because when you trigger an event
        /// you can use this id that is part of the aggregate object.
        /// All message contain an aggregateID
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(ProductLowStockEvent message, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductById(message.AggregateId);

            //TODO: Implement an email service;

            //TODO: Implement a queue process - RabbitMQ

            //TODO: You can implement more repository if necessary.
        }
    }
}
