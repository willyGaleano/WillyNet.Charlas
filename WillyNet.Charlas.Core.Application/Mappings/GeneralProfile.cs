using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs;
using WillyNet.Charlas.Core.Application.DTOs.User;
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

            #region DTOS
            CreateMap<Evento, EventoDto>();
            CreateMap<Estado, EstadoDto>();
            CreateMap<Charla, CharlaDto>();
            CreateMap<Asistencia, AsistenciaDto>();
            CreateMap<CharlaEvento, CharlaEventoDto>();
            CreateMap<UserApp, UserAppDto>();
            #endregion
        }
    }
}
