using System;
using WillyNet.Charlas.Core.Application.DTOs.User;

namespace WillyNet.Charlas.Core.Application.DTOs
{
    public class AsistenciaDto
    {
        public Guid AsistenciaId { get; set; }
        public bool Asistio { get; set; }                
        public CharlaEventoDto CharlaEvento { get; set; }
        public UserAppDto UserApp { get; set; }

    }
}
