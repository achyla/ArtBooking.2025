using System.ComponentModel.DataAnnotations;

namespace Business.Model.Entities;

public class ArtOrganization
{
    public int ArtOrganizationId { get; set; }
    
    [Required, StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(500, MinimumLength = 1)]
    public string Description { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(254)]
    public string Email { get; set; } = string.Empty;

    // Address

    public virtual ICollection<User>? Users { get; set; }
}