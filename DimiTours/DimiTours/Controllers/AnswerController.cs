using Microsoft.AspNetCore.Mvc;

namespace DimiTours.Controllers
{
    [ApiController]
    [Route("api/v1/answer")]
    public class AnswerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
