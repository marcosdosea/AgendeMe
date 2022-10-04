using AgendeMeWeb.Models;
using AutoMapper;
using Core;

namespace AgendeMeWeb.Mappers
{
    public class OrgaoPublicoProfile : Profile
    {
        public OrgaoPublicoProfile()
        {
            CreateMap<OrgaoPublicoViewModel, Orgaopublico>().ReverseMap();
        }
    }
}
