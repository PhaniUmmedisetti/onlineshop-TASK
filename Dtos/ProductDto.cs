using System.Text.Json.Serialization;

namespace onlineshop.DTOs;

public record ProductsDTO
{
    [JsonPropertyName("product_id")]
    public long ProductId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("Description")]
    public string Description { get; set; }

    [JsonPropertyName("in_stock")]
    public string InStock { get; set; }

    [JsonPropertyName("orders")]

    public List<OrdersDTO> Orders { get; set; }

}

public record ProductsCreateDTO
{


    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("Description")]
    public string Description { get; set; }

    [JsonPropertyName("in_stock")]
    public string InStock { get; set; }

    [JsonPropertyName("order_id")]

    public long OrderId { get; set; }

}

public record ProductsUpdateDTO
{

    [JsonPropertyName("price")]
    public decimal? Price { get; set; }


}