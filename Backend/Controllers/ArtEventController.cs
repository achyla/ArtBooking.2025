using Business.Application.DTOs.Events;
using Business.Application.Services.Events;
using Xtech.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ArtEventController : ControllerBase
{
    private readonly IArtEventService _artEventService;

    public ArtEventController(IArtEventService artEventService)
    {
        _artEventService = artEventService;
    }

    /// <summary>
    /// Creates a new art event
    /// </summary>
    /// <param name="eventDto">The event data to create</param>
    /// <param name="artOrganizationId">The ID of the organization associated with the event</param>
    /// <returns>The created art event</returns>
    [HttpPost]
    public ActionResult<ArtEventDto> CreateEvent(CreateArtEventDto eventDto, [FromQuery] int? artOrganizationId = null)
    {
        try
        {
            var createdEvent = _artEventService.CreateEvent(eventDto, artOrganizationId);
            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.ArtEventId }, createdEvent);
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

    /// <summary>
    /// Gets a specific art event by ID
    /// </summary>
    /// <param name="id">The ID of the event to retrieve</param>
    /// <returns>The requested art event</returns>
    [HttpGet("{id}")]
    public ActionResult<ArtEventDto> GetEvent(int id)
    {
        try
        {
            var artEvent = _artEventService.GetEvent(id);

            if (artEvent == null)
            {
                return Problem(
                    statusCode: 404,
                    title: "Event cannot be found",
                    detail: $"Event with id:{id} cannot be found!"
                );
            }

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

    /// <summary>
    /// List art events with pagination, filtering, and sorting
    /// </summary>
    /// <param name="listParams">List parameters including pagination, filters, and sorting</param>
    /// <returns>A paged list of art events</returns>
    [HttpGet("list")]
    public ActionResult<PagedList<ArtEventDto>> ListEvents([FromQuery] PagedListParams<ArtEventFilters> listParams)
    {
        try
        {
            var result = _artEventService.ListEvents(listParams);
            return Ok(result);
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

    /// <summary>
    /// Updates an existing art event
    /// </summary>
    /// <param name="id">The ID of the event to update</param>
    /// <param name="eventDto">The updated event data</param>
    /// <returns>The updated event</returns>
    [HttpPut("{id}")]
    public ActionResult<ArtEventDto> EditEvent(int id, CreateArtEventDto eventDto)
    {
        try
        {
            var updatedEvent = _artEventService.EditEvent(id, eventDto);

            if (updatedEvent == null)
            {
                return Problem(
                    statusCode: 404,
                    title: "Event cannot be found",
                    detail: $"Event with id:{id} cannot be found!"
                );
            }

            return Ok(updatedEvent);
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

    /// <summary>
    /// Deletes an art event
    /// </summary>
    /// <param name="id">The ID of the event to delete</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id}")]
    public ActionResult DeleteEvent(int id)
    {
        try
        {
            _artEventService.DeleteEvent(id);
            return NoContent();
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
