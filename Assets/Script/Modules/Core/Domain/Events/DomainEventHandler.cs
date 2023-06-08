using System;

namespace Modules.Core.Domain.Events
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DomainEventHandler : Attribute
    {
        public Type DomainEventType { get; }

        public DomainEventHandler(Type domainEventType)
        {
            DomainEventType = domainEventType;
        }
    }
}