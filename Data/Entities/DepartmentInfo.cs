﻿namespace EmployeeAccountingSystem.Data.Entities
{
    public class DepartmentInfo : DepartmentEntity
    {
        public int EmployeeCount { get; set; }
        public decimal SumSalary { get; set; }
        public decimal AvgSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public decimal MinSalary { get; set; }
    }
}
