using System;

namespace Hydra.Catalog.Core.Messages
{
    //base class
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            //Return name of the class that it is implementing the message class.
            MessageType = GetType().Name;
        }
    }
}
