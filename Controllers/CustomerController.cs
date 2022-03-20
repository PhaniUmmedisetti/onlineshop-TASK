using onlineshop.DTOs;
using onlineshop.Models;
using onlineshop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace onlineshop.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerRepository _customer;
    private readonly IOrdersRepository _order;

    public CustomerController(ILogger<CustomerController> logger,
    ICustomerRepository customer, IOrdersRepository order)
    {
        _logger = logger;
        _customer = customer;
        _order = order;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerDTO>>> GetList()
    {
        var customersList = await _customer.GetList();

        var dtoList = customersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{customer_id}")]
    public async Task<ActionResult<CustomerDTO>> GetById([FromRoute] long customer_id)
    {
        var customer = await _customer.GetById(customer_id);

        if (customer is null)
            return NotFound("No customer found with given id");

        var dto = customer.asDto;

        dto.MyOrders = (await _order.GetListByCustomerId(customer_id)).Select(x => x.asDto).ToList();

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDTO>> CreateCustomer([FromBody] CustomerCreateDTO Data)
    {


        var toCreateCustomer = new Customer
        {
            Name = Data.Name.Trim(),
            Mobile = Data.Mobile,
            Address = Data.Address,
        };

        var createdCustomer = await _customer.Create(toCreateCustomer);

        return StatusCode(StatusCodes.Status201Created, createdCustomer.asDto);
    }

    [HttpPut("{customer_id}")]
    public async Task<ActionResult> UpdateCustomer([FromRoute] long customer_id,
    [FromBody] CustomerUpdateDTO Data)
    {
        var existing = await _customer.GetById(customer_id);
        if (existing is null)
            return NotFound("No customer found with given id");

        var toUpdateCustomer = existing with
        {

            Address = existing.Address,
        };

        var didUpdate = await _customer.Update(toUpdateCustomer);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update customer");

        return NoContent();
    }


}
