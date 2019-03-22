using System;

namespace ddd.core.Events
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}