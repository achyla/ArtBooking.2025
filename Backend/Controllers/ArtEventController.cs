using Business.Model.Data;
using Business.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ArtEventController : ControllerBase
{
    private readonly ArtBookingDbContext _dbContext;

    public ArtEventController(ArtBookingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public ActionResult<ArtEvent> CreateArtEvent(ArtEvent artEvent)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            _dbContext.ArtEvents.Add(artEvent);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetArtEvent), new { id = artEvent.ArtEventId }, artEvent);
        }
        catch (Exception exp)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occurred",
                detail: exp.Message
            );
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<ArtEvent>> GetAllArtEvents()
    {
        try
        {
            var events = _dbContext.ArtEvents
                .Include(e => e.ArtOrganization)
                .Include(e => e.Venue)
                .Include(e => e.PriceList)
                .ToList();

            if (events == null || events.Count == 0)
                return NoContent();

            return Ok(events);
        }
        catch (Exception exp)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occurred",
                detail: exp.Message
            );
        }
    }

    [HttpGet("{id}")]
    public ActionResult<ArtEvent> GetArtEvent(int id)
    {
        try
        {
            var artEvent = _dbContext.ArtEvents
                .Include(e => e.ArtOrganization)
                .Include(e => e.Venue)
                .Include(e => e.PriceList)
                .FirstOrDefault(e => e.ArtEventId == id);

            if (artEvent == null)
                return Problem(
                    statusCode: 404,
                    title: "Event not found",
                    detail: $"Event with id:{id} cannot be found"
                );

            return Ok(artEvent);
        }
        catch (Exception exp)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occurred",
                detail: exp.Message
            );
        }
    }

    [HttpPut("{id}")]
    public ActionResult<ArtEvent> EditArtEvent(int id, ArtEvent updated)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var existing = _dbContext.ArtEvents.Find(id);
            if (existing == null)
                return Problem(
                    statusCode: 404,
                    title: "Event not found",
                    detail: $"Event with id:{id} cannot be found"
                );

            existing.Name = updated.Name;
            existing.Description = updated.Description;
            existing.EventType = updated.EventType;
            existing.ArtOrganizationId = updated.ArtOrganizationId;
            existing.VenueId = updated.VenueId;
            existing.PriceListId = updated.PriceListId;

            _dbContext.SaveChanges();

            return Ok(existing);
        }
        catch (Exception exp)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occurred",
                detail: exp.Message
            );
        }
    }

    [HttpDelete("{id}")]
    public ActionResult<ArtEvent> DeleteArtEvent(int id)
    {
        try
        {
            var ev = _dbContext.ArtEvents.Find(id);
            if (ev == null)
                return Problem(
                    statusCode: 404,
                    title: "Event not found",
                    detail: $"Event with id:{id} cannot be found"
                );

            _dbContext.ArtEvents.Remove(ev);
            _dbContext.SaveChanges();

            return Ok(ev);
        }
        catch (Exception exp)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occurred",
                detail: exp.Message
            );
        }
    }
}
