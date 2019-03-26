using System;

namespace ddd.core.Messaging
{
    /// <summary>
    /// Repräsentiert eine Event-Nachricht.
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Ermittelt den Identifier der Quelle, die den Event verursacht hat.
        /// </summary>
        Guid QuellId { get; }
    }
}