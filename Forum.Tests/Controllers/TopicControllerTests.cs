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
using System.Threading.Tasks;

namespace Forum.Tests.Controllers
{

    /// <summary>
    /// TopicController unit tests
    /// </summary>
    /// <seealso cref="Forum.Tests.Controllers.BaseController.BaseControllerTests" />
    [TestFixture]
    public class TopicControllerTests : BaseControllerTests
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper? _mapper;
        /// <summary>
        /// The logger
        /// </summary>
        private Mock<ILogger<TopicController>>? _logger;
        /// <summary>
        /// The topic handler
        /// </summary>
        private Mock<ITopicHandler>? _topicHandler;

        /// <summary>
        /// The controller
        /// </summary>
        private TopicController? _controller;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var profile = new MappingProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);
            _topicHandler = new Mock<ITopicHandler>();
            _logger = new Mock<ILogger<TopicController>>();
            _controller = new TopicController(_logger!.Object, _topicHandler.Object, _mapper!);
            _controller.ControllerContext.HttpContext = SetupHttpContext();
        }

        /// <summary>
        /// Tests the index get returns a view with a list of topic vm.
        /// </summary>
        [Test]
        public async Task Test_Index_Get_ReturnsAViewWithATopicVm()
        {
            // Arrange
            var topic = new Topic
            {
                Id = new Guid("ec3214ac-7194-42b6-b29a-a59ac7f31a99"),
                Description = "Test",
                Title = "Test",
                CreationDate = DateTime.Now,
                ApplicationUserId = "2a223ec4-1a61-48f2-b660-e0e6a9f53145",
                ApplicationUser = new ApplicationUser
                {
                    Id = "2a223ec4-1a61-48f2-b660-e0e6a9f53145"
                }
            };

            _topicHandler!.Setup(repo => repo.GetTopicByIdAsync(It.IsAny<string>())).ReturnsAsync(topic);
            var mappedTopics = _mapper!.Map<Topic, TopicVm>(topic);

            // Act
            var result = await _controller!.Index(Guid.NewGuid().ToString());

            // Assert
            var view = (ViewResult)result;
            view.Should().NotBeNull();
            view.Model.Should().BeOfType<TopicVm>();
            view.Model.Should().BeEquivalentTo(mappedTopics);
        }

        /// <summary>
        /// Tests the index get invalid or empty topic identifier throws a invalid identifier exception.
        /// </summary>
        [Test]
        public void Test_Index_Get_InvalidOrEmptyTopicIdThrowsAInvalidIdException()
        {
            // Act
            Action act = () => _controller!.Index(string.Empty).GetAwaiter().GetResult();

            // Assert
            act.Should().Throw<InvalidIdException>();
        }

        /// <summary>
        /// Tests the index get unauthorized topic access throws a invalid user exception.
        /// </summary>
        [Test]
        public void Test_Index_Get_UnauthorizedTopicAccessThrowsAInvalidUserException()
        {
            // Arrange
            var topic = new Topic
            {
                Id = new Guid("ec3214ac-7194-42b6-b29a-a59ac7f31a99"),
                Description = "Test",
                Title = "Test",
                CreationDate = DateTime.Now,
                ApplicationUserId = "aa223ec4-1a61-48f2-b660-e0e6a9f53145",
                ApplicationUser = new ApplicationUser
                {
                    Id = "aa223ec4-1a61-48f2-b660-e0e6a9f53145"
                }
            };

            _topicHandler!.Setup(repo => repo.GetTopicByIdAsync(It.IsAny<string>())).ReturnsAsync(topic);

            // Act
            Action act = () => _controller!.Index("ec3214ac-7194-42b6-b29a-a59ac7f31a99").GetAwaiter().GetResult();

            // Assert
            act.Should().Throw<InvalidUserException>();
        }

        /// <summary>
        /// Tests the index get empty topic list throws a null topic exception.
        /// </summary>
        [Test]
        public void Test_Index_Get_EmptyTopicListThrowsANullTopicException()
        {
            // Arrange
            var topic = new Topic
            {
                Id = new Guid("ec3214ac-7194-42b6-b29a-a59ac7f31a99"),
                Description = "Test",
                Title = "Test",
                CreationDate = DateTime.Now,
                ApplicationUserId = "2a223ec4-1a61-48f2-b660-e0e6a9f53145",
                ApplicationUser = new ApplicationUser
                {
                    Id = "2a223ec4-1a61-48f2-b660-e0e6a9f53145"
                }
            };

            _topicHandler!.SetupSequence(repo => repo.GetTopicByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(topic)
                .ReturnsAsync((Topic)null);

            // Act
            Action act = () => _controller!.Index("ec3214ac-7194-42b6-b29a-a59ac7f31a99").GetAwaiter().GetResult();

            // Assert
            act.Should().Throw<NullTopicException>();
        }

        /// <summary>
        /// Tests the index get topic by identifier asynchronous throws general exception and it will bubble up to controller.
        /// </summary>
        [Test]
        public void Test_Index_Get_GetTopicByIdAsyncThrowsGeneralExceptionAndItWillBubbleUpToController()
        {
            // Arrange
            _topicHandler!.Setup(repo => repo.GetTopicByIdAsync(It.IsAny<string>())).Throws<Exception>();

            // Act
            Action act = () => _controller!.Index(string.Empty).GetAwaiter().GetResult();

            // Assert
            act.Should().Throw<Exception>();
        }
    }
}
