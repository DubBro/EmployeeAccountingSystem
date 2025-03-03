using EmployeeAccountingSystem.Utils.Extensions;

namespace EmployeeAccountingSystem.Data.Repositories;

public class PositionRepository : BaseRepository, IPositionRepository
{
    public PositionRepository(IOptions<Config> options)
        : base(options)
    {
    }

    public async Task<PositionEntity> GetAsync(int id)
    {
        var query = "SELECT * FROM Position WHERE Id = @id";

        using (var connection = new SqlConnection(GetConnectionString()))
        {
            await connection.OpenAsync();

            var command = new SqlCommand(query, connection);

            command.AddParameter("@id", id);

            var adapter = new SqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return new PositionEntity();
            }

            return new PositionEntity()
            {
                Id = (int)ds.Tables[0].Rows[0]["Id"],
                Name = (string)ds.Tables[0].Rows[0]["Name"],
                Description = (ds.Tables[0].Rows[0]["Description"] == DBNull.Value) ? null : (string)ds.Tables[0].Rows[0]["Description"],
            };
        }
    }

    public async Task<IList<PositionEntity>> ListAsync()
    {
        var query = "SELECT * FROM Position";

        using (var connection = new SqlConnection(GetConnectionString()))
        {
            await connection.OpenAsync();

            var adapter = new SqlDataAdapter(query, connection);
            var ds = new DataSet();
            adapter.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return new List<PositionEntity>();
            }

            var result = new List<PositionEntity>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var item = new PositionEntity()
                {
                    Id = (int)ds.Tables[0].Rows[i]["Id"],
                    Name = (string)ds.Tables[0].Rows[i]["Name"],
                    Description = (ds.Tables[0].Rows[i]["Description"] == DBNull.Value) ? null : (string)ds.Tables[0].Rows[i]["Description"],
                };

                result.Add(item);
            }

            return result;
        }
    }
}
