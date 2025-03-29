namespace EmployeeAccountingSystem.BLL.Models;

public class EmployeeModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public int DepartmentId { get; set; }
    public string Department { get; set; } = null!;
    public int PositionId { get; set; }
    public string Position { get; set; } = null!;
}
