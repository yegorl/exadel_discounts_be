using System;

namespace Exadel.CrazyPrice.Services.Bus.EventBus
{
    public partial class InMemoryEventBusSubscriptionsManager
    {
        public class SubscriptionInfo
        {
            private SubscriptionInfo(bool isDynamic, Type handlerType) =>
                (IsDynamic, HandlerType) = (isDynamic, handlerType);

            public bool IsDynamic { get; }

            public Type HandlerType { get; }

            public static SubscriptionInfo Dynamic(Type handlerType) =>
                new(true, handlerType);

            public static SubscriptionInfo Typed(Type handlerType) =>
                new(false, handlerType);
        }
    }
}
