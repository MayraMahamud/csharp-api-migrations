using exercise.pizzashopapi.DTO;
using exercise.pizzashopapi.Repository;
using Microsoft.AspNetCore.Mvc;
using exercise.pizzashopapi.Models;
using exercise.pizzashopapi.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace exercise.pizzashopapi.EndPoints
{
    public static class PizzaShopApi
    {
        public static void ConfigurePizzaShopApi(this WebApplication app)
        {


            var surgeryGroup = app.MapGroup("pizzaShop");

            surgeryGroup.MapGet("/pizzas", GetPizzas);
            surgeryGroup.MapGet("/orderStatus", GetOrderStatus);
            surgeryGroup.MapGet("/orders", GetOrders);
            surgeryGroup.MapGet("/orderByCustomer{id}", GetOrdersByCustomer);
            surgeryGroup.MapGet("/customers", GetCustomers);
            surgeryGroup.MapGet("/customer", GetACustomer);
            surgeryGroup.MapPut("/orderDelivered", OrderDelivered);
            surgeryGroup.MapPost("/pizza", CreatePizza);
            surgeryGroup.MapPost("/customer", CreateCustomer);
            surgeryGroup.MapPost("/order", CreateOrder);



        }

        public static async Task<IResult> GetPizzas(IRepository repository)
        {
            var pizzas = await repository.GetPizzas();
            DTOPizzaResponse response = new DTOPizzaResponse();
            foreach (var pizza in pizzas)
            {
                DTOPizza p = new DTOPizza();
                p.Name = pizza.Name;


            }
            return TypedResults.Ok(await repository.GetPizzas());
        }

        public static async Task<IResult> GetOrders(IRepository repository)
        {
            var orders = await repository.GetOrders();
            DTOOrdersResponse response = new DTOOrdersResponse();
            foreach (var order in orders)
            {
                DTOOrders o = new DTOOrders();
                o.OrderId = order.OrderId;


            }
            return TypedResults.Ok(await repository.GetOrders());
        }


        public static async Task<IResult> GetCustomers(IRepository repository)
        {
            var customers = await repository.GetCustomers();
            DTOCustomerResponse response = new DTOCustomerResponse();
            foreach (var customer in customers)
            {
                DTOCustomer c = new DTOCustomer();
                c.Name = customer.Name;


            }
            return TypedResults.Ok(await repository.GetCustomers());
        }



        public static async Task<IResult> GetACustomer(IRepository repository, int id)
        {

            var customer = await repository.GetCustomerById(id);
            if (customer == null)
            {
                return Results.NotFound();
            }

            DTOCustomerResponse response = new DTOCustomerResponse();
            DTOCustomer dTOCustomer = new DTOCustomer
            {
                Name = $"{customer.Name}"

            };

            return TypedResults.Ok(customer);
        }






        public static async Task<IResult> GetOrdersByCustomer(IRepository repository, int id)
        {
            var order = await repository.GetOrdersByCustomerId(id);
            if (order == null)
            {
                return Results.NotFound();
            }


            DTOOrdersResponse response = new DTOOrdersResponse();
            DTOOrdersResponse dTOOrder = new DTOOrdersResponse();


            return TypedResults.Ok(order);
        }



        public static async Task<IResult> CreatePizza( CreatePizzaDTO createPizzaDTO, IRepository repository)
        {
            if (string.IsNullOrWhiteSpace(createPizzaDTO.Name))
            {
                return Results.BadRequest();
            }

            Pizza newPizza = new Pizza
            {
               Price = createPizzaDTO.Price,
               Name = createPizzaDTO.Name,
               Id = createPizzaDTO.Id


            };
            await repository.CreatePizza(newPizza);
            DTOPizza dTOPizza = new DTOPizza()
            {

                Price = createPizzaDTO.Price,
                Name = createPizzaDTO.Name,
                Id = createPizzaDTO.Id


            };
            return TypedResults.Created($"{newPizza.Id} {newPizza.Name}  {newPizza.Price}", dTOPizza);



        }

        public static async Task<IResult> CreateCustomer(CreateCustomerDTO createCustomerDTO, IRepository repository)
        {
            if (string.IsNullOrWhiteSpace(createCustomerDTO.Name))
            {
                return Results.BadRequest();
            }

            Customer newCustomer = new Customer
            {
               
                Name = createCustomerDTO.Name,
                Id = createCustomerDTO.Id


            };
            await repository.CreateCustomer(newCustomer);
            DTOCustomer dTOCustomer= new DTOCustomer()
            {

               
                Name = createCustomerDTO.Name,
                Id = createCustomerDTO.Id


            };
            return TypedResults.Created($"{newCustomer.Id} {newCustomer.Name}", dTOCustomer);



        }




        public static async Task<IResult> CreateOrder(CreateOrderDTO createOrderDTO, IRepository repository)
        {
           

            Order newOrder = new Order
            {
               
               
                OrderId = createOrderDTO.OrderId,
                CustomerId = createOrderDTO.CustomerId,
                PizzaId = createOrderDTO.PizzaId


            };
            await repository.CreateOrder(newOrder);
            DTOOrders dTOOrder = new DTOOrders()
            {


              
                OrderId = createOrderDTO.OrderId,
                CustomerId = createOrderDTO.CustomerId,
                PizzaId = createOrderDTO.PizzaId


            };
            return TypedResults.Created($"{newOrder.OrderId} {newOrder.PizzaId} {newOrder.CustomerId}", dTOOrder);



        }

      
        public static  async Task<IResult> GetOrderStatus(IRepository repository,  int id )
        {
            var order = await repository.GetOrderById(id);
           var timeElapsed = DateTime.Now - order.OrderTime;
            if (timeElapsed.TotalMinutes < 3)
            {
                order.OrderStatus = "Preparing";
            }
            else if (timeElapsed.TotalMinutes < 15)
            {
                order.OrderStatus = "Cooking";
            }
            else if (timeElapsed.TotalMinutes >= 15)
            {
                order.OrderStatus = "Out for delivery";
            }

            var orderDto = new DTOOrders
            {
                OrderId = order.OrderId,
                OrderStatus = order.OrderStatus
            };
            //return TypedResults.Created(orderDto);
            return TypedResults.Ok(orderDto);

        }


        public static async Task<IResult> OrderDelivered(IRepository repository, int id)
        {
            var order = await repository.GetOrderById(id);
          
           
            await repository.UpdateOrderStatus(id, "delivered");
            return TypedResults.Ok(order);  
        }




    }
}
