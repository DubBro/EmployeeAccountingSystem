﻿using EmployeeAccountingSystem.Utils.Extensions;

namespace EmployeeAccountingSystem.Data.Repositories;

public class EmployeeRepository : BaseRepository, IEmployeeRepository
{
    public EmployeeRepository(IOptions<Config> options)
        : base(options)
    {
    }

    public async Task<EmployeeEntity> GetAsync(int id)
    {
        var query = "SELECT " +
                        "e.Id, e.FirstName, e.LastName, e.MiddleName, e.BirthDate, " +
                        "e.Country, e.City, e.Address, e.Phone, e.HireDate, e.Salary, " +
                        "e.DepartmentId, d.Name AS Department, e.PositionId, p.Name AS Position " +
                    "FROM Employee AS e " +
                    "LEFT JOIN Department AS d ON e.DepartmentId = d.Id " +
                    "LEFT JOIN Position AS p ON e.PositionId = p.Id " +
                    "WHERE e.Id = @id";

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
                return new EmployeeEntity();
            }

            return MapFromDataSetToEmployeeEntity(ds);
        }
    }

    public async Task<IList<EmployeeEntity>> ListAsync()
    {
        var query = "SELECT " +
                        "e.Id, e.FirstName, e.LastName, e.MiddleName, e.BirthDate, " +
                        "e.Country, e.City, e.Address, e.Phone, e.HireDate, e.Salary, " +
                        "e.DepartmentId, d.Name AS Department, e.PositionId, p.Name AS Position " +
                    "FROM Employee AS e " +
                    "LEFT JOIN Department AS d ON e.DepartmentId = d.Id " +
                    "LEFT JOIN Position AS p ON e.PositionId = p.Id";

        using (var connection = new SqlConnection(GetConnectionString()))
        {
            await connection.OpenAsync();

            var adapter = new SqlDataAdapter(query, connection);
            var ds = new DataSet();
            adapter.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return new List<EmployeeEntity>();
            }

            var result = new List<EmployeeEntity>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var item = MapFromDataSetToEmployeeEntity(ds);

                result.Add(item);
            }

            return result;
        }
    }

    // TODO: refactor dublicates in this method
    public async Task<IList<EmployeeEntity>> ListFilteredAsync(EmployeeFilter filter)
    {
        var query = new StringBuilder("SELECT " +
                                            "e.Id, e.FirstName, e.LastName, e.MiddleName, e.BirthDate, " +
                                            "e.Country, e.City, e.Address, e.Phone, e.HireDate, e.Salary, " +
                                            "e.DepartmentId, d.Name AS Department, e.PositionId, p.Name AS Position " +
                                        "FROM Employee AS e " +
                                        "LEFT JOIN Department AS d ON e.DepartmentId = d.Id " +
                                        "LEFT JOIN Position AS p ON e.PositionId = p.Id");

        var filters = new List<StringBuilder>();
        var sqlParams = new List<SqlParameter>();

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            var fltr = new StringBuilder("(e.FirstName LIKE @name OR e.LastName LIKE @name OR e.MiddleName LIKE @name)");
            filters.Add(fltr);

            var param = new SqlParameter("@name", "%" + filter.Name + "%");
            sqlParams.Add(param);
        }

        if (!string.IsNullOrWhiteSpace(filter.Country))
        {
            var fltr = new StringBuilder("e.Country = @country");
            filters.Add(fltr);

            var param = new SqlParameter("@country", filter.Country);
            sqlParams.Add(param);
        }

        if (!string.IsNullOrWhiteSpace(filter.City))
        {
            StringBuilder fltr = new StringBuilder("e.City = @city");
            filters.Add(fltr);

            SqlParameter param = new SqlParameter("@city", filter.City);
            sqlParams.Add(param);
        }

        if (filter.MinSalary > 0)
        {
            StringBuilder fltr = new StringBuilder("e.Salary >= @minSalary");
            filters.Add(fltr);

            SqlParameter param = new SqlParameter("@minSalary", filter.MinSalary);
            sqlParams.Add(param);
        }

        if (filter.MaxSalary > 0)
        {
            StringBuilder fltr = new StringBuilder("e.Salary <= @maxSalary");
            filters.Add(fltr);

            SqlParameter param = new SqlParameter("@maxSalary", filter.MaxSalary);
            sqlParams.Add(param);
        }

        if (filter.Department > 0)
        {
            StringBuilder fltr = new StringBuilder("e.DepartmentId = @departmentId");
            filters.Add(fltr);

            SqlParameter param = new SqlParameter("@departmentId", filter.Department);
            sqlParams.Add(param);
        }

        if (filter.Position > 0)
        {
            StringBuilder fltr = new StringBuilder("e.PositionId = @positionId");
            filters.Add(fltr);

            SqlParameter param = new SqlParameter("@positionId", filter.Position);
            sqlParams.Add(param);
        }

        if (filters.Count > 0)
        {
            StringBuilder condition = new StringBuilder(" WHERE ");
            condition.Append(filters[0]);

            if (filters.Count > 1)
            {
                for (int i = 1; i < filters.Count; i++)
                {
                    condition.Append(" AND ");
                    condition.Append(filters[i]);
                }
            }

            query.Append(condition);
        }

        using (var connection = new SqlConnection(GetConnectionString()))
        {
            await connection.OpenAsync();

            var command = new SqlCommand(query.ToString(), connection);

            if (sqlParams.Count > 0)
            {
                command.Parameters.AddRange(sqlParams.ToArray());
            }

            var adapter = new SqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return new List<EmployeeEntity>();
            }

            var result = new List<EmployeeEntity>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var item = MapFromDataSetToEmployeeEntity(ds);

                result.Add(item);
            }

            return result;
        }
    }

    public async Task<int> AddAsync(EmployeeEntity employeeEntity)
    {
        var query = "INSERT INTO Employee " +
                        "(FirstName, LastName, MiddleName, BirthDate, Country, City, " +
                        "Address, Phone, HireDate, Salary, DepartmentId, PositionId)" +
                    "VALUES " +
                        "(@firstName, @lastName, @middleName, @birthDate, @country, @city, " +
                        "@address, @phone, @hireDate, @salary, @departmentId, @positionId);" +
                    "SELECT SCOPE_IDENTITY()";

        using (var connection = new SqlConnection(GetConnectionString()))
        {
            await connection.OpenAsync();

            var command = new SqlCommand(query, connection);

            SetDbCommandParametersFromEmployeeEntity(employeeEntity, command);

            var transaction = connection.BeginTransaction();

            return await command.ExecuteDbCommandScalarSafetyAsync(transaction);
        }
    }

    public async Task<int> UpdateAsync(EmployeeEntity employeeEntity)
    {
        var query = "UPDATE Employee SET " +
                        "FirstName = @firstName, LastName = @lastName, MiddleName = @middleName, BirthDate = @birthDate, " +
                        "Country = @country, City = @city, Address = @address, Phone = @phone, HireDate = @hireDate, " +
                        "Salary = @salary, DepartmentId = @departmentId, PositionId = @positionId " +
                    "OUTPUT INSERTED.Id " +
                    "WHERE Id = @id";

        using (var connection = new SqlConnection(GetConnectionString()))
        {
            await connection.OpenAsync();

            var command = new SqlCommand(query, connection);

            command.AddParameter("@id", employeeEntity.Id);

            SetDbCommandParametersFromEmployeeEntity(employeeEntity, command);

            var transaction = connection.BeginTransaction();

            return await command.ExecuteDbCommandScalarSafetyAsync(transaction);
        }
    }

    public async Task<int> DeleteAsync(int id)
    {
        var query = "DELETE FROM Employee OUTPUT DELETED.Id WHERE Id = @id";

        using (var connection = new SqlConnection(GetConnectionString()))
        {
            await connection.OpenAsync();

            var command = new SqlCommand(query, connection);

            command.AddParameter("@id", id);

            var transaction = connection.BeginTransaction();

            return await command.ExecuteDbCommandScalarSafetyAsync(transaction);
        }
    }

    private static EmployeeEntity MapFromDataSetToEmployeeEntity(DataSet ds)
    {
        return new EmployeeEntity
        {
            Id = (int)ds.Tables[0].Rows[0]["Id"],
            FirstName = (string)ds.Tables[0].Rows[0]["FirstName"],
            LastName = (string)ds.Tables[0].Rows[0]["LastName"],
            MiddleName = (string)ds.Tables[0].Rows[0]["MiddleName"],
            BirthDate = (DateTime)ds.Tables[0].Rows[0]["BirthDate"],
            Country = (ds.Tables[0].Rows[0]["Country"] == DBNull.Value) ? null : (string)ds.Tables[0].Rows[0]["Country"],
            City = (ds.Tables[0].Rows[0]["City"] == DBNull.Value) ? null : (string)ds.Tables[0].Rows[0]["City"],
            Address = (ds.Tables[0].Rows[0]["Address"] == DBNull.Value) ? null : (string)ds.Tables[0].Rows[0]["Address"],
            Phone = (ds.Tables[0].Rows[0]["Phone"] == DBNull.Value) ? null : (string)ds.Tables[0].Rows[0]["Phone"],
            HireDate = (DateTime)ds.Tables[0].Rows[0]["HireDate"],
            Salary = (decimal)ds.Tables[0].Rows[0]["Salary"],
            DepartmentId = (int)ds.Tables[0].Rows[0]["DepartmentId"],
            Department = (string)ds.Tables[0].Rows[0]["Department"],
            PositionId = (int)ds.Tables[0].Rows[0]["PositionId"],
            Position = (string)ds.Tables[0].Rows[0]["Position"],
        };
    }

    private static void SetDbCommandParametersFromEmployeeEntity(EmployeeEntity employeeEntity, IDbCommand command)
    {
        command.AddParameter("@firstName", employeeEntity.FirstName);
        command.AddParameter("@lastName", employeeEntity.LastName);
        command.AddParameter("@middleName", employeeEntity.MiddleName);
        command.AddParameter("@birthDate", employeeEntity.BirthDate);
        command.AddParameter("@country", (employeeEntity.Country == null) ? DBNull.Value : employeeEntity.Country);
        command.AddParameter("@city", (employeeEntity.City == null) ? DBNull.Value : employeeEntity.City);
        command.AddParameter("@address", (employeeEntity.Address == null) ? DBNull.Value : employeeEntity.Address);
        command.AddParameter("@phone", (employeeEntity.Phone == null) ? DBNull.Value : employeeEntity.Phone);
        command.AddParameter("@hireDate", employeeEntity.HireDate);
        command.AddParameter("@salary", employeeEntity.Salary);
        command.AddParameter("@departmentId", employeeEntity.DepartmentId);
        command.AddParameter("@positionId", employeeEntity.PositionId);
    }
}
