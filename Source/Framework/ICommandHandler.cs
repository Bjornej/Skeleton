using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public interface ICommandHandler
    {
        Task<ExecutionResult> Execute(ICommand command);
    }

    public interface ICommandHandler<T> : IHandleMessages<T>, ICommandHandler where T:ICommand
    {
        Task<ExecutionResult> Execute(T command);
    }
}
