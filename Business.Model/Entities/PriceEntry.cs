using System.ComponentModel.DataAnnotations;

namespace Business.Model.Entities;

public class PriceEntry
{
    public int PriceEntryId { get; set; }

    [Range(0, 100000)]
    public decimal Price { get; set; }

    [StringLength(50)]
    public string? Label { get; set; }

    public int AreaId { get; set; }
    public virtual Area Area { get; set; } = null!;

    public int PriceListId { get; set; }
    public virtual PriceList PriceList { get; set; } = null!;
}
