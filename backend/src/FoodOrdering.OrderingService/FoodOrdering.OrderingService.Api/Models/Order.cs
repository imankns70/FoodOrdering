using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FoodOrdering.OrderingServiceApi.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        [BsonElement("userId")]
        public string UserId { get; set; } = null!;

        [BsonElement("items")]
        public List<OrderItem> Items { get; set; } = new();

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class OrderItem
    {
        public string ProductId { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
