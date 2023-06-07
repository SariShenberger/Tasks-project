using Microsoft.AspNetCore.Mvc;
using tasks.Interfaces;
using Microsoft.AspNetCore.Authorization;
using tasks.Models;
using System.IdentityModel.Tokens.Jwt;

namespace tasks.Controllers;
[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private IConnect TaskService;
    public TaskController(IConnect taskService)
    {
        this.TaskService = taskService;
    }
    [HttpGet]
    [Authorize(Policy = "User")]
    public IEnumerable<Item> Get()
    {
        string token = Request.Headers.Authorization;
        return TaskService.GetAll(GetTokenPassword(idtoken: token));
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult<Item> Get(int id)
    {
        var p = TaskService.Get(id);
        if (p == null)
            return NotFound();
        return p;
    }

    [HttpPost]
    [Authorize(Policy = "User")]
    public ActionResult Post(Item task)
    {

        string tokenStr = Request.Headers.Authorization;
        string newToken = tokenStr.Split(' ')[1];
        var token = new JwtSecurityToken(jwtEncodedString: newToken);
        string id = token.Claims.First(c => c.Type == "id").Value;
        task.Id = int.Parse(id);
        TaskService.Add(task);
        return CreatedAtAction(nameof(Post), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult Put(int id, Item task)
    {
        if (!TaskService.Update(id, task))
            return BadRequest();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult Delete(int id)
    {
        if (!TaskService.Delete(id))
            return NotFound();
        return NoContent();
    }

    private string GetTokenPassword(string idtoken)
    {
        // var token = new JwtSecurityToken(jwtEncodedString: idtoken);
        // string tokenStr=Request.Headers.Authorization;
        string newToken=idtoken.Split(' ')[1];
        var token = new JwtSecurityToken(jwtEncodedString: newToken);
        string password = token.Claims.First(c => c.Type == "password").Value;
        return password;
    }


}