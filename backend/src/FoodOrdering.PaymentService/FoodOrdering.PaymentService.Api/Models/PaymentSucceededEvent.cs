namespace FoodOrdering.PaymentService.Api.Models
{
    public class PaymentSucceededEvent
    {
        public string OrderId { get; set; }
        public string Status { get; set; } = "Succeeded";
        public DateTime PaidAt { get; set; } = DateTime.UtcNow;
    }
}
