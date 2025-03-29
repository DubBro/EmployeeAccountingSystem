using EmployeeAccountingSystem.BLL.Models;

namespace EmployeeAccountingSystem.BLL.Services.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeModel> GetAsync(int id);
    Task<ICollection<EmployeeModel>> ListAsync();
    Task<ICollection<EmployeeModel>> ListFilteredAsync(EmployeeFilterModel filter);
    Task<int> AddAsync(EmployeeModel employeeModel);
    Task<int> UpdateAsync(EmployeeModel employeeModel);
    Task<int> DeleteAsync(int id);
}
