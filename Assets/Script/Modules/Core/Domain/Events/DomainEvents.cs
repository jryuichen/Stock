using System;
using System.Collections.Generic;
using System.Linq;

namespace Modules.Core.Domain.Events
{
    public static class DomainEvents
    {
        private static readonly Dictionary<Type, List<Delegate>> Handlers = new Dictionary<Type, List<Delegate>>();
        private static readonly Dictionary<Type, List<Delegate>> ToBeUnRegisteredHandlers = new Dictionary<Type, List<Delegate>>();

        public static void Register(Type domainEventType, Delegate eventHandler)
        {
            if (!Handlers.ContainsKey(domainEventType))
                Handlers.Add(domainEventType, new List<Delegate>());
            Handlers[domainEventType].Add(eventHandler);
        }

        public static void UnRegister(Type domainEventType, Delegate eventHandler)
        {
            if (!ToBeUnRegisteredHandlers.ContainsKey(domainEventType))
                ToBeUnRegisteredHandlers.Add(domainEventType, new List<Delegate>());
            ToBeUnRegisteredHandlers[domainEventType].Add(eventHandler);
        }

        public static void Dispatch<T>(T domainEvent) where T : IDomainEvent
        {
            var domainEventType = domainEvent.GetType();
            if (!Handlers.ContainsKey(domainEventType)) return;
            DispatchUtilAllHandlersDispatched(domainEvent);
            CleanUpHandlers();
        }

        private static void DispatchUtilAllHandlersDispatched<T>(T domainEvent) where T : IDomainEvent
        {
            var domainEventType = domainEvent.GetType();
            var handlers = Handlers[domainEventType].ToList();
            var dispatchedHandlers = new List<Delegate>();
            do
            {
                DispatchToHandlers(domainEvent, handlers);
                dispatchedHandlers.AddRange(handlers);
                var currentHandlers = Handlers[domainEventType].ToList();
                // Get new handlers added during previous dispatched handlers
                handlers = currentHandlers.Where(ch => dispatchedHandlers.All(dh => dh != ch)).ToList();
            } while (handlers.Count > 0);
        }

        private static void DispatchToHandlers<T>(T domainEvent, List<Delegate> handlers) where T : IDomainEvent
        {
            var domainEventType = domainEvent.GetType();
            foreach (var handler in handlers)
            {
                if (!IsHandlerUnRegistered(domainEventType, handler))
                    handler.DynamicInvoke(domainEvent);
            }
        }

        private static void CleanUpHandlers()
        {
            foreach (var unRegisteredHandler in ToBeUnRegisteredHandlers)
            {
                var domainEventType = unRegisteredHandler.Key;
                if (!Handlers.ContainsKey(domainEventType)) continue;
                foreach (var handler in unRegisteredHandler.Value)
                    Handlers[domainEventType].Remove(handler);
            }
            ToBeUnRegisteredHandlers.Clear();
        }

        private static bool IsHandlerUnRegistered(Type domainEventType, Delegate eventHandler)
        {
            return ToBeUnRegisteredHandlers.ContainsKey(domainEventType) &&
                   ToBeUnRegisteredHandlers[domainEventType].Contains(eventHandler);
        }
    }
}