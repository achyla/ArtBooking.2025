using System.ComponentModel.DataAnnotations;

namespace Business.Model.Entities;

public class ScheduleItem
{
    public int ScheduleItemId { get; set; }

    [Required]
    public DateTime StartDateTime { get; set; }

    //FK
    public int ArtEventId { get; set; }
    public virtual ArtEvent ArtEvent { get; set; } = null!;

    //FK
    public int VenueId { get; set; }
    public virtual Venue Venue { get; set; } = null!;

    // Navigation – bilety (opcjonalnie, na przyszłość)
    public virtual ICollection<Ticket>? Tickets { get; set; }
}
