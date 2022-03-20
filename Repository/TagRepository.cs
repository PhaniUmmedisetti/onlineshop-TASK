using Dapper;
using onlineshop.Models;
using onlineshop.Utilities;

namespace onlineshop.Repositories;

public interface ITagsRepository
{

    Task<List<Tag>> GetAllForOders(long TagId);
    Task<Tag> GetById(long TagId);

    Task<List<Tag>> GetList();
}

public class TagsRepository : BaseRepository, ITagsRepository
{
    public TagsRepository(IConfiguration config) : base(config)
    {

    }




    public async Task<List<Tag>> GetAllForOders(long TagId)
    {
        var query = $@"SELECT * FROM {TableNames.tag} 
        WHERE Tag_id = @TagId";

        using (var con = NewConnection)
            return (await con.QueryAsync<Tag>(query, new { TagId })).AsList();
    }


    public async Task<Tag> GetById(long TagId)
    {
        var query = $@"SELECT * FROM {TableNames.tag} 
        WHERE Tag_id = @TagId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Tag>(query, new { TagId });
    }

    public async Task<List<Tag>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.tag}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Tag>(query)).AsList();
    }



}