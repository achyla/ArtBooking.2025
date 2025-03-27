using System.ComponentModel.DataAnnotations;

namespace Business.Model.Entities;

public class ArtOrganization
{
    public int ArtOrganizationId { get; set; }
    
    [Required, StringLength(100, MinimumLength = 1)]
    public string Name { get; set; }

    [Required, StringLength(500, MinimumLength = 1)]
    public string Description { get; set; }

    [Required, EmailAddress, StringLength(254)]
    public string Email { get; set; }

    // Address

    public virtual ICollection<User>? Users { get; set; }
}