namespace EmployeeAccountingSystem.Data.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<CompanyDTO> GetAsync(int id = 1);
    }
}
