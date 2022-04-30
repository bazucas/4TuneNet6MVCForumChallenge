using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Forum.Web.ViewModels;

public class TopicVm
{
    [ValidateNever]
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [ValidateNever]
    public DateTime CreationDate { get; set; }
    public string? AppUserId { get; set; }
    public string? UserName { get; set; }
}