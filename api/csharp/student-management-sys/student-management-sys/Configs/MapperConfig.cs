using AutoMapper;
using student_management_sys.Dto;
using student_management_sys.Entity;
using student_management_sys.Inputs;

namespace student_management_sys.Configs
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<Account, RegisterInput>().ReverseMap();
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<Account, Student>().ReverseMap();
            CreateMap<Student, StudentInput>().ReverseMap();
        }
    }
}
