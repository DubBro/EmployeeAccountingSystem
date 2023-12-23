namespace EmployeeAccountingSystem.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository(IOptions<Config> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public async Task<DepartmentDTO> GetAsync(int id)
        {
            string query = "SELECT * FROM Department WHERE Id = @id";

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
                    return new DepartmentDTO();
                }

                return new DepartmentDTO()
                {
                    Id = (int)ds.Tables[0].Rows[0]["Id"],
                    Name = (string)ds.Tables[0].Rows[0]["Name"],
                    Description = (ds.Tables[0].Rows[0]["Description"] == DBNull.Value) ? null : (string)ds.Tables[0].Rows[0]["Description"],
                };
            }
        }

        public async Task<IList<DepartmentDTO>> ListAsync()
        {
            string query = "SELECT * FROM Department";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    return new List<DepartmentDTO>();
                }

                var result = new List<DepartmentDTO>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var item = new DepartmentDTO()
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
