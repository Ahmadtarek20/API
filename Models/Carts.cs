namespace AppApi.Models
{
    public class Carts
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Users Users { get; set; }
        public int Amount { get; set; }

    }
}