using Framework;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        public ICommandHandlerFactory Factory { get; set; }

        protected Task<ExecutionResult> Execute(ICommand command)
        {
            return Factory.GetHandlerFor(command).Execute(command);
        }
    }
}
