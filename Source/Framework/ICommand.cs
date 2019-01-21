using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    public interface ICommand
    {
        Guid CorrelationId { get; set; }

        object UserId { get; set; }
    }
}
