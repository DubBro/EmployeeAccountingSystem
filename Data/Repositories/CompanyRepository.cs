namespace EmployeeAccountingSystem.Data.Repositories;

public class CompanyRepository : BaseRepository, ICompanyRepository
{
    public CompanyRepository(IOptions<Config> options)
        : base(options)
    {
    }

    public async Task<CompanyEntity> GetAsync(int id = 1)
    {
        string query = "SELECT * FROM Company WHERE Id = @id";

        using (SqlConnection connection = new SqlConnection(GetConnectionString()))
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
                return new CompanyEntity();
            }

            return new CompanyEntity
            {
                Id = (int)ds.Tables[0].Rows[0]["Id"],
                Name = (string)ds.Tables[0].Rows[0]["Name"],
                Description = (string)ds.Tables[0].Rows[0]["Description"],
            };
        }
    }
}
