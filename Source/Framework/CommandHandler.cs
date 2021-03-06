﻿
using System;
using System.Threading.Tasks;

namespace Framework
{
    public abstract class CommandHandler<T> : ICommandHandler<T> where T : ICommand
    {
        /// <summary>
        ///     Contesto di esecuzzione corrente se eseguito dal bus
        /// </summary>
        protected NServiceBus.IMessageHandlerContext Context { get; set; }

        public IEventPublisher EventPublisher { get; set; }

        public abstract Task Apply(T command);

        public async Task<ExecutionResult> Execute(T command)
        {
            try
            {
                await Apply(command);
                return new ExecutionResult() { Success = true };
            }
            catch (Exception e)
            {
                return new ExecutionResult() { Success = false, Errors = new string[] { e.Message } };
            }
        }

        public Task<ExecutionResult> Execute(Framework.ICommand command)
        {
            return Execute((T)command);
        }

        public Task Handle(T message, NServiceBus.IMessageHandlerContext context)
        {
            Context = context;
            return Execute(message);
        }


        protected Task PublishAsync(IEvent evt)
        {
            if (Context != null)
            {
                return Context.Publish(evt, new NServiceBus.PublishOptions()
                {

                });
            }
            else
            {
                return EventPublisher.PublishAsync(evt);
            }
        }
    }
}
