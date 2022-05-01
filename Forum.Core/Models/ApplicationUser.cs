using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Forum.Core.Models
{
    /// <summary>
    /// ApplicationUser class, inherits from <see cref="IdentityUser"/>
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUser" />
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [Display(Name = "UserName")]
        public string? Name { get; set; }
    }
}
