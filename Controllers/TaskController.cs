using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using hw1.Models;
using hw1.Interfaces;

namespace hw1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class TaskController : ControllerBase{
        private IConnect teskService;
        public TaskController(IConnect teskService)
        {
            this.teskService = teskService;
        }
        [HttpGet]
        public  IEnumerable<Item> Get(){
            return  teskService.GetAll();
        }
        [HttpGet("{id}")]
        public ActionResult<Item> Get(int id)
        {
            var i =  teskService.Get(id);
            if (i == null)
                return NotFound();
             return i;
        }

        
        [HttpPost]
        public ActionResult Post(Item item){
             teskService.Add(item);
            return CreatedAtAction(nameof(Post), new{id=item.Id},item);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Item item){
            if(!  teskService.Update(id, item))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id){
            if(!  teskService.Delete(id))
                return NotFound();
            return NoContent();
            
        }
        }
    }
