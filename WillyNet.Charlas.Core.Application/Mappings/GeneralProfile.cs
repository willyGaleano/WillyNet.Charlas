using AutoMapper;
using WillyNet.Charlas.Core.Application.DTOs;
using WillyNet.Charlas.Core.Application.DTOs.User;
using WillyNet.Charlas.Core.Application.Features.Charlas.Queries.GetAllCharlas;
using WillyNet.Charlas.Core.Application.Features.Eventos.Commands;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region COMMANDS
            CreateMap<CreateEventosCommand, Evento>();
            #endregion

            #region QUERIES
            CreateMap<GetAllPagedCharlasQuery, GetAllCharlaParameters>();
            #endregion

            #region DTOS           
            CreateMap<Asistencia, AsistenciaDto>()
                .ForMember(x => x.AsistenciaId, y => y.MapFrom(z => z.AsistenciaId))
                .ForMember(x => x.EstadoAsistenciaId, y => y.MapFrom(z => z.EstadoAsistenciaId))
                .ForMember(x => x.NombreEstadoAsistencia, y => y.MapFrom(z => z.EstadoAsistencia.Nombre))
                .ForMember(x => x.IdUserApp, y => y.MapFrom(z => z.UserAppId))
                .ForMember(x => x.FirstName, y => y.MapFrom(z => z.UserApp.FirstName))
                .ForMember(x => x.LastName, y => y.MapFrom(z => z.UserApp.LastName))
                .ForMember(x => x.Dni, y => y.MapFrom(z => z.UserApp.Dni))
                .ForMember(x => x.EventoId, y => y.MapFrom(z => z.EventoId))
                .ForMember(x => x.Aforo, y => y.MapFrom(z => z.Evento.Aforo))
                .ForMember(x => x.FechaIni, y => y.MapFrom(z => z.Evento.FechaIni))
                .ForMember(x => x.Duracion, y => y.MapFrom(z => z.Evento.Duracion))
                .ForMember(x => x.FechaFin, y => y.MapFrom(z => z.Evento.FechaFin))
                .ForMember(x => x.CharlaId, y => y.MapFrom(z => z.Evento.CharlaId))
                .ForMember(x => x.NombreCharla, y => y.MapFrom(z => z.Evento.Charla.Nombre))
                .ForMember(x => x.Descripcion, y => y.MapFrom(z => z.Evento.Charla.Descripcion))
                .ForMember(x => x.UrlImage, y => y.MapFrom(z => z.Evento.Charla.UrlImage))
                .ForMember(x => x.DeleteLog, y => y.MapFrom(z => z.Evento.Charla.DeleteLog))
                .ForMember(x => x.EstadoEventoId, y => y.MapFrom(z => z.Evento.EstadoEventoId))
                .ForMember(x => x.NombreEstadoEvento, y => y.MapFrom(z => z.Evento.EstadoEvento.Nombre));

            CreateMap<Charla, CharlaDto>().ReverseMap();
            CreateMap<Control, ControlDto>()
                .ForMember(x => x.ControlId, y => y.MapFrom(z => z.ControlId))
                .ForMember(x => x.FecSesion, y => y.MapFrom(z => z.FecSesion))
                .ForMember(x => x.Tope, y => y.MapFrom(z => z.Tope))
                .ForMember(x => x.UserAppId, y => y.MapFrom(z => z.UserAppId))
                .ForMember(x => x.FirstName, y => y.MapFrom(z => z.UserApp.FirstName))
                .ForMember(x => x.LastName, y => y.MapFrom(z => z.UserApp.LastName))
                .ForMember(x => x.Dni, y => y.MapFrom(z => z.UserApp.Dni));

            CreateMap<EstadoAsistencia, EstadoAsistenciaDto>().ReverseMap();
            CreateMap<EstadoEvento, EstadoEventoDto>().ReverseMap();

            CreateMap<Evento, EventoDto>()
                .ForMember(x => x.EventoId, y => y.MapFrom(z => z.EventoId))
                .ForMember(x => x.Aforo, y => y.MapFrom(z => z.Aforo))
                .ForMember(x => x.FechaIni, y => y.MapFrom(z => z.FechaIni))
                .ForMember(x => x.Duracion, y => y.MapFrom(z => z.Duracion))
                .ForMember(x => x.FechaFin, y => y.MapFrom(z => z.FechaFin))
                .ForMember(x => x.DeleteLogEvento, y => y.MapFrom(z => z.DeleteLog))

                .ForMember(x => x.CharlaId, y => y.MapFrom(z => z.CharlaId))
                .ForMember(x => x.NombreCharla, y => y.MapFrom(z => z.Charla.Nombre))
                .ForMember(x => x.Descripcion, y => y.MapFrom(z => z.Charla.Descripcion))
                .ForMember(x => x.UrlImage, y => y.MapFrom(z => z.Charla.UrlImage))
                .ForMember(x => x.DeleteLogCharla, y => y.MapFrom(z => z.Charla.DeleteLog))

                .ForMember(x => x.EstadoEventoId, y => y.MapFrom(z => z.EstadoEvento.EstadoEventoId))
                .ForMember(x => x.NombreEstadoEvento, y => y.MapFrom(z => z.EstadoEvento.Nombre))
                .ForMember(x => x.ColorEstadoEvento, y => y.MapFrom(z => z.EstadoEvento.Color));

            CreateMap<UserApp, UserAppDto>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
                .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName))
                .ForMember(x => x.Dni, y => y.MapFrom(z => z.Dni))
                .ForMember(x => x.UserName, y => y.MapFrom(z => z.UserName))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
                .ForMember(x => x.ImgUrl, y => y.MapFrom(z => z.ImgUrl));
            #endregion
        }
    }
}
