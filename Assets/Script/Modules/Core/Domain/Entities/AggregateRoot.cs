using System.Collections.Generic;
using Modules.Core.Domain.Events;

namespace Modules.Core.Domain.Entities
{
    public abstract class AggregateRoot : Entity
    {
        // Data
        private Queue<IDomainEvent> Events { get; }

        // Getters
        public bool HasEvents => Events.Count > 0;

        // Constructors
        protected AggregateRoot(Identity id) : base(id)
        {
            Events = new Queue<IDomainEvent>();
        }

        // Interfaces
        public IDomainEvent PopDomainEvent()
        {
            return Events.Dequeue();
        }

        // Methods
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            Events.Enqueue(domainEvent);
        }
    }
}