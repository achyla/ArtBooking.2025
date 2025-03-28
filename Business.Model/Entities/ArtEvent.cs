using System.ComponentModel.DataAnnotations;
using Business.Model.Enums;

namespace Business.Model.Entities;

public class ArtEvent
{
    public int ArtEventId { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string Description { get; set; } = string.Empty;
    
    public EventType EventType { get; set; } // Enums >>> EventType.cs

    //Relacje

    public int ArtOrganizationId { get; set; } // FK
    public virtual ArtOrganization ArtOrganization { get; set; } = null!;//Navigation properties 1:1

    public int VenueId { get; set; } //FK
    public virtual Venue Venue { get; set; } = null!;//Navigation properties 1:1

    public int PriceListId { get; set; } //FK
    public virtual PriceList PriceList { get; set; } = null!;//Navigation properties 1:1

    public virtual ICollection<ScheduleItem>? ScheduleItems { get; set; } //Navigation properties 1:N

}