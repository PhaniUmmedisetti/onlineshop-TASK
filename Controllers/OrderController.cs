using onlineshop.DTOs;
using onlineshop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace onlineshop.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrdersRepository _orders;

    private readonly IProductsRepository _product;
    public OrdersController(ILogger<OrdersController> logger,
    IOrdersRepository orders, IProductsRepository product)
    {
        _logger = logger;
        _orders = orders;
        _product = product;

    }


    [HttpGet]
    public async Task<ActionResult<List<OrdersDTO>>> GetList()
    {
        var ordersList = await _orders.GetList();


        var dtoList = ordersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{order_id}")]
    public async Task<ActionResult<ProductsDTO>> GetById([FromRoute] long order_id)
    {
        var orders = await _orders.GetById(order_id);

        if (orders is null)
            return NotFound("No Order found with given order id");

        var res = orders.asDto;

        res.Products = (await _product.GetListByOrderId(order_id)).Select(x => x.asDto).ToList();
        return Ok(res);
    }



    [HttpPut("{order_id}")]
    public async Task<ActionResult> UpdateOrders([FromRoute] int order_id,
    [FromBody] OrdersCreateDTO Data)
    {
        var existing = await _orders.GetById(order_id);
        if (existing is null)
            return NotFound("No Order found with given id");

        var toUpdateItem = existing with
        {

            OrderStatus = Data.OrderStatus?.Trim(),

        };

        await _orders.Update(toUpdateItem);

        return NoContent();
    }

    [HttpDelete("{order_id}")]
    public async Task<ActionResult> DeleteOrders([FromRoute] int order_id)
    {
        var existing = await _orders.GetById(order_id);
        if (existing is null)
            return NotFound("No orders found with given id");

        await _orders.Delete(order_id);

        return NoContent();
    }
}
