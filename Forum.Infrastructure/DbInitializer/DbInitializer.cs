using Forum.Core.Models;
using Forum.Infrastructure.Context;
using Forum.Shared.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Forum.Infrastructure.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ILogger<DbInitializer> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public DbInitializer(UserManager<IdentityUser> userManager, ILogger<DbInitializer> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

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

        private static async Task<IEnumerable<ApplicationUserWithPassword>> GetMockAppUsers()
        {
            var usersData = await File.ReadAllTextAsync("./SeedData/users.json");
            return JsonSerializer.Deserialize<List<ApplicationUserWithPassword>>(usersData) ?? new List<ApplicationUserWithPassword>();
        }

        private static async Task<IEnumerable<Topic>> GetMockTopics()
        {
            var topicsData = await File.ReadAllTextAsync("./SeedData/topics.json");
            return JsonSerializer.Deserialize<List<Topic>>(topicsData) ?? new List<Topic>();
        }
    }
}
