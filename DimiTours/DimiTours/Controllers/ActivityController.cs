using Microsoft.AspNetCore.Mvc;

namespace DimiTours.Controllers
{
    [ApiController]
    [Route("api/v1/activity")]
    public class ActivityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
