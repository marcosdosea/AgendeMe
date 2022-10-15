using AgendeMeWeb.Models;
using AutoMapper;
using Core;

namespace AgendeMeWeb.Mappers
{
    public class AgendaDoServicoProfile : Profile
    {
        public AgendaDoServicoProfile()
        {
            CreateMap<AgendaDoServicoViewModel, Agendadoservico>().ReverseMap();
        }
    }
}
