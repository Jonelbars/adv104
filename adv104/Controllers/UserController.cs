using adv104.Data;
using adv104.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace adv104.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext appDbContext;


        public UserController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        //Create
        [HttpPost]

        public async Task<ActionResult<List<User>>> AddUser(User newUser)
        {
            if(newUser != null) 
            {
                appDbContext.Users.Add(newUser);
                await appDbContext.SaveChangesAsync();
                return Ok(await appDbContext.Users.ToListAsync());
            }
            return BadRequest("Object instance not set");
        }

        //Read All Users
        [HttpGet]

        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await appDbContext.Users.ToListAsync();
            return Ok(users);
        }

        //Read Single User
        [HttpGet("{id:int}")]

        public async Task<ActionResult<List<User>>> GetUsers(int id)
        {
            var user = await appDbContext.Users.FirstOrDefaultAsync(e => e.Id == id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("User not found");
        }

        //Update User
        [HttpPut]

        public async Task<ActionResult<User>> UpdateUser(User updatedUser)
        {
            if(updatedUser != null)
            {
                var user = await appDbContext.Users.FirstOrDefaultAsync(e => e.Id == updatedUser.Id);
                user!.Firstname = updatedUser.Firstname;
                user.Lastname = updatedUser.Lastname;
                user.Email = updatedUser.Email;
                user.Gender = updatedUser.Gender;
                await appDbContext.SaveChangesAsync();
                return Ok(user);
            }
            return BadRequest("User not found");
        }

        //Delete
        [HttpDelete]

        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var user = await appDbContext.Users.FirstOrDefaultAsync(e => e.Id ==id);
            if (user != null)
            {
                appDbContext.Users.Remove(user);
                await appDbContext.SaveChangesAsync();
                return Ok(await appDbContext.Users.ToListAsync());
            }
            return NotFound();
        }
    }
}
