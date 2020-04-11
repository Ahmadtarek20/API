using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace AppApi.Models
{
    public class Product
    {
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public bool HasDiscount { get; set; }
    public double DiscountAmount { get; set; }
    public virtual Category Category { get; set; }
    public int CategoryId {get;set;}
    public List<Carts> Carts { get; set; }
    public ICollection<Photo> Photos { get; set; }
    }
}