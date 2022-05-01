using Forum.Core.Models;

namespace Forum.Infrastructure.DbInitializer
{
    internal class ApplicationUserWithPassword : ApplicationUser
    {
        public string? Password { get; set; }
    }
}
