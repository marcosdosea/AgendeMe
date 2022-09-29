using AgendeMeWeb.Models;
using AutoMapper;
using Core;

namespace AgendeMeWeb.Mappers
{
    public class PrefeituraProfile : Profile
    {
        public PrefeituraProfile()
        {
            CreateMap<PrefeituraViewModel, Prefeitura>().ReverseMap();
        }
    }
}
