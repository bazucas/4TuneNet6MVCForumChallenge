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

        // CREATE TOPIC
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TopicVm topicVm)
        {
            if (!ModelState.IsValid) return View(topicVm);

            var topic = new Topic
            {
                Id = topicVm.Id,
                Description = topicVm.Description,
                CreationDate = topicVm.CreationDate,
                Title = topicVm.Title
            };

            await _topicService.AddTopicAsync(topic);
            await _topicService.SaveAllAsync();
            TempData["success"] = "Topic created successfully";
            return RedirectToAction("Index");
        }


        //EDIT TOPIC
        public async Task<IActionResult> Edit([FromQuery] string topicId)
        {
            if (topicId == string.Empty) return NotFound();

            var topic = await _topicService.GetTopicByIdAsync(topicId);

            if (topic is null)
            {
                return NotFound();
            }

            var topicVm = _mapper.Map<Topic, TopicVm>(topic);

            return View(topicVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TopicVm topicVm)
        {
            if (!ModelState.IsValid) return View(topicVm);

            var topic = new Topic
            {
                Id = topicVm.Id,
                Description = topicVm.Description,
                CreationDate = topicVm.CreationDate,
                Title = topicVm.Title
            };

            //await _topicService.UpdateTopic(topicId);
            //await _unitOfWork.SaveAsync();
            return RedirectToAction("Index");
        }


        // DELETE TOPIC
        public async Task<IActionResult> Delete([FromQuery] string topicId)
        {
            if (topicId == string.Empty) return View();

            var topic = await _topicService.GetTopicByIdAsync(topicId);

            if (topic is null) return View();

            var topicVm = _mapper.Map<Topic, TopicVm>(topic);
            return View(topicVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(string topicId)
        {
            throw new NotImplementedException();
        }
    }
}
