namespace ProductService.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsAvailable { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
