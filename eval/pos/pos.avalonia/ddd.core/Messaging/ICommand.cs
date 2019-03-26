using System;

namespace ddd.core.Messaging
{
    /// <summary>
    /// Repräsentiert ein Kommando.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Der Kommando-Identifier.
        /// </summary>
        Guid Id { get; }
    }
}