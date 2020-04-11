using System.Collections.Generic;
using System.Threading.Tasks;
using AppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data
{
    public class ProductRepository : IProducts
    {
        private readonly ApiDbContext _context;
        public ProductRepository(ApiDbContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Product> GetProduct(int id)
        {
            var product =await _context.Products.Include(p =>p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _context.Products.Include(a=>a.Photos).ToListAsync();
            return products;
        }

        public async Task<bool> SaveAll()
        {
           return await _context.SaveChangesAsync() > 0;
        }
    }
}