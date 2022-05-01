using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Forum.Web.ViewModels;

/// <summary>
/// Topic View Model
/// </summary>
public class TopicVm
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    [ValidateNever]
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    [Required]
    public string Title { get; set; }
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [Required]
    public string Description { get; set; }
    /// <summary>
    /// Gets or sets the creation date.
    /// </summary>
    /// <value>
    /// The creation date.
    /// </value>
    [ValidateNever]
    public DateTime CreationDate { get; set; }
    /// <summary>
    /// Gets or sets the application user identifier.
    /// </summary>
    /// <value>
    /// The application user identifier.
    /// </value>
    public string? AppUserId { get; set; }
    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    /// <value>
    /// The name of the user.
    /// </value>
    public string? UserName { get; set; }
}