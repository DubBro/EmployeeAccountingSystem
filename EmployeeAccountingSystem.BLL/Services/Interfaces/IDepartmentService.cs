using EmployeeAccountingSystem.BLL.Models;

namespace EmployeeAccountingSystem.BLL.Services.Interfaces;

public interface IDepartmentService
{
    Task<DepartmentModel> GetAsync(int id);
    Task<ICollection<DepartmentModel>> ListAsync();
    Task<ICollection<DepartmentInfoModel>> ListDepartmentsInfoAsync();
}
