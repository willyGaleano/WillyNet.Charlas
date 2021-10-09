using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs;
using WillyNet.Charlas.Core.Application.DTOs.User;
using WillyNet.Charlas.Core.Application.Features.Charlas.Queries.GetAllCharlas;
using WillyNet.Charlas.Core.Application.Features.Eventos.Command;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region COMMANDS
            CreateMap<CreateEventoCommand, Evento>();
            #endregion

            #region QUERIES
            CreateMap<GetAllPagedCharlasQuery, GetAllCharlaParameters>();
            #endregion

            #region DTOS
            CreateMap<Evento, EventoDto>();
            CreateMap<Estado, EstadoDto>();
            CreateMap<Charla, CharlaDto>();
            CreateMap<Asistencia, AsistenciaDto>();
            CreateMap<CharlaEvento, CharlaEventoDto>();
            CreateMap<UserApp, UserAppDto>();
            CreateMap<CharlaEvento, CharlaEventoTableDto>()
                .ForMember(x => x.CharlaEventoId, y => y.MapFrom(z => z.CharlaEventoId))
                .ForMember(x => x.CharlaId, y => y.MapFrom(z => z.CharlaId))
                .ForMember(x => x.NombreCharla, y => y.MapFrom(z => z.Charla.Nombre))
                .ForMember(x => x.Descripcion, y => y.MapFrom(z => z.Charla.Descripcion))
                .ForMember(x => x.UrlImage, y => y.MapFrom(z => z.Charla.UrlImage))
                .ForMember(x => x.EventoId, y => y.MapFrom(z => z.EventoId))
                .ForMember(x => x.Aforo, y => y.MapFrom(z => z.Evento.Aforo))
                .ForMember(x => x.FechaIni, y => y.MapFrom(z => z.Evento.FechaIni))
                .ForMember(x => x.Duracion, y => y.MapFrom(z => z.Evento.Duracion))
                .ForMember(x => x.FechaFin, y => y.MapFrom(z => z.Evento.FechaFin))
                .ForMember(x => x.EstadoId, y => y.MapFrom(z => z.Evento.EstadoId))
                .ForMember(x => x.NombreEstado, y => y.MapFrom(z => z.Evento.Estado.Nombre));
                
                

            #endregion
        }
    }
}
