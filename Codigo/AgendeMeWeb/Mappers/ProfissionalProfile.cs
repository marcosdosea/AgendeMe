using AgendeMeWeb.Models;
using AutoMapper;
using Core;

namespace AgendeMeWeb.Mappers
{
    public class ProfissionalProfile : Profile
    {
        public ProfissionalProfile()
        {
            CreateMap<ProfissionalViewModel, Cargoprofissionalprefeitura>().ReverseMap();
        }
    }
}
