using Forum.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Context
{
    /// <summary>
    /// ApplicationDbContext class, inherits from IdentityDbContext
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext" />
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the topics.
        /// </summary>
        /// <value>
        /// The topics.
        /// </value>
        public DbSet<Topic>? Topics { get; set; }
        /// <summary>
        /// Gets or sets the application users.
        /// </summary>
        /// <value>
        /// The application users.
        /// </value>
        public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
    }
}
