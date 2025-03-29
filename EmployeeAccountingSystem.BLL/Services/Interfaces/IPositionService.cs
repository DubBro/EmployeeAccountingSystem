namespace EmployeeAccountingSystem.Services.Interfaces;

public interface IPositionService
{
    Task<PositionDTO> GetAsync(int id);
    Task<IList<PositionDTO>> ListAsync();
}
