using System.ComponentModel.DataAnnotations;

namespace Business.Model.Entities;

public class PriceList
{
    public int PriceListId { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    // FK who created this PriceList
    public int ArtOrganizationId { get; set; }
    public virtual ArtOrganization ArtOrganization { get; set; } = null!;

    // Pozycje cennika
    public virtual ICollection<PriceEntry>? Entries { get; set; }
}
