using AutoMapper;
using Company_MVC03.DAL.Models;
using Company_MVC03.PL.Dtos;

namespace Company_MVC03.PL.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>().ForMember(d => d.Name, o => o.MapFrom(s => $"{s.EmpName} Hello"));
            CreateMap<Employee, CreateEmployeeDto>();
        }
    }
}
