using AutoMapper;
using Forum.Business.Handlers.Interfaces;
using Forum.Core.Models;
using Forum.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    [Authorize]
    public class TopicController : Controller
    {
        private readonly ILogger<TopicController> _logger;
        private readonly ITopicHandler _topicHandler;
        private readonly IMapper _mapper;

        public TopicController(ILogger<TopicController> logger, ITopicHandler topicService, IMapper mapper)
        {
            _logger = logger;
            _topicHandler = topicService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index([FromQuery] string topicId)
        {
            //var topicId = Request.Query["topicId"];

            // TODO: Validar User
            // if(userContext != userTopic) return RedirectToAction("Index", "Forum");

            if (topicId == string.Empty) return View();
            var topic = await _topicHandler.GetTopicByIdAsync(topicId);
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
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var topic = new Topic
            {
                Id = topicVm.Id,
                Description = topicVm.Description,
                CreationDate = topicVm.CreationDate,
                Title = topicVm.Title,
                ApplicationUserId = userId
            };

            await _topicHandler.AddTopicAsync(topic);
            await _topicHandler.SaveAllAsync();
            return RedirectToAction("Index", "Forum");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TopicVm topicVm)
        {
            if (!ModelState.IsValid) return View(topicVm);

            var topic = new Topic
            {
                Id = topicVm.Id,
                Title = topicVm.Title,
                Description = topicVm.Description
            };

            await _topicHandler.UpdateTopicAsync(topic);
            await _topicHandler.SaveAllAsync();
            return RedirectToAction("Index", "Forum");
        }


        // DELETE TOPIC
        public async Task<IActionResult> Delete([FromQuery] string topicId)
        {
            // TODO: Validar User
            // if(userContext != userTopic) return RedirectToAction("Index", "Forum");

            if (topicId == string.Empty) return View();
            var topic = await _topicHandler.GetTopicByIdAsync(topicId);
            if (topic is null) return View();
            var topicVm = _mapper.Map<Topic, TopicVm>(topic);
            return View(topicVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(string? id)
        {
            //TODO: Validar user
            if (!ModelState.IsValid || id is null) return RedirectToAction("Index", "Forum");
            await _topicHandler.DeleteTopicAsync(id);
            await _topicHandler.SaveAllAsync();
            return RedirectToAction("Index", "Forum");
        }
    }
}
