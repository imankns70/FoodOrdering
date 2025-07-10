namespace FoodOrdering.PaymentService.Api.Models
{
    public class OrderCreatedEvent
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public List<OrderItem> Items { get; set; }
    }
    public class OrderItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
