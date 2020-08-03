using System;
using MediatR;

namespace Hydra.Catalog.Core.Messages
{
    /// <summary>
    /// All events are delivered by Mediator library
    /// INotification - Metiatr Inteface. This interface is only to mark that this is a notification. 
    /// It does not implement any method, it is an empty inteface.
    /// </summary>
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        public Event()
        {
            //Time that the event was triggered
            Timestamp = DateTime.Now;
        }
    }
}
