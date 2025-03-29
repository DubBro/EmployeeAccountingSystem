using EmployeeAccountingSystem.DAL.Repositories;
using EmployeeAccountingSystem.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeAccountingSystem.DAL.Infrastructure.DependencyInjection;

public static class DalServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
    }
}
