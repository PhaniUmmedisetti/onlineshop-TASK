using Dapper;
using onlineshop.Models;
using onlineshop.Utilities;

namespace onlineshop.Repositories;

public interface IOrdersRepository
{
    Task Update(Order Item);
    Task Delete(long OrderId);
    Task<List<Order>> GetList();
    Task<Order> GetById(long OrderId);

    Task<List<Order>> GetListByCustomerId(long CustomerId);

    Task<List<Order>> GetListByProductId(long ProductId);
}

public class OrdersRepository : BaseRepository, IOrdersRepository
{
    public OrdersRepository(IConfiguration config) : base(config)
    {

    }



    public async Task Delete(long OrderId)
    {
        var query = $@"DELETE FROM {TableNames.order} WHERE order_id = @OrderId";

        using (var con = NewConnection)
            await con.ExecuteAsync(query, new { OrderId });
    }



    public async Task<List<Order>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.order}";

        using (var con = NewConnection)

            return (await con.QueryAsync<Order>(query)).AsList();
    }


    public async Task<Order> GetById(long OrderId)
    {
        var query = $@"SELECT * FROM {TableNames.order} 
        WHERE order_id = @OrderId";

        using (var con = NewConnection)
            return (await con.QuerySingleOrDefaultAsync<Order>(query, new { OrderId }));
    }
    public async Task Update(Order Item)
    {
        var query = $@"UPDATE {TableNames.order} SET order_status = @OrderStatus";


        using (var con = NewConnection)
            await con.ExecuteAsync(query, Item);
    }

    public async Task<List<Order>> GetListByCustomerId(long CustomerId)
    {
        var query = $@"SELECT * FROM {TableNames.order}  WHERE customer_id = @CustomerId";



        using (var con = NewConnection)
            return (await con.QueryAsync<Order>(query, new { CustomerId })).AsList();
    }

    public async Task<List<Order>> GetListByProductId(long ProductId)
    {
        var query = $@"SELECT *FROM {TableNames.order_product} op
        LEFT JOIN {TableNames.order} o ON o.order_id = op.order_id
        WHERE op.product_id = @ProductId";

        using (var con = NewConnection)

            return (await con.QueryAsync<Order>(query, new { ProductId })).AsList();

    }
}