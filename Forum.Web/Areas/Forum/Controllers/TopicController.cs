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
            try
            {
                if (topicId is "" or null)
                {
                    TempData["error"] = "Invalid TopicId";
                    RedirectToAction("Index", "Forum");
                }
                if (!await CurrentUserIsTopicOwner(topicId))
                {
                    TempData["error"] = "Invalid User";
                    return RedirectToAction("Index", "Forum");
                }
                var topic = await _topicHandler.GetTopicByIdAsync(topicId!);
                if (topic is null)
                {
                    TempData["error"] = "Topic is null";
                    RedirectToAction("Index", "Forum");
                }
                var topicVm = _mapper.Map<Topic, TopicVm>(topic!);
                return View(topicVm);
            }
            catch (Exception)
            {
                TempData["error"] = "An exception occurred";
                // TODO: log exception, for example
            }
            return RedirectToAction("Index", "Forum");
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
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["error"] = "Information not valid";
                    return View(topicVm);
                }
                var userId = GetUserId();
                var topic = new Topic
                {
                    Id = Guid.NewGuid(),
                    Description = topicVm.Description,
                    CreationDate = DateTime.Now,
                    Title = topicVm.Title,
                    ApplicationUserId = userId
                };
                await _topicHandler.AddTopicAsync(topic);
                await _topicHandler.SaveAllAsync();
                TempData["Success"] = "Topic Created Successfully.";
            }
            catch (Exception)
            {
                TempData["error"] = "An exception occurred";
                // TODO: log exception, for example
            }
            return RedirectToAction("Index", "Forum");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TopicVm topicVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["error"] = "Information not valid";
                    return View(topicVm);
                }
                var topic = new Topic
                {
                    Id = topicVm.Id,
                    Title = topicVm.Title,
                    Description = topicVm.Description
                };
                await _topicHandler.UpdateTopicAsync(topic);
                await _topicHandler.SaveAllAsync();
                TempData["Success"] = "Topic Updated Successfully.";
            }
            catch (Exception)
            {
                TempData["error"] = "An exception occurred";
                // TODO: log exception, for example
            }
            return RedirectToAction("Index", "Forum");
        }

        public async Task<IActionResult> Delete([FromQuery] string topicId)
        {
            try
            {
                if (topicId is "" or null)
                {
                    TempData["error"] = "Invalid TopicId";
                    RedirectToAction("Index", "Forum");
                }
                if (!await CurrentUserIsTopicOwner(topicId))
                {
                    TempData["error"] = "Invalid User";
                    return RedirectToAction("Index", "Forum");
                }
                var topic = await _topicHandler.GetTopicByIdAsync(topicId!);
                if (topic is null)
                {
                    TempData["error"] = "Topic is null";
                    RedirectToAction("Index", "Forum");
                }
                var topicVm = _mapper.Map<Topic, TopicVm>(topic!);
                return View(topicVm);
            }
            catch (Exception)
            {
                TempData["error"] = "An exception occurred";
                // TODO: log exception, for example
            }
            return RedirectToAction("Index", "Forum");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(string? id)
        {
            try
            {
                if (!ModelState.IsValid || id is null)
                {
                    TempData["error"] = "Information not valid";
                    return RedirectToAction("Index", "Forum");
                }
                await _topicHandler.DeleteTopicAsync(id);
                await _topicHandler.SaveAllAsync();
                TempData["Success"] = "Topic Deleted Successfully.";
            }
            catch (Exception)
            {
                TempData["error"] = "An exception occurred";
                // TODO: log exception, for example
            }
            return RedirectToAction("Index", "Forum");
        }

        private async Task<bool> CurrentUserIsTopicOwner(string? topicId)
        {
            return GetUserId() == (await _topicHandler.GetTopicByIdAsync(topicId!))?.ApplicationUserId;
        }

        private string? GetUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
