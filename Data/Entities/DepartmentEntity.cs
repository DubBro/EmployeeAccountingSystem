namespace EmployeeAccountingSystem.Data.Entities;

public class DepartmentEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
