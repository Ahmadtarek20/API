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
    public class CategoriesController : ControllerBase
    {
        private const string V = "api/categories";
        private ApiDbContext dbContext;

        public CategoriesController(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    [HttpGet]
    public async Task<IActionResult> Get() {
        return Ok(await dbContext.Categorys.ToListAsync());
    }
    [HttpPost]
    public IActionResult Post([FromBody]CategoryDto category)
    {
    if(!ModelState.IsValid)
    return BadRequest();
    var categoryToAdd = new Category(){Name = category.Name};
    var cat = dbContext.Categorys.Add(categoryToAdd);
    dbContext.SaveChanges();
    return Ok(cat.Entity);
    }

    [HttpDelete]
    public IActionResult Delete(int id){
        if(id == 0)
        return BadRequest();
        var category = dbContext.Categorys.FirstOrDefault(c=>c.Id == id);
        dbContext.Categorys.Remove(category);
        dbContext.SaveChanges();
        return Ok(category);
    }

    [HttpPut]
    public IActionResult Update([FromBody]Category category)
{
     if(!ModelState.IsValid)
     return BadRequest();

     var catToUpdate = dbContext.Categorys.FirstOrDefault(c=>c.Id == category.Id);
     if(catToUpdate == null){
    return BadRequest();
     }
     catToUpdate.Name = category.Name;
     dbContext.SaveChanges();
     return Ok(category);

}

}
}