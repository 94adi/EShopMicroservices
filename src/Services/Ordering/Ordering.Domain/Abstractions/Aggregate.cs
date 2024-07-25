﻿
namespace Ordering.Domain.Abstractions
{
    public abstract class Aggregate<Tid> : Entity<Tid>, IAggregate<Tid>
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public IDomainEvent[] ClearDomainEvents()
        {
            var events = _domainEvents.ToArray();

            _domainEvents.Clear();

            return events;
        }
    }
}
