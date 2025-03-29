namespace EmployeeAccountingSystem.BLL.Models;

public class DepartmentInfoModel : DepartmentModel
{
    public int EmployeeCount { get; set; }
    public decimal SumSalary { get; set; }
    public decimal AvgSalary { get; set; }
    public decimal MaxSalary { get; set; }
    public decimal MinSalary { get; set; }
}
