
using onlineshop.DTOs;

namespace onlineshop.Models;


public record Order
{

    public long OrderId { get; set; }
    public string OrderStatus { get; set; }





    public OrdersDTO asDto => new OrdersDTO
    {
        OrderId = OrderId,
        OrderStatus = OrderStatus,

    };
}
