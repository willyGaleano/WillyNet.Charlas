using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillyNet.Charlas.Core.Application.DTOs
{
    public class EventoDto
    {
        public Guid EventoId { get; set; }
        public short Aforo { get; set; }
        public DateTime FechaIni { get; set; }
        public short Duracion { get; set; }
        public DateTime FechaFin { get; set; }
        public bool DeleteLogEvento { get; set; }

        public Guid CharlaId { get; set; }
        public string NombreCharla { get; set; }
        public string Descripcion { get; set; }
        public string UrlImage { get; set; }
        public bool DeleteLogCharla { get; set; }

        public Guid EstadoEventoId { get; set; }
        public string NombreEstadoEvento { get; set; }
        public string ColorEstadoEvento { get; set; }

    }
}
