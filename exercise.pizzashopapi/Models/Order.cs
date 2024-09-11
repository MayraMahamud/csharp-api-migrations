using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.pizzashopapi.Models
{
    [Table("order")]
    public class Order
    {
        [Column("orderId")]

        public int OrderId { get; set; }
        [Column("customerId")]

        public int CustomerId { get; set; }
        [Column("pizzaId")]

        public int PizzaId { get; set; }

        public DateTime OrderTime { get; set; } = DateTime.Now;
        public string OrderStatus { get; set; } = "Pending";



    }
}
