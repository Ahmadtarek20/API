using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppApi.Data;
using AppApi.Dto;
using AppApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AppApi.Controllers
{
    [Route(V)]
    public class EditUserController : ControllerBase
    {
        private const string V = "api/edituser";
         private ApiDbContext dbContext;

        public EditUserController(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

     [HttpGet("{id}")]
    public async Task<IActionResult> Getuser(int id) {
        var user =await dbContext.Users.FirstOrDefaultAsync(x=>x.id==id);
            return Ok(user);
    }

    [HttpPost]
    public IActionResult Update([FromBody]Users users)
    {
     if(!ModelState.IsValid)
     return BadRequest();

     var userToupdat = dbContext.Users.FirstOrDefault(c=>c.id == users.id);
     if(userToupdat == null){
    return BadRequest();
     }
     userToupdat.UserName = users.UserName;
     dbContext.SaveChanges();
     return Ok(users);

     }
    }
}