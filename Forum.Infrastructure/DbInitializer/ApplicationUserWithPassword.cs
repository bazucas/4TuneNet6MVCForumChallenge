using Forum.Core.Models;

namespace Forum.Infrastructure.DbInitializer
{
    /// <summary>
    /// ApplicationUserWithPassword class, inherits from ApplicationUser
    /// </summary>
    /// <seealso cref="Forum.Core.Models.ApplicationUser" />
    internal class ApplicationUserWithPassword : ApplicationUser
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string? Password { get; set; }
    }
}
