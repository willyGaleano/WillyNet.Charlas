using System;
using WillyNet.Charlas.Core.Application.DTOs.User;

namespace WillyNet.Charlas.Core.Application.DTOs
{
    public class AsistenciaDto
    {
        public Guid AsistenciaId { get; set; }

        public Guid EstadoAsistenciaId { get; set; }
        public string NombreEstadoAsistencia { get; set; }

        public string IdUserApp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Dni { get; set; }

        public Guid EventoId { get; set; }
        public short Aforo { get; set; }
        public DateTime FechaIni { get; set; }
        public short Duracion { get; set; }
        public DateTime FechaFin { get; set; }

        public Guid CharlaId { get; set; }
        public string NombreCharla { get; set; }
        public string Descripcion { get; set; }
        public string UrlImage { get; set; }
        public bool DeleteLog { get; set; }

        public Guid EstadoEventoId { get; set; }
        public string NombreEstadoEvento { get; set; }  
    }
}
