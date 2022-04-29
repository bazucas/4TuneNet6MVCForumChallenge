using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    public class ForumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
