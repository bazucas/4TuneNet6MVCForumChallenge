using AutoMapper;
using Forum.Business.Services.Interfaces;
using Forum.Core.Models;
using Forum.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    public class TopicController : Controller
    {
        private readonly ILogger<TopicController> _logger;
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;

        public TopicController(ILogger<TopicController> logger, ITopicService topicService, IMapper mapper)
        {
            _logger = logger;
            _topicService = topicService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index([FromQuery] string topicId)
        {
            //var topicId = Request.Query["topicId"];

            if (topicId == string.Empty) return View();

            var topic = await _topicService.GetTopicByIdAsync(topicId);

            if (topic is null) return View();

            var topicVm = _mapper.Map<Topic, TopicVm>(topic);
            return View(topicVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit()
        {
            throw new NotImplementedException();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}
