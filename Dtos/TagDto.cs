using System.Text.Json.Serialization;

namespace onlineshop.DTOs;

public record TagsDTO
{
    [JsonPropertyName("tag_id")]
    public long TagId { get; set; }

    [JsonPropertyName("name")]

    public string Name { get; set; }


}




