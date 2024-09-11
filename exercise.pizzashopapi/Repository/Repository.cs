using exercise.pizzashopapi.Data;
using exercise.pizzashopapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;



namespace exercise.pizzashopapi.Repository
{
    public class Repository : IRepository
    {
        private DataContext _db;


        public Repository(DataContext db)
        {
            _db = db;
        }

        //public IEnumerable<Order> GetOrdersByCustomer(int id)
        //{
        //    throw NotImplementedException();
        //}

        public IEnumerable<Order> GetOrdersByCustomer()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Pizza>> GetPizzas()
        {
            return await _db.Pizzas.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _db.Orders.ToListAsync();
        }


        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _db.Customers.ToListAsync();
        }




        public async Task<List<Order>> GetOrdersByCustomerId(int id)
        {
  
            return await _db.Orders.Where(o => o.CustomerId == id).ToListAsync();


        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _db.Customers.FindAsync(id);

        }

        //public async Task <Order> GetOrderById(int id)
        //{
        //    return await _db.Orders.FindAsync(id);
        //}

        //public async Task<Order> UpdateOrderStatus(int id, string newStatus)
        //{
        //    var order = await _db.Orders.FindAsync(id);
        //        if(order != null)
        //        {
        //        order.OrderStatus = newStatus;
        //        await _db.SaveChangesAsync();
        //        } return order;
        //}

        public async Task<Pizza> CreatePizza(Pizza pizza)
        {
            _db.Pizzas.Add(pizza);
            await _db.SaveChangesAsync();
            return new Pizza { Id = pizza.Id, Name = pizza.Name, Price = pizza.Price };

        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();
            return new Customer { Id = customer.Id, Name = customer.Name };

        }
        public async Task<Order> CreateOrder(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return new Order { OrderId = order.OrderId, CustomerId = order.CustomerId, PizzaId = order.PizzaId };

        }










        public async Task<Order> GetOrderById(int id)
        {
            return await _db.Orders.FindAsync(id);

        }

        public async Task<Order> UpdateOrderStatus(int id, string newStatus)
        {
            var order = await _db.Orders.FindAsync(id);
            if(order != null)
            {
                order.OrderStatus = newStatus;
                await _db.SaveChangesAsync();
            }
            return order;
        }

    }
}
