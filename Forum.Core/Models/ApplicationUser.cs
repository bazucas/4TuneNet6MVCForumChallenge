using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Forum.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? Name { get; set; }
    }
}
