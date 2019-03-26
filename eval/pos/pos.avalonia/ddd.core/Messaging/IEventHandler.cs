namespace ddd.core.Messaging
{
    /// <summary>
    /// Marker-Interface, um Handler einfacher per Refelction zu ermitteln.
    /// </summary>
    public interface IEventHandler {}

    public interface IEventHandler<T> : IEventHandler
        where T : IEvent
    {
        void Handle(T @event);
    }
}