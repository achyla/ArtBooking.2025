using Business.Model.Data;
using Business.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ArtBookingDbContext _dbContext;

    public UserController(ArtBookingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
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
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
        try
        {
            var users = _dbContext.Users
                .Include(u => u.ArtOrganization)
                .ToList();

            if (users == null || users.Count == 0)
                return NoContent();

            return Ok(users);
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
    public ActionResult<User> GetUser(int id)
    {
        try
        {
            var user = _dbContext.Users
                .Include(u => u.ArtOrganization)
                .FirstOrDefault(u => u.UserId == id);

            if (user == null)
                return Problem(
                    statusCode: 404,
                    title: "User not found",
                    detail: $"User with id:{id} cannot be found"
                );

            return Ok(user);
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
    public ActionResult<User> EditUser(int id, User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var existingUser = _dbContext.Users.Find(id);

            if (existingUser == null)
                return Problem(
                    statusCode: 404,
                    title: "User not found",
                    detail: $"User with id:{id} cannot be found"
                );

            existingUser.LoginName = user.LoginName;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.Email = user.Email;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Role = user.Role;
            existingUser.ArtOrganizationId = user.ArtOrganizationId;

            _dbContext.SaveChanges();

            return Ok(existingUser);
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
    public ActionResult<User> DeleteUser(int id)
    {
        try
        {
            var user = _dbContext.Users.Find(id);

            if (user == null)
                return Problem(
                    statusCode: 404,
                    title: "User not found",
                    detail: $"User with id:{id} cannot be found"
                );

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            return Ok(user);
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
