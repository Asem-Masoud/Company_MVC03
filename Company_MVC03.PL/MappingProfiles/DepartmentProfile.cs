using AutoMapper;
using Company_MVC03.DAL.Models;
using Company_MVC03.PL.Dtos;

namespace Company_MVC03.PL.MappingProfiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            //CreateMap<DepartmentDetailsDto, Department>().ReverseMap();
            // CreateMap<DepartmentCreateUpdateDto, Department>().ReverseMap();
        }
    }
}
