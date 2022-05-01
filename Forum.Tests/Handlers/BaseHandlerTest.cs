using Moq;
using NUnit.Framework;

namespace Forum.Tests
{
    [TestFixture]
    public abstract class BaseHandlerTest
    {
        [TearDown]
        protected void TearDown()
        {
            Mock.VerifyAll();
        }
    }
}