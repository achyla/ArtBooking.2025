using System.ComponentModel.DataAnnotations;

namespace Business.Model.Entities;

public class Ticket
{
    public int TicketId { get; set; }

    public int ScheduleItemId { get; set; }
    public virtual ScheduleItem ScheduleItem { get; set; } = null!;

    public int SeatId { get; set; }
    public virtual Seat Seat { get; set; } = null!;

    public int PriceEntryId { get; set; }
    public virtual PriceEntry PriceEntry { get; set; } = null!;

    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
}
