using AgendeMeWeb.Models;
using AutoMapper;
using Core;

namespace AgendeMeWeb.Mappers
{
    public class AgendarServicoProfile : Profile
    {
        public AgendarServicoProfile()
        {
            CreateMap<AgendarServicoViewModel, Agendamento>().ReverseMap();
        }
    }
}
