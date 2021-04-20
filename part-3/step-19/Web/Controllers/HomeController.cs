using Microsoft.AspNetCore.Mvc;
using Web.Helpers;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly RequestHelper _requestHelper;

        public HomeController(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public IActionResult Index()
        {
            var data = _requestHelper.GetData();
            return View(data);
        }

        public IActionResult RequestData()
        {
            var data = _requestHelper.GetData();
            return PartialView("_RequestData", data);
        }
    }
}