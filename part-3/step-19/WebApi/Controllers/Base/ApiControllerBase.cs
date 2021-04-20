using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.Base
{
    public class ApiControllerBase<TController, TManager> : ControllerBase
    {
        private readonly TManager _manager;
        private readonly ILogger<TController> _logger;

        public ApiControllerBase(
            TManager manager,
            ILogger<TController> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task<IActionResult> Execute<TResult>(Func<TManager, Task<TResult>> managerCall)
        {
            try
            {
                _logger.LogInformation($"ApiControllerBase.Execute - {managerCall.Method}");
                var result = await managerCall(_manager);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went seriously wrong: {ex.Message}");
                return StatusCode(500, "Something went seriously wrong!");
            }
        }
    }
}