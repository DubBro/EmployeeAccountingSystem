using AutoMapper;
using EmployeeAccountingSystem.BLL.Services;
using EmployeeAccountingSystem.BLL.Services.Interfaces;
using EmployeeAccountingSystem.DAL.Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeAccountingSystem.BLL.Infrastructure.DependencyInjection;

public static class BllServiceCollectionExtensions
{
    public static void AddStorage(this IServiceCollection services)
    {
        services.AddRepositories();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IEmployeeService, IEmployeeService>();
        services.AddScoped<IPositionService, PositionService>();
    }

    public static void AddBllMaps(this IMapperConfigurationExpression configuration)
    {
        configuration.AddMaps(typeof(BllServiceCollectionExtensions));
    }
}
