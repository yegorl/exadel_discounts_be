using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Modules.EventBus.Abstractions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
