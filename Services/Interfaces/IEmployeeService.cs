namespace EmployeeAccountingSystem.Services.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeDTO> GetAsync(int id);
    Task<IList<EmployeeDTO>> ListAsync();
    Task<IList<EmployeeDTO>> ListFilteredAsync(EmployeeFilterDTO filter);
    Task<int> AddAsync(EmployeeDTO employeeDTO);
    Task<int> UpdateAsync(EmployeeDTO employeeDTO);
    Task<int> DeleteAsync(int id);
}
