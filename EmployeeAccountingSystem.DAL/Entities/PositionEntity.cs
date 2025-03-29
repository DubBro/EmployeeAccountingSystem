namespace EmployeeAccountingSystem.DAL.Entities;

public class PositionEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
