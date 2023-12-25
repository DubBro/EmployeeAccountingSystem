namespace EmployeeAccountingSystem.Models.DTOs
{
    public class DepartmentInfoDTO : DepartmentDTO
    {
        public int EmployeeCount { get; set; }
        public decimal SumSalary { get; set; }
        public decimal AvgSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public decimal MinSalary { get; set; }
    }
}
