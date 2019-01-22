using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    public abstract class Command : ICommand
    {
        public Guid CorrelationId { get; set; }
        public object UserId { get; set; }
    }
}
