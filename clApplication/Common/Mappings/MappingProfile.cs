using AutoMapper;
using clApplication.DTOs;
using clDomain.Entities;

namespace clApplication.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Orden, OrdenDto>()
                .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles));

            CreateMap<DetalleOrden, DetalleOrdenDto>();

            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol.Nombre));
        }
    }
}
