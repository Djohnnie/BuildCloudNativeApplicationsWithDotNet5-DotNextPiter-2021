using WebApi.Controllers.Base;
using WebApi.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ApiControllerBase<StatusController, IStatusManager>
    {
        public StatusController(
            IStatusManager statusManager,
            ILogger<StatusController> logger) : base(statusManager, logger) { }

        [HttpGet]
        public Task<IActionResult> GetStatus()
        {
            return Execute(x => x.GetMachineName());
        }
    }
}