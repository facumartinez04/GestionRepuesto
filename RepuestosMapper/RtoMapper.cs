using AutoMapper;
using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Modelos.Dtos;
namespace GestionRepuestoAPI.RepuestosMapper
{
    public class RtoMapper : Profile
    {
        public RtoMapper()
        {
            CreateMap<Repuesto, RepuestoCrearDto>().ReverseMap();
            CreateMap<Repuesto, RepuestoLeerDto>().ReverseMap();

            CreateMap<Usuario, UsuarioCrearDto>().ReverseMap();
            CreateMap<Usuario, UsuarioLeerDto>().ReverseMap();
            CreateMap<Usuario, UsuarioEditarDto>().ReverseMap();
            CreateMap<Usuario, RefreshTokenDto>().ReverseMap();


            CreateMap<Usuario, LoginRespuestaDto>()
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.nombreUsuario));

            CreateMap<UsuarioPermiso, UsuarioPermisoDto>()
                .ForMember(dest => dest.idUsuario, opt => opt.MapFrom(src => src.idUsuario))
                .ForMember(dest => dest.idPermiso, opt => opt.MapFrom(src => src.idPermiso))
                .ReverseMap();

            CreateMap<UsuarioRol, UsuarioRolDto>()
                .ForMember(dest => dest.idUsuario, opt => opt.MapFrom(src => src.idUsuario))
                .ForMember(dest => dest.idRol, opt => opt.MapFrom(src => src.idRol))
                .ReverseMap();

            CreateMap<Rol, Rol>(); 

            CreateMap<Permiso, Permiso>();


            CreateMap<Permiso, PermisoCrearDto>().ReverseMap();
            CreateMap<Permiso, PermisoLeerDto>().ReverseMap();


            CreateMap<Rol, UsuarioRolDto>().ReverseMap();
            CreateMap<Rol, RolDto>().ReverseMap();
        }

    }
}
