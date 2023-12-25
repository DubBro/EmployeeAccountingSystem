namespace EmployeeAccountingSystem.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<DepartmentDTO> GetAsync(int id);
        Task<IList<DepartmentDTO>> ListAsync();
        Task<IList<DepartmentInfoDTO>> ListDepartmentsInfoAsync();
    }
}
