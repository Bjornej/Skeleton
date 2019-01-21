using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    public interface IEvent
    {
        Guid CorrelationId { get; set; }

        object UserId { get; set; }
    }
}
