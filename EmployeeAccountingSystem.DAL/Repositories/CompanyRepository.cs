using System.Data;
using EmployeeAccountingSystem.DAL.Entities;
using EmployeeAccountingSystem.DAL.Repositories.Interfaces;
using EmployeeAccountingSystem.DAL.Utils.Extensions;
using EmployeeAccountingSystem.Shared.Configurations;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace EmployeeAccountingSystem.DAL.Repositories;

public class CompanyRepository : BaseRepository, ICompanyRepository
{
    public CompanyRepository(IOptions<Config> options)
        : base(options)
    {
    }

    public async Task<CompanyEntity> GetAsync(int id = 1)
    {
        var query = "SELECT * FROM Company WHERE Id = @id";

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
