using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AppApi.Data;
using AppApi.Dto;
using AppApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AppApi.Controllers
{
    [Route(V)]
    public class CartsController : ControllerBase
    {
         private const string V = "api/carts";
         private ApiDbContext dbContext;
         

        public CartsController(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
         public async Task<IActionResult> Get() {
        return Ok(await dbContext.Carts.ToListAsync());
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> Getcarts(int userId) {
        var user =await dbContext.Carts.Where(x=>x.UserId==userId).ToListAsync();
            return Ok(user);
        }
         [HttpPost]
         [Route("[action]")]
         public IActionResult ProductDetails([FromBody]List<int>ids)  
         {
         List<Product>products= new List<Product>();
         foreach(var id in ids){
             products.Add(dbContext.Products.FirstOrDefault(p =>p.Id == id));
         }
         return Ok(products);
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public IActionResult dropdata([FromBody]int productId)
        {
        if(productId == 0)
        return BadRequest();
        var Cart = dbContext.Carts.FirstOrDefault(c=>c.ProductId == productId);
        dbContext.Carts.Remove(Cart);
        dbContext.SaveChanges();
        return Ok(Cart);
        }

        [HttpPost]
        public IActionResult Post ([FromBody]CartsDto CartsDto) 
        {
         var user = dbContext.Users.FirstOrDefault(us => us.id == CartsDto.UserId);
         var Product = dbContext.Products.FirstOrDefault(pr => pr.Id == CartsDto.ProductId);
         if(user ==null || Product == null) 
          return BadRequest();

         var cartToAdd = new Carts(){
             UserId = CartsDto.UserId,
             ProductId = CartsDto.ProductId,
             Amount = CartsDto.Amount
         };
         var pro = dbContext.Carts.Add(cartToAdd);
         var saverows = dbContext.SaveChanges();

         var addedcart =dbContext.Carts.FirstOrDefault(p => p.Id == pro.Entity.Id);

         dbContext.SaveChanges();
         return Ok(pro.Entity);
        }
    }
}