using AutoMapper;
using Forum.Business.Handlers.Interfaces;
using Forum.Core.Models;
using Forum.Shared.Exceptions;
using Forum.Shared.Helpers;
using Forum.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.Web.Areas.Forum.Controllers
{
    /// <summary>
    /// TopicController inherits from <see cref="Controller"/>
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Area("Forum")]
    [Authorize]
    public class TopicController : Controller
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<TopicController> _logger;
        /// <summary>
        /// The topic handler
        /// </summary>
        private readonly ITopicHandler _topicHandler;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="topicHandler">The topic service.</param>
        /// <param name="mapper">The mapper.</param>
        public TopicController(ILogger<TopicController> logger, ITopicHandler topicHandler, IMapper mapper)
        {
            _logger = logger;
            _topicHandler = topicHandler;
            _mapper = mapper;
        }

        /// <summary>
        /// Indexes the specified topic identifier.
        /// </summary>
        /// <param name="topicId">The topic identifier.</param>
        /// <returns></returns>
        /// <exception cref="Forum.Shared.Exceptions.InvalidIdException"></exception>
        /// <exception cref="Forum.Shared.Exceptions.InvalidUserException"></exception>
        /// <exception cref="Forum.Shared.Exceptions.NullTopicException"></exception>
        public async Task<IActionResult> Index([FromQuery] string topicId)
        {
            try
            {
                if (topicId is "" or null) throw new InvalidIdException();

                if (!await CurrentUserIsTopicOwner(topicId)) throw new InvalidUserException(Info.AccessOtherUserTopic);

                var topic = await _topicHandler.GetTopicByIdAsync(topicId!);

                if (topic is null) throw new NullTopicException();

                var topicVm = _mapper.Map<Topic, TopicVm>(topic);

                return View(topicVm);
            }
            catch (InvalidIdException ex)
            {
                _logger.LogError(Info.TopicIdDontExist, ex);
            }
            catch (InvalidUserException ex)
            {
                _logger.LogError(Info.UnauthorizedTopicAccess, ex);
            }
            catch (NullTopicException ex)
            {
                _logger.LogError(Info.EmptyTopic, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(Info.GenericException, ex);
            }

            return RedirectToAction("Index", "Forum");
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates the specified topic vm.
        /// </summary>
        /// <param name="topicVm">The topic vm.</param>
        /// <returns></returns>
        /// <exception cref="Forum.Shared.Exceptions.ModelStateNotValidException"></exception>
        /// <exception cref="Forum.Shared.Exceptions.InvalidUserException"></exception>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TopicVm topicVm)
        {
            try
            {
                if (!ModelState.IsValid) throw new ModelStateNotValidException();

                var userId = GetUserId();

                if (userId is null) throw new InvalidUserException(Info.NoIdUser);

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

                TempData[Info.Success] = Info.TopicCreated;
            }
            catch (ModelStateNotValidException ex)
            {
                _logger.LogError(Info.ModelStateNotValid, ex);
                return View(topicVm);
            }
            catch (InvalidUserException ex)
            {
                _logger.LogError(Info.UnauthorizedTopicAccess, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(Info.GenericException, ex);
            }

            return RedirectToAction("Index", "Forum");
        }

        /// <summary>
        /// Edits the specified topic vm.
        /// </summary>
        /// <param name="topicVm">The topic vm.</param>
        /// <returns></returns>
        /// <exception cref="Forum.Shared.Exceptions.ModelStateNotValidException"></exception>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TopicVm topicVm)
        {
            try
            {
                if (!ModelState.IsValid) throw new ModelStateNotValidException();

                var topic = new Topic
                {
                    Id = topicVm.Id,
                    Title = topicVm.Title,
                    Description = topicVm.Description
                };

                await _topicHandler.UpdateTopicAsync(topic);
                await _topicHandler.SaveAllAsync();

                TempData[Info.Success] = Info.TopicEdited;
            }
            catch (ModelStateNotValidException ex)
            {
                _logger.LogError(Info.ModelStateNotValid, ex);
                return View(topicVm);
            }
            catch (Exception ex)
            {
                _logger.LogError(Info.GenericException, ex);
            }

            return RedirectToAction("Index", "Forum");
        }

        /// <summary>
        /// Deletes the specified topic identifier.
        /// </summary>
        /// <param name="topicId">The topic identifier.</param>
        /// <returns></returns>
        /// <exception cref="Forum.Shared.Exceptions.InvalidIdException"></exception>
        /// <exception cref="Forum.Shared.Exceptions.InvalidUserException"></exception>
        /// <exception cref="Forum.Shared.Exceptions.NullTopicException"></exception>
        public async Task<IActionResult> Delete([FromQuery] string topicId)
        {
            try
            {
                if (topicId is "" or null) throw new InvalidIdException();

                if (!await CurrentUserIsTopicOwner(topicId)) throw new InvalidUserException(Info.AccessOtherUserTopic);

                var topic = await _topicHandler.GetTopicByIdAsync(topicId!);

                if (topic is null) throw new NullTopicException();

                var topicVm = _mapper.Map<Topic, TopicVm>(topic!);

                return View(topicVm);
            }
            catch (InvalidIdException ex)
            {
                _logger.LogError(Info.TopicIdDontExist, ex);
            }
            catch (InvalidUserException ex)
            {
                _logger.LogError(Info.UnauthorizedTopicAccess, ex);
            }
            catch (NullTopicException ex)
            {
                _logger.LogError(Info.EmptyTopic, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(Info.GenericException, ex);
            }
            return RedirectToAction("Index", "Forum");
        }

        /// <summary>
        /// Deletes the post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="Forum.Shared.Exceptions.ModelStateNotValidException"></exception>
        /// <exception cref="Forum.Shared.Exceptions.InvalidIdException"></exception>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(string? id)
        {
            try
            {
                if (!ModelState.IsValid) throw new ModelStateNotValidException();

                if (id is "" or null) throw new InvalidIdException();

                await _topicHandler.DeleteTopicAsync(id);
                await _topicHandler.SaveAllAsync();

                TempData[Info.Success] = Info.TopicDeleted;
            }
            catch (ModelStateNotValidException ex)
            {
                _logger.LogError(Info.ModelStateNotValid, ex);
            }
            catch (InvalidIdException ex)
            {
                _logger.LogError(Info.TopicIdDontExist, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(Info.GenericException, ex);
            }

            return RedirectToAction("Index", "Forum");
        }

        /// <summary>
        /// Currents the user is topic owner.
        /// </summary>
        /// <param name="topicId">The topic identifier.</param>
        /// <returns></returns>
        private async Task<bool> CurrentUserIsTopicOwner(string? topicId)
        {
            return GetUserId() == (await _topicHandler.GetTopicByIdAsync(topicId!))?.ApplicationUserId;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <returns></returns>
        private string? GetUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
