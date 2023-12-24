namespace EmployeeAccountingSystem.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IOptions<Config> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public async Task<EmployeeEntity> GetAsync(int id)
        {
            string query = "SELECT " +
                                "e.Id, e.FirstName, e.LastName, e.MiddleName, e.BirthDate, " +
                                "e.Country, e.City, e.Address, e.Phone, e.HireDate, e.Salary, " +
                                "e.DepartmentId, d.Name AS Department, e.PositionId, p.Name AS Position " +
                            "FROM Employee AS e " +
                            "LEFT JOIN Department AS d ON e.DepartmentId = d.Id " +
                            "LEFT JOIN Position AS p ON e.PositionId = p.Id " +
                            "WHERE e.Id = @id";

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
                    return new EmployeeEntity();
                }

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
        }

        public async Task<IList<EmployeeEntity>> ListAsync()
        {
            string query = "SELECT " +
                                "e.Id, e.FirstName, e.LastName, e.MiddleName, e.BirthDate, " +
                                "e.Country, e.City, e.Address, e.Phone, e.HireDate, e.Salary, " +
                                "e.DepartmentId, d.Name AS Department, e.PositionId, p.Name AS Position " +
                            "FROM Employee AS e " +
                            "LEFT JOIN Department AS d ON e.DepartmentId = d.Id " +
                            "LEFT JOIN Position AS p ON e.PositionId = p.Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    return new List<EmployeeEntity>();
                }

                List<EmployeeEntity> result = new List<EmployeeEntity>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var item = new EmployeeEntity()
                    {
                        Id = (int)ds.Tables[0].Rows[i]["Id"],
                        FirstName = (string)ds.Tables[0].Rows[i]["FirstName"],
                        LastName = (string)ds.Tables[0].Rows[i]["LastName"],
                        MiddleName = (string)ds.Tables[0].Rows[i]["MiddleName"],
                        BirthDate = (DateTime)ds.Tables[0].Rows[i]["BirthDate"],
                        Country = (ds.Tables[0].Rows[i]["Country"] == DBNull.Value) ? null : (string)ds.Tables[0].Rows[i]["Country"],
                        City = (ds.Tables[0].Rows[i]["City"] == DBNull.Value) ? null : (string)ds.Tables[0].Rows[i]["City"],
                        Address = (ds.Tables[0].Rows[i]["Address"] == DBNull.Value) ? null : (string)ds.Tables[0].Rows[i]["Address"],
                        Phone = (ds.Tables[0].Rows[i]["Phone"] == DBNull.Value) ? null : (string)ds.Tables[0].Rows[i]["Phone"],
                        HireDate = (DateTime)ds.Tables[0].Rows[i]["HireDate"],
                        Salary = (decimal)ds.Tables[0].Rows[i]["Salary"],
                        DepartmentId = (int)ds.Tables[0].Rows[i]["DepartmentId"],
                        Department = (string)ds.Tables[0].Rows[i]["Department"],
                        PositionId = (int)ds.Tables[0].Rows[i]["PositionId"],
                        Position = (string)ds.Tables[0].Rows[i]["Position"],
                    };

                    result.Add(item);
                }

                return result;
            }
        }

        public async Task<IList<EmployeeEntity>> ListFilteredAsync(Filter filter)
        {
            StringBuilder query = new StringBuilder("SELECT " +
                                                        "e.Id, e.FirstName, e.LastName, e.MiddleName, e.BirthDate, " +
                                                        "e.Country, e.City, e.Address, e.Phone, e.HireDate, e.Salary, " +
                                                        "e.DepartmentId, d.Name AS Department, e.PositionId, p.Name AS Position " +
                                                    "FROM Employee AS e " +
                                                    "LEFT JOIN Department AS d ON e.DepartmentId = d.Id " +
                                                    "LEFT JOIN Position AS p ON e.PositionId = p.Id");

            List<StringBuilder> filters = new List<StringBuilder>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                StringBuilder fltr = new StringBuilder("(e.FirstName LIKE @name OR e.LastName LIKE @name OR e.MiddleName LIKE @name)");
                filters.Add(fltr);

                SqlParameter param = new SqlParameter("@name", "%" + filter.Name + "%");
                sqlParams.Add(param);
            }

            if (!string.IsNullOrEmpty(filter.Country))
            {
                StringBuilder fltr = new StringBuilder("e.Country = @country");
                filters.Add(fltr);

                SqlParameter param = new SqlParameter("@country", filter.Country);
                sqlParams.Add(param);
            }

            if (!string.IsNullOrEmpty(filter.City))
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

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(query.ToString(), connection);

                if (sqlParams.Count > 0)
                {
                    command.Parameters.AddRange(sqlParams.ToArray());
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                var a = command.CommandText;

                if (ds.Tables[0].Rows.Count == 0)
                {
                    return new List<EmployeeEntity>();
                }

                List<EmployeeEntity> result = new List<EmployeeEntity>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var item = new EmployeeEntity()
                    {
                        Id = (int)ds.Tables[0].Rows[i]["Id"],
                        FirstName = (string)ds.Tables[0].Rows[i]["FirstName"],
                        LastName = (string)ds.Tables[0].Rows[i]["LastName"],
                        MiddleName = (string)ds.Tables[0].Rows[i]["MiddleName"],
                        BirthDate = (DateTime)ds.Tables[0].Rows[i]["BirthDate"],
                        Country = (ds.Tables[0].Rows[i]["Country"] == DBNull.Value) ? null : (string)ds.Tables[0].Rows[i]["Country"],
                        City = (ds.Tables[0].Rows[i]["City"] == DBNull.Value) ? null : (string)ds.Tables[0].Rows[i]["City"],
                        Address = (ds.Tables[0].Rows[i]["Address"] == DBNull.Value) ? null : (string)ds.Tables[0].Rows[i]["Address"],
                        Phone = (ds.Tables[0].Rows[i]["Phone"] == DBNull.Value) ? null : (string)ds.Tables[0].Rows[i]["Phone"],
                        HireDate = (DateTime)ds.Tables[0].Rows[i]["HireDate"],
                        Salary = (decimal)ds.Tables[0].Rows[i]["Salary"],
                        DepartmentId = (int)ds.Tables[0].Rows[i]["DepartmentId"],
                        Department = (string)ds.Tables[0].Rows[i]["Department"],
                        PositionId = (int)ds.Tables[0].Rows[i]["PositionId"],
                        Position = (string)ds.Tables[0].Rows[i]["Position"],
                    };

                    result.Add(item);
                }

                return result;
            }
        }

        public async Task<int> AddAsync(EmployeeEntity employeeEntity)
        {
            string query = "INSERT INTO Employee " +
                                "(FirstName, LastName, MiddleName, BirthDate, Country, City, " +
                                "Address, Phone, HireDate, Salary, DepartmentId, PositionId)" +
                            "VALUES " +
                                "(@firstName, @lastName, @middleName, @birthDate, @country, @city, " +
                                "@address, @phone, @hireDate, @salary, @departmentId, @positionId);" +
                            "SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(query, connection);

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    SqlParameter paramFirstName = new SqlParameter("@firstName", employeeEntity.FirstName);
                    command.Parameters.Add(paramFirstName);

                    SqlParameter paramLastName = new SqlParameter("@lastName", employeeEntity.LastName);
                    command.Parameters.Add(paramLastName);

                    SqlParameter paramMiddleName = new SqlParameter("@middleName", employeeEntity.MiddleName);
                    command.Parameters.Add(paramMiddleName);

                    SqlParameter paramBirthDate = new SqlParameter("@birthDate", employeeEntity.BirthDate);
                    command.Parameters.Add(paramBirthDate);

                    SqlParameter paramCountry = new SqlParameter("@country", (employeeEntity.Country == null) ? DBNull.Value : employeeEntity.Country);
                    command.Parameters.Add(paramCountry);

                    SqlParameter paramCity = new SqlParameter("@city", (employeeEntity.City == null) ? DBNull.Value : employeeEntity.City);
                    command.Parameters.Add(paramCity);

                    SqlParameter paramAddress = new SqlParameter("@address", (employeeEntity.Address == null) ? DBNull.Value : employeeEntity.Address);
                    command.Parameters.Add(paramAddress);

                    SqlParameter paramPhone = new SqlParameter("@phone", (employeeEntity.Phone == null) ? DBNull.Value : employeeEntity.Phone);
                    command.Parameters.Add(paramPhone);

                    SqlParameter paramHireDate = new SqlParameter("@hireDate", employeeEntity.HireDate);
                    command.Parameters.Add(paramHireDate);

                    SqlParameter paramSalary = new SqlParameter("@salary", employeeEntity.Salary);
                    command.Parameters.Add(paramSalary);

                    SqlParameter paramDepartmentId = new SqlParameter("@departmentId", employeeEntity.DepartmentId);
                    command.Parameters.Add(paramDepartmentId);

                    SqlParameter paramPositionId = new SqlParameter("@positionId", employeeEntity.PositionId);
                    command.Parameters.Add(paramPositionId);

                    var result = await command.ExecuteScalarAsync();

                    await transaction.CommitAsync();

                    return Convert.ToInt32(result);
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<int> UpdateAsync(EmployeeEntity employeeEntity)
        {
            string query = "UPDATE Employee SET " +
                                "FirstName = @firstName, LastName = @lastName, MiddleName = @middleName, BirthDate = @birthDate, " +
                                "Country = @country, City = @city, Address = @address, Phone = @phone, HireDate = @hireDate, " +
                                "Salary = @salary, DepartmentId = @departmentId, PositionId = @positionId " +
                            "OUTPUT INSERTED.Id " +
                            "WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(query, connection);

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    SqlParameter paramId = new SqlParameter("@id", employeeEntity.Id);
                    command.Parameters.Add(paramId);

                    SqlParameter paramFirstName = new SqlParameter("@firstName", employeeEntity.FirstName);
                    command.Parameters.Add(paramFirstName);

                    SqlParameter paramLastName = new SqlParameter("@lastName", employeeEntity.LastName);
                    command.Parameters.Add(paramLastName);

                    SqlParameter paramMiddleName = new SqlParameter("@middleName", employeeEntity.MiddleName);
                    command.Parameters.Add(paramMiddleName);

                    SqlParameter paramBirthDate = new SqlParameter("@birthDate", employeeEntity.BirthDate);
                    command.Parameters.Add(paramBirthDate);

                    SqlParameter paramCountry = new SqlParameter("@country", (employeeEntity.Country == null) ? DBNull.Value : employeeEntity.Country);
                    command.Parameters.Add(paramCountry);

                    SqlParameter paramCity = new SqlParameter("@city", (employeeEntity.City == null) ? DBNull.Value : employeeEntity.City);
                    command.Parameters.Add(paramCity);

                    SqlParameter paramAddress = new SqlParameter("@address", (employeeEntity.Address == null) ? DBNull.Value : employeeEntity.Address);
                    command.Parameters.Add(paramAddress);

                    SqlParameter paramPhone = new SqlParameter("@phone", (employeeEntity.Phone == null) ? DBNull.Value : employeeEntity.Phone);
                    command.Parameters.Add(paramPhone);

                    SqlParameter paramHireDate = new SqlParameter("@hireDate", employeeEntity.HireDate);
                    command.Parameters.Add(paramHireDate);

                    SqlParameter paramSalary = new SqlParameter("@salary", employeeEntity.Salary);
                    command.Parameters.Add(paramSalary);

                    SqlParameter paramDepartmentId = new SqlParameter("@departmentId", employeeEntity.DepartmentId);
                    command.Parameters.Add(paramDepartmentId);

                    SqlParameter paramPositionId = new SqlParameter("@positionId", employeeEntity.PositionId);
                    command.Parameters.Add(paramPositionId);

                    var result = await command.ExecuteScalarAsync();

                    await transaction.CommitAsync();

                    return Convert.ToInt32(result);
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            string query = "DELETE FROM Employee " +
                            "OUTPUT DELETED.Id " +
                            "WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(query, connection);

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    SqlParameter paramId = new SqlParameter("@id", id);
                    command.Parameters.Add(paramId);

                    var result = await command.ExecuteScalarAsync();

                    await transaction.CommitAsync();

                    return Convert.ToInt32(result);
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
