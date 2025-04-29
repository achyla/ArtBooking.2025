using Business.Application.DTOs.Venues;
using Business.Application.Services.Venues;
using Business.Model.Entities.Venues;
using Xtech.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VenueController : ControllerBase
    {
        private readonly IVenueService _venueService;

        public VenueController(IVenueService venueService)
        {
            _venueService = venueService;
        }

        /// <summary>
        /// Creates a new venue
        /// </summary>
        /// <param name="venueDto">The venue data to create</param>
        /// <param name="artOrganizationId">The ID of the organization associated with the venue</param>
        /// <returns>The created venue</returns>
        [HttpPost]
        public ActionResult<VenueDto> CreateVenue(CreateVenueDto venueDto, [FromQuery] int? artOrganizationId = null)
        {
            try
            {
                var createdVenue = _venueService.CreateVenue(venueDto, artOrganizationId);
                return CreatedAtAction(nameof(GetVenue), new { id = createdVenue.VenueId }, createdVenue);
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
        /// Gets a specific venue by ID
        /// </summary>
        /// <param name="id">The ID of the venue to retrieve</param>
        /// <returns>The requested venue</returns>
        [HttpGet("{id}")]
        public ActionResult<VenueDto> GetVenue(int id)
        {
            try
            {
                var venue = _venueService.GetVenue(id);

                if (venue == null)
                {
                    return Problem(
                        statusCode: 404,
                        title: "Venue cannot be found",
                        detail: $"Venue with id:{id} cannot be found!"
                    );
                }

                return Ok(venue);
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
        /// List venues with pagination, filtering, and sorting
        /// </summary>
        /// <param name="listParams">List parameters including pagination, filters, and sorting</param>
        /// <returns>A paged list of venues</returns>
        [HttpGet("list")]
        public ActionResult<PagedList<VenueDto>> ListVenues([FromQuery] PagedListParams<VenueFilters> listParams)
        {
            try
            {
                var result = _venueService.ListVenues(listParams);
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
        /// Updates an existing venue
        /// </summary>
        /// <param name="id">The ID of the venue to update</param>
        /// <param name="venueDto">The updated venue data</param>
        /// <returns>The updated venue</returns>
        [HttpPut("{id}")]
        public ActionResult<VenueDto> EditVenue(int id, CreateVenueDto venueDto)
        {
            try
            {
                var updatedVenue = _venueService.EditVenue(id, venueDto);

                if (updatedVenue == null)
                {
                    return Problem(
                        statusCode: 404,
                        title: "Venue cannot be found",
                        detail: $"Venue with id:{id} cannot be found!"
                    );
                }

                return Ok(updatedVenue);
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
        /// Deletes a venue
        /// </summary>
        /// <param name="id">The ID of the venue to delete</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteVenue(int id)
        {
            try
            {
                _venueService.DeleteVenue(id);
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
}