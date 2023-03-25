using Microsoft.AspNetCore.Mvc;
using hw1.Models;
using hw1.Interfaces;
using Microsoft.AspNetCore.Authorization;
using hw1.Services;
using System.IdentityModel.Tokens.Jwt;
// using System.IdentityModel.Tokens.Jwt;

namespace hw1.Controllers
{
    // [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "User")]

    public class TaskController : ControllerBase
    {
        private IConnect taskService;
        public TaskController(IConnect taskService)
        {
            this.taskService = taskService;
        }
        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Item> Get()
        {
            var token = Request.Headers.Authorization;
            return taskService.GetAll(GetTokenPassword(token));
        }
        [NonAction]
        [HttpGet("{id}")]
        [Route("[action]")]
        public ActionResult<Item> Get(int id)
        {
            var token = Request.Headers.Authorization;
            string password=GetTokenPassword(token);
            var i =  taskService.Get(password,id);
                if (i == null)
                    return NotFound();
                 return i;
        }


        [HttpPost]
        [Route("[action]")]
        public ActionResult Post(Item item)
        {
            taskService.Add(item);
            return CreatedAtAction(nameof(Post), new { id = item.Id }, item);
        }

        [HttpPut]
        [HttpPut("{id}")]
        [Route("[action]")]
        public ActionResult Put(int id, Item item)
        {
            if (!taskService.Update(id, item))
                return BadRequest();
            return NoContent();
        }
        [HttpDelete]
        [HttpDelete("{id}")]
        [Route("[action]")]
        public ActionResult Delete(int id)
        {
            if (!taskService.Delete(id))
                return NotFound();
            return NoContent();

        }

        private string GetTokenPassword(string idtoken)
        {
            var token = new JwtSecurityToken(jwtEncodedString: idtoken);
            string password = token.Claims.First(c => c.Type == "password").Value;
            return password.Split(" ")[1];
        }

    }
}
