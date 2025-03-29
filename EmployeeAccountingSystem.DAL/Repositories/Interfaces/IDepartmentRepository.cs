using EmployeeAccountingSystem.DAL.Entities;

namespace EmployeeAccountingSystem.DAL.Repositories.Interfaces;

public interface IDepartmentRepository
{
    Task<DepartmentEntity> GetAsync(int id);
    Task<IList<DepartmentEntity>> ListAsync();
    Task<IList<DepartmentInfo>> ListDepartmentsInfoAsync();
}
