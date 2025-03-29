using AutoMapper;
using EmployeeAccountingSystem.BLL.Models;
using EmployeeAccountingSystem.DAL.Entities;

namespace EmployeeAccountingSystem.BLL.Infrastructure.Mapping;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<DepartmentEntity, DepartmentModel>();
        CreateMap<DepartmentInfo, DepartmentInfoModel>();
    }
}
