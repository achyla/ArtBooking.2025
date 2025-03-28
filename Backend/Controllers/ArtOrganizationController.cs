using Business.Model.Data;
using Business.Model.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ArtOrganizationController : ControllerBase
{
    private readonly ArtBookingDbContext _dbContext;

    public ArtOrganizationController(ArtBookingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public ActionResult<ArtOrganization> CreateOrganization(ArtOrganization organization)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            _dbContext.Add(organization);
            _dbContext.SaveChanges();
        }
        catch (Exception exp)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occured",
                // just for debugging purposes
                detail: exp.Message
            );
        }

        return CreatedAtAction(nameof(GetOrganization), new { id = organization.ArtOrganizationId }, organization);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ArtOrganization>> GetAllOrganizations()
    {
        try{
            var organizations = _dbContext.ArtOrganizations.ToList();
            if(organizations == null) return NoContent();
            return Ok(organizations);
        }
        catch (Exception exp)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occured",
                // just for debugging purposes
                detail: exp.Message
            );
        }
    }

    [HttpGet("{id}")]
    public ActionResult<ArtOrganization> GetOrganization(int id)
    {
        try
        {
            var organization = _dbContext.ArtOrganizations.Find(id);

            if (organization == null) return Problem(
                statusCode: 404,
                title: "Organization cannot be found",
                detail: $"Organization with id:{id} cannot be found!"
            );
            return Ok(organization);
        }
        catch (Exception exp)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occured",
                // just for debugging purposes
                detail: exp.Message
            );
        }
    }




    [HttpPut("{id}")]
    public ActionResult<ArtOrganization> EditOrganization(int id, ArtOrganization organization)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {      
            var existingOrganization = _dbContext.ArtOrganizations.Find(id);

            if (existingOrganization == null) return Problem(
                statusCode: 404,
                title: "Organization cannot be found",
                detail: $"Organization with id:{id} cannot be found!"
            );
            
            existingOrganization.Name = organization.Name;
            existingOrganization.Description = organization.Description;
            existingOrganization.Email = organization.Email;

            _dbContext.SaveChanges();
            return Ok(existingOrganization);

        }
        catch (Exception exp)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occured",
                // just for debugging purposes
                detail: exp.Message
            );
        }
    }
    
    [HttpDelete("{id}")]
    public ActionResult<ArtOrganization> DeleteOrganization(int id)
    {
        try
        {
            var organization = _dbContext.ArtOrganizations.Find(id);
            if (organization == null) return Problem(
                    statusCode: 404,
                    title: "Organization cannot be found",
                    detail: $"Organization with id:{id} cannot be found!"
            );

            _dbContext.Remove(organization);
            _dbContext.SaveChanges();

            return Ok(organization);
        }
        catch (Exception exp)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occured",
                // just for debugging purposes
                detail: exp.Message
            );
        }

    }
    
}