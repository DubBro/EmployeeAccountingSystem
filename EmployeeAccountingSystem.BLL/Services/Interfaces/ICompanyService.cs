using EmployeeAccountingSystem.BLL.Models;

namespace EmployeeAccountingSystem.BLL.Services.Interfaces;

public interface ICompanyService
{
    Task<CompanyModel> GetAsync(int id = 1);
}
