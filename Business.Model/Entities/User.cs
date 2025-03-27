using System.ComponentModel.DataAnnotations;
using Business.Model.Enums;

namespace Business.Model.Entities;

public class User
{
    public int UserId { get; set; }
    
    [Required, StringLength(50, MinimumLength = 3)]
    public string LoginName { get; set; }

    [Required] // zakładam, że hash ma maksymalną długość
    public string PasswordHash { get; set; }

    [Required, EmailAddress, StringLength(254)]
    public string Email { get; set; }

    [Required, StringLength(50, MinimumLength = 1)]
    public string FirstName { get; set; }

    [Required, StringLength(50, MinimumLength = 1)]
    public string LastName { get; set; }
    
    public UserRole? Role { get; set; }
    public int? ArtOrganizationId { get; set; }
    public virtual ArtOrganization? ArtOrganization { get; set; }
}
