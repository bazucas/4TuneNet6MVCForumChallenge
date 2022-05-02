using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Claims;

namespace Forum.Tests.Controllers.BaseController;

/// <summary>
///  Abstract class to serve as a base for all controller tests
/// </summary>
public abstract class BaseControllerTests
{
    /// <summary>
    /// Tears down.
    /// </summary>
    [TearDown]
    protected void TearDown()
    {
        Mock.VerifyAll();
    }

    /// <summary>
    /// Setups the HTTP context.
    /// </summary>
    /// <returns></returns>
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
