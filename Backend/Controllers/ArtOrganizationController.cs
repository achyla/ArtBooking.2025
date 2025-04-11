using Xtech.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using Business.Application.Services.Organizations;
using Business.Application.DTOs.Organizations;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ArtOrganizationController : ControllerBase
{
    private readonly IArtOrganizationService _artOrganizationService;

    public ArtOrganizationController(IArtOrganizationService artOrganizationService)
    {
        _artOrganizationService = artOrganizationService;
    }

    [HttpPost]
    public ActionResult<ArtOrganizationDto> CreateOrganization(CreateArtOrganizationDto organizationDto)
    {
        try
        {
            var createdOrganization = _artOrganizationService.CreateOrganization(organizationDto);
            return CreatedAtAction(nameof(GetOrganization), new { id = createdOrganization.ArtOrganizationId }, createdOrganization);
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
    public ActionResult<ArtOrganizationDto> GetOrganization(int id)
    {
        try
        {
            var organization = _artOrganizationService.GetOrganization(id);

            if (organization == null)
            {
                return Problem(
                    statusCode: 404,
                    title: "Organization cannot be found",
                    detail: $"Organization with id:{id} cannot be found!"
                );
            }

            return Ok(organization);
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
    /// List organizations with pagination, filtering, and sorting
    /// </summary>
    /// <param name="listParams">List parameters including pagination, filters, and sorting</param>
    /// <returns>A paged list of art organizations</returns>
    [HttpGet("list")]
    public ActionResult<PagedList<ArtOrganizationDto>> ListOrganizations([FromQuery] PagedListParams<ArtOrganizationFilters> listParams)
    {
        try
        {
            var result = _artOrganizationService.ListOrganizations(listParams);
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
    /// Updates an existing art organization
    /// </summary>
    /// <param name="id">The ID of the organization to update</param>
    /// <param name="organizationDto">The updated organization data</param>
    /// <returns>The updated organization</returns>
    [HttpPut("{id}")]
    public ActionResult<ArtOrganizationDto> EditOrganization(int id, CreateArtOrganizationDto organizationDto)
    {
        try
        {
            var updatedOrganization = _artOrganizationService.EditOrganization(id, organizationDto);

            if (updatedOrganization == null)
            {
                return Problem(
                    statusCode: 404,
                    title: "Organization cannot be found",
                    detail: $"Organization with id:{id} cannot be found!"
                );
            }

            return Ok(updatedOrganization);
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