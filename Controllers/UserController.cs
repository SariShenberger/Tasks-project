using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using hw1.Models;
using hw1.Services;
using hw1.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace hw1.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
       private IUser UserService;
        public UserController(IUser UserService)
        {
            this.UserService = UserService;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<String> Login([FromBody] User user)
        {
            var token = UserService.login(user);
            if (token == null)
                return NotFound();
            return new OkObjectResult(TaskTokenService.WriteToken(token));
        }


        [HttpPost]
        [Route("[action]")]
        [Authorize(Policy = "Admin")]
        public IActionResult GenerateBadge([FromBody] User user)
        {
            user.Admin=false;
            var token = UserService.GenerateBadge(user);
            return new OkObjectResult(TaskTokenService.WriteToken(token));
        }
        [HttpGet]
        [Route("[action]")]
        [Authorize(Policy = "Admin")]
        public IEnumerable<User> Get(){
            return UserService.GetAll();
        }
        [HttpGet]
        [HttpGet("{password}")]
        [Route("[action]")]
        [Authorize(Policy = "Admin")]
        public User Get(string password){
            return UserService.Get(password);
        }
    }



