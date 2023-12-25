namespace EmployeeAccountingSystem.Data.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<DepartmentEntity> GetAsync(int id);
        Task<IList<DepartmentEntity>> ListAsync();
        Task<IList<DepartmentInfo>> ListDepartmentsInfoAsync();
    }
}
