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
                if (topicId is "" or null) throw new InvalidIdException();

                if (!await CurrentUserIsTopicOwner(topicId)) throw new InvalidUserException(Info.AccessOtherUserTopic);

                var topic = await _topicHandler.GetTopicByIdAsync(topicId!);

                if (topic is null) throw new NullTopicException();

                var topicVm = _mapper.Map<Topic, TopicVm>(topic);

                return View(topicVm);
            }
            catch (InvalidIdException ex)
            {
                TempData[Info.Error] = Info.InvalidTopicId;
                _logger.LogError(Info.TopicIdDontExist, ex);
            }
            catch (InvalidUserException ex)
            {
                TempData[Info.Error] = Info.InvalidUser;
                _logger.LogError(Info.UnauthorizedTopicAccess, ex);
            }
            catch (NullTopicException ex)
            {
                TempData[Info.Error] = Info.NoTopic;
                _logger.LogError(Info.EmptyTopic, ex);
            }
            catch (Exception ex)
            {
                TempData[Info.Error] = Info.Exception;
                _logger.LogError(Info.GenericException, ex);
            }

            return RedirectToAction("Index", "Forum");
        }

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
                TempData[Info.Error] = Info.InvalidForm;
                _logger.LogError(Info.ModelStateNotValid, ex);
                return View(topicVm);
            }
            catch (InvalidUserException ex)
            {
                TempData[Info.Error] = Info.InvalidUser;
                _logger.LogError(Info.UnauthorizedTopicAccess, ex);
            }
            catch (Exception ex)
            {
                TempData[Info.Error] = Info.Exception;
                _logger.LogError(Info.GenericException, ex);
            }

            return RedirectToAction("Index", "Forum");
        }

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
                TempData[Info.Error] = Info.InvalidForm;
                _logger.LogError(Info.ModelStateNotValid, ex);
                return View(topicVm);
            }
            catch (Exception ex)
            {
                TempData[Info.Error] = Info.Exception;
                _logger.LogError(Info.GenericException, ex);
            }

            return RedirectToAction("Index", "Forum");
        }

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
                TempData[Info.Error] = Info.InvalidTopicId;
                _logger.LogError(Info.TopicIdDontExist, ex);
            }
            catch (InvalidUserException ex)
            {
                TempData[Info.Error] = Info.InvalidUser;
                _logger.LogError(Info.UnauthorizedTopicAccess, ex);
            }
            catch (NullTopicException ex)
            {
                TempData[Info.Error] = Info.NoTopic;
                _logger.LogError(Info.EmptyTopic, ex);
            }
            catch (Exception ex)
            {
                TempData[Info.Error] = Info.Exception;
                _logger.LogError(Info.GenericException, ex);
            }
            return RedirectToAction("Index", "Forum");
        }

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
                TempData[Info.Error] = Info.InvalidForm;
                _logger.LogError(Info.ModelStateNotValid, ex);
            }
            catch (InvalidIdException ex)
            {
                TempData[Info.Error] = Info.InvalidTopicId;
                _logger.LogError(Info.TopicIdDontExist, ex);
            }
            catch (Exception ex)
            {
                TempData[Info.Error] = Info.Exception;
                _logger.LogError(Info.GenericException, ex);
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
