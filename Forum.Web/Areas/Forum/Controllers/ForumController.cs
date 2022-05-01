using AutoMapper;
using Forum.Business.Handlers.Interfaces;
using Forum.Core.Models;
using Forum.Shared.Exceptions;
using Forum.Shared.Helpers;
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
            try
            {
                var topicList = await _forumHandler.GetAllTopicsWithUserInfoAsync();

                if (!topicList.Any()) throw new EmptyTopicListException();

                var topicVms = _mapper.Map<IEnumerable<Topic>, IEnumerable<TopicVm>>(topicList);

                return View(topicVms);
            }
            catch (EmptyTopicListException ex)
            {
                TempData[Info.Error] = Info.EmptyTopicList;
                _logger.LogError(Info.NoTopicsInDb, ex);
            }
            catch (Exception ex)
            {
                TempData[Info.Error] = Info.Exception;
                _logger.LogError(Info.GenericException, ex);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
