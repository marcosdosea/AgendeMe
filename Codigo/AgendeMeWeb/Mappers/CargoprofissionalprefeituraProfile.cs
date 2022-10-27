using AgendeMeWeb.Models;
using AutoMapper;
using Core;

namespace AgendeMeWeb.Mappers
{
    public class CargoprofissionalprefeituraProfile : Profile
    {
        public CargoprofissionalprefeituraProfile()
        {
            CreateMap<ProfissionalViewModel, Cargoprofissionalprefeitura>().ReverseMap();
        }
    }
}
