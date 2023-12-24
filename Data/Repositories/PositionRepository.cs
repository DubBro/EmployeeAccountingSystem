namespace EmployeeAccountingSystem.Data.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly string _connectionString;

        public PositionRepository(IOptions<Config> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public async Task<PositionEntity> GetAsync(int id)
        {
            string query = "SELECT * FROM Position WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(query, connection);
                SqlParameter paramId = new SqlParameter("@id", id);
                command.Parameters.Add(paramId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
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
            string query = "SELECT * FROM Position";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    return new List<PositionEntity>();
                }

                List<PositionEntity> result = new List<PositionEntity>();

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
}
