namespace EmployeeAccountingSystem.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CompanyDTO, CompanyViewModel>();
        CreateMap<DepartmentDTO, DepartmentViewModel>();
        CreateMap<PositionModel, PositionViewModel>();
        CreateMap<EmployeeModel, EmployeeViewModel>().ReverseMap();
        CreateMap<DepartmentInfoModel, DepartmentInfoViewModel>();

        CreateMap<EmployeeFilterRequest, EmployeeFilterDTO>();
    }
}
