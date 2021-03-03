using Exadel.CrazyPrice.Services.Bus.EventBus.Abstractions;
using Exadel.CrazyPrice.Services.Bus.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exadel.CrazyPrice.Services.Bus.EventBus
{
    public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string> OnEventRemoved;

        public InMemoryEventBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
        }

        public bool IsEmpty => !_handlers.Keys.Any();

        public void Clear() => _handlers.Clear();

        public void AddDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler =>
            DoAddSubscription(typeof(TH), eventName, isDynamic: true);

        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();

            DoAddSubscription(typeof(TH), eventName, isDynamic: false);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }
        }

        public void RemoveDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler =>
            DoRemoveHandler(eventName, FindDynamicSubscriptionToRemove<TH>(eventName));

        public void RemoveSubscription<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent =>
            DoRemoveHandler(GetEventKey<T>(), FindSubscriptionToRemove<T, TH>());

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
            => GetHandlersForEvent(GetEventKey<T>());
        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) =>
            _handlers[eventName];

        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent =>
            HasSubscriptionsForEvent(GetEventKey<T>());
        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        public Type GetEventTypeByName(string eventName) =>
            _eventTypes.SingleOrDefault(t => t.Name == eventName);

        public string GetEventKey<T>() =>
            typeof(T).Name;

        private void DoAddSubscription(Type handlerType, string eventName, bool isDynamic)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<SubscriptionInfo>());
            }

            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }

            _handlers[eventName]
                .Add(isDynamic ? SubscriptionInfo.Dynamic(handlerType) : SubscriptionInfo.Typed(handlerType));
        }

        private void DoRemoveHandler(string eventName, SubscriptionInfo subsToRemove)
        {
            if (subsToRemove == null)
            {
                return;
            }

            _handlers[eventName].Remove(subsToRemove);

            if (_handlers[eventName].Any())
            {
                return;
            }

            _handlers.Remove(eventName);
            var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
            if (eventType != null)
            {
                _eventTypes.Remove(eventType);
            }
            RaiseOnEventRemoved(eventName);
        }

        private void RaiseOnEventRemoved(string eventName)
        {
            var handler = OnEventRemoved;
            handler?.Invoke(this, eventName);
        }

        private SubscriptionInfo FindDynamicSubscriptionToRemove<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler =>
            DoFindSubscriptionToRemove(eventName, typeof(TH));

        private SubscriptionInfo FindSubscriptionToRemove<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T> =>
            DoFindSubscriptionToRemove(GetEventKey<T>(), typeof(TH));

        private SubscriptionInfo DoFindSubscriptionToRemove(string eventName, Type handlerType) =>
            !HasSubscriptionsForEvent(eventName)
                ? null
                : _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);
    }
}
