namespace EmployeeAccountingSystem.MVC.ViewModels.Requests;

public class EmployeeFilterRequest
{
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }
    public int Department { get; set; }
    public int Position { get; set; }
}
