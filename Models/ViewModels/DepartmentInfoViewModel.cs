namespace EmployeeAccountingSystem.Models.ViewModels;

public class DepartmentInfoViewModel : DepartmentViewModel
{
    public int EmployeeCount { get; set; }
    public decimal SumSalary { get; set; }
    public decimal AvgSalary { get; set; }
    public decimal MaxSalary { get; set; }
    public decimal MinSalary { get; set; }
}
