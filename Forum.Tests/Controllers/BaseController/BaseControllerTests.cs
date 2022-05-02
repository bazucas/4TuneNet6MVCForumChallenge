using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Claims;

namespace Forum.Tests.Controllers.BaseController;

public abstract class BaseControllerTests
{
    [TearDown]
    protected void TearDown()
    {
        Mock.VerifyAll();
    }

    protected HttpContext SetupHttpContext()
    {
        var context = new DefaultHttpContext();
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, "John Doe"),
            new(ClaimTypes.NameIdentifier, "2a223ec4-1a61-48f2-b660-e0e6a9f53145")
        };
        context.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
        return context;
    }
}
