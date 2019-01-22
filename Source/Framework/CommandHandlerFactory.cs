using Castle.Windsor;

namespace Framework
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private IWindsorContainer Container { get; set; }

        public CommandHandlerFactory(IWindsorContainer container)
        {
            Container = container;
        }

        public ICommandHandler GetHandlerFor(ICommand command)
        {
            return (ICommandHandler) Container.Kernel.Resolve((typeof(ICommandHandler<>).MakeGenericType(command.GetType())));
        }
    }
}
