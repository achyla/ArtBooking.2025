using System.ComponentModel.DataAnnotations;

namespace Business.Model.Entities;

public class Seat
{
    public int SeatId { get; set; }

    [Required]
    public string Row { get; set; } = string.Empty;

    [Required]
    public int Number { get; set; }

    public int AreaId { get; set; }
    public virtual Area Area { get; set; } = null!;
}