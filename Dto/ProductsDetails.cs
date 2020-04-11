using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AppApi.Models;

namespace AppApi.Dto
{
    public class ProductsDetails
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public bool HasDiscount { get; set; }
        public double DiscountAmount { get; set; }
        public int CategoryId { get; set; }
        public string  PhotoUrl { get; set; }
        public ICollection<PhotosDetails> Photos { get; set; }
    }
}