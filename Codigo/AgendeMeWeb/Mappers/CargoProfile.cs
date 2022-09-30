using AgendeMeWeb.Models;
using AutoMapper;
using Core;

namespace AgendeMeWeb.Mappers
{
    public class CargoProfile : Profile
    {
        public CargoProfile()
        {
            CreateMap<CargoViewModel, Cargo>().ReverseMap();
        }
    }
}
