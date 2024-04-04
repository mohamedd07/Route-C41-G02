using AutoMapper;
using Route_C41_G02_DAL.Models;
using Route_C41_G02_PL.ViewModels;

namespace Route_C41_G02_PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();/*.ForMember(d=> d.Name , o=> o.MapFrom(s=> s.Name))*/;
        }
    }
}
