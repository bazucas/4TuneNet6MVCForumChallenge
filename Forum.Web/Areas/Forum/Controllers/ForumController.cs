using AutoMapper;
using Forum.Business.Handlers.Interfaces;
using Forum.Core.Models;
using Forum.Shared.Exceptions;
using Forum.Shared.Helpers;
using Forum.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.Forum.Controllers
{
    /// <summary>
    /// ForumController inherits from <see cref="Controller"/>
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Area("Forum")]
    public class ForumController : Controller
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ForumController> _logger;
        /// <summary>
        /// The forum handler
        /// </summary>
        private readonly IForumHandler _forumHandler;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ForumController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="forumService">The forum service.</param>
        /// <param name="mapper">The mapper.</param>
        public ForumController(ILogger<ForumController> logger, IForumHandler forumService, IMapper mapper)
        {
            _logger = logger;
            _forumHandler = forumService;
            _mapper = mapper;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Forum.Shared.Exceptions.EmptyTopicListException"></exception>
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
