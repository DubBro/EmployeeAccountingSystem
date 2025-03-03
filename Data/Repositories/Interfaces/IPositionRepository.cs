namespace EmployeeAccountingSystem.Data.Repositories.Interfaces;

public interface IPositionRepository
{
    Task<PositionEntity> GetAsync(int id);
    Task<IList<PositionEntity>> ListAsync();
}
