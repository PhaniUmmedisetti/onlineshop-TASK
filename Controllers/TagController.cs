using onlineshop.DTOs;
using onlineshop.Models;
using onlineshop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace onlineshop.Controllers;

[ApiController]
[Route("api/tags")]
public class TagsController : ControllerBase
{
    private readonly ILogger<TagsController> _logger;
    private readonly ITagsRepository _tags;
    //private readonly ICustomerRepository _hardware;

    public TagsController(ILogger<TagsController> logger,
    ITagsRepository tags)
    {
        _logger = logger;
        _tags = tags;

    }

    [HttpGet]
    public async Task<ActionResult<List<TagsDTO>>> GetAllTags()
    {
        var tagsList = await _tags.GetList();


        var dtoList = tagsList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{tag_id}")]
    public async Task<ActionResult<TagsDTO>> GetById([FromRoute] long tag_id)
    {
        var tags = await _tags.GetById(tag_id);

        if (tags is null)
            return NotFound("No tag found with given id");


        return Ok(tags.asDto);
    }




}
