using Business.Model.Data;
using Business.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ArtBookingDbContext _dbContext;

    public TicketController(ArtBookingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public ActionResult<Ticket> CreateTicket(Ticket ticket)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            bool seatTaken = _dbContext.Tickets.Any(t =>
                t.ScheduleItemId == ticket.ScheduleItemId &&
                t.SeatId == ticket.SeatId
            );

            if (seatTaken)
                return Problem(
                    statusCode: 409,
                    title: "Seat already taken",
                    detail: "This seat is already reserved for the selected schedule item."
                );

            _dbContext.Tickets.Add(ticket);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.TicketId }, ticket);
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
    public ActionResult<Ticket> GetTicket(int id)
    {
        try
        {
            var ticket = _dbContext.Tickets
                .Include(t => t.ScheduleItem)
                .Include(t => t.Seat)
                .Include(t => t.PriceEntry)
                .FirstOrDefault(t => t.TicketId == id);

            if (ticket == null)
                return Problem(
                    statusCode: 404,
                    title: "Ticket not found",
                    detail: $"Ticket with id:{id} cannot be found"
                );

            return Ok(ticket);
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

    [HttpGet("byschedule/{scheduleItemId}")]
    public ActionResult<IEnumerable<Ticket>> GetTicketsForSchedule(int scheduleItemId)
    {
        try
        {
            var tickets = _dbContext.Tickets
                .Where(t => t.ScheduleItemId == scheduleItemId)
                .Include(t => t.Seat)
                .ToList();

            if (tickets == null || tickets.Count == 0)
                return NoContent();

            return Ok(tickets);
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
