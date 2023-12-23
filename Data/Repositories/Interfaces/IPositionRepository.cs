namespace EmployeeAccountingSystem.Data.Repositories.Interfaces
{
    public interface IPositionRepository
    {
        Task<PositionDTO> GetAsync(int id);
        Task<IList<PositionDTO>> ListAsync();
    }
}
