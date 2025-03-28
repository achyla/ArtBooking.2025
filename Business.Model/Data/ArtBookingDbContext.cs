using Business.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Model.Data;

public class ArtBookingDbContext : DbContext
{
    public ArtBookingDbContext(DbContextOptions<ArtBookingDbContext> options) : base(options)
    {

    }

    public DbSet<ArtOrganization> ArtOrganizations { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Venue> Venues { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<Seat> Seats { get; set; }

    public DbSet<ArtEvent> ArtEvents { get; set; }
    public DbSet<ScheduleItem> ScheduleItems { get; set; }

    public DbSet<PriceList> PriceLists { get; set; }
    public DbSet<PriceEntry> PriceEntries { get; set; }

    public DbSet<Ticket> Tickets { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ArtOrganization>().HasKey(x => x.ArtOrganizationId);
        modelBuilder.Entity<User>().HasKey(x => x.UserId);
        modelBuilder.Entity<Venue>().HasKey(x => x.VenueId);
        modelBuilder.Entity<Area>().HasKey(x => x.AreaId);
        modelBuilder.Entity<Seat>().HasKey(x => x.SeatId);
        modelBuilder.Entity<ArtEvent>().HasKey(x => x.ArtEventId);
        modelBuilder.Entity<ScheduleItem>().HasKey(x => x.ScheduleItemId);
        modelBuilder.Entity<PriceList>().HasKey(x => x.PriceListId);
        modelBuilder.Entity<PriceEntry>().HasKey(x => x.PriceEntryId);
        modelBuilder.Entity<Ticket>().HasKey(x => x.TicketId);
    }
}