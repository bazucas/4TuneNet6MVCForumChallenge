using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Core.Models;

/// <summary>
/// Topic
/// </summary>
public class Topic
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    [Required]
    public string? Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [Required]
    public string? Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the creation date.
    /// </summary>
    /// <value>
    /// The creation date.
    /// </value>
    [Required]
    public DateTime CreationDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the application user identifier.
    /// </summary>
    /// <value>
    /// The application user identifier.
    /// </value>
    public string? ApplicationUserId { get; set; }
    /// <summary>
    /// Gets or sets the application user.
    /// </summary>
    /// <value>
    /// The application user.
    /// </value>
    [ForeignKey("ApplicationUserId")]
    [ValidateNever]
    public ApplicationUser? ApplicationUser { get; set; }
}