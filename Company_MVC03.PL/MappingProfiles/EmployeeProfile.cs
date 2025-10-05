using AutoMapper;
using Company_MVC03.DAL.Models;
using Company_MVC03.PL.Dtos;

namespace Company_MVC03.PL.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<Employee, CreateEmployeeDto>();
        }
    }
}
