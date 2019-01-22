using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Commands;

namespace Web.Controllers
{
    public class TestController : BaseController
    {
        public Task<ExecutionResult> Prova()
        {
            return Execute(new TestCommand() { });
        }
    }
}
