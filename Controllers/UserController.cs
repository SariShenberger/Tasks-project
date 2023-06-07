using Microsoft.AspNetCore.Mvc;
using tasks.Models;
using tasks.Services;
using tasks.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Tasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
         IUser UserService;

        public UserController(IUser UserService) {
            this.UserService=UserService;
        }
 
        [HttpPost]
        [Route("[action]")]
        public ActionResult<String> Login([FromBody] User User)
        {
            var dt = DateTime.Now;
            if (User.UserName != "Sari"
            || User.Password != $"S{dt.Year}#{dt.Day}!"){
                User.Admin=false;
            } else{User.Admin=true;}
            return new OkObjectResult(new{Token=TaskTokenService.WriteToken(UserService.login(User)),Admin=User.Admin});
        }
        [HttpGet]
        [Authorize(Policy = "Admin")]

        public IEnumerable<User> Get()
        {
            return UserService.GetAll();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult Delete (int id)
        {
            if (! UserService.Delete(id.ToString()))//
                return NotFound();
            return NoContent();            
        }
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public ActionResult Post( User user)
        {
            UserService.Add(user);
            return CreatedAtAction(nameof(Post), new { id = user.Password }, user);
        }
           

        [HttpPut("{password}")]
        [Authorize(Policy = "Admin")]
        public ActionResult Put(string password, User user)
        {
            UserService.Update(password, user);
            return CreatedAtAction(nameof(Post), new { id = user.Password }, user);
        }
           
    }}
 
    //     [HttpPost]
    //     [Route("[action]")]
    //     [Authorize(Policy = "Admin")]
    //     public IActionResult GenerateBadge([FromBody] Agent Agent)
    //     {
    //         var claims = new List<Claim>
    //         {
    //             new Claim("type", "Agent"),
    //             new Claim("ClearanceLevel", Agent.ClearanceLevel.ToString()),
    //         };

    //         var token = FbiTokenService.GetToken(claims);

    //         return new OkObjectResult(FbiTokenService.WriteToken(token));
    //     }
    // }



