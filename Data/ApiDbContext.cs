using AppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data
{
    public class ApiDbContext:DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options):base(options){}

         protected override void OnModelCreating(ModelBuilder modelBuilder){

            modelBuilder.Entity<Product>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(p=>p.CategoryId);

                modelBuilder.Entity<Product>().HasKey(p=>p.Id);

                modelBuilder.Entity<Product>().Property(p=>p.Name).HasMaxLength(50);

                modelBuilder.Entity<Carts>()
                .HasKey(bc => new { bc.ProductId, bc.UserId , bc.Id});

               modelBuilder.Entity<Carts>()
                .HasOne(bc => bc.Product)
                .WithMany(b => b.Carts)
                .HasForeignKey(bc => bc.ProductId);

               modelBuilder.Entity<Carts>()
                .HasOne(bc => bc.Users)
                .WithMany(c => c.Carts)
                .HasForeignKey(bc => bc.UserId);

        }
        public DbSet<Users> Users {get; set;}
        public DbSet<Category> Categorys {get; set;}
        public DbSet<Product> Products { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Carts> Carts { get; set; }


    }
}