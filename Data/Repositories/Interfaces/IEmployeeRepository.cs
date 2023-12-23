namespace EmployeeAccountingSystem.Data.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDTO> GetAsync(int id);
        Task<IList<EmployeeDTO>> ListAsync();
        Task<IList<EmployeeDTO>> ListFilteredAsync(Filter filter);
        Task<int> AddAsync(EmployeeDTO employeeDTO);
        Task<int> UpdateAsync(EmployeeDTO employeeDTO);
        Task<int> DeleteAsync(int id);
    }
}
