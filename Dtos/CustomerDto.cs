using System.Text.Json.Serialization;

namespace onlineshop.DTOs;

public record CustomerDTO
{
    [JsonPropertyName("customer_id")]
    public long CustomerId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }



    [JsonPropertyName("my_orders")]
    public List<OrdersDTO> MyOrders { get; set; }

}

public record CustomerCreateDTO
{
    [JsonPropertyName("customer_id")]
    public long CustomerId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }


    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

}

public record CustomerUpdateDTO
{


    [JsonPropertyName("address")]
    public string Address { get; set; }




}