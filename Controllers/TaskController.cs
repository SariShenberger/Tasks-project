using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using hw1.Models;

namespace hw1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase{
        [HttpGet]
        public  IEnumerable<Item> Get(){
            return TaskService.GetAll();
        }
        [HttpGet("{id}")]
        public ActionResult<Item> Get(int id)
        {
            var i = TaskService.Get(id);
            if (i == null)
                return NotFound();
             return i;
        }

        
        [HttpPost]
        public ActionResult Post(Item item){
            TaskService.Add(item);
            return CreatedAtAction(nameof(Post), new{id=item.Id},item);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Item item){
            if(! TaskService.Update(id, item))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id){
            if(! TaskService.Delete(id))
                return NotFound();
            return NoContent();
            
        }
        }
    }
