using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductsApi.Controllers
{
    [Authorize]  // Атрибут для защиты API от неавторизованных пользователей
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Метод для получения списка продуктов
        [HttpGet]
        public IActionResult GetProducts()
        {
            // Пример логики получения продуктов
            var products = new[]
            {
                new { Id = 1, Name = "Product 1" },
                new { Id = 2, Name = "Product 2" }
            };

            // Возвращаем список продуктов
            return Ok(new { message = "Products retrieved successfully", data = products });
        }
    }
}
