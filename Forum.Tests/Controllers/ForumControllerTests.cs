using AutoMapper;
using FluentAssertions;
using Forum.Business.Handlers.Interfaces;
using Forum.Core.Models;
using Forum.Shared.Exceptions;
using Forum.Tests.Controllers.BaseController;
using Forum.Web.Areas.Forum.Controllers;
using Forum.Web.Helpers;
using Forum.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Tests.Controllers
{
    /// <summary>
    /// ForumController unit tests
    /// </summary>
    /// <seealso cref="Forum.Tests.Controllers.BaseController.BaseControllerTests" />
    [TestFixture]
    internal class ForumControllerTests : BaseControllerTests
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper? _mapper;
        /// <summary>
        /// The logger
        /// </summary>
        private Mock<ILogger<ForumController>>? _logger;
        /// <summary>
        /// The forum handler
        /// </summary>
        private Mock<IForumHandler>? _forumHandler;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var profile = new MappingProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);
            _forumHandler = new Mock<IForumHandler>();
            _logger = new Mock<ILogger<ForumController>>();
        }

        /// <summary>
        /// Tests the index get returns a view with a list of topic vm.
        /// </summary>
        [Test]
        public async Task Test_Index_Get_ReturnsAViewWithAListOfTopicVm()
        {
            // Arrange
            var topicsList = new List<Topic>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Description = "Test",
                    Title = "Test",
                    CreationDate = DateTime.Now,
                    ApplicationUserId = Guid.NewGuid().ToString(),
                    ApplicationUser = new ApplicationUser()
                }
            };

            _forumHandler!.Setup(repo => repo.GetAllTopicsWithUserInfoAsync()).ReturnsAsync(topicsList);
            var controller = new ForumController(_logger!.Object, _forumHandler.Object, _mapper!);
            var mappedTopics = _mapper!.Map<List<Topic>, List<TopicVm>>(topicsList);

            // Act
            var result = await controller.Index();

            // Assert
            var view = (ViewResult)result;
            view.Should().NotBeNull();
            view.Model.Should().BeOfType<List<TopicVm>>();
            view.Model.Should().BeEquivalentTo(mappedTopics);
        }

        /// <summary>
        /// Tests the index get empty topic list throws a empty topic list exception.
        /// </summary>
        [Test]
        public void Test_Index_Get_EmptyTopicListThrowsAEmptyTopicListException()
        {
            // Arrange
            var topicsList = new List<Topic>();
            _forumHandler!.Setup(repo => repo.GetAllTopicsWithUserInfoAsync()).ReturnsAsync(topicsList);
            var controller = new ForumController(_logger!.Object, _forumHandler.Object, _mapper!);

            // Act
            Action act = () => controller.Index().GetAwaiter().GetResult();

            // Assert
            act.Should().Throw<EmptyTopicListException>();
        }


        /// <summary>
        /// Tests the index get empty topic list throws a generic exception.
        /// </summary>
        [Test]
        public void Test_Index_Get_EmptyTopicListThrowsAGenericException()
        {
            // Arrange
            _forumHandler!.Setup(repo => repo.GetAllTopicsWithUserInfoAsync()).Throws<Exception>();
            var controller = new ForumController(_logger!.Object, _forumHandler.Object, _mapper!);

            // Act
            Action act = () => controller.Index().GetAwaiter().GetResult();

            // Assert
            act.Should().Throw<Exception>();
        }

        // TODO: Continue with the rest of the unit tests
    }
}
