using onlineshop.DTOs;

namespace onlineshop.Models;



public record Tag
{
    public long TagId { get; set; }
    public string Name { get; set; }

    public long OrderId { get; set; }

    public long ProductId { get; set; }


    public TagsDTO asDto => new TagsDTO
    {
        TagId = TagId,
        Name = Name,



    };
}