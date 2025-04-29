namespace Business.Application.DTOs.Venues
{
    public class VenueDto
    {
        public int VenueId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? State { get; set; }
        public string Country { get; set; } = null!;
        public string? PostalCode { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Website { get; set; }
        public int? Capacity { get; set; }
        public string? ImageUrl { get; set; }
        public int ArtOrganizationId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}