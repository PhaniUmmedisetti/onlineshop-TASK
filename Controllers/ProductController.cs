using onlineshop.DTOs;
using onlineshop.Models;
using onlineshop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace onlineshop.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductsRepository _products;

    private readonly IOrdersRepository _orders;

    public ProductsController(ILogger<ProductsController> logger,
    IProductsRepository products, IOrdersRepository orders)
    {
        _logger = logger;
        _products = products;
        _orders = orders;

    }

    [HttpGet]
    public async Task<ActionResult<List<ProductsDTO>>> GetAllProducts()
    {
        var productsList = await _products.GetList();


        var dtoList = productsList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{product_id}")]
    public async Task<ActionResult<ProductsDTO>> GetById([FromRoute] long product_id)
    {
        var products = await _products.GetById(product_id);

        if (products is null)
            return NotFound("No Product found with given id");

        var res = products.asDto;

        res.Orders = (await _orders.GetListByProductId(product_id)).Select(x => x.asDto).ToList();
        return Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult<ProductsDTO>> CreateProducts([FromBody] ProductsCreateDTO Data)
    {

        var toCreateProducts = new Product
        {
            Name = Data.Name.Trim(),
            Description = Data.Description,
            Price = Data.Price,


        };

        var createdProducts = await _products.Create(toCreateProducts);

        return StatusCode(StatusCodes.Status201Created, createdProducts.asDto);
    }



    [HttpDelete("{product_id}")]
    public async Task<ActionResult> DeleteProducts([FromRoute] long product_id)
    {
        var existing = await _products.GetById(product_id);
        if (existing is null)
            return NotFound("No product found with given id");

        var didDelete = await _products.Delete(product_id);

        return NoContent();
    }
}