using System;
using Modules.Core.Domain.Events;

namespace Modules.Core.Domain.Common
{
    public class LoadingStateChanged : IDomainEvent
    {
        public LoadingState LoadingState { get; }

        // Constructors
        public LoadingStateChanged(LoadingState loadingState)
        {
            LoadingState = loadingState;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class LoadingStateChangedHandler : DomainEventHandler
    {
        public LoadingStateChangedHandler() : base(typeof(LoadingStateChanged))
        {
        }
    }
}