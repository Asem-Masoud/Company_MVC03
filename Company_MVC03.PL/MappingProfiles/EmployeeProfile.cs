using AutoMapper;
using Company_MVC03.DAL.Models;
using Company_MVC03.PL.Dtos;

namespace Company_MVC03.PL.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>()
                /*.ForMember(d => d.Name, o => o.MapFrom(s => $"{s.Name}"))*/;
            CreateMap<Employee, CreateEmployeeDto>()
            /*.ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department.Name))*/;
        }
    }
}
