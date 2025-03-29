using EmployeeAccountingSystem.DAL.Entities;

namespace EmployeeAccountingSystem.DAL.Repositories.Interfaces;

public interface IPositionRepository
{
    Task<PositionEntity> GetAsync(int id);
    Task<IList<PositionEntity>> ListAsync();
}
