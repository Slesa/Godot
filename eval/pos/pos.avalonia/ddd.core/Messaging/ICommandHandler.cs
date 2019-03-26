namespace ddd.core.Messaging
{
    /// <summary>
    /// Marker-Interface, um Handler einfacher per Refelction zu ermitteln.
    /// </summary>
    public interface ICommandHandler { }

    public interface ICommandHandler<T> : ICommandHandler
        where T : ICommand
    {
        void Handle(T command);
    }
}