using Forum.Core.Models;
using Forum.Infrastructure.Context;
using Forum.Shared.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Forum.Infrastructure.DbInitializer
{
    /// <summary>
    /// DbInitializer implements <see cref="IDbInitializer"/>
    /// </summary>
    /// <seealso cref="Forum.Infrastructure.DbInitializer.IDbInitializer" />
    public class DbInitializer : IDbInitializer
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<DbInitializer> _logger;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbInitializer"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="logger">The logger.</param>
        public DbInitializer(UserManager<IdentityUser> userManager, ILogger<DbInitializer> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// Initializes the specified database.
        /// </summary>
        /// <param name="db">The database.</param>
        public async Task Initialize(ApplicationDbContext? db)
        {
            try
            {
                if ((await db?.Database.GetPendingMigrationsAsync()!).Any())
                {
                    await db.Database.MigrateAsync();
                }

                var appUsers = await GetMockAppUsers();
                foreach (var user in appUsers)
                {
                    if (await _userManager.FindByIdAsync(user.Id) is null)
                        await _userManager.CreateAsync(new ApplicationUser
                        {
                            Id = user.Id,
                            UserName = user.Email,
                            Email = user.Email,
                            Name = user.Name,
                            PhoneNumber = user.PhoneNumber,
                        }, user.Password);
                }

                var topics = await GetMockTopics();
                foreach (var topic in topics)
                {
                    if (await db.Topics!.FindAsync(topic.Id) is not null) continue;
                    db.Topics?.Add(topic);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(Info.MigrationsPending, ex);
            }
        }

        /// <summary>
        /// Gets the mock application users.
        /// </summary>
        /// <returns>A List of <see cref="IEnumerable{ApplicationUserWithPassword}"/> T = <see cref="ApplicationUserWithPassword"/></returns>
        private static async Task<IEnumerable<ApplicationUserWithPassword>> GetMockAppUsers()
        {
            var usersData = await File.ReadAllTextAsync("./SeedData/users.json");
            return JsonSerializer.Deserialize<List<ApplicationUserWithPassword>>(usersData) ?? new List<ApplicationUserWithPassword>();
        }

        /// <summary>
        /// Gets the mock topics.
        /// </summary>
        /// <returns>A List of <see cref="IEnumerable{Topic}"/> T = <see cref="Topic"/></returns>
        private static async Task<IEnumerable<Topic>> GetMockTopics()
        {
            var topicsData = await File.ReadAllTextAsync("./SeedData/topics.json");
            return JsonSerializer.Deserialize<List<Topic>>(topicsData) ?? new List<Topic>();
        }
    }
}
