using onlineshop.DTOs;

namespace onlineshop.Models;

public enum Gender
{
    Male = 1,
    Female = 2,

}

public record Customer
{
    public long CustomerId { get; set; }
    public string Name { get; set; }

    public Gender Gender { get; set; }
    public string Address { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public long Mobile { get; set; }

    public long OrderId { get; set; }


    public CustomerDTO asDto => new CustomerDTO
    {
        CustomerId = CustomerId,
        Name = Name,
        Gender = Gender.ToString().ToLower(),
        Address = Address,
        Mobile = Mobile,



    };
}

