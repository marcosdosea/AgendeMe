using AgendeMeWeb.Models;
using AutoMapper;
using Core;

namespace AgendeMeWeb.Mappers
{
    public class DiaAgendamentoProfile : Profile
    {
        public DiaAgendamentoProfile()
        {
            CreateMap<DiaAgendamentoViewModel, Diaagendamento>().ReverseMap();
        }
    }
}
