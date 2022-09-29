using AgendeMeWeb.Models;
using AutoMapper;
using Core;

namespace AgendeMeWeb.Mappers
{
    public class CidadaoProfile : Profile
    {
        public CidadaoProfile()
        {
            CreateMap<CidadaoViewModel, Cidadao>().ReverseMap();
        }
    }
}
