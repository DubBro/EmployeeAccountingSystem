using EmployeeAccountingSystem.DAL.Entities;

namespace EmployeeAccountingSystem.DAL.Repositories.Interfaces;

public interface ICompanyRepository
{
    Task<CompanyEntity> GetAsync(int id = 1);
}
