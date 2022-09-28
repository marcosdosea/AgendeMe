using AgendeMeWeb.Models;
using AutoMapper;
using Core;

namespace AgendeMeWeb.Mappers
{
    public class AreaDeServicoProfile : Profile
    {
        public AreaDeServicoProfile()
        {
            CreateMap<AreaDeServicoViewModel, Areadeservico>().ReverseMap();
        }
    }
}
