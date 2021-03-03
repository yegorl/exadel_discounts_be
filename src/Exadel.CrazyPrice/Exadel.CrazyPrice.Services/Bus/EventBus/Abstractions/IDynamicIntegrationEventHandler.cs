using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Services.Bus.EventBus.Abstractions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
