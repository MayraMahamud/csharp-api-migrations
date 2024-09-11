using exercise.pizzashopapi.Models;

namespace exercise.pizzashopapi.Repository
{
    public interface IRepository
    {
        Task<List<Order>> GetOrdersByCustomerId(int id);

        Task<IEnumerable<Pizza>> GetPizzas();

        Task<IEnumerable<Customer>> GetCustomers();

        Task<IEnumerable<Order>> GetOrders();

        Task<Customer> GetCustomerById(int id);

        //Task<Order> GetOrderById(int id);

        //Task<Order> UpdateOrderStatus(int id, string newStatus);

        Task<Pizza> CreatePizza(Pizza pizza);
        Task<Customer> CreateCustomer(Customer customer);
        Task<Order> CreateOrder(Order order);
        Task<Order> GetOrderById(int id);
        //Task<Order> GetOrderStatus(int id);
        Task<Order> UpdateOrderStatus(int id, string newStatus);


    }
}
