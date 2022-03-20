using onlineshop.Models;
using Dapper;
using onlineshop.Utilities;


namespace onlineshop.Repositories;

public interface ICustomerRepository
{
    Task<Customer> Create(Customer Item);
    Task<bool> Update(Customer Item);
    Task<Customer> GetById(long CustomerId);
    Task<List<Customer>> GetList();

}
public class CustomerRepository : BaseRepository, ICustomerRepository
{
    public CustomerRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Customer> Create(Customer Item)
    {
        var query = $@"INSERT INTO ""{TableNames.customer}"" 
        (customer_id, name, gender, mobile, address) 
        VALUES (@CustomerId, @Name, @Gender, @Mobile, @Address) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Customer>(query, Item);
            return res;
        }
    }


    public async Task<Customer> GetById(long CustomerId)
    {
        var query = $@"SELECT * FROM ""{TableNames.customer}"" 
        WHERE customer_id = @CustomerId";


        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Customer>(query, new { CustomerId });
    }

    public async Task<List<Customer>> GetList()
    {

        var query = $@"SELECT * FROM ""{TableNames.customer}""";

        List<Customer> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Customer>(query)).AsList();
        return res;
    }

    public async Task<bool> Update(Customer Item)
    {
        var query = $@"UPDATE ""{TableNames.customer}"" SET address = @Address WHERE customer_id = @CustomerId";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, Item);
            return rowCount == 1;
        }
    }
}