using System.ComponentModel.DataAnnotations;

namespace Business.Model.Entities;

public class Area
{
    public int AreaId { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public int VenueId { get; set; }
    public virtual Venue Venue { get; set; } = null!;

    public virtual ICollection<Seat>? Seats { get; set; }
    public virtual ICollection<PriceEntry>? PriceEntries { get; set; }
}
