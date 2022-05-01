namespace Forum.Web.Extensions;

/// <summary>
/// Extension method that receives <see cref="IServiceCollection"/> and adds the client variables configurations.
/// </summary>
public static class AppSessionsExtensions
{
    /// <summary>
    /// Adds the sessions services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddSessionsServices(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Identity/Account/Login";
            options.LogoutPath = "/Identity/Account/Logout";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        });

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(100);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        }); return services;
    }
}