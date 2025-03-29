using System.Data;
using EmployeeAccountingSystem.DAL.Entities;
using EmployeeAccountingSystem.DAL.Repositories.Interfaces;
using EmployeeAccountingSystem.DAL.Utils.Extensions;
using EmployeeAccountingSystem.Shared.Configurations;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace EmployeeAccountingSystem.DAL.Repositories;

public class DepartmentRepository : BaseRepository, IDepartmentRepository
{
    public DepartmentRepository(IOptions<Config> options)
        : base(options)
    {
    }

    public async Task<DepartmentEntity> GetAsync(int id)
    {
        var query = "SELECT * FROM Department WHERE Id = @id";

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
                return new DepartmentEntity();
            }

            return new DepartmentEntity()
            {
                Id = (int)ds.Tables[0].Rows[0]["Id"],
                Name = (string)ds.Tables[0].Rows[0]["Name"],
                Description = (ds.Tables[0].Rows[0]["Description"] == DBNull.Value)
                    ? null
                    : (string)ds.Tables[0].Rows[0]["Description"],
            };
        }
    }

    public async Task<IList<DepartmentEntity>> ListAsync()
    {
        var query = "SELECT * FROM Department";

        using (var connection = new SqlConnection(GetConnectionString()))
        {
            await connection.OpenAsync();

            var adapter = new SqlDataAdapter(query, connection);
            var ds = new DataSet();
            adapter.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return new List<DepartmentEntity>();
            }

            var result = new List<DepartmentEntity>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var item = new DepartmentEntity()
                {
                    Id = (int)ds.Tables[0].Rows[i]["Id"],
                    Name = (string)ds.Tables[0].Rows[i]["Name"],
                    Description = (ds.Tables[0].Rows[i]["Description"] == DBNull.Value)
                        ? null
                        : (string)ds.Tables[0].Rows[i]["Description"],
                };

                result.Add(item);
            }

            return result;
        }
    }

    public async Task<IList<DepartmentInfo>> ListDepartmentsInfoAsync()
    {
        var query = "SELECT " +
                        "d.Id, d.Name, d.Description, COUNT(e.Id) AS EmployeeCount, " +
                        "SUM(e.Salary) AS SumSalary, AVG(e.Salary) AS AvgSalary, " +
                        "MAX(e.Salary) AS MaxSalary, MIN(e.Salary) AS MinSalary " +
                    "FROM Department AS d " +
                    "LEFT JOIN Employee AS e ON e.DepartmentId = d.Id " +
                    "GROUP BY d.Id, d.Name, d.Description";

        using (var connection = new SqlConnection(GetConnectionString()))
        {
            await connection.OpenAsync();

            var adapter = new SqlDataAdapter(query, connection);
            var ds = new DataSet();
            adapter.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return new List<DepartmentInfo>();
            }

            var result = new List<DepartmentInfo>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var item = new DepartmentInfo()
                {
                    Id = (int)ds.Tables[0].Rows[i]["Id"],
                    Name = (string)ds.Tables[0].Rows[i]["Name"],
                    Description = (ds.Tables[0].Rows[i]["Description"] == DBNull.Value)
                        ? null
                        : (string)ds.Tables[0].Rows[i]["Description"],
                    EmployeeCount = (int)ds.Tables[0].Rows[i]["EmployeeCount"],
                    SumSalary = (decimal)ds.Tables[0].Rows[i]["SumSalary"],
                    AvgSalary = (decimal)ds.Tables[0].Rows[i]["AvgSalary"],
                    MaxSalary = (decimal)ds.Tables[0].Rows[i]["MaxSalary"],
                    MinSalary = (decimal)ds.Tables[0].Rows[i]["MinSalary"],
                };

                result.Add(item);
            }

            return result;
        }
    }
}
