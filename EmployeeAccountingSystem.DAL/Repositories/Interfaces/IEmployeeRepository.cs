using EmployeeAccountingSystem.DAL.Entities;

namespace EmployeeAccountingSystem.DAL.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<EmployeeEntity> GetAsync(int id);
    Task<ICollection<EmployeeEntity>> ListAsync();
    Task<ICollection<EmployeeEntity>> ListFilteredAsync(EmployeeFilter filter);
    Task<int> AddAsync(EmployeeEntity employeeEntity);
    Task<int> UpdateAsync(EmployeeEntity employeeEntity);
    Task<int> DeleteAsync(int id);
}
