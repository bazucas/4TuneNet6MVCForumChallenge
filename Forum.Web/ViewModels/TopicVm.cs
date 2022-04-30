using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Forum.Web.ViewModels;

public class TopicVm
{
    [ValidateNever]
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    [ValidateNever]
    public DateTime CreationDate { get; set; }
}