using EcommerceDev.Core.Entities;
using EcommerceDev.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceDev.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> SeedData(
        [FromServices] EcommerceDbContext context)
        {
            var customer = new Customer("Luis", "luis@mail.com", "12345678", DateTime.Now.AddYears(-30), "12345678910");
            var address = new CustomerAddress(
                idCustomer: customer.Id,
                recipientName: "Luis Felipe",
                addressLine1: "Av. Paulista, 1000",
                addressLine2: "Apto 1204",
                zipCode: "01310-100",
                district: "Bela Vista",
                state: "SP",
                city: "São Paulo",
                country: "Brasil"
            );

            var category = new ProductCategory("Tecnologia", "Computador");

            var product = new Product("Notebook acer", "Um notebook excelente para jogos",
                6_000m, "Acer", 100, category.Id);

            var order = new Order(customer.Id, address.Id, 10, 6000, new List<OrderItem>
        {
            new OrderItem(product.Id, 1, 6000)
        });

            var objects = new List<object>
        {
            customer,
            product,
            category,
            order,
            address
        };

            await context.AddRangeAsync(objects);
            await context.SaveChangesAsync();

            return Ok(product.Id);
        }
    }
}
