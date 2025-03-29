using AutoMapper;
using EmployeeAccountingSystem.BLL.Models;
using EmployeeAccountingSystem.DAL.Entities;

namespace EmployeeAccountingSystem.BLL.Infrastructure.Mapping;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeEntity, EmployeeModel>().ReverseMap();
        CreateMap<EmployeeFilterModel, EmployeeFilter>();
    }
}
