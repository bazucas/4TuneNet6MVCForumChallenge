using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    public class TopicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
