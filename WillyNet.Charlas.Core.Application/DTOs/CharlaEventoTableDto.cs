using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillyNet.Charlas.Core.Application.DTOs
{
    public class CharlaEventoTableDto
    {
        public Guid CharlaEventoId { get; set; }
        public Guid CharlaId { get; set; }
        public string NombreCharla { get; set; }
        public string Descripcion { get; set; }
        public string UrlImage { get; set; }
        public Guid EventoId { get; set; }
        public short Aforo { get; set; }
        public DateTime FechaIni { get; set; }
        public short Duracion { get; set; }
        public DateTime FechaFin { get; set; }
        public Guid EstadoId { get; set; }
        public string NombreEstado { get; set; }
    }
}
