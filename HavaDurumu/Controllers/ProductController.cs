using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HavaDurumu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //Girişi yapmış kişiler erişebilsin dedik sadece => [Authorize] yazarak...... 

    //[Authorize] //Sadece [Authorize] yazarak => Güvenlik altına almış olduk Buradaki Controlleri 


    //Bunu yazarsakta buraya sadece Yönetiji Rölü olanlar rerişebilecektir.....
    [Authorize(Roles = "Admin, Süper Admin")]
    public class ProductController : ControllerBase
    {
        private List<Product> _products; // Örnek bir ürün listesi, gerçek bir veritabanı veya servisten alınabilir.

        public ProductController()
        {
            // Örnek bir ürün listesi oluşturuluyor.
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Elma", Price = 10.99m },
                new Product { Id = 2, Name = "Havuç", Price = 19.99m },
                new Product { Id = 3, Name = "Üzüm", Price = 5.99m }
            };
        }

        [HttpGet(Name = "Products")]
        public IActionResult Get()
        {
            return Ok(_products); // Ürün listesini döndürüyoruz.
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
