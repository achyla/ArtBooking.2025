using System.ComponentModel.DataAnnotations;

namespace Business.Model.Entities;

public class Venue
{
    public int VenueId { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string Address { get; set; } = string.Empty;

    public int Capacity { get; set; }

    // FK if Venue has its own Pricelist
    // In ArtEvent Controller logic will be impletemented if PriceList is null
    
    public int? PriceListId { get; set; }
    public virtual PriceList? PriceList { get; set; }

    public virtual ICollection<ScheduleItem>? ScheduleItems { get; set; }
    public virtual ICollection<Area>? Areas { get; set; }
}