using AutoMapper;
using TesteAPI.Models;
using TesteAPI.ViewModels;

namespace TesteAPI.Configurations
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Teste, TesteViewModel>().ReverseMap();
        }
    }
}