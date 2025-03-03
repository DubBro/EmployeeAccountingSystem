namespace EmployeeAccountingSystem.Data.Repositories;

public abstract class BaseRepository
{
    private readonly string _connectionString;

    public BaseRepository(IOptions<Config> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    protected string GetConnectionString()
    {
        return _connectionString;
    }
}
