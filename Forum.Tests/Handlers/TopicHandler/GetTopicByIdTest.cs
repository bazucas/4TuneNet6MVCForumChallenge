using AutoMapper;
using Forum.Infrastructure.Repository.Interfaces;
using Forum.Web.Helpers;
using Moq;
using NUnit.Framework;

namespace Forum.Tests.Handlers.TopicHandler
{
    [TestFixture]
    public class GetTopicByIdTest : BaseHandlerTest
    {
        private IMapper? _mapper;
        private Business.Handlers.TopicHandler? _handler;
        private Mock<IUnitOfWork>? _uOwMock;

        [SetUp]
        public void Setup()
        {
            var profile = new MappingProfiles();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);
            _uOwMock = new Mock<IUnitOfWork>();
            _handler = new Business.Handlers.TopicHandler(_uOwMock.Object);
        }

        [Test]
        public void Test_Method_Returns()
        {
            // arrange

            // act

            // assert

        }
    }
}