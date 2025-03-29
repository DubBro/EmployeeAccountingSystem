namespace EmployeeAccountingSystem.Models.DTOs;

public class EmployeeFilterDTO
{
    public string? Name { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }
    public int Department { get; set; }
    public int Position { get; set; }
}
