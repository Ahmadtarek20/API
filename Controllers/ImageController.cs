using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AppApi.Data;
using AppApi.Dto;
using AppApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private ApiDbContext dbContext;
        public IHostingEnvironment hostinEnviroment;
        public ImageController (ApiDbContext dbContext,
            IHostingEnvironment hostinEnviroment) {

            this.hostinEnviroment = hostinEnviroment;
            this.dbContext = dbContext;
        }

         private string GetSavedImagePath(IFormFile image)
        {
            var uploadsFolder = Path.Combine(hostinEnviroment.WebRootPath, "images");
            var uniqueFileName = Guid.NewGuid() + "-" + image.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            image.CopyTo(new FileStream(filePath, FileMode.Create));

            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult PostInge ([FromForm] PhotosDetails photosDetails)
        {
            var product =  dbContext.Products.FirstOrDefault(c=>c.Id == photosDetails.ProductId);
           if (product == null)
               return BadRequest();

            var imagepath = "defult.png";
            if(photosDetails.Photo != null)
            {
                imagepath = GetSavedImagePath(photosDetails.Photo);
            }
            var Photo = new Photo
            {
                ProductId = product.Id,
                Url = imagepath,
            };
            var pro = dbContext.Photos.Add(Photo);
            dbContext.SaveChanges();
            return Ok(Photo);
        }
         [HttpGet]
        public async Task<IActionResult> Get() {
        return Ok(await dbContext.Photos.ToListAsync());
    }
    }
}