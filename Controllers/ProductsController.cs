using Microsoft.AspNetCore.Mvc;
using ProductCatalogApi.V1.Models;

namespace ProductCatalogApi.V1.Controllers;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> Products =
    [
        new Product { Id = 1, Name = "Laptop Pro 14", Category = "Electronics", Price = 6499.00m, IsActive = true },
        new Product { Id = 2, Name = "Office Chair Comfort", Category = "Furniture", Price = 899.00m, IsActive = true },
        new Product { Id = 5, Name = "Noise Canceling Headphones", Category = "Electronics", Price = 1299.00m, IsActive = false }
    ];

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Ok(Products);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);

        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create([FromBody] Product product)
    {
        var createdProduct = new Product
        {
            Id = Products.Count == 0 ? 1 : Products.Max(p => p.Id) + 1,
            Name = product.Name,
            Category = product.Category,
            Price = product.Price,
            IsActive = product.IsActive
        };

        Products.Add(createdProduct);

        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpDelete]
    public IActionResult Delete()
    {
        return NoContent();
    }

}
