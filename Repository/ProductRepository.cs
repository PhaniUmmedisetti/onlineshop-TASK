using Dapper;
using onlineshop.Models;
using onlineshop.Utilities;

namespace onlineshop.Repositories;

public interface IProductsRepository
{
    Task<Product> Create(Product Item);

    Task<bool> Delete(long ProductId);
    Task<List<Product>> GetList();
    Task<Product> GetById(long ProductId);

    Task<List<Product>> GetListByOrderId(long OrderId);
}

public class ProductsRepository : BaseRepository, IProductsRepository
{
    public ProductsRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Product> Create(Product Item)
    {
        var query = $@"INSERT INTO {TableNames.product} (product_id, product_name, price,
        discription, in_stock) VALUES (@ProductId, @ProductName, @Price, @Discription, @InStock) 
        RETURNING *";

        using (var con = NewConnection)
            return await con.QuerySingleAsync<Product>(query, Item);
    }


    public async Task<bool> Delete(long ProductId)
    {
        var query = $@"DELETE FROM {TableNames.product} WHERE Product_id = @ProductId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { ProductId });

            return res == 1;
        }
    }




    public async Task<List<Product>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.product}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Product>(query)).AsList();
    }

    public async Task<Product> GetById(long ProductId)
    {
        var query = $@"SELECT * FROM {TableNames.product} 
        WHERE Product_id = @ProductId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Product>(query, new { ProductId });
    }

    public async Task<List<Product>> GetListByOrderId(long OrderId)
    {
        var query = $@"SELECT *FROM {TableNames.order_product} op
        LEFT JOIN {TableNames.product} p ON p.product_id = op.product_id
        WHERE op.order_id = @OrderId";

        using (var con = NewConnection)

            return (await con.QueryAsync<Product>(query, new { OrderId })).AsList();
    }
}