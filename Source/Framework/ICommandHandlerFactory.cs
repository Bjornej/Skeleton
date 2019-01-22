using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler GetHandlerFor(ICommand command);
    }
}
