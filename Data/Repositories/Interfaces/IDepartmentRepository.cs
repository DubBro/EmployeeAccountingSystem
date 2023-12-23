namespace EmployeeAccountingSystem.Data.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<DepartmentDTO> GetAsync(int id);
        Task<IList<DepartmentDTO>> ListAsync();
    }
}
