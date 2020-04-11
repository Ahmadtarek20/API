using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppApi.Data;
using AppApi.Dto;
using AppApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace AppApi.Controllers
{
    [Route(V)]
    public class ProductsController : ControllerBase
    {
        private const string V = "api/product";
        private readonly IProducts _reboproducts;
        private readonly IMapper _mapper;
        private ApiDbContext dbContext;
        public ProductsController(IProducts reboproducts, IMapper mapper , ApiDbContext dbContext)
        {
            _mapper = mapper;
            _reboproducts = reboproducts;
            this.dbContext = dbContext;

        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _reboproducts.GetProducts();
             var productstoRetern = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productstoRetern);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _reboproducts.GetProduct(id);
            var producttoRetern = _mapper.Map<ProductsDetails>(product);
            return Ok(producttoRetern);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductDto product){
        if(!ModelState.IsValid) 
        return BadRequest();
        var category = dbContext.Categorys.FirstOrDefault(cat=> cat.Id == product.CategoryId);
        var productToAdd = new Product(){
            Name = product.Name,
            Price = product.Price,
            HasDiscount = product.HasDiscount,
            Category = category,
            DiscountAmount = product.DiscountAmount,
            CategoryId = product.CategoryId,
           
        };
        var prop = dbContext.Products.Add(productToAdd);
        var rowsEffected = dbContext.SaveChanges();
    
        var addedProduct = dbContext.Products.FirstOrDefault(p=>p.Id == prop.Entity.Id);      
        dbContext.SaveChanges();
        return Ok(prop.Entity);
        }
    } 

}