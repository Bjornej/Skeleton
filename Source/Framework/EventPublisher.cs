using NServiceBus;
using System.Threading.Tasks;

namespace Framework
{
    public class EventPublisher : IEventPublisher
    {
        public IEndpointInstance EndpointInstance { get; set; }
        public Task PublishAsync(IEvent evt)
        {
            return EndpointInstance.Publish(evt, new PublishOptions() { });
        }
    }
}
