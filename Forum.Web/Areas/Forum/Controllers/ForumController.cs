using AutoMapper;
using Forum.Business.Handlers.Interfaces;
using Forum.Core.Models;
using Forum.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    public class ForumController : Controller
    {
        private readonly ILogger<ForumController> _logger;
        private readonly IForumHandler _forumHandler;
        private readonly IMapper _mapper;

        public ForumController(ILogger<ForumController> logger, IForumHandler forumService, IMapper mapper)
        {
            _logger = logger;
            _forumHandler = forumService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var topicList = await _forumHandler.GetAllTopicsAsync();

            var topicVms = _mapper.Map<IEnumerable<Topic>, IEnumerable<TopicVm>>(topicList);

            return View(topicVms);
        }
    }
}
