using EmployeeAccountingSystem.Shared.Configurations;
using Microsoft.Extensions.Options;

namespace EmployeeAccountingSystem.DAL.Repositories;

public abstract class BaseRepository
{
    private readonly string _connectionString;

    // TODO: Test how work IOptions, IOptionsSnapshot, IOptionsMonitor
    protected BaseRepository(IOptions<Config> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    protected string GetConnectionString()
    {
        return _connectionString;
    }
}
