namespace exercise.pizzashopapi.DTO
{
    public class CreateOrderDTO
    {
        public int OrderId { get; set; }
        


        public int CustomerId { get; set; }

        public int PizzaId { get; set; }

        //public DateTime OrderTime { get; set; }
        //public string OrderStatus { get; set; }
    }
}
