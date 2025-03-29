using EmployeeAccountingSystem.BLL.Models;

namespace EmployeeAccountingSystem.BLL.Services.Interfaces;

public interface IPositionService
{
    Task<PositionModel> GetAsync(int id);
    Task<ICollection<PositionModel>> ListAsync();
}
