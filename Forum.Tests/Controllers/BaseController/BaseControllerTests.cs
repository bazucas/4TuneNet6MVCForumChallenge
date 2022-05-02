using Moq;
using NUnit.Framework;

namespace Forum.Tests.Controllers.BaseController;

public abstract class BaseControllerTests
{
    protected BaseControllerTests() { }

    [TearDown]
    protected void TearDown()
    {
        Mock.VerifyAll();
    }

    //protected HttpContext SetupHttpContext()
    //{
    //    var context = new DefaultHttpContext();
    //    context.Request.Headers["Authorization"] = "Bearer " + valid_token;

    //    var token = new JwtSecurityTokenHandler().ReadJwtToken(valid_token);

    //    context.User = new ClaimsPrincipal(new ClaimsIdentity(token.Claims));

    //    return context;
    //}
}