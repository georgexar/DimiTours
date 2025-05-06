using Microsoft.AspNetCore.Mvc;

namespace DimiTours.Controllers
{
    [ApiController]
    [Route("api/v1/question")]
    public class QuestionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
