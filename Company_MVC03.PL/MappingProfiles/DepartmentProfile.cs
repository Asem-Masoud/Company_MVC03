using AutoMapper;
using Company_MVC03.DAL.Models;
using Company_MVC03.PL.Dtos;

namespace Company_MVC03.PL.MappingProfiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<CreateDepartmentDto, Department>().ReverseMap();
            // CreateMap<CreateDepartmentDto, Department>().ReverseMap();
        }
    }
}
