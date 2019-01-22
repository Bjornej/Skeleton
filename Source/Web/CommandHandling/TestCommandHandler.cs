using Framework;
using System.Threading.Tasks;
using Web.Commands;

namespace Web.CommandHandling
{
    public class TestCommandHandler : CommandHandler<TestCommand>
    {
        public override Task Apply(TestCommand command)
        {
            var a = 2;

            return Task.CompletedTask;
        }
    }
}
