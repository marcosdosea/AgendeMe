using AgendeMeWeb.Models;
using AutoMapper;
using Core;

namespace AgendeMeWeb.Mappers
{
    public class ServicoPublicoProfile : Profile
    {
        public ServicoPublicoProfile()
        {
            CreateMap<ServicoPublicoViewModel, Servicopublico>().ReverseMap();
        }
    }
}
