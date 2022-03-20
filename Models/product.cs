using onlineshop.DTOs;

namespace onlineshop.Models;



public record Product
{
    public long ProductId { get; set; }
    public string Name { get; set; }

    public decimal Price { get; set; }
    public string Description { get; set; }
    public string InStock { get; set; }

    public long OrderId { get; set; }




    public ProductsDTO asDto => new ProductsDTO
    {
        ProductId = ProductId,
        Name = Name,
        Price = Price,
        InStock = InStock,
        Description = Description,
    };
}
