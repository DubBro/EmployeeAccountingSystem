namespace EmployeeAccountingSystem.DAL.Entities;

// TODO: this is not an entity
public class DepartmentInfo : DepartmentEntity
{
    public int EmployeeCount { get; set; }
    public decimal SumSalary { get; set; }
    public decimal AvgSalary { get; set; }
    public decimal MaxSalary { get; set; }
    public decimal MinSalary { get; set; }
}
