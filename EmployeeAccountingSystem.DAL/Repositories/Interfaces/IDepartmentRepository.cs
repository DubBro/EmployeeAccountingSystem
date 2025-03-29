using EmployeeAccountingSystem.DAL.Entities;

namespace EmployeeAccountingSystem.DAL.Repositories.Interfaces;

public interface IDepartmentRepository
{
    Task<DepartmentEntity> GetAsync(int id);
    Task<ICollection<DepartmentEntity>> ListAsync();
    Task<ICollection<DepartmentInfo>> ListDepartmentsInfoAsync();
}
