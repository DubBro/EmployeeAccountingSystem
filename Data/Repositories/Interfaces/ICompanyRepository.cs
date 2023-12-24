namespace EmployeeAccountingSystem.Data.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<CompanyEntity> GetAsync(int id = 1);
    }
}
