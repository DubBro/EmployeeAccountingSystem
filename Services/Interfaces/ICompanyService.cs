namespace EmployeeAccountingSystem.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyDTO> GetAsync(int id = 1);
    }
}
