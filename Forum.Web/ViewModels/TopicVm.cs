using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;

namespace Forum.Web.ViewModels;

public class TopicVm
{
    [ValidateNever]
    public Guid Id { get; set; }
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? Description { get; set; }

    [ValidateNever]
    public DateTime CreationDate { get; set; }
}