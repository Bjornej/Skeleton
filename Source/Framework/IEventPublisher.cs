using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public interface IEventPublisher
    {
        Task PublishAsync(IEvent evt);
    }
}
